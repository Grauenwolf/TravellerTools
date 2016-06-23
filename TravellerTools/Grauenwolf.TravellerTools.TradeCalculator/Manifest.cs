using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class Manifest
    {
        readonly List<TradeBid> m_Bids = new List<TradeBid>();
        readonly List<TradeOffer> m_Offers = new List<TradeOffer>();

        public World Origin { get; set; }
        public World Destination { get; set; }
        public PassengerList PassengerList { get; set; }
        public FreightList FreightList { get; set; }

        public List<TradeOffer> Offers
        {
            get { return m_Offers; }
        }

        public List<TradeBid> Bids
        {
            get { return m_Bids; }
        }

    }
}
