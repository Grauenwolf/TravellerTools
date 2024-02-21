namespace Grauenwolf.TravellerTools.Characters;

public static class CareerTypesExtension
{
    public static string ToDescription(this CareerTypes type)
    {
        return type switch
        {
            CareerTypes.FieldScience => "Field Science",
            CareerTypes.LegalGoodsTrader => "Legal Goods Trader",
            CareerTypes.ShadyGoodsTrader => "Shady Goods Trader",
            CareerTypes.StarportEmployee => "Starport Employee",
            CareerTypes.StarportOfficer => "Starport Officer",
            CareerTypes.ArtistOrPerformer => "Artist/Performer ",
            CareerTypes.CorporateMerchant => "Corporate Merchant",
            CareerTypes.MilitaryNavy => "Navy",
            CareerTypes.ViolentNonMilitary => "Violent Non-military",
            _ => type.ToString(),
        };
    }
}

/// <summary>
/// Career types are used for the encounter generator.
/// </summary>
[Flags]
public enum CareerTypes
{
    None = 0,
    Science = 0x1,
    FieldScience = 0x2,
    Illegal = 0x4,
    LegalGoodsTrader = 0x8,
    ShadyGoodsTrader = 0x10,
    StarportEmployee = 0x20,
    StarportOfficer = 0x40,
    Religious = 0x80,
    Military = 0x100,
    Spy = 0x200,
    Precareer = 0x400,
    Pisonic = 0x800,
    ArtistOrPerformer = 0x1000,
    Outsider = 0x2000,
    Diplomat = 0x4000,
    Healer = 0x8000,
    Corporate = 0x10000,

    /// <summary>
    /// Non-military government positions
    /// </summary>
    Government = 0x20000,

    /// <summary>
    /// Nobility and noble-equivalents (e.g. Solomoni party leaders)
    /// </summary>
    Noble = 0x40000,

    CorporateMerchant = 0x80000,
    FreeTrader = 0x100000,
    Police = 0x200000,
    Belter = 0x400000,
    MilitaryNavy = 0x800000,
    Scout = 0x1000000,
    Civilian = 0x2000000,
    ViolentNonMilitary = 0x4000000,
    Violent = 0x8000000,
}
