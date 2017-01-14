using Grauenwolf.TravellerTools.Animals.AE;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grauenwolf.TravellerTools.Animals.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = new Animal();
            a.Armor = 5;
            AnimalBuilderAE.RunScript(a, "animal.Armor = 0;");
            Assert.AreEqual(0, a.Armor);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var animal = new Animal();
            animal.Skills.Add("Stealth", 2);
            animal.Skills.Remove("Stealth");
            AnimalBuilderAE.RunScript(animal, "animal.Skills.Remove(\"Stealth\");");
            Assert.IsNull(animal.Skills["Stealth"]);


        }
    }
}
