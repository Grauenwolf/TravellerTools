using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Animals.AE
{
    public class AnimalBuilderAE
    {
        static AnimalTemplates s_Templates;

        static public void SetDataPath(string dataPath)
        {
            var file = new FileInfo(Path.Combine(dataPath, "Animals-AE.xml"));
            var converter = new XmlSerializer(typeof(AnimalTemplates));
            using (var stream = file.OpenRead())
                s_Templates = ((AnimalTemplates)converter.Deserialize(stream));

        }

        static public ImmutableList<AnimalTemplatesTerrain> TerrainTypeList
        {
            get { return ImmutableList.Create(s_Templates.Terrains); }
        }

        static public ImmutableList<AnimalTemplatesAnimalClass> AnimalClassList
        {
            get { return ImmutableList.Create(s_Templates.AnimalClasses); }
        }

        static public ImmutableList<AnimalTemplatesBehavior> BehaviorList
        {
            get { return ImmutableList.Create(s_Templates.Behaviors); }
        }

        static public Dictionary<string, List<Animal>> BuildPlanetSet()
        {
            var result = new Dictionary<string, List<Animal>>();
            foreach (var terrain in s_Templates.Terrains)
            {
                var terrainList = new List<Animal>();
                foreach (var option in s_Templates.EncounterTable)
                {
                    retry:
                    var animal = BuildAnimal(terrain.Name, null, option.Evolution);
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

        static public List<Animal> BuildTerrainSet(string terrainType)
        {
            var result = new List<Animal>();
            foreach (var option in s_Templates.EncounterTable)
            {
                //retry:
                var animal = BuildAnimal(terrainType, null, option.Evolution);
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

        static public Animal BuildAnimal(string terrainType, string animalClassName = null, int evolutionRolls = 0)
        {
            var dice = new Dice();

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

            //Starting Skills
            foreach (var skill in animalClass.Skills)
                result.Skills.Add(skill.Name, skill.Score);

            //Diet
            var diet = dice.ChooseWithOdds(animalClass.Diets);
            result.Diet = diet.Diet;
            if (diet.Skill != null)
                result.Skills.Add(diet.Skill.Name, diet.Skill.Score);
            foreach (var att in diet.Attribute)
            {
                result.Increase(att.Name, dice.D(att.Bonus));
            }

            //Behavior
            var behaviorMeta = dice.ChooseByRoll(diet.Behaviors, "1D");
            var behavior = BehaviorList.Single(x => x.Name == behaviorMeta.Behavior);

            result.Behavior = behaviorMeta.Behavior;
            result.Attack = behavior.Attack.ToIntOrNull();
            result.Flee = behavior.Flee.ToIntOrNull();

            //subtract the reaction to make it act like a DM
            result.Attack -= behaviorMeta.Reaction;
            result.Flee -= behaviorMeta.Reaction;

            if (behavior.Feature != null)
                result.Features.Add(behavior.Feature.Text);

            if (behavior.Chart != null)
            {
                var option = dice.ChooseByRoll(behavior.Chart.Option, behavior.Chart.Roll);
                if (option.Feature != null)
                    result.Features.Add(behavior.Feature.Text);
                if (option.Attribute != null)
                    result.Increase(option.Attribute.Name, dice.D(option.Attribute.Bonus));
                if (option.Skill != null)
                    result.Skills.Increase(option.Skill.Name, option.Skill.Bonus);
            }

            result.QuirkRolls += dice.Next(10) == 9 ? 1 : 0;

            //Evolution Rolls


            //Skill Rolls
            while (result.PhysicalSkills > 0 || result.SocialSkills > 0 || result.EvolutionSkills > 0)
            {
                if (result.PhysicalSkills > 0)
                {
                    result.PhysicalSkills -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "PhysicalSkills"), dice);
                }

                if (result.SocialSkills > 0)
                {
                    result.SocialSkills -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "SocialSkills"), dice);
                }

                if (result.EvolutionSkills > 0)
                {
                    result.EvolutionSkills -= 1;
                    RollOnChart(result, animalClass.Chart.Single(x => x.Name == "EvolutionSkills"), dice);
                }
            }

            return result;
        }

        static void RollOnChart(Animal result, AnimalTemplatesAnimalClassChart chart, Dice dice)
        {
            var option = dice.ChooseByRoll(chart.Option, "D6");

            if (option.Attribute != null)
                foreach (var att in option.Attribute)
                    result.Increase(att.Name, dice.D(att.Bonus));

            if (option.Skill != null)
                result.Skills.Increase(option.Skill.Name, option.Skill.Bonus);


        }
    }
}
