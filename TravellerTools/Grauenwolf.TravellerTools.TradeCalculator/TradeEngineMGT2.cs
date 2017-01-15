using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.TradeCalculator
{



    public class TradeEngineMgt2 : TradeEngine
    {
        protected override string DataFileName
        {
            get { return "TradeGoods-MGT2.xml"; }
        }

        string PassengerTraffic(int modifier, Dice random)
        {
            var roll = random.D(2, 6) + modifier;

            if (roll <= 1) return "0";
            if (roll <= 3) return "1D";
            if (roll <= 6) return "2D";
            if (roll <= 10) return "3D";
            if (roll <= 13) return "4D";
            if (roll <= 15) return "5D";
            if (roll == 16) return "6D";
            if (roll == 17) return "7D";
            if (roll == 18) return "8D";
            if (roll == 19) return "9D";
            return "10D";
        }

        string FreightTraffic(int modifier, Dice random)
        {
            var roll = random.D(2, 6) + modifier;

            if (roll <= 1) return "0";
            if (roll <= 3) return "1D";
            if (roll <= 5) return "2D";
            if (roll <= 8) return "3D";
            if (roll <= 11) return "4D";
            if (roll <= 14) return "5D";
            if (roll == 16) return "6D";
            if (roll == 17) return "7D";
            if (roll == 18) return "8D";
            if (roll == 19) return "9D";
            return "10D";
        }

        public override async Task<PassengerList> PassengersAsync(World origin, World destination, Dice random)
        {

            var baseDM = 0;
            var lowDM = 1;
            var basicDM = 0;
            var middleDM = 0;
            var highDM = -4;

            if (origin.PopulationCode.Value <= 1)
                baseDM += -4;
            else if (origin.PopulationCode.Value == 6 || origin.PopulationCode.Value == 7)
                baseDM += 1;
            else if (origin.PopulationCode.Value >= 8)
                baseDM += 3;

            switch (origin.StarportCode.ToString())
            {
                case "A": baseDM += 2; break;
                case "B": baseDM += 1; break;
                case "E": baseDM += -1; break;
                case "X": baseDM += -3; break;
            }

            if (origin.ContainsRemark("A")) baseDM += 1;
            if (origin.ContainsRemark("R")) baseDM += -4;

            var result = new PassengerList();
            result.LowPassengers = random.D(PassengerTraffic(baseDM + lowDM, random));
            result.BasicPassengers = random.D(PassengerTraffic(baseDM + basicDM, random));
            result.MiddlePassengers = random.D(PassengerTraffic(baseDM + middleDM, random));
            result.HighPassengers = random.D(PassengerTraffic(baseDM + highDM, random));

            for (var i = 0; i < result.HighPassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "High").ConfigureAwait(false));
            for (var i = 0; i < result.MiddlePassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Middle").ConfigureAwait(false));
            for (var i = 0; i < result.BasicPassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Basic").ConfigureAwait(false));
            for (var i = 0; i < result.LowPassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Low").ConfigureAwait(false));

            return result;
        }

        public override FreightList Freight(World origin, World destination, Dice random)
        {
            var result = new FreightList();

            var baseDM = 0;
            var incidentalDM = 2;
            var minorDM = 0;
            var majorDM = -4;

            if (origin.PopulationCode.Value <= 1)
                baseDM += -4;
            else if (origin.PopulationCode.Value == 6 || origin.PopulationCode.Value == 7)
                baseDM += 2;
            else if (origin.PopulationCode.Value >= 8)
                baseDM += 4;

            if (origin.TechCode.Value <= 6) baseDM += -1;
            if (origin.TechCode.Value >= 9) baseDM += -2;

            if (origin.ContainsRemark("A")) baseDM += -2;
            if (origin.ContainsRemark("R")) baseDM += -6;

            result.Incidental = random.D(FreightTraffic(baseDM + incidentalDM, random));
            result.Minor = random.D(FreightTraffic(baseDM + minorDM, random));
            result.Major = random.D(FreightTraffic(baseDM + majorDM, random));


            var lots = new List<FreightLot>();
            for (var i = 0; i < result.Incidental; i++)
            {
                int size = random.D("1D6");
                int value = FreightCost(destination.JumpDistance) * size;
                lots.Add(new FreightLot(size, value));
            }

            for (var i = 0; i < result.Minor; i++)
            {
                int size = random.D("1D6") * 5;
                int value = FreightCost(destination.JumpDistance) * size;
                lots.Add(new FreightLot(size, value));
            }

            for (var i = 0; i < result.Major; i++)
            {
                int size = random.D("1D6") * 10;
                int value = FreightCost(destination.JumpDistance) * size;
                lots.Add(new FreightLot(size, value));
            }

            //Add contents
            foreach (var lot in lots)
            {
                var good = random.Choose(m_LegalTradeGoods);
                var detail = good.ChooseRandomDetail(random);
                lot.Contents = detail.Name;
                lot.ActualValue = detail.Price * 1000 * lot.Size;
            }

            result.Lots.AddRange(lots.OrderByDescending(f => f.Size));

            return result;
        }

        int FreightCost(int distance)
        {
            switch (distance)
            {
                case 1: return 1000;
                case 2: return 1600;
                case 3: return 3000;
                case 4: return 7000;
                case 5: return 7700;
                case 6: return 86000;
                default: return 0;
            }
        }

        override protected decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll)
        {
            roll = random.D(3, 6) + purchaseBonus + brokerScore;
            if (roll < 0)
                return 2M;

            switch (roll)
            {
                case 0: return 1.75M;
                case 1: return 1.5M;
                case 2: return 1.35M;
                case 3: return 1.25M;
                case 4: return 1.20M;
                case 5: return 1.15M;
                case 6: return 1.1M;
                case 7: return 1.05M;
                case 8: return 1M;
                case 9: return .95M;
                case 10: return .9M;
                case 11: return 0.85M;
                case 12: return 0.8M;
                case 13: return 0.75M;
                case 14: return 0.7M;
                case 15: return 0.65M;
                case 16: return 0.60M;
                case 17: return 0.55M;
                case 18: return 0.50M;
                case 19: return 0.45M;
                case 20: return 0.4M;
                case 21: return 0.35M;
                case 22: return 0.30M;
                default: return 0.25M;
            }

        }

        override protected decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll)
        {
            roll = random.D(3, 6) + saleBonus + brokerScore;
            if (roll < 0)
                return 0.30M;

            switch (roll)
            {
                case 0: return 0.4M;
                case 1: return 0.45M;
                case 2: return 0.50M;
                case 3: return 0.55M;
                case 4: return 0.60M;
                case 5: return 0.65M;
                case 6: return 0.70M;
                case 7: return 0.75M;
                case 8: return 0.80M;
                case 9: return 0.85M;
                case 10: return 0.9M;
                case 11: return 1.0M;
                case 12: return 1.05M;
                case 13: return 1.10M;
                case 14: return 1.15M;
                case 15: return 1.20M;
                case 16: return 1.25M;
                case 17: return 1.30M;
                case 18: return 1.35M;
                case 19: return 1.4M;
                case 20: return 1.45M;
                case 21: return 1.50M;
                case 22: return 1.55M;
                default: return 1.60M;
            }

        }
        internal override void OnManifestsBuilt(ManifestCollection result)
        {
            result.Edition = Edition.MGT2;
        }
    }
}

