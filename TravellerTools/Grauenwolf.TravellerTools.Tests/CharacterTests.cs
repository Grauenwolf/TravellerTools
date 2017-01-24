//using Grauenwolf.TravellerTools.Characters;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.IO;
//using static System.Diagnostics.Debug;

//namespace Grauenwolf.TravellerTools.Tests
//{
//    [TestClass]
//    public class CharacterTests

//    {
//        static CharacterTests()
//        {
//            CharacterBuilder.SetDataPath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
//        }

//        [TestMethod]
//        public void CreateCharacter()
//        {
//            var result = CharacterBuilder.Build(new CharacterBuilderOptions() { });
//            PrintCharacter(result);
//        }

//        [TestMethod]
//        public void CreateTeenager()
//        {
//            var result = CharacterBuilder.Build(new CharacterBuilderOptions() { MaxAge = 18 });
//            PrintCharacter(result);
//        }

//        private void PrintCharacter(Character result)
//        {
//            WriteLine($"Strength: {result.Strength} ({result.StrengthDM})");
//            WriteLine($"Dexterity: {result.Dexterity} ({result.DexterityDM})");
//            WriteLine($"Endurance: {result.Endurance} ({result.EnduranceDM})");
//            WriteLine($"Intellect: {result.Intellect} ({result.IntellectDM})");
//            WriteLine($"Education: {result.Education} ({result.EducationDM})");
//            WriteLine($"SocialStanding: {result.SocialStanding} ({result.SocialStandingDM})");
//            WriteLine("");
//            WriteLine("Skills");
//            Indent();
//            foreach (var skill in result.Skills)
//                WriteLine(skill);
//            Unindent();
//            WriteLine("History");
//            Indent();
//            foreach (var line in result.History)
//                WriteLine(line);
//            Unindent();
//            WriteLine("Trace");
//            Indent();
//            foreach (var line in result.Trace)
//                WriteLine(line);
//            Unindent();

//        }
//    }
//}
