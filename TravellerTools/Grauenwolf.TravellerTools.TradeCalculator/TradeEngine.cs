
using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public abstract class TradeEngine
    {
        protected ImmutableList<TradeGood> m_LegalTradeGoods;
        protected ImmutableList<TradeGood> m_TradeGoods;

        protected abstract string DataFileName { get; }


        public async Task<Manifest> BuildManifestAsync(World origin, World destination, Dice random, bool illegalGoods)
        {
            var result = new Manifest() { Origin = origin, Destination = destination };
            result.PassengerList = await PassengersAsync(origin, destination, random).ConfigureAwait(false);
            result.FreightList = Freight(origin, destination, random);

            IReadOnlyList<TradeGood> goods;
            if (!illegalGoods)
                goods = m_LegalTradeGoods;
            else
                goods = m_TradeGoods;


            var offers = new List<TradeOffer>();
            var bids = new List<TradeBid>();

            foreach (var good in goods)
            {

                //List the goods that are readily available or usually cheap on this planet
                var purchaseDM = PurchaseDM(destination, good);
                if (purchaseDM > 0 || (purchaseDM >= 0 && good.Availability == "*") || (good.AvailabilityList.Any(a => destination.ContainsRemark(a))))
                    offers.Add(new TradeOffer() { Type = good.Name, PurchaseDM = purchaseDM });

                //List the goods that are usually desired on this planet
                var saleDM = SaleDM(destination, good);
                if (saleDM > 0)
                    bids.Add(new TradeBid()
                    {
                        Type = good.Name,
                        SaleDM = saleDM,
                    });
            }

            result.Offers.AddRange(offers.OrderBy(o => o.Type));
            result.Bids.AddRange(bids.OrderBy(o => o.Type));



            return result;
        }

        public async Task<ManifestCollection> BuildManifestsAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance, bool advancedMode, bool illegalGoods, int brokerScore)
        {
            var random = new Dice();

            var worlds = await TravellerMapService.WorldsNearAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance).ConfigureAwait(false);
            var result = await BuildManifestsAsync(worlds, random, illegalGoods).ConfigureAwait(false);

            result.TradeList = BuildTradeList(result.Origin, advancedMode, illegalGoods, brokerScore);

            result.SectorX = sectorX;
            result.SectorY = sectorY;
            result.HexX = hexX;
            result.HexY = hexY;
            result.MaxJumpDistance = maxJumpDistance;
            result.BerthingCost = CalculateBerthignCost(result.Origin, random);
            result.AdvancedMode = advancedMode;
            result.IllegalGoods = illegalGoods;
            result.BrokerScore = brokerScore;

            OnManifestsBuilt(result);

            return result;
        }

        internal abstract void OnManifestsBuilt(ManifestCollection result);

        public TradeList BuildTradeList(World origin, bool advancedMode, bool illegalGoods, int brokerScore)
        {
            IReadOnlyList<TradeGood> goods;
            if (!illegalGoods)
                goods = m_LegalTradeGoods;
            else
                goods = m_TradeGoods;

            var random = new Dice();
            var result = new TradeList();

            List<TradeOffer> availableLots = new List<TradeOffer>();

            foreach (var good in goods)
                if (good.Availability == "*" || (good.AvailabilityList.Any(a => origin.ContainsRemark(a))))
                    AddTradeGood(origin, random, availableLots, good, advancedMode, brokerScore);

            foreach (var good in random.Choose(goods, 6))
                AddTradeGood(origin, random, availableLots, good, advancedMode, brokerScore);

            //TODO - deduplicator

            List<TradeBid> requests = new List<TradeBid>();
            if (!advancedMode)
                foreach (var good in goods)
                {
                    var bid = new TradeBid()
                    {
                        Type = good.Name,
                        Subtype = null,
                        BasePrice = good.BasePrice * 1000,
                        SaleDM = SaleDM(origin, good),
                    };

                    //TODO: Auto-bump the price so that the merchant isn't buying from the PCs at a higher price than he would sell to them 
                    int roll;
                    bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, brokerScore, out roll);
                    bid.Roll = roll;

                    requests.Add(bid);
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
                            };

                            //TODO: Auto-bump the price so that the merchant isn't buying from the PCs at a higher price than he would sell to them 
                            int roll;
                            bid.PriceModifier = SalePriceModifier(random, bid.SaleDM, brokerScore, out roll);
                            bid.Roll = roll;

                            requests.Add(bid);
                        }

            result.Lots.AddRange(availableLots.OrderBy(r => r.Type).ThenBy(r => r.Subtype));
            result.Bids.AddRange(requests.OrderBy(r => r.Type).ThenBy(r => r.Subtype));

            return result;
        }

        public abstract FreightList Freight(World origin, World destination, Dice random);


        public abstract Task<PassengerList> PassengersAsync(World origin, World destination, Dice random);



        public string PassengerTrait(Dice random, ref bool isPatron)
        {
            int roll1 = random.D66();

            switch (roll1)
            {
                case 11: return "Loyal";
                case 12: return "Distracted by other worries";
                case 13: return "In debt to criminals";
                case 14: return "Makes very bad jokes";
                case 15: return "Will betray characters";
                case 16: return "Aggressive";

                case 21: return "Has secret allies";
                case 22: return "Secret anagathic user";
                case 23: return "Looking for something";
                case 24: return "Helpful";
                case 25: return "Forgetful";
                case 26:
                    isPatron = true;
                    return "Wants to hire the characters";

                case 31: return "Has useful contacts";
                case 32: return "Artistic";
                case 33: return "Easily confused";
                case 34: return "Unusually ugly";
                case 35: return "Worried about current situation";
                case 36: return "Shows pictures of children";

                case 41: return "Rumor-monger";
                case 42: return "Unusually provincial";
                case 43: return "Drunkard or drug addict";
                case 44: return "Government informant";
                case 45: return "Mistakes a PC for someone else";
                case 46: return "Possess unusually advanced technology";

                case 51: return "Unusually handsome or beautiful";
                case 52: return "Spying on the characters";
                case 53: return "Possesses a TAS membership";
                case 54: return "Is secretly hostile to characters";
                case 55: return "Wants to borrow money";
                case 56: return "Is convinced the PCs are dangerous";

                case 61: return "Involved in political intrigue";
                case 62: return "Has a dangerous secret";
                case 63: return "Wants to get off-planet as soon as possible";
                case 64: return "Attracted to a player character";
                case 65: return "From offworld";
                case 66: return "Possesses telepathy or other usual ability";
            }
            return null;
        }

        public void SetDataPath(string dataPath)
        {
            var file = new FileInfo(Path.Combine(dataPath, DataFileName));
            var converter = new XmlSerializer(typeof(TradeGoods));
            using (var stream = file.OpenRead())
                m_TradeGoods = ((TradeGoods)converter.Deserialize(stream)).TradeGood.ToImmutableList();

            m_LegalTradeGoods = m_TradeGoods.Where(g => g.Legal).ToImmutableList();
        }

        private void AddTradeGood(World origin, Dice random, IList<TradeOffer> result, TradeGood good, bool advancedMode, int brokerScore)
        {
            if (string.IsNullOrEmpty(good.Tons))
                throw new ArgumentException("good.Tons is empty for " + good.Name);

            var tonsRemaining = random.D(good.Tons);
            if (!advancedMode)
            {
                var lot = new TradeOffer()
                {
                    Type = good.Name,
                    Subtype = null,
                    Tons = tonsRemaining,
                    BasePrice = good.BasePrice * 1000,
                    PurchaseDM = PurchaseDM(origin, good)
                };

                int roll;
                lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out roll);
                lot.Roll = roll;

                result.Add(lot);
            }
            else
                while (tonsRemaining > 0)
                {
                    var detail = good.ChooseRandomDetail(random);
                    var lot = new TradeOffer()
                    {
                        Type = good.Name,
                        Subtype = random.Choose(detail.NameList),
                        Tons = Math.Min(tonsRemaining, random.D(detail.Tons)),
                        BasePrice = detail.Price * 1000,
                        PurchaseDM = PurchaseDM(origin, good)
                    };

                    int roll;
                    lot.PriceModifier = PurchasePriceModifier(random, lot.PurchaseDM, brokerScore, out roll);
                    lot.Roll = roll;


                    result.Add(lot);

                    tonsRemaining -= lot.Tons;
                }
        }

        async Task<ManifestCollection> BuildManifestsAsync(List<World> worlds, Dice random, bool illegalGoods)
        {
            var result = new ManifestCollection();
            result.Origin = worlds[0];
            for (var i = 1; i < worlds.Count; i++)
                result.Add(await BuildManifestAsync(result.Origin, worlds[i], random, illegalGoods).ConfigureAwait(false));
            return result;
        }

        private int CalculateBerthignCost(World origin, Dice random)
        {
            switch (origin.StarportCode.ToString())
            {
                case "A": return random.D(1, 6) * 1000;
                case "B": return random.D(1, 6) * 500;
                case "C": return random.D(1, 6) * 100;
                case "D": return random.D(1, 6) * 10;
                default: return 0;
            }
        }


        protected async Task<Passenger> PassengerDetailAsync(Dice random, string travelType)
        {
            var user = await NameService.CreateRandomPersonAsync().ConfigureAwait(false);

            bool isPatron = false;

            var result = new Passenger()
            {
                TravelType = travelType,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                ApparentAge = 12 + random.D(1, 60),
            };
            Passenger.AddPassengerType(result, random);

            SimpleCharacterEngine.AddTrait(result, random);
            SimpleCharacterEngine.AddCharacteristics(result, random);


            if (isPatron)
            {
                //TODO: add support for patron features
            }

            return result;
        }

        int PurchaseDM(World world, TradeGood good)
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

        protected abstract decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll);



        int SaleDM(World world, TradeGood good)
        {
            return -PurchaseDM(world, good);
        }

        protected abstract decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll);

    }




}

