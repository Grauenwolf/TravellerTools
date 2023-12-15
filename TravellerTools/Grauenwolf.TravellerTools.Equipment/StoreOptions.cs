namespace Grauenwolf.TravellerTools.Equipment;

public class StoreOptions
{
    public StoreOptions()
    {
    }

    public bool AutoRoll { get; set; }
    public int BrokerScore { get; set; }
    public bool DiscountPrices { get; set; }
    public bool DrugsRestricted { get; set; }
    public bool FullList { get; set; }
    public bool InformationRestricted { get; set; }
    public EHex LawLevel { get; set; }
    public EHex Population { get; set; }
    public bool PsionicsRestricted { get; set; }
    public int? Seed { get; set; }
    public EHex Starport { get; set; }
    public int StreetwiseScore { get; set; }
    public EHex TechLevel { get; set; }
    public bool TechnologyRestricted { get; set; }
    public List<string> TradeCodes { get; } = new();
    public bool WeaponsRestricted { get; set; }
}
