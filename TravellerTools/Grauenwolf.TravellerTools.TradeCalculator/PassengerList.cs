using System.Collections.ObjectModel;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class PassengerList
    {

        public int BasicPassengers { get; set; }
        public int HighPassengers { get; set; }
        public int LowPassengers { get; set; }
        public int MiddlePassengers { get; set; }
        public Collection<Passenger> Passengers { get; } = new Collection<Passenger>();
    }
}
