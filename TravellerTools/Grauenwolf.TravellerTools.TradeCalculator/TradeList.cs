using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class TradeGoodsList
    {
        public List<TradeBid> Bids { get; } = new List<TradeBid>();
        public List<TradeOffer> Lots { get; } = new List<TradeOffer>();
    }
}
