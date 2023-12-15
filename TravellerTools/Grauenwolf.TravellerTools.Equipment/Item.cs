namespace Grauenwolf.TravellerTools.Equipment;

public class Item
{
    public decimal AmmoPrice { get; set; }

    public string AmmoPriceFormatted => Price switch
    {
        0 => "",
        < 1_000_000 => "Cr" + AmmoPrice.ToString(),
        _ => "MCr" + (AmmoPrice / 1_000_000.0M).ToString(),
    };

    public int Availability { get; set; }

    /// <summary>
    /// Base price without discounts.
    /// </summary>
    public decimal BasePrice { get; set; }

    public bool BlackMarket { get; set; }
    public string? Book { get; set; }
    public string? BookAndPage => string.IsNullOrEmpty(Page) ? Book : $"{Book} ({Page})";
    public int Category { get; set; }
    public string? Contraband { get; set; }
    public int Law { get; set; }
    public string? LegalStatus { get; set; }
    public string? Mass { get; set; }
    public string? Mod { get; set; }
    public string? Name { get; set; }
    public bool NotAvailable { get; set; }
    public string? Notes { get; set; }
    public string? Page { get; set; }
    public decimal Price { get; set; }

    public string PriceFormatted => Price switch
    {
        0 => "",
        < 1_000_000 => "Cr" + Price.ToString(),
        _ => "MCr" + (Price / 1_000_000.0M).ToString(),
    };

    public string? PriceModifier { get; set; }
    public int? SentencingDM { get; set; }
    public string? Skill { get; set; }
    public string? Species { get; set; }
    public int TechLevel { get; set; }

    public override string ToString()
    {
        return $"{Name}    TL {TechLevel} Price {Price} Law {Law}/{Category} Book {BookAndPage} Skill {Skill} Mass {Mass} Ammo Price {AmmoPrice}";
    }
}
