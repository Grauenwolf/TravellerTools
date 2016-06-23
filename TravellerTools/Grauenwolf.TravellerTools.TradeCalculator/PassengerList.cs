using System.Collections.ObjectModel;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class PassengerList
    {
        readonly Collection<Passenger> m_Passengers = new Collection<Passenger>();

        public int LowPassengers { get; set; }

        public int MiddlePassengers { get; set; }

        public int HighPassengers { get; set; }

        public Collection<Passenger> Passengers
        {
            get { return m_Passengers; }
        }
    }
}
