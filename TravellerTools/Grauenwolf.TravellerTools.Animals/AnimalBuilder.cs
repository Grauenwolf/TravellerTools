using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Animals
{


    public static class AnimalBuilder
    {
        static public void SetDataPath(string dataPath)
        {
            var file = new FileInfo(Path.Combine(dataPath, "Animals.xml"));
            var converter = new XmlSerializer(typeof(AnimalTemplates));
            using (var stream = file.OpenRead())
                m_Templates = ((AnimalTemplates)converter.Deserialize(stream));

        }

        static AnimalTemplates m_Templates;

        static public ImmutableList<AnimalTemplatesTerrain> TerrainTypeList
        {
            get { return ImmutableList.Create(m_Templates.TerrainList); }
        }

        static public ImmutableList<AnimalTemplatesAnimalType> AnimalTypeList
        {
            get { return ImmutableList.Create(m_Templates.AnimalTypeList); }
        }


        static public Dictionary<string, List<Animal>> BuildPlanetSet()
        {
            var result = new Dictionary<string, List<Animal>>();
            foreach (var terrain in m_Templates.TerrainList)
            {
                var terrainList = new List<Animal>();
                foreach (var option in m_Templates.EncounterTable)
                {
                    var animal = BuildAnimal(terrain.Name, option.AnimalType);
                    animal.Roll = (int)option.Roll;
                    terrainList.Add(animal);
                }
                result.Add(terrain.Name, terrainList);
            }

            return result;
        }

        static public List<Animal> BuildTerrainSet(string terrainType)
        {
            var result = new List<Animal>();
            foreach (var option in m_Templates.EncounterTable)
            {
                var animal = BuildAnimal(terrainType, option.AnimalType);
                animal.Roll = (int)option.Roll;
                result.Add(animal);
            }

            return result;
        }

        static public Animal BuildAnimal(string terrainType, string animalType)
        {
            var dice = new Dice();

            var selectedTerrainType = TerrainTypeList.Single(x => x.Name == terrainType);
            var selectedAnimalType = AnimalTypeList.Single(x => x.Name == animalType);


            //Type
            var result = new Animal() { TerrainType = selectedTerrainType.Name, AnimalType = selectedAnimalType.Name };
            var behaviorName = (selectedAnimalType.Option.Single((dice.D(selectedTerrainType.TypeDM) + dice.D(2, 6)).Limit(1, 13))).Behavior;
            var behavior = selectedAnimalType.Behavior.Single(x => x.Name == behaviorName);
            result.Behavior = behaviorName;
            var movement = selectedTerrainType.Option.Single(dice.D(6));

            //Size
            var sizeRoll = (dice.D(selectedTerrainType.SizeDM) + dice.D(movement.SizeDM) + dice.D(2, 6)).Limit(1, 13);
            var size = m_Templates.SizeTable.Single(sizeRoll);
            result.Size = sizeRoll;
            result.Movement = movement.Movement;
            result.WeightKG = size.WeightKG;

            //Attributes
            result.Strength = dice.D(size.Strength);
            result.Dexterity = dice.D(size.Dexterity);
            result.Endurance = dice.D(size.Endurance);
            result.Pack = dice.D(2, 6);
            result.Instinct = dice.D(2, 6);

            if (behaviorName == "Killer")
            {
                if (dice.D(2) == 1)
                    result.Strength += 4;
                else
                    result.Dexterity += 4;
            }


            var intOdds = dice.D(100); //Each point of instinct gives a 10% change of intelligence 1 and a 1% chance of intelligence 2
            if (result.Instinct >= intOdds)
                result.Intelligence = 2;
            else if ((result.Instinct * 10) >= intOdds)
                result.Intelligence = 1;

            if (behavior.Attribute != null)
                foreach (var item in behavior.Attribute)
                {
                    int bonus = item.Add;
                    switch (item.Name)
                    {
                        case "Strength": result.Strength += bonus; break;
                        case "Dexterity": result.Dexterity += bonus; break;
                        case "Endurance": result.Endurance += bonus; break;
                        case "Intelligence": result.Intelligence += bonus; break;
                        case "Instinct": result.Instinct += bonus; break;
                        case "Pack": result.Pack += bonus; break;
                        default: throw new System.Exception("Unkonw attribute " + item.Name);
                    }
                }

            //Skills
            var basicTraining = new List<string>() { "Survival", "Recon", "Athletics", "Melee" };
            if (behavior.Skill != null)
                foreach (var item in behavior.Skill)
                    basicTraining.Add(item.Name);

            var skillPicks = dice.D(6) + result.InstinctDM;
            for (int i = 0; i < skillPicks; i++)
                result.Skills.Increase(dice.Choose(basicTraining), 1);


            //Base skills added at level 0 if you don't already have them
            result.Skills.Add("Survival");
            result.Skills.Add("Recon");
            result.Skills.Add("Athletics");

            //Weapons
            var baseDamage = (int)m_Templates.DamageTable.Last(x => result.Strength >= x.MinValue).Damage;

            var weapon = m_Templates.WeaponTable.Single((dice.D(2, 6) + (int)selectedAnimalType.WeaponDM).Limit(1, 13));
            if (weapon.Weapon != "None")
                result.Weapons.Add(new Weapon() { Name = weapon.Weapon, Damage = (baseDamage + int.Parse(weapon.Bonus)) + "d6" });

            if (selectedAnimalType.Name == "Scavenger" && !(result.Weapons.Any(x => x.Name.Contains("Teeth"))))
                result.Weapons.Add(new Weapon() { Name = "Teeth", Damage = baseDamage + "d6" });

            result.NumberEncountered = m_Templates.NumberTable.Last(x => result.Pack.Limit(0, 15) >= x.MinValue).Number;
            result.Attack = behavior.Attack;
            result.Flee = behavior.Flee;

            //Armor
            result.Armor = m_Templates.ArmorTable.Single(dice.D(2, 6)).Armor;

            return result;

        }


    }















}
