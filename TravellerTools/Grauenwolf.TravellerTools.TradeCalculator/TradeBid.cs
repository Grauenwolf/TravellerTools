namespace Grauenwolf.TravellerTools.TradeCalculator
{
    /// <summary>
    /// A bid is the desire to buy a trade good [from the players].
    /// </summary>
    public class TradeBid
    {
        public string Type { get; set; }
        public string Subtype { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CurrentPrice { get { return BasePrice * PriceModifier; } }
        public int SaleDM { get; set; }
        public decimal PriceModifier { get; set; }
        public int Roll { get; internal set; }
    }
}
