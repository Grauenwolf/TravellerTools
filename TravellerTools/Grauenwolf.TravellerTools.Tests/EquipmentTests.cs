using Grauenwolf.TravellerTools.Equipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Grauenwolf.TravellerTools.Tests
{
    [TestClass]
    public class EquipmentTests
    {
        [TestMethod]
        public void EquipmentExporter()
        {
            var eb = new EquipmentBuilder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var options = new StoreOptions() { TechLevel = 999 };
            var store = eb.AvailabilityTable(options);

            var output = new StringBuilder();
            output.AppendLine("Section\tSubsection\tName\tTL\tMass\tPrice\tAmmoPrice\tSpecies\tSkill\tBook\tPage\tContraband\tCategory\tLaw\tNotes\tMod");

            foreach (var section in store.Sections.OrderBy(s => s.Name))
            {
                foreach (var item in section.Items)
                    output.AppendLine($"{section.Name}\t{""}\t{item.Name}\t{item.TechLevel}\t{item.Mass}\t{item.PriceFormatted}\t{item.AmmoPrice}\t{item.Species}\t{item.Skill}\t{item.Book}\t{item.Page}\t{item.Contraband}\t{item.Category}\t{item.Law}\t{item.Notes}\t{item.Mod}");

                foreach (var subsection in section.Subsections.OrderBy(s => s.Name))
                    foreach (var item in subsection.Items.OrderBy(s => s.Name).ThenBy(s => s.TechLevel))
                        output.AppendLine($"{section.Name}\t{subsection.Name}\t{item.Name}\t{item.TechLevel}\t{item.Mass}\t{item.PriceFormatted}\t{item.AmmoPrice}\t{item.Species}\t{item.Skill}\t{item.Book}\t{item.Page}\t{item.Contraband}\t{item.Category}\t{item.Law}\t{item.Notes}\t{item.Mod}");
            }

            File.WriteAllText(@"C:\temp\test.txt", output.ToString());
        }

        [TestMethod]
        public void EquipmentTest1()
        {
            var eb = new EquipmentBuilder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var options = new StoreOptions() { LawLevel = 2, Population = 6, Starport = 'B', TechLevel = 10, AutoRoll = true };
            var store = eb.AvailabilityTable(options);

            foreach (var section in store.Sections)
                foreach (var item in section.Items)
                    Debug.WriteLine($"{item.Name} TL: {item.TechLevel} CR{item.Price.ToString("N0")} Roll {item.Availability} {(item.BlackMarket ? " Black Market" : "")}");
        }

        [TestMethod]
        public void SectionExporter()
        {
            var eb = new EquipmentBuilder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var options = new StoreOptions() { TechLevel = 999 };
            var store = eb.AvailabilityTable(options);

            var output = new StringBuilder();
            output.AppendLine("Section,Subsection,Item Count");

            foreach (var section in store.Sections.OrderBy(s => s.Name))
            {
                if (section.Items.Count > 0)
                    output.AppendLine($"{section.Name},,{section.Items.Count}");

                foreach (var subsection in section.Subsections.OrderBy(s => s.Name))
                    output.AppendLine($"{section.Name},{subsection.Name},{subsection.Items.Count}");
            }

            File.WriteAllText(@"C:\temp\sections.csv", output.ToString());
        }
    }
}

// Section	Subsection	Name	TL	Mass	Price	AmmoPrice	Species	Skill	Book	Page 	Contraband	Category	Law
