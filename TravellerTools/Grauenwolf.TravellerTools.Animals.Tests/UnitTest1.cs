using Grauenwolf.TravellerTools.Animals.AE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Animals.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dice dice = new Dice();
            var a = new Animal();
            a.Armor = 5;
            AnimalBuilderAE.RunScript(a, dice, "animal.Armor = 0;");
            Assert.AreEqual(0, a.Armor);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Dice dice = new Dice();
            var animal = new Animal();
            animal.Skills.Add("Stealth", 2);
            animal.Skills.Remove("Stealth");
            AnimalBuilderAE.RunScript(animal, dice, "animal.Skills.Remove(\"Stealth\");");
            Assert.IsNull(animal.Skills["Stealth"]);

            if (animal.Movement == "Flight")
            {
                animal.Movement = "Walk";
                animal.Strength += dice.D(1, 6);
            }
            else
            {
                animal.Movement += " Flight";
                animal.Strength -= 1;
                animal.Armor -= 1;
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            AnimalBuilderAE.SetDataPath(".");
            Parallel.For(0, 100, _ =>
            {
                var result = AnimalBuilderAE.BuildPlanetSet();
            });
        }
    }
}
