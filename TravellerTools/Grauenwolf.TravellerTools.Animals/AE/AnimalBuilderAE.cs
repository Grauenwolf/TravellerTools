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

        static public Animal BuildAnimal(string terrainType, string animalClassName = null, int evolution = 0)
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
            var result = new Animal() { TerrainType = selectedTerrainType.Name, AnimalClass = animalClassName };


            var diet = dice.ChooseWithOdds(animalClass.Diets);
            result.Diet = diet.Diet;

            var behaviorMeta = dice.ChooseByRoll(diet.Behaviors, "1D");
            var behavior = BehaviorList.Single(x => x.Name == behaviorMeta.Behavior);

            result.Behavior = behaviorMeta.Behavior;
            result.Attack = behavior.Attack.ToIntOrNull();
            result.Flee = behavior.Flee.ToIntOrNull();

            //subtract the reaction to make it act like a DM
            result.Attack -= behaviorMeta.Reaction;
            result.Flee -= behaviorMeta.Reaction;



            return result;
        }
    }
}
