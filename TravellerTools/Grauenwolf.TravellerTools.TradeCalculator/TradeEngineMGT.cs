using Grauenwolf.TravellerTools.Maps;
using System;
using System.Threading.Tasks;
namespace Grauenwolf.TravellerTools.TradeCalculator
{

    public class TradeEngineMGT : TradeEngine
    {
        protected override string DataFileName
        {
            get { return "TradeGoods-MGT.xml"; }
        }

        public override async Task<PassengerList> PassengersAsync(World origin, World destination, Dice random)
        {
            var result = new PassengerList();

            Action<string, string, string> SetValues = (low, middle, high) =>
            {
                result.LowPassengers = random.D(low);
                result.MiddlePassengers = random.D(middle);
                result.HighPassengers = random.D(high);
            };

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
                result.Passengers.Add(await PassengerDetailAsync(random, "High").ConfigureAwait(false));
            for (var i = 0; i < result.MiddlePassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Middle").ConfigureAwait(false));
            for (var i = 0; i < result.LowPassengers; i++)
                result.Passengers.Add(await PassengerDetailAsync(random, "Low").ConfigureAwait(false));

            return result;
        }


    }
}

