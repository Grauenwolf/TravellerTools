using Grauenwolf.TravellerTools.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using static System.StringComparison;

namespace Grauenwolf.TravellerTools.Equipment
{
    public class EquipmentBuilder
    {
        readonly Catalog m_Book;

        public EquipmentBuilder(string dataPath)
        {
            var file = new FileInfo(Path.Combine(dataPath, "Equipment.xml"));
            var converter = new XmlSerializer(typeof(Catalog));

            using (var stream = file.OpenRead())
                m_Book = (Catalog)converter.Deserialize(stream);
        }

        public Store AvailabilityTable(World world)
        {
            StoreOptions options = new StoreOptions()
            {
                LawLevel = world.LawCode.Value,
                Population = world.PopulationCode.Value,
                Starport = world.StarportCode.ToString(),
                TechLevel = world.TechCode.Value,
            };

            options.TradeCodes.AddRange(world.RemarksList);
            return AvailabilityTable(options);
        }

        public Store AvailabilityTable(StoreOptions options)
        {
            var actualSeed = options.Seed ?? (new Random()).Next();
            var dice = new Dice(actualSeed);

            var result = new Store() { Options = options };

            foreach (var sectionXml in m_Book.Section)
            {
                var section = result.Sections.SingleOrDefault(s => s.Name == sectionXml.Name);
                if (section == null)
                {
                    section = new Section() { Name = sectionXml.Name };
                    result.Sections.Add(section);
                }

                foreach (var itemXml in sectionXml.Item)
                {
                    var item = new Item
                    {
                        Book = sectionXml.Book,
                        Category = itemXml.Category,
                        Law = itemXml.Law,
                        TechLevel = itemXml.TL,
                        Price = itemXml.Price,
                        Name = itemXml.Name,
                        Mod = itemXml.Mod
                    };


                    var availabilityDM = 0;
                    if (string.Equals(item.Mod, "Specialized", OrdinalIgnoreCase))
                        availabilityDM += -1;
                    if (string.Equals(item.Mod, "Military", OrdinalIgnoreCase))
                        availabilityDM += -2;


                    var techDiff = Math.Abs(options.TechLevel.Value - item.TechLevel);
                    if (3 <= techDiff && techDiff <= 4) availabilityDM += -1;
                    else if (5 <= techDiff && techDiff <= 9) availabilityDM += -2;
                    else if (10 <= techDiff) availabilityDM += -4;

                    if (options.Starport == "A" || options.Starport == "B") availabilityDM += 1;
                    else if (options.Starport == "X") availabilityDM += -4;

                    if (options.TradeCodes.Contains("Hi", OrdinalIgnoreCase) || options.TradeCodes.Contains("Ht", OrdinalIgnoreCase) || options.TradeCodes.Contains("In", OrdinalIgnoreCase) || options.TradeCodes.Contains("Ri", OrdinalIgnoreCase)) availabilityDM += 2;
                    if (options.TradeCodes.Contains("Lt", OrdinalIgnoreCase) || options.TradeCodes.Contains("Na", OrdinalIgnoreCase) || options.TradeCodes.Contains("NI", OrdinalIgnoreCase) || options.TradeCodes.Contains("Po", OrdinalIgnoreCase)) availabilityDM += -2;

                    if (options.Population == 0) availabilityDM += -4;
                    else if (1 <= options.Population && options.Population <= 2) availabilityDM += -2;
                    else if (3 <= options.Population && options.Population <= 5) availabilityDM += -1;
                    else if (9 <= options.Population && options.Population <= 11) availabilityDM += 1;
                    else if (12 <= options.Population) availabilityDM += 2;

                    if (options.LawLevel >= item.Law)
                    {
                        item.BlackMarket = true;

                        if (options.LawLevel == 0) availabilityDM += 2;
                        else if (1 <= options.LawLevel && options.LawLevel <= 3) availabilityDM += 1;
                        else if (4 <= options.LawLevel && options.LawLevel <= 6) availabilityDM += 0;
                        else if (7 <= options.LawLevel && options.LawLevel <= 9) availabilityDM += -1;
                        else if (10 <= options.LawLevel) availabilityDM += -2;

                        switch (item.Category)
                        {
                            case 1: availabilityDM += 4; item.Price *= 2; break;
                            case 2: availabilityDM += 2; item.Price *= 3; break;
                            case 3: availabilityDM += 0; item.Price *= 5; break;
                            case 4: availabilityDM += -2; item.Price *= 10; break;
                            case 5: availabilityDM += -4; item.Price *= 20; break;
                            case 6: availabilityDM += -6; item.Price *= 20; break;
                        }

                        item.SentencingDM = (options.LawLevel.Value - item.Law) + item.Category;
                    }

                    if (item.BlackMarket)
                        availabilityDM += options.StreetwiseScore;
                    else
                        availabilityDM += Math.Max(options.BrokerScore, options.StreetwiseScore);


                    item.Availability = 8 - availabilityDM;

                    if (!options.Roll)
                        section.Items.Add(item);
                    else
                    {
                        var roll = dice.D(2, 6) - item.Availability;
                        if (roll >= 0)
                        {
                            section.Items.Add(item);
                        }
                        else if (roll == -1)
                        {
                            item.Price *= 2;
                            section.Items.Add(item);
                        }
                        else if (roll == -2)
                        {
                            item.Price *= 3;
                            section.Items.Add(item);
                        }
                    }
                }
            }
            return result;
        }
    }

    static class Helpers
    {
        public static bool Contains(this IEnumerable<string> list, string value, StringComparison comparisonType)
        {
            foreach (var item in list)
                if (string.Equals(item, value, comparisonType))
                    return true;
            return false;
        }
    }
}
