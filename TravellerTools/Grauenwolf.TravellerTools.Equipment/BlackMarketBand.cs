namespace Grauenwolf.TravellerTools.Equipment;

public class BlackMarketBand(int categoryCode, string categoryName, int dM, int priceModifier, string titleText)
{
    public int CategoryCode { get; } = categoryCode;
    public string CategoryName { get; } = categoryName;
    public int DM { get; } = dM;
    public int PriceModifier { get; } = priceModifier;
    public string TitleText { get; } = titleText;
}
