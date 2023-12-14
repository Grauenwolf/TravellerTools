using Grauenwolf.TravellerTools.Maps;
using System.Xml.Serialization;
using Tortuga.Anchor;
using static System.StringComparison;

namespace Grauenwolf.TravellerTools.Equipment;

public class EquipmentBuilder
{
    readonly Catalog m_Book;

    public EquipmentBuilder(string dataPath)
    {
        var file = new FileInfo(Path.Combine(dataPath, "Equipment.xml"));
        var converter = new XmlSerializer(typeof(Catalog));

        using (var stream = file.OpenRead())
            m_Book = (Catalog)converter.Deserialize(stream)!;
    }

    public Store AvailabilityTable(World world)
    {
        StoreOptions options = new StoreOptions()
        {
            LawLevel = world.LawCode,
            Population = world.PopulationCode,
            Starport = world.StarportCode,
            TechLevel = world.TechCode,
        };

        options.TradeCodes.AddRange(world.RemarksList.Keys);
        return AvailabilityTable(options);
    }

    public Store AvailabilityTable(StoreOptions options)
    {
        var actualSeed = options.Seed ?? (new Random()).Next();
        var dice = new Dice(actualSeed);

        var result = new Store();

        result.Books.AddRange(m_Book.Books);

        foreach (var sectionXml in m_Book.Section)
        {
            var section = result.Sections.SingleOrDefault(s => s.Name == sectionXml.Name);
            if (section == null)
            {
                section = new Section() { Name = sectionXml.Name };
                result.Sections.Add(section);
            }

            if (sectionXml.Item != null)
                foreach (var itemXml in sectionXml.Item)
                {
                    ProcessItem(options, dice, sectionXml, section, itemXml);
                }

            if (sectionXml.Subsection != null)
                foreach (var subsectionXml in sectionXml.Subsection)
                {
                    subsectionXml.Book = subsectionXml.Book ?? sectionXml.Book;
                    subsectionXml.Category = ReadString(subsectionXml.Category, sectionXml.Category);
                    subsectionXml.Contraband = ReadString(subsectionXml.Contraband, sectionXml.Contraband);
                    subsectionXml.Law = ReadString(subsectionXml.Law, sectionXml.Law);
                    subsectionXml.Mod = subsectionXml.Mod ?? sectionXml.Mod;
                    subsectionXml.Page = subsectionXml.Page ?? sectionXml.Page;
                    subsectionXml.Skill = subsectionXml.Skill ?? sectionXml.Skill;
                    subsectionXml.TL = ReadString(subsectionXml.TL, sectionXml.TL);
                    subsectionXml.Species = ReadString(subsectionXml.Species, sectionXml.Species);

                    var subsection = section.Subsections.SingleOrDefault(s => s.Name == subsectionXml.Name);
                    if (subsection == null)
                    {
                        subsection = new Subsection() { Name = subsectionXml.Name /*, SectionKey = section.Key*/ };
                        section.Subsections.Add(subsection);
                    }

                    if (subsectionXml.Item != null)
                        foreach (var itemXml in subsectionXml.Item)
                        {
                            ProcessItem(options, dice, subsectionXml, subsection, itemXml);
                        }
                }
        }

        //cleanup

        for (int i = result.Sections.Count - 1; i >= 0; i--)
        {
            var section = result.Sections[i];
            for (int j = section.Subsections.Count - 1; j >= 0; j--)
            {
                if (section.Subsections[j].Items.Count == 0)
                    section.Subsections.RemoveAt(j);
            }

            if (section.Items.Count == 0 && section.Subsections.Count == 0)
                result.Sections.RemoveAt(i);
        }

        return result;
    }

    public List<string> GetSectionNames()
    {
        var result = new HashSet<string>();

        foreach (var sectionXml in m_Book.Section)
            result.Add(sectionXml.Name);

        return result.OrderBy(s => s).ToList();
    }

    public List<string> GetSpeciesNames()
    {
        var result = new HashSet<string>();

        foreach (var sectionXml in m_Book.Section)
        {
            if (sectionXml.Item != null)
                foreach (var itemXml in sectionXml.Item.Where(x => !x.Species.IsNullOrEmpty()))
                    result.Add(itemXml.Species);

            if (sectionXml.Subsection != null)
                foreach (var subsectionXml in sectionXml.Subsection)
                    if (subsectionXml.Item != null)
                        foreach (var itemXml in subsectionXml.Item.Where(x => !x.Species.IsNullOrEmpty()))
                            result.Add(itemXml.Species);
        }

        return result.OrderBy(s => s).ToList();
    }

    private static int ParseInt(string? arg1, string? arg2)
    {
        if (!string.IsNullOrWhiteSpace(arg1))
            return int.Parse(arg1);

        if (!string.IsNullOrWhiteSpace(arg2))
            return int.Parse(arg2);

        return 0;
    }

    private static void ProcessItem(StoreOptions options, Dice dice, CatalogSection sectionXml, IHasItems section, CatalogSectionItem itemXml)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options), $"{nameof(options)} is null.");
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");
        if (sectionXml == null)
            throw new ArgumentNullException(nameof(sectionXml), $"{nameof(sectionXml)} is null.");
        if (section == null)
            throw new ArgumentNullException(nameof(section), $"{nameof(section)} is null.");
        if (itemXml == null)
            throw new ArgumentNullException(nameof(itemXml), $"{nameof(itemXml)} is null.");

        var item = new Item
        {
            Book = itemXml.Book ?? sectionXml.Book,
            Category = ParseInt(itemXml.Category, sectionXml.Category),
            Law = ParseInt(itemXml.Law, sectionXml.Law),
            TechLevel = ParseInt(itemXml.TL, sectionXml.TL),
            Price = itemXml.PriceCredits,
            BasePrice = itemXml.PriceCredits,
            Name = itemXml.Name,
            Mod = itemXml.Mod ?? sectionXml.Mod,
            Contraband = itemXml.Contraband ?? sectionXml.Contraband,
            Mass = itemXml.Mass,
            Notes = itemXml.Notes,
            Species = itemXml.Species ?? sectionXml.Species,
            AmmoPrice = itemXml.AmmoPriceCredits,
            Skill = itemXml.Skill ?? sectionXml.Skill,
            Page = itemXml.Page ?? sectionXml.Page,
        };
        if (item.Category == 0)
            item.Category = 1; //force a minimum category

        var availabilityDM = 0;
        if (string.Equals(item.Mod, "Specialized", OrdinalIgnoreCase))
            availabilityDM += -1;
        if (string.Equals(item.Mod, "Military", OrdinalIgnoreCase))
            availabilityDM += -2;

        var techDiff = options.TechLevel.Value - item.TechLevel;

        if (techDiff < 0) item.NotAvailable = true;  //item is not available.
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
            section.Items.Add(item);
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
                section.Items.Add(item);
            }
            else if (roll == -1)
            {
                item.Price *= 2;
                item.PriceModifier += "Rare item, price was doubled.";
                section.Items.Add(item);
            }
            else if (roll == -2)
            {
                item.Price *= 3;
                item.PriceModifier += "Very rare item, price was tripled.";
                section.Items.Add(item);
            }
        }
        //Cleanup
        item.PriceModifier = item.PriceModifier?.Trim();
        item.LegalStatus = item.LegalStatus?.Trim();
    }

    private static string? ReadString(string? arg1, string? arg2)
    {
        if (!string.IsNullOrWhiteSpace(arg1))
            return arg1;

        if (!string.IsNullOrWhiteSpace(arg2))
            return arg2;

        return null;
    }
}
