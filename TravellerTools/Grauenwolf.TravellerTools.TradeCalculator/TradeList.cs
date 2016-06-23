using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class TradeList
    {
        readonly List<TradeOffer> m_Offers = new List<TradeOffer>();
        readonly List<TradeBid> m_Bids = new List<TradeBid>();

        public List<TradeOffer> Lots
        {
            get { return m_Offers; }
        }

        public List<TradeBid> Bids
        {
            get { return m_Bids; }
        }

    }
}
