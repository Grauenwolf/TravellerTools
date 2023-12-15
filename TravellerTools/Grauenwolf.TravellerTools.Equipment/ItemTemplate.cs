using System.Globalization;

namespace Grauenwolf.TravellerTools.Equipment;

public class ItemTemplate
{
    public string AmmoPrice { get; init; } = "";
    public string Book { get; init; } = "";
    public string Category { get; init; } = "";
    public string Contraband { get; init; } = "";
    public string Law { get; init; } = "";
    public string Mass { get; init; } = "";
    public string Name { get; init; } = "";
    public string Notes { get; init; } = "";
    public string Page { get; init; } = "";
    public string Price { get; init; } = "";
    public string Section { get; init; } = "";
    public string Skill { get; init; } = "";
    public string Species { get; init; } = "";
    public string Subsection { get; init; } = "";
    public string TL { get; init; } = "";

    public Item ToItem()
    {
        return new Item()
        {
            AmmoPrice = ParsePriceString(AmmoPrice),
            Price = ParsePriceString(Price),
            BasePrice = ParsePriceString(Price),
            Book = Book,
            Contraband = Contraband,
            Law = ParseInt(Law),
            Mass = Mass,
            TechLevel = ParseInt(TL),
            Skill = Skill,
            Name = Name,
            Notes = Notes,
            Page = Page,
            Species = Species,
            Category = ParseInt(Category),
        };
    }

    static int ParseInt(string? arg1)
    {
        if (!string.IsNullOrWhiteSpace(arg1))
            return int.Parse(arg1);
        return 0;
    }

    static decimal ParsePriceString(string? price)
    {
        if (string.IsNullOrWhiteSpace(price))
            return 0;
        if (price.StartsWith("Cr"))
            return decimal.Parse(price[2..], CultureInfo.InvariantCulture);
        if (price.StartsWith("KCr"))
            return decimal.Parse(price[3..], CultureInfo.InvariantCulture) * 1000M;
        if (price.StartsWith("MCr"))
            return decimal.Parse(price[3..], CultureInfo.InvariantCulture) * 1000M * 1000M;

        return decimal.Parse(price, CultureInfo.InvariantCulture);
    }
}
