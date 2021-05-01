using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class TradeEngineMgt : TradeEngine
    {
        public TradeEngineMgt(TravellerMapService mapService, string dataPath, NameGenerator nameGenerator) : base(mapService, dataPath, nameGenerator)
        {
        }

        protected override string DataFileName
        {
            get { return "TradeGoods-MGT.xml"; }
        }

        public override FreightList Freight(World origin, World destination, Dice random)
        {
            if (origin == null)
                throw new ArgumentNullException(nameof(origin), $"{nameof(origin)} is null.");

            if (destination == null)
                throw new ArgumentNullException(nameof(destination), $"{nameof(destination)} is null.");

            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

            var result = new FreightList();

            Action<string, string, string> SetValues = (low, middle, high) =>
            {
                result.Incidental = random.D(low);
                result.Minor = random.D(middle);
                result.Major = random.D(high);
            };

            var traffic = origin.PopulationCode.Value;

            if (origin.ContainsRemark("Ag")) traffic += 2;
            if (origin.ContainsRemark("Ai")) traffic += -3;
            if (origin.ContainsRemark("Ba")) traffic += -99999;
            if (origin.ContainsRemark("De")) traffic += -3;
            if (origin.ContainsRemark("Fl")) traffic += -3;
            if (origin.ContainsRemark("Ga")) traffic += 2;
            if (origin.ContainsRemark("Hi")) traffic += 2;
            //if (origin.ContainsRemark("Ht")) traffic += 0;
            if (origin.ContainsRemark("IC")) traffic += -3;
            if (origin.ContainsRemark("In")) traffic += 3;
            if (origin.ContainsRemark("Lo")) traffic += -5;
            //if (origin.ContainsRemark("Lt")) traffic += 0;
            if (origin.ContainsRemark("Na")) traffic += -3;
            if (origin.ContainsRemark("NI")) traffic += -3;
            if (origin.ContainsRemark("Po")) traffic += -3;
            if (origin.ContainsRemark("Ri")) traffic += 2;
            //if (origin.ContainsRemark("Va")) traffic += 0;
            if (origin.ContainsRemark("Wa")) traffic += -3;
            if (origin.ContainsRemark("A")) traffic += 5;
            if (origin.ContainsRemark("R")) traffic += -5;

            if (destination.ContainsRemark("Ag")) traffic += 1;
            if (destination.ContainsRemark("Ai")) traffic += 1;
            if (destination.ContainsRemark("Ba")) traffic += -5;
            if (destination.ContainsRemark("De")) traffic += 0;
            if (destination.ContainsRemark("Fl")) traffic += 0;
            if (destination.ContainsRemark("Ga")) traffic += 1;
            if (destination.ContainsRemark("Hi")) traffic += 0;
            //if (destination.ContainsRemark("Ht")) traffic += 0;
            if (destination.ContainsRemark("IC")) traffic += 0;
            if (destination.ContainsRemark("In")) traffic += 2;
            if (destination.ContainsRemark("Lo")) traffic += 0;
            //if (destination.ContainsRemark("Lt")) traffic += 0;
            if (destination.ContainsRemark("Na")) traffic += 1;
            if (destination.ContainsRemark("NI")) traffic += 1;
            if (destination.ContainsRemark("Po")) traffic += -3;
            if (destination.ContainsRemark("Ri")) traffic += 2;
            //if (destination.ContainsRemark("Va")) traffic += 0;
            if (destination.ContainsRemark("Wa")) traffic += 0;
            if (destination.ContainsRemark("A")) traffic += -5;
            if (destination.ContainsRemark("R")) traffic += -99999;

            var tlDiff = Math.Abs(origin.TechCode.Value - destination.TechCode.Value);
            if (tlDiff > 5) tlDiff = 5;
            traffic -= tlDiff;

            if (traffic <= 0) SetValues("0", "0", "0");
            if (traffic == 1) SetValues("0", "1D-4", "1D-4");
            if (traffic == 2) SetValues("0", "1D-1", "1D-2");
            if (traffic == 3) SetValues("0", "1D", "1D-1");
            if (traffic == 4) SetValues("0", "1D+1", "1D");
            if (traffic == 5) SetValues("0", "1D+2", "1D+1");
            if (traffic == 6) SetValues("0", "1D+3", "1D+2");
            if (traffic == 7) SetValues("0", "1D+4", "1D+3");
            if (traffic == 8) SetValues("0", "1D+5", "1D+4");
            if (traffic == 9) SetValues("1D-2", "1D+6", "1D+5");
            if (traffic == 10) SetValues("1D", "1D+7", "1D+6");
            if (traffic == 11) SetValues("1D+1", "1D+8", "1D+7");
            if (traffic == 12) SetValues("1D+2", "1D+9", "1D+8");
            if (traffic == 13) SetValues("1D+3", "1D+10", "1D+9");
            if (traffic == 14) SetValues("1D+4", "1D+12", "1D+10");
            if (traffic == 15) SetValues("1D+5", "1D+14", "1D+11");
            if (traffic >= 16) SetValues("1D+6", "1D+16", "1D+12");

            if (result.Incidental < 0) result.Incidental = 0;
            if (result.Minor < 0) result.Minor = 0;
            if (result.Major < 0) result.Major = 0;

            var lots = new List<FreightLot>();
            for (var i = 0; i < result.Incidental; i++)
            {
                int size = random.D("1D6");
                int value = (1000 + (destination.JumpDistance - 1 * 200)) * size;
                lots.Add(new FreightLot(size, value));
            }

            for (var i = 0; i < result.Minor; i++)
            {
                int size = random.D("1D6") * 5;
                int value = (1000 + (destination.JumpDistance - 1 * 200)) * size;
                lots.Add(new FreightLot(size, value));
            }

            for (var i = 0; i < result.Major; i++)
            {
                int size = random.D("1D6") * 10;
                int value = (1000 + ((destination.JumpDistance - 1) * 200)) * size;
                lots.Add(new FreightLot(size, value));
            }

            //Add contents
            foreach (var lot in lots)
            {
                var good = random.Choose(LegalTradeGoods);
                var detail = good.ChooseRandomDetail(random);
                lot.Contents = detail.Name;
                lot.ActualValue = detail.Price * 1000 * lot.Size;
            }

            result.Lots.AddRange(lots.OrderByDescending(f => f.Size));

            return result;
        }

        public override PassengerList Passengers(World origin, World destination, Dice random, bool advancedCharacters)
        {
            var result = new PassengerList();

            void SetValues(string low, string middle, string high)
            {
                result.LowPassengers = random.D(low);
                result.MiddlePassengers = random.D(middle);
                result.HighPassengers = random.D(high);
            }

            var traffic = origin.PopulationCode.Value;

            if (origin.ContainsRemark("Ag")) traffic += 0;
            if (origin.ContainsRemark("Ai")) traffic += 1;
            if (origin.ContainsRemark("Ba")) traffic += -5;
            if (origin.ContainsRemark("De")) traffic += -1;
            if (origin.ContainsRemark("Fl")) traffic += 0;
            if (origin.ContainsRemark("Ga")) traffic += 2;
            if (origin.ContainsRemark("Hi")) traffic += 0;
            //if (origin.ContainsRemark("Ht")) traffic += 0;
            if (origin.ContainsRemark("IC")) traffic += 1;
            if (origin.ContainsRemark("In")) traffic += 2;
            if (origin.ContainsRemark("Lo")) traffic += 0;
            //if (origin.ContainsRemark("Lt")) traffic += 0;
            if (origin.ContainsRemark("Na")) traffic += 0;
            if (origin.ContainsRemark("NI")) traffic += 0;
            if (origin.ContainsRemark("Po")) traffic += -2;
            if (origin.ContainsRemark("Ri")) traffic += -1;
            //if (origin.ContainsRemark("Va")) traffic += 0;
            if (origin.ContainsRemark("Wa")) traffic += 0;
            if (origin.ContainsRemark("A")) traffic += 2;
            if (origin.ContainsRemark("R")) traffic += 4;

            if (destination.ContainsRemark("Ag")) traffic += 0;
            if (destination.ContainsRemark("Ai")) traffic += -1;
            if (destination.ContainsRemark("Ba")) traffic += -5;
            if (destination.ContainsRemark("De")) traffic += -1;
            if (destination.ContainsRemark("Fl")) traffic += 0;
            if (destination.ContainsRemark("Ga")) traffic += 2;
            if (destination.ContainsRemark("Hi")) traffic += 4;
            //if (destination.ContainsRemark("Ht")) traffic += 0;
            if (destination.ContainsRemark("IC")) traffic += -1;
            if (destination.ContainsRemark("In")) traffic += 1;
            if (destination.ContainsRemark("Lo")) traffic += -4;
            //if (destination.ContainsRemark("Lt")) traffic += 0;
            if (destination.ContainsRemark("Na")) traffic += 0;
            if (destination.ContainsRemark("NI")) traffic += -1;
            if (destination.ContainsRemark("Po")) traffic += -1;
            if (destination.ContainsRemark("Ri")) traffic += 2;
            //if (destination.ContainsRemark("Va")) traffic += 0;
            if (destination.ContainsRemark("Wa")) traffic += 0;
            if (destination.ContainsRemark("A")) traffic += -2;
            if (destination.ContainsRemark("R")) traffic += -4;

            if (traffic <= 0) SetValues("0", "0", "0");
            if (traffic == 1) SetValues("2D-6", "1D-2", "0");
            if (traffic == 2) SetValues("2D", "1D", "1D-1D");
            if (traffic == 3) SetValues("2D", "2D-1D", "2D-2D");
            if (traffic == 4) SetValues("3D-1D", "2D-1D", "2D-1D");
            if (traffic == 5) SetValues("3D-1D", "3D-2D", "2D-1D");
            if (traffic == 6) SetValues("3D", "3D-2D", "3D-2D");
            if (traffic == 7) SetValues("3D", "3D-2D", "3D-2D");
            if (traffic == 8) SetValues("4D", "3D-1D", "3D-1D");
            if (traffic == 9) SetValues("4D", "3D", "3D-1D");
            if (traffic == 10) SetValues("5D", "3D", "3D-1D");
            if (traffic == 11) SetValues("5D", "4D", "3D");
            if (traffic == 12) SetValues("6D", "4D", "3D");
            if (traffic == 13) SetValues("6D", "4D", "4D");
            if (traffic == 14) SetValues("7D", "5D", "4D");
            if (traffic == 15) SetValues("8D", "5D", "4D");
            if (traffic >= 16) SetValues("9D", "6D", "5D");

            if (result.LowPassengers < 0) result.LowPassengers = 0;
            if (result.MiddlePassengers < 0) result.MiddlePassengers = 0;
            if (result.HighPassengers < 0) result.HighPassengers = 0;

            for (var i = 0; i < result.HighPassengers; i++)
                result.Passengers.Add(PassengerDetail(random, "High", advancedCharacters));
            for (var i = 0; i < result.MiddlePassengers; i++)
                result.Passengers.Add(PassengerDetail(random, "Middle", advancedCharacters));
            for (var i = 0; i < result.LowPassengers; i++)
                result.Passengers.Add(PassengerDetail(random, "Low", advancedCharacters));

            return result;
        }

        internal override void OnManifestsBuilt(ManifestCollection result)
        {
            result.Edition = Edition.MGT;
        }

        override protected decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

            roll = random.D(3, 6) + purchaseBonus + brokerScore;
            if (roll < 0)
                return 4M;

            return roll switch
            {
                0 => 3M,
                1 => 2M,
                2 => 1.75M,
                3 => 1.5M,
                4 => 1.35M,
                5 => 1.25M,
                6 => 1.2M,
                7 => 1.15M,
                8 => 1.1M,
                9 => 1.05M,
                10 => 1M,
                11 => 0.95M,
                12 => 0.9M,
                13 => 0.85M,
                14 => 0.8M,
                15 => 0.75M,
                16 => 0.7M,
                17 => 0.65M,
                18 => 0.55M,
                19 => 0.5M,
                20 => 0.4M,
                _ => 0.25M,
            };
        }

        override protected decimal SalePriceModifier(Dice random, int saleBonus, int brokerScore, out int roll)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

            roll = random.D(3, 6) + saleBonus + brokerScore;
            if (roll < 0)
                return 0.25M;

            return roll switch
            {
                0 => 0.45M,
                1 => 0.50M,
                2 => 0.55M,
                3 => 0.60M,
                4 => 0.65M,
                5 => 0.75M,
                6 => 0.80M,
                7 => 0.85M,
                8 => 0.90M,
                9 => 0.95M,
                10 => 1M,
                11 => 1.05M,
                12 => 1.1M,
                13 => 1.15M,
                14 => 1.2M,
                15 => 1.25M,
                16 => 1.35M,
                17 => 1.5M,
                18 => 1.75M,
                19 => 2M,
                20 => 3M,
                _ => 4M,
            };
        }

        public override World GenerateRandomWorld()
        {
            throw new NotImplementedException();
        }
    }
}
