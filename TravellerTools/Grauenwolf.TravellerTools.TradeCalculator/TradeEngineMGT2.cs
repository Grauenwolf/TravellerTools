using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Math;

namespace Grauenwolf.TravellerTools.TradeCalculator
{



    public class TradeEngineMgt2 : TradeEngine
    {
        public TradeEngineMgt2(MapService mapService, string dataPath, INameService nameService) : base(mapService, dataPath, nameService) { }

        protected override string DataFileName
        {
            get { return "TradeGoods-MGT2.xml"; }
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
                var good = random.Choose(LegalTradeGoods);
                var detail = good.ChooseRandomDetail(random);
                lot.Contents = detail.Name;
                lot.ActualValue = detail.Price * 1000 * lot.Size;
            }

            result.Lots.AddRange(lots.OrderByDescending(f => f.Size));

            return result;
        }

        public override async Task<PassengerList> PassengersAsync(World origin, World destination, Dice random, bool advancedCharacters)
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
                result.Passengers.Add(await PassengerDetailAsync(random, "High", advancedCharacters).ConfigureAwait(false));
            for (var i = 0; i < result.MiddlePassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Middle", advancedCharacters).ConfigureAwait(false));
            for (var i = 0; i < result.BasicPassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Basic", advancedCharacters).ConfigureAwait(false));
            for (var i = 0; i < result.LowPassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Low", advancedCharacters).ConfigureAwait(false));

            return result;
        }

        internal override void OnManifestsBuilt(ManifestCollection result)
        {
            result.Edition = Edition.MGT2;
        }

        override protected decimal PurchasePriceModifier(Dice random, int purchaseBonus, int brokerScore, out int roll)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

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
            if (random == null)
                throw new ArgumentNullException(nameof(random), $"{nameof(random)} is null.");

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

        static int FreightCost(int distance)
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

        static string FreightTraffic(int modifier, Dice random)
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

        static string PassengerTraffic(int modifier, Dice random)
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

        public override World GenerateRandomWorld()
        {
            var dice = new Dice();


            EHex starportCode; // UWP[0]
            EHex sizeCode; // UWP[1]
            EHex atmosphereCode; // UWP[2]
            EHex hydrographicsCode; // UWP[3]
            EHex populationCode; // UWP[4]
            EHex governmentCode; // UWP[5]
            EHex lawCode; // UWP[6]
            EHex techCode; // UWP[8]

            sizeCode = Max(dice.D(2, 6) - 2, 0);

            atmosphereCode = Max(dice.D(2, 6) - 7 + sizeCode.Value, 0);


            if (sizeCode <= 1)
                hydrographicsCode = 0;
            else if (atmosphereCode <= 1 || atmosphereCode.Between('A', 'C'))
                hydrographicsCode = dice.D(2, 6) - 7 + atmosphereCode.Value - 4;
            else
                hydrographicsCode = dice.D(2, 6) - 7;

            hydrographicsCode = Max(hydrographicsCode.Value, 0);

            populationCode = Max(dice.D(2, 6) - 2, 0);
            governmentCode = Max(dice.D(2, 6) - 7 + populationCode.Value, 0);
            lawCode = Max(dice.D(2, 6) - 7 + governmentCode.Value, 0);

            var starportDM = 0;
            if (populationCode >= 10)
                starportDM += 2;
            else if (populationCode >= 8)
                starportDM += 1;
            else if (populationCode <= 2)
                starportDM -= 2;
            else if (populationCode <= 4)
                starportDM -= 1;

            var starportRoll = dice.D(2, 6) + starportDM;
            if (starportRoll <= 2)
                starportCode = 'X';
            else if (starportRoll == 3 || starportRoll == 4)
                starportCode = 'E';
            else if (starportRoll == 5 || starportRoll == 6)
                starportCode = 'D';
            else if (starportRoll == 7 || starportRoll == 8)
                starportCode = 'C';
            else if (starportRoll == 9 || starportRoll == 10)
                starportCode = 'B';
            else
                starportCode = 'A';



            var techDM = 0;
            if (starportCode == 'A')
                techDM += 6;
            else if (starportCode == 'B')
                techDM += 4;
            else if (starportCode == 'C')
                techDM += 2;
            else if (starportCode == 'X')
                techDM -= 4;

            if (sizeCode <= 1)
                techDM += 2;
            else if (sizeCode <= 4)
                techDM += 1;

            if (atmosphereCode <= 3)
                techDM += 1;
            else if (atmosphereCode >= 10)
                techDM += 1;

            if (hydrographicsCode == 0)
                techDM += 1;
            else if (hydrographicsCode == 9)
                techDM += 1;
            else if (hydrographicsCode == 10)
                techDM += 2;

            if (populationCode.Between(1, 5))
                techDM += 1;
            else if (populationCode == 9)
                techDM += 1;
            else if (populationCode == 9)
                techDM += 2;
            else if (populationCode == 10)
                techDM += 4;


            if (governmentCode == 0)
                techDM += 1;
            else if (governmentCode == 5)
                techDM += 1;
            else if (governmentCode == 7)
                techDM += 2;
            else if (governmentCode == 13 || governmentCode == 14)
                techDM += -2;

            techCode = Max(dice.D(1, 6) + techDM, 0);

            if (atmosphereCode.Between(0, 1) && techCode < 8)
                techCode = 8;
            else if (atmosphereCode.Between(2, 3) && techCode < 5)
                techCode = 5;
            else if ((atmosphereCode == 4 || atmosphereCode == 7 || atmosphereCode == 9) && techCode < 3)
                techCode = 3;
            else if (atmosphereCode == 10 && techCode < 8)
                techCode = 8;
            else if (atmosphereCode == 11 && techCode < 9)
                techCode = 9;
            else if (atmosphereCode == 12 && techCode < 10)
                techCode = 10;
            else if ((atmosphereCode == 13 || atmosphereCode == 14) && techCode < 5)
                techCode = 5;
            else if (atmosphereCode == 15 && techCode < 8)
                techCode = 8;

            var uwp = $"{starportCode}{sizeCode}{atmosphereCode}{hydrographicsCode}{populationCode}{governmentCode}{lawCode}-{techCode}";
            return new World(uwp, "Origin", 0);
        }
    }
}

