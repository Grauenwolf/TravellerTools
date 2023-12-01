using Grauenwolf.TravellerTools.Characters.Careers;

namespace Grauenwolf.TravellerTools.Characters;

public class Character
{
    public int Age { get; set; } = 18;
    public int BenefitRolls { get; set; }
    public List<CareerHistory> CareerHistory { get; } = new();
    public List<Contact> Contacts { get; } = new();
    public int CurrentTerm { get; set; }
    public int Debt { get; set; }
    public int Dexterity { get; set; }
    public int DexterityDM => DMCalc(Dexterity);
    public int Education { get; set; }
    public int EducationDM => DMCalc(Education);
    public EducationHistory? EducationHistory { get; set; }
    public int Endurance { get; set; }

    public int EnduranceDM => DMCalc(Endurance);

    public string? FirstAssignment { get; set; }

    public string? FirstCareer { get; set; }

    public string? Gender { get; set; }

    public HistoryCollection History { get; } = new();

    public int Intellect { get; set; }

    public int IntellectDM => DMCalc(Intellect);

    public bool IsDead { get; set; }

    public CareerHistory? LastCareer { get; set; }

    public int? MaxAge { get; set; }

    public string? Name { get; set; }

    public int? Parole { get; set; }

    public List<string> Personality { get; } = new();

    public string PersonalityList => string.Join(", ", Personality);

    public int PreviousPsiAttempts { get; set; }

    public int? Psi { get; set; }

    public int PsiDM => Psi == null ? -100 : DMCalc(Psi.Value);

    /// <summary>
    /// Gets or sets the seed used to randomly create the character.
    /// </summary>
    /// <value>The seed.</value>
    public int Seed { get; set; }

    public SkillCollection Skills { get; } = new();

    public int SocialStanding { get; set; }

    public int SocialStandingDM => DMCalc(SocialStanding);

    public int Strength { get; set; }

    public int StrengthDM => DMCalc(Strength);

    public string? Title { get; set; }

    public List<string> Trace { get; } = new();

    public WeaponCollection Weapons { get; } = new();

    internal List<int> BenefitRollDMs { get; } = new();

    internal NextTermBenefits CurrentTermBenefits { get; set; } = new();

    internal LongTermBenefits LongTermBenefits { get; } = new();

    internal NextTermBenefits NextTermBenefits { get; set; } = new();

    //internal int UnusedAllies { get; private set; }
    internal Queue<ContactType> UnusedContacts { get; } = new();

    public void AddAlly(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Ally);
    }

    public void AddContact(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Contact);
    }

    public void AddEnemy(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Enemy);
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
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Rival);
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

        if (NextTermBenefits.EnlistmentDM.ContainsKey(career) == true)
            result += NextTermBenefits.EnlistmentDM[career];
        if (LongTermBenefits.EnlistmentDM.ContainsKey(career))
            result += LongTermBenefits.EnlistmentDM[career];

        if (assignment != null)
        {
            if (NextTermBenefits.EnlistmentDM.ContainsKey(assignment) == true)
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
