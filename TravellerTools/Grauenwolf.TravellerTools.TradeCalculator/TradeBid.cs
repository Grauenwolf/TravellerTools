namespace Grauenwolf.TravellerTools.TradeCalculator
{
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
