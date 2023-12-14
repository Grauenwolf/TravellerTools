namespace Grauenwolf.TravellerTools.TradeCalculator;

/// <summary>
/// A bid is the desire to buy a trade good [from the players].
/// </summary>
public class TradeBid
{
    public decimal AgedPrice => BasePrice * AgedPriceModifier;
    public decimal AgedPriceModifier { get; set; }
    public int AgedRoll { get; internal set; }
    public decimal BasePrice { get; set; }
    public decimal CurrentPrice => BasePrice * PriceModifier;
    public bool Legal { get; set; }
    public decimal PriceModifier { get; set; }
    public int Roll { get; internal set; }
    public int SaleDM { get; set; }
    public string? Subtype { get; set; }
    public string? Type { get; set; }
}
