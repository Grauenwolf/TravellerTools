using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public abstract class TradeEngine
    {
        readonly CharacterBuilder m_CharacterBuilder;
        readonly INameService m_NameService;
        ImmutableArray<string> m_Personalities;

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected TradeEngine(TravellerMapService mapService, string dataPath, INameService nameService)
        {
            MapService = mapService;
            m_NameService = nameService;
            var file = new FileInfo(Path.Combine(dataPath, DataFileName));
            var converter = new XmlSerializer(typeof(TradeGoods));
            using (var stream = file.OpenRead())
                TradeGoods = ((TradeGoods)converter.Deserialize(stream)).TradeGood.ToImmutableList();

            LegalTradeGoods = TradeGoods.Where(g => g.Legal).ToImmutableList();
            m_CharacterBuilder = new CharacterBuilder(dataPath);

            var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
            m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();
        }

        public TravellerMapService MapService { get; }
        protected abstract string DataFileName { get; }
        protected ImmutableList<TradeGood> LegalTradeGoods { get; }
        protected ImmutableList<TradeGood> TradeGoods { get; }

        /// <summary>
        /// This has the cargo, people, etc. that want to travel from one location to another.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="random"></param>
        /// <param name="illegalGoods"></param>
        /// <param name="advancedCharacters"></param>
        /// <returns></returns>
        public async Task<Manifest> BuildManifestAsync(World origin, World destination, Dice random, bool illegalGoods, bool advancedCharacters)
        {
            var result = new Manifest() { Origin = origin, Destination = destination };

            result.PassengerList = await PassengersAsync(origin, destination, random, advancedCharacters).ConfigureAwait(false);

            result.FreightList = Freight(origin, destination, random);

            IReadOnlyList<TradeGood> goods;
            if (!illegalGoods)
                goods = LegalTradeGoods;
            else
                goods = TradeGoods;

            var offers = new List<TradeOffer>();
            var bids = new List<TradeBid>();

            foreach (var good in goods)
            {
                //List the goods that are readily available or usually cheap on this planet
                var purchaseDM = PurchaseDM(destination, good);
                if (good.BasePrice > 0 && purchaseDM > 0) //(purchaseDM > 0 || (purchaseDM >= 0 && good.Availability == "*") || (good.AvailabilityList.Any(a => destination.ContainsRemark(a)))))
                    offers.Add(new TradeOffer() { Type = good.Name, PurchaseDM = purchaseDM, Legal = good.Legal, TradeGood = good });

                //List the goods that are usually desired on this planet
                var saleDM = SaleDM(destination, good);
                if (saleDM > 0)
                    bids.Add(new TradeBid()
                    {
                        Type = good.Name,
                        SaleDM = saleDM,
                        Legal = good.Legal
                    });
            }

            result.Offers.AddRange(offers.OrderBy(o => o.Type));
            result.Bids.AddRange(bids.OrderBy(o => o.Type));

            return result;
        }

        /// <summary>
        /// This builds all of the manifests for a given location, plus the avaiable trade goods.
        /// </summary>
        /// <param name="sectorX"></param>
        /// <param name="sectorY"></param>
        /// <param name="hexX"></param>
        /// <param name="hexY"></param>
        /// <param name="maxJumpDistance"></param>
        /// <param name="advancedMode"></param>
        /// <param name="illegalGoods"></param>
        /// <param name="brokerScore"></param>
        /// <param name="seed"></param>
        /// <param name="advancedCharacters"></param>
        /// <returns></returns>
        public async Task<ManifestCollection> BuildManifestsAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance, bool advancedMode, bool illegalGoods, int brokerScore, int? seed, bool advancedCharacters, int streetwiseScore, bool raffleGoods, string milieu)
        {
            var actualSeed = seed ?? (new Random()).Next();
            var random = new Dice(actualSeed);

            var worlds = await MapService.WorldsNearAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance).ConfigureAwait(false);
            var result = await BuildManifestsAsync(worlds, random, illegalGoods, advancedCharacters).ConfigureAwait(false);

            result.TradeList = BuildTradeGoodsList(result.Origin, advancedMode, illegalGoods, brokerScore, random, raffleGoods, streetwiseScore);

            result.SectorX = sectorX;
            result.SectorY = sectorY;
            result.HexX = hexX;
            result.HexY = hexY;
            result.MaxJumpDistance = maxJumpDistance;
            result.HighportDetails = CalculateStarportDetails(result.Origin, random, true);
            result.DownportDetails = CalculateStarportDetails(result.Origin, random, false);
            result.AdvancedMode = advancedMode;
            result.IllegalGoods = illegalGoods;
            result.Raffle = raffleGoods;
            result.BrokerScore = brokerScore;
            result.StreetwiseScore = streetwiseScore;
            result.Seed = actualSeed;
            result.AdvancedCharacters = advancedCharacters;
            result.Milieu = milieu;

            OnManifestsBuilt(result);

            return result;
        }

        public async Task<ManifestCollection> BuildManifestsAsync(string originUwp, string destinationUwp, int distance, bool advancedMode, bool illegalGoods, int brokerScore, int? seed, bool advancedCharacters, int streetwiseScore, bool raffleGoods, string milieu, TasZone originTasZone, TasZone destinationTasZone)
        {
            var actualSeed = seed ?? (new Random()).Next();
            var random = new Dice(actualSeed);

            var origin = new World(originUwp, "Origin " + originUwp, 0, originTasZone);

            var worlds = new List<World>() { origin };
            if (!string.IsNullOrEmpty(destinationUwp))
                worlds.Add(new World(destinationUwp, "Destination " + destinationUwp, distance, destinationTasZone));

            var result = await BuildManifestsAsync(worlds, random, illegalGoods, advancedCharacters).ConfigureAwait(false);

            result.TradeList = BuildTradeGoodsList(result.Origin, advancedMode, illegalGoods, brokerScore, random, raffleGoods, streetwiseScore);

            result.HighportDetails = CalculateStarportDetails(result.Origin, random, true);
            result.DownportDetails = CalculateStarportDetails(result.Origin, random, false);
            result.AdvancedMode = advancedMode;
            result.IllegalGoods = illegalGoods;
            result.Raffle = raffleGoods;
            result.BrokerScore = brokerScore;
            result.StreetwiseScore = streetwiseScore;
            result.Seed = actualSeed;
            result.AdvancedCharacters = advancedCharacters;
            result.Milieu = milieu;

            OnManifestsBuilt(result);

            return result;
        }

        public TradeGoodsList BuildTradeGoodsList(World origin, bool advancedMode, bool illegalGoods, int brokerScore, Dice random, bool raffleGoods, int streetwiseScore, World? destination = null)
        {
            if (origin == null)
                throw new ArgumentNullException(nameof(origin), $"{nameof(origin)} is null.");

            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

            IReadOnlyList<TradeGood> goods;
            if (!illegalGoods)
                goods = LegalTradeGoods;
            else
                goods = TradeGoods;
            var result = new TradeGoodsList();

            //Goods that are available for purchase.

            List<TradeOffer> availableLots = new List<TradeOffer>();

            var randomGoods = new List<TradeGood>();

            if (raffleGoods)
            {
                /*
                 * Goods with *: Always available
                 * Good with no mark: Only 1 chance
                 * Other goods: 5 chances plus 20 chances per matching remark (trade code)
                 * 6 goods are selected by the raffle
                 */
                foreach (var good in goods)
                {
                    if (good.Availability == "*")
                    {
                        AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore));
                    }
                    else if (good.Availability == "") //extremely rare
                    {
                        randomGoods.Add(good);
                    }
                    else
                    {
                        for (var i = 0; i < 5 + (20 * good.AvailabilityList.Count(a => origin.ContainsRemark(a))); i++)
                            randomGoods.Add(good);
                    }
                }

                for (var i = 0; i < origin.PopulationCode.Value; i++)
                {
                    var good = random.Choose(randomGoods);
                    AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore));
                    randomGoods = randomGoods.Where(g => g != good).ToList();
                }
            }
            else
            {
                /*
                 * Goods with *: Always available
                 * Matching Trade remarks: Always available
                 * Other goods: 1 chance. 1d6 Selected
                 */

                foreach (var good in goods)
                {
                    if (good.Availability == "*")
                    {
                        AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore));
                    }
                    else if (good.AvailabilityList.Any(a => origin.ContainsRemark(a)))
                    {
                        AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore));
                    }
                    else
                    {
                        randomGoods.Add(good);
                    }
                }

                var picks = random.D(6);
                for (var i = 0; i < picks; i++)
                {
                    var good = random.Pick(randomGoods);
                    AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore));
                }
            }

            if (destination != null) //Add destination DMs
            {
                foreach (var lot in availableLots)
                    lot.DestinationDM = SaleDM(destination, lot.TradeGood);
            }

            //Goods the planet wants to buy

            List<TradeBid> requests = new List<TradeBid>();
            if (!advancedMode)
                foreach (var good in goods)
                {
                    if (good.BasePrice == 0) //special case
                    {
                        foreach (var detail in good.Details)
                            foreach (var name in detail.NameList)
                            {
                                var bid = new TradeBid()
                                {
                                    Type = name,
                                    Subtype = null,
                                    BasePrice = detail.Price * 1000,
                                    SaleDM = SaleDM(origin, good),
                                    Legal = good.Legal
                                };

                                //TODO: Auto-bump the price so that the merchant isn't buying from the PCs at a higher price than he would sell to them
                                int roll;
                                bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, (good.Legal ? brokerScore : streetwiseScore), out roll);
                                bid.Roll = roll;

                                requests.Add(bid);
                            }
                    }
                    else
                    {
                        var bid = new TradeBid()
                        {
                            Type = good.Name,
                            Subtype = null,
                            BasePrice = good.BasePrice * 1000,
                            SaleDM = SaleDM(origin, good),
                            Legal = good.Legal
                        };

                        //TODO: Auto-bump the price so that the merchant isn't buying from the PCs at a higher price than he would sell to them
                        int roll;
                        bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, (good.Legal ? brokerScore : streetwiseScore), out roll);
                        bid.Roll = roll;

                        requests.Add(bid);
                    }
                }
            else
                foreach (var good in goods)
                    foreach (var detail in good.Details)
                        foreach (var name in detail.NameList)
                        {
                            var bid = new TradeBid()
                            {
                                Type = good.Name,
                                Subtype = name,
                                BasePrice = detail.Price * 1000,
                                SaleDM = SaleDM(origin, good),
                                Legal = good.Legal
                            };

                            //TODO: Auto-bump the price so that the merchant isn't buying from the PCs at a higher price than he would sell to them
                            int roll;
                            bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, (good.Legal ? brokerScore : streetwiseScore), out roll);
                            bid.Roll = roll;

                            requests.Add(bid);
                        }

            result.Lots.AddRange(availableLots.OrderBy(r => r.Type).ThenBy(r => r.Subtype));
            result.Bids.AddRange(requests.OrderBy(r => r.Type).ThenBy(r => r.Subtype));

            return result;
        }

        public abstract FreightList Freight(World origin, World destination, Dice random);

        public abstract World GenerateRandomWorld();

        public abstract Task<PassengerList> PassengersAsync(World origin, World destination, Dice random, bool advancedCharacters);

        internal abstract void OnManifestsBuilt(ManifestCollection result);

        protected async Task<Passenger> PassengerDetailAsync(Dice random, string travelType, bool advancedCharacters)
        {
            var user = await m_NameService.CreateRandomPersonAsync(random);

            var result = new Passenger()
            {
                TravelType = travelType,
                Name = $"{user.FirstName} {user.LastName}",
                Gender = user.Gender,
                ApparentAge = 12 + random.D(1, 60),
            };
            Passenger.AddPassengerType(result, random);

            SimpleCharacterEngine.AddTrait(result, random);

            if (!advancedCharacters)
            {
                SimpleCharacterEngine.AddCharacteristics(result, random);

                //Add personality
                int personalityTraits = random.D(3);
                for (var i = 0; i < personalityTraits; i++)
                    result.Personality.Add(random.Choose(m_Personalities));
            }
            else
            {
                result.Seed = random.Next();
                var options = new CharacterBuilderOptions() { MaxAge = result.ApparentAge, Name = result.Name, Seed = result.Seed };
                var character = m_CharacterBuilder.Build(options);

                result.Strength += character.Strength;
                result.Dexterity += character.Dexterity;
                result.Endurance += character.Endurance;
                result.Intellect += character.Intellect;
                result.Education += character.Education;
                result.Social += character.SocialStanding;

                result.Skills = string.Join(", ", character.Skills.Where(s => s.Level > 0).Select(s => s.ToString()).OrderBy(s => s));

                result.Title = character.Title;
                result.Personality.AddRange(character.Personality);
            }

            return result;
        }

        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        protected abstract decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll);

        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        protected abstract decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll);

        public static StarportDetails CalculateStarportDetails(World origin, Dice dice, bool highPort)
        {
            var result = new StarportDetails();
            switch (origin.StarportCode.ToString())
            {
                case "A":
                    result.BerthingCost = dice.D(1, 6) * 1000;
                    result.BerthingCostPerDay = 500;
                    result.RefinedFuelCost = 500;
                    result.UnrefinedFuelCost = 100;

                    if (highPort)
                    {
                        result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-5"));
                        result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-4"));
                        result.BerthingWaitTimeCapital = WaitTime(dice, dice.D("1D6-4"));

                        result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-5"));
                        result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6-4"));
                        result.FuelWaitTimeCapital = WaitTime(dice, dice.D("1D6-3"));
                    }
                    else
                    {
                        result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-5"));
                        result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-5"));

                        result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-5"));
                        result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6-4"));
                    }
                    return result;

                case "B":
                    result.BerthingCost = dice.D(1, 6) * 500;
                    result.BerthingCostPerDay = 200;
                    result.RefinedFuelCost = 500;
                    result.UnrefinedFuelCost = 100;

                    if (highPort)
                    {
                        result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-5"));
                        result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-4"));
                        result.BerthingWaitTimeCapital = WaitTime(dice, dice.D("1D6-3"));

                        result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                        result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));
                        result.FuelWaitTimeCapital = WaitTime(dice, dice.D("1D6-1"));
                    }
                    else
                    {
                        result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-4"));
                        result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-3"));

                        result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                        result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));
                    }
                    return result;

                case "C":
                    result.BerthingCost = dice.D(1, 6) * 100;
                    result.BerthingCostPerDay = 100;
                    result.RefinedFuelCost = 500;
                    result.UnrefinedFuelCost = 100;

                    if (highPort)
                    {
                        result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                        result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));
                        result.BerthingWaitTimeCapital = WaitTime(dice, dice.D("1D6-1"));

                        result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                        result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));
                        result.FuelWaitTimeCapital = WaitTime(dice, dice.D("1D6-1"));
                    }
                    else
                    {
                        result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                        result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));

                        result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                        result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));
                    }
                    return result;

                case "D":
                    if (highPort) return null;

                    result.BerthingCost = dice.D(1, 6) * 10;
                    result.BerthingCostPerDay = 10;
                    result.UnrefinedFuelCost = 100;

                    result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-3"));
                    result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-2"));

                    result.FuelWaitTimeSmall = WaitTime(dice, dice.D("1D6-1"));
                    result.FuelWaitTimeStar = WaitTime(dice, dice.D("1D6"));
                    return result;

                case "E":
                    if (highPort) return null;

                    result.BerthingCost = 0;
                    result.BerthingCostPerDay = 0;

                    result.BerthingWaitTimeSmall = WaitTime(dice, dice.D("1D6-2"));
                    result.BerthingWaitTimeStar = WaitTime(dice, dice.D("1D6-1"));

                    return result;

                default: return null;
            }
        }

        static int PurchaseDM(World world, TradeGood good)
        {
            int purchase = int.MinValue;
            int sale = int.MinValue;

            foreach (var item in good.PurchaseDMs)
                if (world.ContainsRemark(item.Tag))
                    purchase = Math.Max(purchase, item.Bonus);

            foreach (var item in good.SaleDMs)
                if (world.ContainsRemark(item.Tag))
                    sale = Math.Max(sale, item.Bonus);

            if (purchase == int.MinValue)
                purchase = 0;
            if (sale == int.MinValue)
                sale = 0;
            return purchase - sale;
        }

        static int SaleDM(World world, TradeGood good)
        {
            return -PurchaseDM(world, good);
        }

        static string WaitTime(Dice dice, int roll)
        {
            if (roll < 0)
                roll = 0;

            switch (roll)
            {
                case 0: return "No wait";
                case 1: return $"{dice.D(6)} minutes";
                case 2: return $"{dice.D(6) * 10} minutes";
                case 3: return $"1 hour";
                case 4: return $"{dice.D(6) } hours";
                case 5: return $"{dice.D(2, 6) } hours";
                case 6: return $"1 day";
                default: return $"{dice.D(6) } days";
            }
        }

        private void AddTradeGood(World origin, Dice random, IList<TradeOffer> result, TradeGood good, bool advancedMode, int brokerScore)
        {
            if (string.IsNullOrEmpty(good.Tons))
                throw new ArgumentException("good.Tons is empty for " + good.Name);

            if (good.BasePrice == 0) //special case
            {
                var detail = good.ChooseRandomDetail(random);
                var lot = new TradeOffer()
                {
                    Type = good.Name,
                    Subtype = random.Choose(detail.NameList),
                    Tons = Math.Max(1, random.D(detail.Tons)),
                    BasePrice = detail.Price * 1000,
                    PurchaseDM = PurchaseDM(origin, good),
                    Legal = good.Legal,
                    TradeGood = good
                };

                if (!advancedMode) //move the names around
                {
                    lot.Type = lot.Subtype;
                    lot.Subtype = null;
                }

                int roll;
                lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out roll);
                lot.Roll = roll;

                result.Add(lot);
            }
            else if (!advancedMode)
            {
                var lot = new TradeOffer()
                {
                    Type = good.Name,
                    Subtype = null,
                    Tons = random.D(good.Tons),
                    BasePrice = good.BasePrice * 1000,
                    PurchaseDM = PurchaseDM(origin, good),
                    Legal = good.Legal,
                    TradeGood = good
                };

                int roll;
                lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out roll);
                lot.Roll = roll;

                result.Add(lot);
            }
            else
            {
                var tonsRemaining = random.D(good.Tons);
                while (tonsRemaining > 0)
                {
                    var detail = good.ChooseRandomDetail(random);
                    var lot = new TradeOffer()
                    {
                        Type = good.Name,
                        Subtype = random.Choose(detail.NameList),
                        Tons = Math.Min(tonsRemaining, random.D(detail.Tons)),
                        BasePrice = detail.Price * 1000,
                        PurchaseDM = PurchaseDM(origin, good),
                        Legal = good.Legal,
                        TradeGood = good
                    };

                    int roll;
                    lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out roll);
                    lot.Roll = roll;

                    result.Add(lot);

                    tonsRemaining -= lot.Tons;
                }
            }
        }

        /// <summary>
        /// This has the cargo, people, etc. that want to travel from one location to another.
        /// </summary>
        /// <param name="worlds">The worlds.</param>
        /// <param name="random">The random.</param>
        /// <param name="illegalGoods">if set to <c>true</c> [illegal goods].</param>
        /// <param name="advancedCharacters">if set to <c>true</c> [advanced characters].</param>
        /// <returns></returns>
        async Task<ManifestCollection> BuildManifestsAsync(IReadOnlyList<World> worlds, Dice random, bool illegalGoods, bool advancedCharacters)
        {
            var result = new ManifestCollection();
            result.Origin = worlds[0];
            for (var i = 1; i < worlds.Count; i++)
                if (!worlds[i].UWP.Contains("?")) //skip uncharted words
                    result.Add(await BuildManifestAsync(result.Origin, worlds[i], random, illegalGoods, advancedCharacters).ConfigureAwait(false));
            return result;
        }
    }
}
