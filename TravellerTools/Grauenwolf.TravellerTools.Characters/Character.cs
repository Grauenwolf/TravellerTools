using Grauenwolf.TravellerTools.Characters.Careers;
using System;
using System.Collections.Generic;
using Tortuga.Anchor.Collections;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class Character : ModelBase
    {
        public int Strength { get { return Get<int>(); } set { Set(value); } }
        public int Dexterity { get { return Get<int>(); } set { Set(value); } }
        public int Endurance { get { return Get<int>(); } set { Set(value); } }
        public int Intellect { get { return Get<int>(); } set { Set(value); } }
        public int Education { get { return Get<int>(); } set { Set(value); } }
        public int SocialStanding { get { return Get<int>(); } set { Set(value); } }


        public int Age { get { return GetDefault<int>(18); } set { Set(value); } }
        public int CurrentTerm { get { return Get<int>(); } set { Set(value); } }




        [CalculatedField("Strength")]
        public int StrengthDM { get { return DMCalc(Strength); } }

        [CalculatedField("Dexterity")]
        public int DexterityDM { get { return DMCalc(Dexterity); } }

        [CalculatedField("Endurance")]
        public int EnduranceDM { get { return DMCalc(Endurance); } }

        [CalculatedField("Intellect")]
        public int IntellectDM { get { return DMCalc(Intellect); } }

        [CalculatedField("Education")]
        public int EducationDM { get { return DMCalc(Education); } }

        [CalculatedField("SocialStanding")]
        public int SocialStandingDM { get { return DMCalc(SocialStanding); } }


        static int DMCalc(int value)
        {
            if (value <= 0)
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

        public int GetDM(string attributeName)
        {
            switch (attributeName)
            {
                case "Strength":
                case "Str":
                    return StrengthDM;

                case "Dexterity":
                case "Dex":
                    return DexterityDM;

                case "Endurance":
                case "End":
                    return EnduranceDM;

                case "Intellect":
                case "Int":
                    return IntellectDM;

                case "Education":
                case "Edu":
                    return EducationDM;

                case "SS":
                case "SocialStanding":
                    return SocialStandingDM;

                default:
                    throw new ArgumentOutOfRangeException("attributeName", attributeName, "Unknown attribute " + attributeName);
            }
        }

        public void Increase(string attributeName, int bonus)
        {
            switch (attributeName)
            {
                case "Strength":
                case "Str":
                    Strength += bonus; return;

                case "Dexterity":
                case "Dex":
                    Dexterity += bonus; return;

                case "Endurance":
                case "End":
                    Endurance += bonus; return;

                case "Intellect":
                case "Int":
                    Intellect += bonus; return;

                case "Education":
                case "Edu":
                    Education += bonus; return;

                case "SS":
                case "SocialStanding":
                    SocialStanding += bonus; return;

                //case "Armor": Armor += bonus; return;

                //case "QuirkRolls":
                //case "Quirks":
                //    QuirkRolls += bonus; return;

                //case "PhysicalSkills": PhysicalSkills += bonus; return;
                //case "SocialSkills": SocialSkills += bonus; return;

                //case "EvolutionSkills": EvolutionSkills += bonus; return;
                //case "EvolutionDM": EvolutionDM += bonus; return;
                //case "EvolutionRolls": EvolutionRolls += bonus; return;
                //case "InitiativeDM": InitiativeDM += bonus; return;

                default:
                    throw new ArgumentOutOfRangeException("attributeName", attributeName, "Unknown attribute " + attributeName);
            }
        }

        public SkillCollection Skills { get { return GetNew<SkillCollection>(); } }

        public WeaponCollection Weapons { get { return GetNew<WeaponCollection>(); } }

        //public FeatureCollection Features { get { return GetNew<FeatureCollection>(); } }

        public void AddHistory(string text)
        {
            History.Add(new History(CurrentTerm, text));
        }

        public HistoryCollection History { get { return GetNew<HistoryCollection>(); } }

        public ObservableCollectionExtended<string> Trace { get; } = new ObservableCollectionExtended<string>();

        public string Name { get { return Get<string>(); } set { Set(value); } }

        internal NextTermBenefits CurrentTermBenefits { get; set; }

        internal NextTermBenefits NextTermBenefits { get; set; }

        internal LongTermBenefits LongTermBenefits { get { return GetNew<LongTermBenefits>(); } }

        internal List<int> BenefitRollDMs { get; } = new List<int>();
        public int BenefitRolls { get; set; }

        public List<CareerHistory> CareerHistory { get; } = new List<CareerHistory>();

        public EducationHistory EducationHistory;

        public CareerHistory LastCareer { get; set; }
        public string Title { get; set; }

        internal int GetEnlistmentBonus(string career, string branch)
        {
            var result = NextTermBenefits.QualificationDM;

            if (NextTermBenefits.EnlistmentDM.ContainsKey(career))
                result += NextTermBenefits.EnlistmentDM[career];
            if (LongTermBenefits.EnlistmentDM.ContainsKey(career))
                result += LongTermBenefits.EnlistmentDM[career];
            if (branch != null)
            {
                if (NextTermBenefits.EnlistmentDM.ContainsKey(branch))
                    result += NextTermBenefits.EnlistmentDM[branch];
                if (LongTermBenefits.EnlistmentDM.ContainsKey(branch))
                    result += LongTermBenefits.EnlistmentDM[branch];
            }

            return result;
        }
        //public void IncreaseAge()
        //{
        //    Age += 4;
        //    CurrentTerm += 1;
        //}
    }
}





