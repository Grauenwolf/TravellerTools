using Grauenwolf.TravellerTools.Characters.Careers;
using System;
using System.Collections.Generic;
using Tortuga.Anchor.Collections;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Characters
{
    public class Character : ModelBase
    {
        public EducationHistory EducationHistory;

        public int Age { get { return GetDefault<int>(18); } set { Set(value); } }

        public int BenefitRolls { get; set; }

        public ObservableCollectionExtended<CareerHistory> CareerHistory { get; } = new ObservableCollectionExtended<CareerHistory>();

        public int CurrentTerm { get { return Get<int>(); } set { Set(value); } }

        public int Debt { get; set; }

        public int Dexterity { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("Dexterity")]
        public int DexterityDM { get { return DMCalc(Dexterity); } }

        public int Education { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("Education")]
        public int EducationDM { get { return DMCalc(Education); } }

        public int Endurance { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("Endurance")]
        public int EnduranceDM { get { return DMCalc(Endurance); } }

        public string FirstAssignment { get; set; }

        public string FirstCareer { get; set; }

        public string Gender { get { return Get<string>(); } set { Set(value); } }

        public HistoryCollection History { get { return GetNew<HistoryCollection>(); } }

        public int Intellect { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("Intellect")]
        public int IntellectDM { get { return DMCalc(Intellect); } }

        public bool IsDead { get; set; }

        public CareerHistory LastCareer { get; set; }

        public int? MaxAge { get { return Get<int?>(); } set { Set(value); } }

        public string Name { get { return Get<string>(); } set { Set(value); } }

        public int? Parole { get; set; }

        public ObservableCollectionExtended<string> Personality { get; } = new ObservableCollectionExtended<string>();

        [CalculatedField("Personality")]
        public string PersonalityList => string.Join(", ", Personality);

        public int PreviousPsiAttempts { get; set; }

        public int? Psi { get { return Get<int?>(); } set { Set(value); } }

        [CalculatedField("Psi")]
        public int PsiDM { get { return Psi == null ? -100 : DMCalc(Psi.Value); } }

        /// <summary>
        /// Gets or sets the seed used to randomly create the character.
        /// </summary>
        /// <value>The seed.</value>
        public int Seed { get; set; }

        public SkillCollection Skills { get { return GetNew<SkillCollection>(); } }

        public int SocialStanding { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("SocialStanding")]
        public int SocialStandingDM { get { return DMCalc(SocialStanding); } }

        public int Strength { get { return Get<int>(); } set { Set(value); } }

        [CalculatedField("Strength")]
        public int StrengthDM { get { return DMCalc(Strength); } }

        public string Title { get; set; }

        public ObservableCollectionExtended<string> Trace { get; } = new ObservableCollectionExtended<string>();

        public WeaponCollection Weapons { get { return GetNew<WeaponCollection>(); } }

        internal List<int> BenefitRollDMs { get; } = new List<int>();

        internal NextTermBenefits CurrentTermBenefits { get; set; }

        internal LongTermBenefits LongTermBenefits { get { return GetNew<LongTermBenefits>(); } }

        internal NextTermBenefits NextTermBenefits { get; set; }

        internal int UnusedAllies { get; private set; }

        internal int UnusedContacts { get; private set; }

        internal int UnusedEnemies { get; private set; }

        internal int UnusedRivals { get; private set; }

        public void AddAlly(int count = 1)
        {
            UnusedAllies += count;
        }

        public void AddContact(int count = 1)
        {
            UnusedContacts += count;
        }

        public void AddEnemy(int count = 1)
        {
            UnusedEnemies += count;
        }

        [Obsolete("Explicitly indicate the age.")]
        public void AddHistory(string text)
        {
            History.Add(new History(CurrentTerm, Age, text));
        }

        /// <summary>
        /// Adds the history.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="dice">The dice.</param>
        /// <returns>Returns the age rolled for this event.</returns>
        public int AddHistory(string text, Dice dice)
        {
            var age = Age + dice.D(4);
            History.Add(new History(CurrentTerm, age, text));
            return age;
        }

        public void AddHistory(string text, int age)
        {
            History.Add(new History(CurrentTerm, age, text));
        }

        public void AddRival(int count = 1)
        {
            UnusedRivals += count;
        }

        /// <summary>
        /// Gets the character builder options needed to recreate the character.
        /// </summary>
        public CharacterBuilderOptions GetCharacterBuilderOptions()
        {
            return new CharacterBuilderOptions()
            {
                Seed = Seed,
                FirstAssignment = FirstAssignment,
                FirstCareer = FirstCareer,
                Gender = Gender,
                MaxAge = Age,
                Name = Name
            };
        }

        public int GetDM(string attributeName)
        {
            return attributeName switch
            {
                "Strength" or "Str" => StrengthDM,
                "Dexterity" or "Dex" => DexterityDM,
                "Endurance" or "End" => EnduranceDM,
                "Intellect" or "Int" => IntellectDM,
                "Education" or "Edu" => EducationDM,
                "SS" or "Soc" or "SocialStanding" => SocialStandingDM,
                _ => throw new ArgumentOutOfRangeException(nameof(attributeName), attributeName, "Unknown attribute " + attributeName),
            };
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
                    throw new ArgumentOutOfRangeException(nameof(attributeName), attributeName, "Unknown attribute " + attributeName);
            }
        }

        //public FeatureCollection Features { get { return GetNew<FeatureCollection>(); } }
        internal int GetEnlistmentBonus(string career, string? assignment)
        {
            var result = NextTermBenefits.QualificationDM + LongTermBenefits.QualificationDM;

            if (NextTermBenefits.EnlistmentDM.ContainsKey(career))
                result += NextTermBenefits.EnlistmentDM[career];
            if (LongTermBenefits.EnlistmentDM.ContainsKey(career))
                result += LongTermBenefits.EnlistmentDM[career];

            if (assignment != null)
            {
                if (NextTermBenefits.EnlistmentDM.ContainsKey(assignment))
                    result += NextTermBenefits.EnlistmentDM[assignment];
                if (LongTermBenefits.EnlistmentDM.ContainsKey(assignment))
                    result += LongTermBenefits.EnlistmentDM[assignment];
            }

            return result;
        }

        static int DMCalc(int value)
        {
            return value switch
            {
                <= 0 => -3,
                <= 2 => -2,
                <= 5 => -1,
                <= 8 => 0,
                <= 11 => 1,
                <= 14 => 2,
                _ => 3
            };
        }
    }
}
