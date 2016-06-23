using Grauenwolf.TravellerTools.Characters;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Animals.AE
{
    public class Animal : ModelBase
    {
        public string AnimalClass { get { return Get<string>(); } set { Set(value); } }
        public string TerrainType { get { return Get<string>(); } set { Set(value); } }



        public string Diet { get { return Get<string>(); } set { Set(value); } }
        public string Behavior { get { return Get<string>(); } set { Set(value); } }
        public string Movement { get { return Get<string>(); } set { Set(value); } }


        public int Size { get { return Get<int>(); } set { Set(value); } }

        public int WeightKG { get { return Get<int>(); } set { Set(value); } }


        public int Strength { get { return Get<int>(); } set { Set(value); } }
        public int Dexterity { get { return Get<int>(); } set { Set(value); } }
        public int Endurance { get { return Get<int>(); } set { Set(value); } }
        public int Intelligence { get { return Get<int>(); } set { Set(value); } }
        public int Instinct { get { return Get<int>(); } set { Set(value); } }
        public int Pack { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("Strength")]
        public int StrengthDM { get { return DMCalc(Strength); } }
        [CalculatedField("Dexterity")]
        public int DexterityDM { get { return DMCalc(Dexterity); } }
        [CalculatedField("Endurance")]
        public int EnduranceDM { get { return DMCalc(Endurance); } }
        [CalculatedField("Intelligence")]
        public int IntelligenceDM { get { return DMCalc(Intelligence); } }
        [CalculatedField("Instinct")]
        public int InstinctDM { get { return DMCalc(Instinct); } }
        [CalculatedField("Pack")]
        public int PackDM { get { return DMCalc(Pack); } }

        /// <summary>
        /// Roll used for encounter tables.
        /// </summary>
        public int Roll { get { return Get<int>(); } set { Set(value); } }


        public SkillCollection Skills { get { return GetNew<SkillCollection>(); } }

        public WeaponCollection Weapons { get { return GetNew<WeaponCollection>(); } }

        public FeatureCollection Features { get { return GetNew<FeatureCollection>(); } }


        public int Armor { get { return Get<int>(); } set { Set(value); } }

        public int? Attack { get { return Get<int?>(); } set { Set(value); } }

        public int? Flee { get { return Get<int?>(); } set { Set(value); } }

        public string NumberEncountered { get { return Get<string>(); } set { Set(value); } }

        public int QuirkRolls { get { return Get<int>(); } set { Set(value); } }
        public int PhysicalSkills { get { return Get<int>(); } set { Set(value); } }
        public int SocialSkills { get { return Get<int>(); } set { Set(value); } }
        public int EvolutionSkills { get { return Get<int>(); } set { Set(value); } }
        public int EvolutionDM { get { return Get<int>(); } set { Set(value); } }

        public int EvolutionRolls { get { return Get<int>(); } set { Set(value); } }

        int DMCalc(int value)
        {
            if (value == 0)
                return -3;
            if (value <= 2)
                return -2;
            if (value <= 5)
                return -1;
            if (value <= 8)
                return 0;
            if (value <= 11)
                return 1;
            if (value <= 14)
                return 2;
            //if (value >= 15)
            return 3;
        }

        public void Increase(string attributeName, int bonus)
        {
            switch (attributeName)
            {
                case "Size": Size += bonus; return;

                case "Strength":
                case "Str":
                    Strength += bonus; return;

                case "Dexterity":
                case "Dex":
                    Dexterity += bonus; return;

                case "Endurance":
                case "End":
                    Endurance += bonus; return;

                case "Intelligence":
                case "Int":
                    Intelligence += bonus; return;

                case "Instinct":
                case "Ins":
                    Instinct += bonus; return;

                case "Pack": Pack += bonus; return;

                case "Armor": Armor += bonus; return;

                case "QuirkRolls":
                case "Quirks":
                    QuirkRolls += bonus; return;

                case "PhysicalSkills": PhysicalSkills += bonus; return;
                case "SocialSkills": SocialSkills += bonus; return;
                case "EvolutionSkills": EvolutionSkills += bonus; return;
                case "EvolutionDM": EvolutionDM += bonus; return;
                case "EvolutionRolls": EvolutionRolls += bonus; return;

                default:
                    throw new System.ArgumentOutOfRangeException("attributeName", attributeName, "Unknown attribute " + attributeName);
            }
        }

    }


}


