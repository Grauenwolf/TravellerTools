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

        //static public ImmutableList<AnimalTemplatesAnimalType> AnimalTypeList
        //{
        //    get { return ImmutableList.Create(s_Templates.AnimalTypeList); }
        //}

        static public Dictionary<string, List<Animal>> BuildPlanetSet()
        {
            var result = new Dictionary<string, List<Animal>>();
            foreach (var terrain in s_Templates.Terrains)
            {
                var terrainList = new List<Animal>();
                foreach (var option in s_Templates.EncounterTable)
                {
                    retry:
                    var animal = BuildAnimal(terrain.Name, option.Evolution);
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
                retry:
                var animal = BuildAnimal(terrainType, option.Evolution);
                if (animal.Diet == option.Diet)
                {
                    animal.Roll = option.Roll;
                    result.Add(animal);
                }
                else
                    goto retry; //we're looking for a specific diet for this slot
            }

            return result;
        }

        static public Animal BuildAnimal(string terrainType, int evolution = 0)
        {
            var dice = new Dice();

            var selectedTerrainType = TerrainTypeList.Single(x => x.Name == terrainType);


            //Type
            var result = new Animal() { TerrainType = selectedTerrainType.Name };


            return result;
        }
    }
}
