using Microsoft.VisualBasic.FileIO;
using System.Xml.Serialization;
using Tortuga.Anchor;
using static System.StringComparison;

namespace Grauenwolf.TravellerTools.Equipment;

public class EquipmentBuilder
{
    //readonly List<ItemTemplate> m_AllItems;
    readonly List<string> m_AllSpecies;

    readonly Catalog m_Book;
    readonly List<SectionTemplate> m_ItemCatalog;
    readonly List<string> m_SectionNames;

    public EquipmentBuilder(string dataPath)
    {
        var file = new FileInfo(Path.Combine(dataPath, "Equipment.xml"));
        var converter = new XmlSerializer(typeof(Catalog));

        using (var stream = file.OpenRead())
            m_Book = (Catalog)converter.Deserialize(stream)!;

        var items = new List<ItemTemplate>();

        var equipmentFile = new FileInfo(Path.Combine(dataPath, "Equipment.csv"));
        using (var parser = new TextFieldParser(equipmentFile.FullName))
        {
            parser.SetDelimiters(",");
            var headers = parser.ReadFields()!.Select((x, i) => new { key = x, value = i }).ToDictionary(x => x.key, x => x.value);

            while (!parser.EndOfData)
            {
                var row = parser.ReadFields()!;
                items.Add(new()
                {
                    AmmoPrice = row[headers["AmmoPrice"]],
                    Book = row[headers["Book"]],
                    Category = row[headers["Category"]],
                    Contraband = row[headers["Contraband"]],
                    Law = row[headers["Law"]],
                    Mass = row[headers["Mass"]],
                    Name = row[headers["Name"]],
                    Notes = row[headers["Notes"]],
                    Page = row[headers["Page"]],
                    Price = row[headers["Price"]],
                    Section = row[headers["Section"]],
                    Skill = row[headers["Skill"]],
                    Species = row[headers["Species"]],
                    Subsection = row[headers["Subsection"]],
                    TL = row[headers["TL"]]
                });
            }

            var sections = new List<SectionTemplate>(items.Select(x => x.Section).Distinct().Select(x => new SectionTemplate(x)));
            foreach (var section in sections)
            {
                section.Items.AddRange(items.Where(x => x.Section == section.Name && x.Subsection.IsNullOrWhiteSpace()));

                section.Subsections.AddRange(
                    items.Where(x => x.Section == section.Name && !x.Subsection.IsNullOrWhiteSpace())
                    .Select(x => x.Subsection).Distinct().Select(x => new SubsectionTemplate(x)));

                foreach (var subSection in section.Subsections)
                    subSection.Items.AddRange(items.Where(x => x.Section == section.Name && x.Subsection == subSection.Name));
            }

            m_ItemCatalog = sections;
            //m_AllItems = items;
            m_AllSpecies = items.Where(x => !x.Species.IsNullOrWhiteSpace()).Select(x => x.Species).Distinct().OrderBy(s => s).ToList();
            m_SectionNames = sections.Select(x => x.Name).OrderBy(s => s).ToList();
        }
    }

    //public Store AvailabilityTable(World world)
    //{
    //    StoreOptions options = new StoreOptions()
    //    {
    //        LawLevel = world.LawCode,
    //        Population = world.PopulationCode,
    //        Starport = world.StarportCode,
    //        TechLevel = world.TechCode,
    //    };

    //    options.TradeCodes.AddRange(world.RemarksList.Keys);
    //    return AvailabilityTable(options);
    //}

    public Store AvailabilityTable(StoreOptions options)
    {
        var actualSeed = options.Seed ?? (new Random()).Next();
        var dice = new Dice(actualSeed);

        var result = new Store();

        result.Books.AddRange(m_Book.Books);
        foreach (var sectionTemplate in m_ItemCatalog)
        {
            var section = new Section() { Name = sectionTemplate.Name };
            section.Items.AddRange(
                sectionTemplate.Items.Select(x => ProcessItem(options, dice, x.ToItem())).Where(x => !x.NotAvailable));

            foreach (var subsectionTemplate in sectionTemplate.Subsections)
            {
                var subsection = new Subsection { Name = subsectionTemplate.Name };
                subsection.Items.AddRange(
                    subsectionTemplate.Items.Select(x => ProcessItem(options, dice, x.ToItem())).Where(x => !x.NotAvailable));

                if (subsection.Items.Any())
                    section.Subsections.Add(subsection);
            }

            if (section.Items.Any() || section.Subsections.Any())
                result.Sections.Add(section);
        }

        return result;
    }

    public List<string> GetSectionNames() => m_SectionNames;

    public List<string> GetSpeciesNames() => m_AllSpecies;

    static int ParseInt(string? arg1, string? arg2)
    {
        if (!string.IsNullOrWhiteSpace(arg1))
            return int.Parse(arg1);

        if (!string.IsNullOrWhiteSpace(arg2))
            return int.Parse(arg2);

        return 0;
    }

    static Item ProcessItem(StoreOptions options, Dice dice, Item item)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options), $"{nameof(options)} is null.");
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        if (item.Category == 0)
            item.Category = 1; //force a minimum category

        var availabilityDM = 0;
        if (string.Equals(item.Mod, "Specialized", OrdinalIgnoreCase))
            availabilityDM += -1;
        if (string.Equals(item.Mod, "Military", OrdinalIgnoreCase))
            availabilityDM += -2;

        var techDiff = options.TechLevel.Value - item.TechLevel;

        if (techDiff < 0 && !options.FullList) item.NotAvailable = true;  //item is not available.
        else if (3 <= techDiff && techDiff <= 4) availabilityDM += -1;
        else if (5 <= techDiff && techDiff <= 9) availabilityDM += -2;
        else if (10 <= techDiff) availabilityDM += -4;

        if (options.Starport == 'A' || options.Starport == 'B') availabilityDM += 1;
        else if (options.Starport == 'X') availabilityDM += -4;

        if (options.TradeCodes.Contains("Hi", OrdinalIgnoreCase) || options.TradeCodes.Contains("Ht", OrdinalIgnoreCase) || options.TradeCodes.Contains("In", OrdinalIgnoreCase) || options.TradeCodes.Contains("Ri", OrdinalIgnoreCase)) availabilityDM += 2;
        if (options.TradeCodes.Contains("Lt", OrdinalIgnoreCase) || options.TradeCodes.Contains("Na", OrdinalIgnoreCase) || options.TradeCodes.Contains("NI", OrdinalIgnoreCase) || options.TradeCodes.Contains("Po", OrdinalIgnoreCase)) availabilityDM += -2;

        if (options.Population == 0) availabilityDM += -4;
        else if (1 <= options.Population && options.Population <= 2) availabilityDM += -2;
        else if (3 <= options.Population && options.Population <= 5) availabilityDM += -1;
        else if (9 <= options.Population && options.Population <= 11) availabilityDM += 1;
        else if (12 <= options.Population) availabilityDM += 2;

        if (item.Law > 0 && options.LawLevel >= item.Law)
        {
            if (string.IsNullOrEmpty(item.Contraband))
            {
                item.BlackMarket = true;
                item.LegalStatus += "Unclassified. ";
            }
            else if (item.Contraband == "Weapons" && options.WeaponsRestricted)
            {
                item.BlackMarket = true;
                item.LegalStatus += "Weapons. ";
            }
            else if (item.Contraband == "Information" && options.InformationRestricted)
            {
                item.BlackMarket = true;
                item.LegalStatus += "Information. ";
            }
            else if (item.Contraband == "Drugs" && options.DrugsRestricted)
            {
                item.BlackMarket = true;
                item.LegalStatus += "Drugs. ";
            }
            else if (item.Contraband == "Psionics" && options.PsionicsRestricted)
            {
                item.BlackMarket = true;
                item.LegalStatus += "Psionics. ";
            }
        }
        if (options.TechnologyRestricted && item.TechLevel >= Tables.RestrictedTechLevel(options.LawLevel))
        {
            item.BlackMarket = true;
            item.LegalStatus += "Advanced Technology. ";
        }

        if (item.BlackMarket)
        {
            if (options.LawLevel == 0) availabilityDM += 2;
            else if (1 <= options.LawLevel && options.LawLevel <= 3) availabilityDM += 1;
            else if (4 <= options.LawLevel && options.LawLevel <= 6) availabilityDM += 0;
            else if (7 <= options.LawLevel && options.LawLevel <= 9) availabilityDM += -1;
            else if (10 <= options.LawLevel) availabilityDM += -2;

            switch (item.Category)
            {
                case 1:
                    availabilityDM += 4;
                    item.Price *= 2;
                    item.PriceModifier = "Illegal item, 2x black market price in effect. ";
                    break;

                case 2:
                    availabilityDM += 2;
                    item.Price *= 3;
                    item.PriceModifier = "Illegal item, 3x black market price in effect. ";
                    break;

                case 3:
                    availabilityDM += 0;
                    item.Price *= 5;
                    item.PriceModifier = "Illegal item, 5x black market price in effect. ";
                    break;

                case 4:
                    availabilityDM += -2;
                    item.Price *= 10;
                    item.PriceModifier = "Illegal item, 10x black market price in effect. ";
                    break;

                case 5:
                    availabilityDM += -4;
                    item.Price *= 20;
                    item.PriceModifier = "Illegal item, 20x black market price in effect. ";
                    break;

                case 6:
                    availabilityDM += -6;
                    var penality = 10 * dice.D(2, 6);
                    item.Price *= penality;
                    item.PriceModifier = $"Illegal item, {penality}x black market price in effect. ";
                    break;
            }
            if (item.Category >= 1 && item.Category <= 6)
                item.SentencingDM = (options.LawLevel.Value - item.Law) + item.Category;
        }

        if (item.BlackMarket)
            availabilityDM += options.StreetwiseScore;
        else
            availabilityDM += Math.Max(options.BrokerScore, options.StreetwiseScore);
        item.Availability = 8 - availabilityDM;

        if (item.NotAvailable)
        {
            //skip item
        }
        else if (!options.AutoRoll)
        {
            //nothing to do
        }
        else
        {
            var roll = dice.D(2, 6) - item.Availability;
            if (roll >= 0)
            {
                if (options.DiscountPrices && roll > 0)
                {
                    item.Price *= (1 - (roll * .05M));
                    item.PriceModifier += $"Common item, price reduced by {roll * 5}%";
                }
            }
            else if (roll == -1)
            {
                item.Price *= 2;
                item.PriceModifier += "Rare item, price was doubled.";
            }
            else if (roll == -2)
            {
                item.Price *= 3;
                item.PriceModifier += "Very rare item, price was tripled.";
            }
            else
                item.NotAvailable = true;
        }
        //Cleanup
        item.PriceModifier = item.PriceModifier?.Trim();
        item.LegalStatus = item.LegalStatus?.Trim();

        return item;
    }
}
