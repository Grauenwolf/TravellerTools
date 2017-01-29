using Grauenwolf.TravellerTools.Equipment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Grauenwolf.TravellerTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var eb = new EquipmentBuilder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var options = new StoreOptions() { LawLevel = 2, Population = 6, Starport = "B", TechLevel = 10, Roll = true };
            var store = eb.AvailabilityTable(options);


            foreach (var section in store.Sections)
                foreach (var item in section.Items)
                    Debug.WriteLine($"{item.Name} TL: {item.TechLevel} CR{item.Price.ToString("N0")} Roll {item.Availability} {(item.BlackMarket ? " Black Market" : "")}");
        }
    }
}
