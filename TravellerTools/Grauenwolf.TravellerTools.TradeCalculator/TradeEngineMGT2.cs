using System;
using System.Linq;
using System.Threading.Tasks;
using Grauenwolf.TravellerTools.Maps;
namespace Grauenwolf.TravellerTools.TradeCalculator
{

    

    public class TradeEngineMGT2 : TradeEngine
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

        public override async Task<PassengerList> PassengersAsync(World origin, World destination, Dice random)
        {
            var result = new PassengerList();

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
    }
}

