using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public abstract class TradeEngine
    {
        readonly CharacterBuilder m_CharacterBuilder;
        readonly NameGenerator m_NameGenerator;
        ImmutableArray<string> m_Personalities;

        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected TradeEngine(TravellerMapService mapService, string dataPath, NameGenerator nameGenerator)
        {
            MapService = mapService;
            m_NameGenerator = nameGenerator;
            var file = new FileInfo(Path.Combine(dataPath, DataFileName));
            var converter = new XmlSerializer(typeof(TradeGoods));
            using (var stream = file.OpenRead())
                TradeGoods = ((TradeGoods)converter.Deserialize(stream)!).TradeGood.ToImmutableList();

            LegalTradeGoods = TradeGoods.Where(g => g.Legal).ToImmutableList();
            m_CharacterBuilder = new CharacterBuilder(dataPath, m_NameGenerator);

            var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
            m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();
        }

        public TravellerMapService MapService { get; }
        protected abstract string DataFileName { get; }
        protected ImmutableList<TradeGood> LegalTradeGoods { get; }
        protected ImmutableList<TradeGood> TradeGoods { get; }

        protected virtual bool UseCounterpartyScore => false;

        public static StarportDetails? CalculateStarportDetails(World origin, Dice dice, bool highPort)
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

        /// <summary>
        /// Builds the trade goods list.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="advancedMode">if set to <c>true</c> [advanced mode].</param>
        /// <param name="illegalGoods">if set to <c>true</c> [illegal goods].</param>
        /// <param name="brokerScore">The broker score.</param>
        /// <param name="random">The random.</param>
        /// <param name="raffleGoods">if set to <c>true</c> [raffle goods].</param>
        /// <param name="streetwiseScore">The streetwise score.</param>
        /// <param name="counterpartyScore">The counterparty score. This is only used in MGT2022.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>TradeGoodsList.</returns>
        /// <exception cref="System.ArgumentNullException">origin</exception>
        /// <exception cref="System.ArgumentNullException">random</exception>
        public TradeGoodsList BuildTradeGoodsList(World origin, bool advancedMode, bool illegalGoods, int brokerScore, Dice random, bool raffleGoods, int streetwiseScore, int counterpartyScore, World? destination = null)
        {
            if (origin == null)
                throw new ArgumentNullException(nameof(origin), $"{nameof(origin)} is null.");

            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

            if (!UseCounterpartyScore)
                counterpartyScore = 0;

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
                 * PopulationCode goods are selected by the raffle
                 */
                foreach (var good in goods)
                {
                    if (good.Availability == "*")
                    {
                        AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, true);
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
                    var isCommonGood = good.AvailabilityList.Any(a => origin.ContainsRemark(a));
                    AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, isCommonGood);
                    randomGoods = randomGoods.Where(g => g != good).ToList();
                }
            }
            else
            {
                /*
                 * Goods with *: Always available
                 * Matching Trade remarks: Always available
                 * Other goods: 1 chance. Population Selected
                 */

                foreach (var good in goods)
                {
                    if (good.Availability == "*")
                    {
                        AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, true);
                    }
                    else if (good.AvailabilityList.Any(a => origin.ContainsRemark(a)))
                    {
                        AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, true);
                    }
                    else
                    {
                        randomGoods.Add(good);
                    }
                }

                var picks = Math.Min(randomGoods.Count, origin.PopulationCode.Value);
                for (var i = 0; i < picks; i++)
                {
                    var good = random.Pick(randomGoods);
                    AddTradeGood(origin, random, availableLots, good, advancedMode, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, false);
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
                                bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, out roll);
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
                        bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, out roll);
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
                            bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, (good.Legal ? brokerScore : streetwiseScore) - counterpartyScore, out roll);
                            bid.Roll = roll;

                            requests.Add(bid);
                        }

            result.Lots.AddRange(availableLots.OrderBy(r => r.Type).ThenBy(r => r.Subtype));
            result.Bids.AddRange(requests.OrderBy(r => r.Type).ThenBy(r => r.Subtype));

            return result;
        }

        public abstract FreightList Freight(World origin, World destination, Dice random, bool variableFees);

        public abstract PassengerList Passengers(World origin, World destination, Dice random, bool variablePrice);

        //internal abstract void OnManifestsBuilt(ManifestCollection result);

        protected static FreightLot GenerateMail(World origin, Dice dice, int traffic)
        {
            var mailDM = traffic switch
            {
                <= -10 => -2,
                <= -5 => -1,
                <= 4 => 0,
                <= 9 => 1,
                _ => 2
            };
            if (origin.TechCode.Value <= 5)
                mailDM += -4;

            var mailRoll = 12 - mailDM;

            var containerCount = dice.D(6);
            var mailLot = new FreightLot(5 * containerCount, 25000 * containerCount) { Contents = $"{containerCount} mail containers.", Size = 5 * containerCount, MailRoll = mailRoll };
            return mailLot;
        }

        protected void AddLotDetails(World origin, World destination, Dice dice, List<FreightLot> lots, bool variableFees)
        {
            var govTable = new OddsTable<string>
            {
                { origin.Sector + " sector goverment", 2 },
                { destination.Sector + " sector goverment", 2 },
                { origin.SubsectorName + " subsector goverment", 4 },
                { destination.SubsectorName + " subsector goverment", 4 },
                { origin.Name + " local goverment", 8 },
                { destination.Name + " local goverment", 8 }
            };

            if (origin.AllegianceName != null)
                govTable.Add(origin.AllegianceName + " govenment", 1);
            if (destination.AllegianceName != null)
                govTable.Add(destination.AllegianceName + " govenment", 1);

            foreach (var mBase in origin.MilitaryBases)
                govTable.Add(origin.Name + " " + mBase.Description, 8);
            foreach (var mBase in destination.MilitaryBases)
                govTable.Add(destination.Name + " " + mBase.Description, 8);

            foreach (var lot in lots)
            {
                if (variableFees)
                {
                    var feeModifer = dice.D(2, 6) switch
                    {
                        2 => 0.5M,
                        3 => 0.6M,
                        4 => 0.7M,
                        5 => 0.8M,
                        6 => 0.9M,
                        7 => 1.0M,
                        8 => 1.1M,
                        9 => 1.2M,
                        10 => 1.3M,
                        11 => 1.4M,
                        _ => 1.5M,
                    };

                    lot.ShippingFee = (int)Math.Floor(feeModifer * lot.ShippingFee);
                }

                var declaredValueModifer = dice.D(4, 6) switch
                {
                    4 => .10M,
                    5 => .20M,
                    6 => .30M,
                    7 => .40M,
                    8 => .50M,
                    9 => .60M,
                    10 => .70M,
                    11 => .80M,
                    12 => .90M,
                    13 => 1.00M,
                    14 => 1.25M,
                    15 => 1.50M,
                    16 => 1.75M,
                    17 => 2.00M,
                    18 => 2.50M,
                    19 => 3.00M,
                    20 => 3.50M,
                    21 => 4.00M,
                    22 => 5.00M,
                    23 => 7.50M,
                    _ => 10.00M
                };

                var good = dice.Choose(LegalTradeGoods);
                var detail = good.ChooseRandomDetail(dice);
                lot.Contents = detail.Name;
                lot.DeclaredValue = Math.Floor(detail.Price * 1000 * lot.Size * declaredValueModifer);
                lot.LateFee = (int)Math.Floor(lot.ShippingFee * ((dice.D(1, 6) + 4) * 0.1));

                var baseDays = destination.JumpDistance switch
                {
                    1 or 2 => 14.0M,
                    3 or 4 => 28.0M,
                    5 or 6 => 42.0M,
                    _ => 999.0M,
                };

                var dayModifer = dice.D(2, 6) switch
                {
                    2 => 0.5M,
                    3 => 0.6M,
                    4 => 0.7M,
                    5 => 0.8M,
                    6 => 0.9M,
                    7 => 1.0M,
                    8 => 1.1M,
                    9 => 1.2M,
                    10 => 1.3M,
                    11 => 1.4M,
                    _ => 1.5M,
                };

                lot.DueInDays = (int)Math.Ceiling(baseDays * dayModifer);

                switch (dice.D(20))
                {
                    case <= 17: //company 85%
                        lot.Owner = m_NameGenerator.CreateCompanyName(dice); //5% chance of megacorp
                        lot.OwnerIsMegacorp = m_NameGenerator.IsMegacorp(lot.Owner);
                        break;

                    case 18: //govenment 5%
                        lot.Owner = govTable.Choose(dice);
                        break;

                    default: //individual 10%
                        {
                            var user = m_NameGenerator.CreateRandomPerson(dice);

                            var options = new CharacterBuilderOptions() { MaxAge = 22 + dice.D(1, 50), Gender = user.Gender, Name = $"{user.FirstName} {user.LastName}", Seed = dice.Next() };

                            lot.OwnerCharacter = m_CharacterBuilder.Build(options);
                            lot.Owner = (lot.OwnerCharacter.Title + " " + lot.OwnerCharacter.Name).Trim();
                            break;
                        }
                }
            }
        }

        protected Passenger PassengerDetail(Dice dice, string travelType, decimal ticketPrice, bool variablePrice)
        {
            var user = m_NameGenerator.CreateRandomPerson(dice);

            var result = new Passenger()
            {
                TravelType = travelType,
                Name = $"{user.FirstName} {user.LastName}",
                Gender = user.Gender,
                ApparentAge = 12 + dice.D(1, 60),
                TicketPrice = ticketPrice,
            };

            if (variablePrice)
            {
                var dayModifer = dice.D(2, 6) switch
                {
                    2 => 0M,
                    3 => 0.50M,
                    4 => 0.75M,
                    5 => 1.00M,
                    6 => 1.00M,
                    7 => 1.00M,
                    8 => 1.00M,
                    9 => 1.00M,
                    10 => 1.25M,
                    11 => 1.50M,
                    _ => 2.00M,
                };
                result.TicketPrice *= dayModifer;
            }

            Passenger.AddPassengerType(result, dice);

            SimpleCharacterEngine.AddTrait(result, dice);

            //if (!advancedCharacters)
            //{
            //    SimpleCharacterEngine.AddCharacteristics(result, random);

            //    //Add personality
            //    int personalityTraits = random.D(3);
            //    for (var i = 0; i < personalityTraits; i++)
            //        result.Personality.Add(random.Choose(m_Personalities));
            //}
            //else
            //{
            result.Seed = dice.Next();
            var options = new CharacterBuilderOptions() { MaxAge = result.ApparentAge, Gender = result.Gender, Name = result.Name, Seed = result.Seed };
            var character = m_CharacterBuilder.Build(options);

            result.PermalinkDetails = character.GetCharacterBuilderOptions().ToQueryString();
            result.Strength += character.Strength;
            result.Dexterity += character.Dexterity;
            result.Endurance += character.Endurance;
            result.Intellect += character.Intellect;
            result.Education += character.Education;
            result.Social += character.SocialStanding;

            result.Skills = string.Join(", ", character.Skills.Where(s => s.Level > 0).Select(s => s.ToString()).OrderBy(s => s));

            result.Title = character.Title;
            result.Personality.AddRange(character.Personality);
            //}

            return result;
        }

        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        protected abstract decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll);

        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        protected abstract decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll);

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

            return roll switch
            {
                0 => "No wait",
                1 => $"{dice.D(6)} minutes",
                2 => $"{dice.D(6) * 10} minutes",
                3 => $"1 hour",
                4 => $"{dice.D(6)} hours",
                5 => $"{dice.D(2, 6)} hours",
                6 => $"1 day",
                _ => $"{dice.D(6)} days",
            };
        }

        private void AddTradeGood(World origin, Dice random, IList<TradeOffer> result, TradeGood good, bool advancedMode, int brokerScore, bool isCommonGood)
        {
            if (string.IsNullOrEmpty(good.Tons))
                throw new ArgumentException("good.Tons is empty for " + good.Name);

            if (good.BasePrice == 0) //special case
            {
                var detail = good.ChooseRandomDetail(random);
                var lot = new TradeOffer(
                    type: good.Name,
                    subtype: random.Choose(detail.NameList),
                    tons: Math.Max(1, random.D(detail.Tons)),
                    basePrice: detail.Price * 1000,
                    purchaseDM: PurchaseDM(origin, good),
                    legal: good.Legal,
                    tradeGood: good,
                    isCommonGood: isCommonGood
                );

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
                var lot = new TradeOffer
                (
                    type: good.Name,
                    subtype: null,
                    tons: random.D(good.Tons),
                    basePrice: good.BasePrice * 1000,
                    purchaseDM: PurchaseDM(origin, good),
                    legal: good.Legal,
                    tradeGood: good,
                    isCommonGood: isCommonGood
                );

                lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out int roll);
                lot.Roll = roll;

                result.Add(lot);
            }
            else
            {
                var tonsRemaining = random.D(good.Tons);
                while (tonsRemaining > 0)
                {
                    var detail = good.ChooseRandomDetail(random);
                    var lot = new TradeOffer
                    (
                        type: good.Name,
                        subtype: random.Choose(detail.NameList),
                        tons: Math.Min(tonsRemaining, random.D(detail.Tons)),
                        basePrice: detail.Price * 1000,
                        purchaseDM: PurchaseDM(origin, good),
                        legal: good.Legal,
                        tradeGood: good,
                        isCommonGood: isCommonGood
                    );

                    lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out int roll);
                    lot.Roll = roll;

                    result.Add(lot);

                    tonsRemaining -= lot.Tons;
                }
            }
        }

        ///// <summary>
        ///// This has the cargo, people, etc. that want to travel from one location to another.
        ///// </summary>
        ///// <param name="worlds">The worlds.</param>
        ///// <param name="random">The random.</param>
        ///// <param name="illegalGoods">if set to <c>true</c> [illegal goods].</param>
        ///// <param name="advancedCharacters">if set to <c>true</c> [advanced characters].</param>
        ///// <returns></returns>
        //ManifestCollection BuildManifests(IReadOnlyList<World> worlds, Dice random, bool illegalGoods, bool advancedCharacters)
        //{
        //    var result = new ManifestCollection(worlds[0]);

        //    for (var i = 1; i < worlds.Count; i++)
        //        if (worlds[i].UWP != null && !worlds[i].UWP!.Contains("?")) //skip uncharted words
        //            result.Add(BuildManifest(result.Origin, worlds[i], random, illegalGoods, advancedCharacters));

        //    return result;
        //}
    }
}
