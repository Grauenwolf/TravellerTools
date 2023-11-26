namespace Grauenwolf.TravellerTools.TradeCalculator;

/// <summary>
/// A bid is the desire to sell a trade good [to the players].
/// </summary>
public class TradeOffer
{
    public TradeOffer(string type, int purchaseDM, bool legal, TradeGood tradeGood, bool isCommonGood)
    {
        Type = type;
        PurchaseDM = purchaseDM;
        Legal = legal;
        TradeGood = tradeGood;
        IsCommonGood = isCommonGood;
    }

    public TradeOffer(string type, string? subtype, int tons, decimal basePrice, int purchaseDM, bool legal, TradeGood tradeGood, bool isCommonGood)
    {
        Type = type;
        Subtype = subtype;
        Tons = tons;
        BasePrice = basePrice;
        PurchaseDM = purchaseDM;
        Legal = legal;
        TradeGood = tradeGood;
        IsCommonGood = isCommonGood;
    }

    public decimal BasePrice { get; }
    public decimal CurrentPrice => BasePrice * PriceModifier;
    public int? DestinationDM { get; set; }

    /// <summary>
    /// If true, this good is commonly available on this planet
    /// </summary>
    public bool IsCommonGood { get; }

    public bool Legal { get; }
    public decimal PriceModifier { get; set; }
    public int PurchaseDM { get; }
    public int Roll { get; internal set; }
    public string? Subtype { get; set; }
    public int Tons { get; }
    public string? Type { get; set; }

    /// <summary>
    /// This is used for
    /// </summary>
    internal TradeGood TradeGood { get; }
}