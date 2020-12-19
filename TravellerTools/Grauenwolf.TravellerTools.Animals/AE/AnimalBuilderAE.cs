using CSScriptLib;
using Grauenwolf.TravellerTools.Characters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Animals.AE
{
    public static class AnimalBuilderAE
    {
        static AnimalTemplates s_Templates;

        static public void SetDataPath(string dataPath)
        {
            var file = new FileInfo(Path.Combine(dataPath, "Animals-AE.xml"));

            var converter = new XmlSerializer(typeof(AnimalTemplates));
            using (var stream = file.OpenRead())
                s_Templates = ((AnimalTemplates)converter.Deserialize(stream));
        }

        static public ImmutableList<TerrainTemplate> TerrainTypeList
        {
            get { return ImmutableList.Create(s_Templates.Terrains); }
        }

        static public ImmutableList<AnimalClassTemplate> AnimalClassList
        {
            get { return ImmutableList.Create(s_Templates.AnimalClasses); }
        }

        static public ImmutableList<Behavior> BehaviorList
        {
            get { return ImmutableList.Create(s_Templates.Behaviors); }
        }

        static public Dictionary<string, List<Animal>> BuildPlanetSet(Dice dice)
        {
            var result = new Dictionary<string, List<Animal>>();
            foreach (var terrain in s_Templates.Terrains)
            {
                var terrainList = new List<Animal>();
                foreach (var option in s_Templates.EncounterTable)
                {
                retry:
                    var animal = BuildAnimal(dice, terrain.Name, null, option.Evolution);
                    //animal.Roll = option.Roll;
                    //terrainList.Add(animal);

                    if (animal.Diet == option.Diet)
                    {
                        animal.Roll = option.Roll;
                        terrainList.Add(animal);
                    }
                    else
                        goto retry; //we're looking for a specific diet for this slot
                }
                result.Add(terrain.Name, terrainList);
            }

            return result;
        }

        static public List<Animal> BuildTerrainSet(Dice dice, string terrainType)
        {
            var result = new List<Animal>();
            foreach (var option in s_Templates.EncounterTable)
            {
                //retry:
                var animal = BuildAnimal(dice, terrainType, null, option.Evolution);
                animal.Roll = option.Roll;
                result.Add(animal);

                //if (animal.Diet == option.Diet)
                //{
                //    animal.Roll = option.Roll;
                //    result.Add(animal);
                //}
                //else
                //    goto retry; //we're looking for a specific diet for this slot
            }

            return result;
        }

        static public Animal BuildAnimal(Dice dice, string terrainType, string animalClassName = null, int evolutionRolls = 0)
        {
            //Options
            var selectedTerrainType = TerrainTypeList.Single(x => x.Name == terrainType);
            var terrainPenalty = 0;

            if (animalClassName == null)
            {
                var animalClassNotes = dice.ChooseWithOdds(selectedTerrainType.AnimalClasses);
                animalClassName = animalClassNotes.AnimalClass;
                terrainPenalty = animalClassNotes.Penalty;
            }
            else
            {
                var animalClassNotes = selectedTerrainType.AnimalClasses.SingleOrDefault(ac => ac.AnimalClass == animalClassName);
                if (animalClassNotes == null)
                    terrainPenalty = -5; //What is it doing here?
                else
                    terrainPenalty = animalClassNotes.Penalty;
            }
            var animalClass = AnimalClassList.Single(x => x.Name == animalClassName);

            //Type
            var result = new Animal() { TerrainType = selectedTerrainType.Name, AnimalClass = animalClassName, EvolutionRolls = evolutionRolls };
            var movement = dice.ChooseByRoll(selectedTerrainType.MovementChart, "D6");
            result.Movement = !string.IsNullOrWhiteSpace(animalClass.Movement) ? animalClass.Movement : movement.Movement;
            result.InitiativeDM += animalClass.InitiativeDM;

            //Starting Skills
            foreach (var skill in animalClass.Skills)
                if (skill.ScoreSpecified)
                    result.Skills.Add(skill.Name, skill.Score);
                else
                    result.Skills.Increase(skill.Name, skill.Bonus);

            //Class features
            if (animalClass.Features != null)
                foreach (var feature in animalClass.Features)
                    result.Features.Add(feature.Text);

            //Diet
            var diet = dice.ChooseWithOdds(animalClass.Diets);
            result.Diet = diet.Diet;
            if (diet.Skills != null)
                foreach (var skill in diet.Skills)
                    if (skill.ScoreSpecified)
                        result.Skills.Add(skill.Name, skill.Score);
                    else
                        result.Skills.Increase(skill.Name, skill.Bonus);

            foreach (var att in diet.Attribute)
                result.Increase(att.Name, dice.D(att.Bonus));

            var weaponDM = 0;
            switch (result.Diet)
            {
                case "Carnivore":
                    result.InitiativeDM += 1;
                    weaponDM = 8;
                    break;

                case "Omnivore":
                    weaponDM = 4;
                    break;

                case "Herbivore":
                    result.InitiativeDM += -1;
                    weaponDM = -6;
                    break;
            }

            //Behavior
            var behaviorMeta = dice.ChooseByRoll(diet.Behaviors, "1D");
            var behavior = BehaviorList.Single(x => x.Name == behaviorMeta.Behavior);

            result.Behavior = behaviorMeta.Behavior;
            result.Attack = behavior.Attack.ToIntOrNull();
            result.Flee = behavior.Flee.ToIntOrNull();
            result.InitiativeDM += behavior.InitiativeDM;

            if (result.BehaviorCount == 1)
            {//subtract the reaction to make it act like a DM
                result.Attack -= behaviorMeta.Reaction;
                result.Flee -= behaviorMeta.Reaction;
            }
            else
            {
                var shortList = diet.Behaviors.Where(x => x.Behavior != behaviorMeta.Behavior).ToList();
                var behaviorMeta2 = dice.ChooseByRoll(shortList, "1D5");
                result.SecondaryBehavior = behaviorMeta2.Behavior;
            }

            AddBehavior(result, behavior, dice);

            result.QuirkRolls += dice.D("D6") == 6 ? 1 : 0;

            //We do this in a loop because each chart can add rolls to other charts
            while (result.QuirkRolls > 0 || result.EvolutionRolls > 0 || result.PhysicalSkills > 0 || result.SocialSkills > 0 || result.EvolutionSkills > 0)
            {
                //Evolution Rolls
                while (result.EvolutionRolls > 0)
                {
                    result.EvolutionRolls -= 1;
                    if (dice.D(2) == 0)
                        RollOnChart(result, animalClass.Chart.Single(x => x.Name == "AdditionalSkills"), dice, $"1D6+{result.EvolutionDM}");
                    else
                        RollOnChart(result, animalClass.Chart.Single(x => x.Name == "OtherBenefits"), dice, $"1D6+{result.EvolutionDM}");
                }

                //Skill Rolls

                while (result.PhysicalSkills > 0)
                {
                    result.PhysicalSkills -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "PhysicalSkills"), dice);
                }

                while (result.SocialSkills > 0)
                {
                    result.SocialSkills -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "SocialSkills"), dice);
                }

                while (result.EvolutionSkills > 0)
                {
                    result.EvolutionSkills -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "EvolutionSkills"), dice);
                }

                //Quirk Rolls
                while (result.QuirkRolls > 0)
                {
                    result.QuirkRolls -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "Quirks"), dice, "2D");
                }
            }

            //Size
            var sizeRoll = (dice.D(selectedTerrainType.SizeDM) + dice.D(movement.SizeDM) + dice.D(2, 6)).Limit(1, 13);
            var size = s_Templates.SizeTable.Single(sizeRoll);
            result.Size = sizeRoll;
            result.WeightKG = size.WeightKG;

            //Attributes
            result.Strength += dice.D(size.Strength) + terrainPenalty;
            result.Dexterity += dice.D(size.Dexterity) + terrainPenalty;
            result.Endurance += dice.D(size.Endurance) + terrainPenalty;
            result.Pack += dice.D(2, 6) + terrainPenalty;
            result.Instinct += dice.D(2, 6) + terrainPenalty;

            if (dice.D(6) + terrainPenalty >= 5)
                result.Intelligence += 1;

            //Weapons
            var baseDamage = s_Templates.DamageTable.Last(x => result.Strength >= x.MinValue).Damage;

            var weapon = s_Templates.WeaponTable.Single((dice.D("2D6") + weaponDM).Limit(1, 20));
            if (weapon.Weapon != "None")
                result.Weapons.Add(new Weapon() { Name = weapon.Weapon, Damage = (baseDamage + weapon.Bonus) + "d6" });

            if (result.Weapons.Count > 0)
                result.Skills.Add("Melee"); //always have melee if you have any weapons. Page 23

            //Armor
            result.Armor += s_Templates.ArmorTable.Single(dice.D(2, 6)).Armor;

            //Bonus skills
            if (result.Skills.Count > 0)
            {
                var bonusSkills = dice.D(1, 6);
                dice.Choose(result.Skills).Level += 1;
            }

            //Run post-scripts
            foreach (var script in result.PostScripts)
                RunScript(result, dice, script);
            result.PostScripts.Clear();

            //Finishing touches
            result.NumberEncountered = s_Templates.NumberTable.Last(x => result.Pack.Limit(0, 15) >= x.MinValue).Number;

            result.Strength = Math.Max(result.Strength, 0);
            result.Dexterity = Math.Max(result.Dexterity, 0);
            result.Endurance = Math.Max(result.Endurance, 0);
            result.Pack = Math.Max(result.Pack, 0);
            result.Instinct = Math.Max(result.Instinct, 0);
            result.Intelligence = Math.Max(result.Intelligence, 0);
            result.Armor = Math.Max(result.Armor, 0);

            return result;
        }

        private static void AddBehavior(Animal result, Behavior behavior, Dice dice)
        {
            if (behavior.Attributes != null)
                foreach (var att in behavior.Attributes)
                    result.Increase(att.Name, dice.D(att.Bonus));

            if (behavior.Features != null)
                foreach (var feature in behavior.Features)
                    result.Features.Add(feature.Text);

            if (behavior.Charts != null)
                foreach (var chart in behavior.Charts)
                    RollOnChart(result, chart, dice, chart.Roll);
        }

        static void RollOnChart(Animal result, Chart chart, Dice dice, string dieCode = "D6")
        {
            var option = dice.ChooseByRoll(chart.Option, dieCode);

            if (option.Attributes != null)
                foreach (var att in option.Attributes)
                    result.Increase(att.Name, dice.D(att.Bonus));

            if (option.Charts != null)
                foreach (var subChart in option.Charts)
                {
                    RollOnChart(result, subChart, dice, subChart.Roll);
                }

            if (option.Features != null)
                foreach (var feature in option.Features)
                    result.Features.Add(feature.Text);

            if (option.PostScripts != null)
                foreach (var script in option.PostScripts)
                    result.PostScripts.Add(script.Text);

            if (option.Skills != null)
                foreach (var skill in option.Skills)
                    if (skill.ScoreSpecified)
                        result.Skills.Add(skill.Name, skill.Score);
                    else
                        result.Skills.Increase(skill.Name, skill.Bonus);

            if (option.Behaviors != null)
                foreach (var behaviorMeta in option.Behaviors)
                {
                    var behavior = BehaviorList.SingleOrDefault(x => x.Name == behaviorMeta.Name);
                    if (behavior == null)
                        throw new BookException($"Chart {chart.Name} refers to unknown behavior named '{behaviorMeta.Name}'");
                    result.Behavior = result.Behavior + ", " + behavior.Name;
                    AddBehavior(result, behavior, dice);
                }
        }

        static RoslynEvaluator s_Engine = CSScript.RoslynEvaluator;
        static ConcurrentDictionary<string, MethodDelegate> s_CachedScripts = new ConcurrentDictionary<string, MethodDelegate>();

        public static void RunScript(Animal animal, Dice dice, string script)
        {
            try
            {
                var runScript = s_CachedScripts.GetOrAdd(script, s =>
                             s_Engine.CreateDelegate(
@"void SayHello(Grauenwolf.TravellerTools.Animals.AE.Animal animal, Grauenwolf.TravellerTools.Dice dice)
{
    " + s + @";
}"));
                runScript(animal, dice);
            }
            catch (Exception ex)
            {
                throw new BookException("Error running script: " + Environment.NewLine + script, ex);
            }
        }
    }
}
