namespace Grauenwolf.TravellerTools.Equipment;

public class Store
{
    internal Store()
    {
    }

    public List<BlackMarketBand> BlackMarketBands { get; } = new();
    public List<CatalogBook> Books { get; } = new();
    public List<Section> Sections { get; } = new();

    public List<TLBand> TLBands { get; } = new();
}
