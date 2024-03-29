﻿using Grauenwolf.TravellerTools.Characters.Careers;
using System.Text;

namespace Grauenwolf.TravellerTools.Characters;

public class Character : IContactGroup

{
    public int Age { get; set; }
    public int BenefitRolls { get; set; }

    /// <summary>
    /// This applies to all benefit rolls.
    /// </summary>
    public int BenefitRollsPermanentDM { get; set; }

    public List<CareerHistory> CareerHistory { get; } = new();

    public string Characteristics
    {
        get
        {
            var result = new StringBuilder();
            result.Append($"STR {Strength} ({StrengthDM}) ");
            result.Append($"DEX {Dexterity} ({DexterityDM}) ");
            result.Append($"END {Endurance} ({EnduranceDM}) ");
            result.Append($"INT {Intellect} ({IntellectDM}) ");
            result.Append($"EDU {Education} ({EducationDM}) ");
            result.Append($"SOC {SocialStanding} ({SocialStandingDM}) ");

            if (Psi.HasValue)
                result.Append($"PSI {Psi} ({PsiDM}) ");
            if (Following.HasValue)
                result.Append($"FOL {Following} ({FollowingDM}) ");
            if (Territory.HasValue)
                result.Append($"TER {Territory} ({TerritoryDM}) ");

            return result.ToString().Trim();
        }
    }

    public bool CharismaReplacesSocialStanding { get; set; }

    public List<Contact> Contacts { get; } = new();
    public int CurrentTerm { get; set; }
    public int Debt { get; set; }
    public int Dexterity { get; set; }
    public int DexterityDM => Tables.DMCalc(Dexterity);
    public int Education { get; set; }
    public int EducationDM => Tables.DMCalc(Education);
    public EducationHistory? EducationHistory { get; set; }
    public int Endurance { get; set; }
    public int EnduranceDM => Tables.DMCalc(Endurance);
    public string? FirstAssignment { get; set; }
    public string? FirstCareer { get; set; }
    public int? Following { get; set; }

    public int FollowingDM
    {
        get
        {
            return Following switch
            {
                null => 0,
                <= 3 => 0,
                <= 6 => 1,
                <= 9 => 2,
                <= 12 => 3,
                _ => 4
            };
        }
    }

    public string? Gender { get; set; }

    public HistoryCollection History { get; } = new();

    public int Intellect { get; set; }

    public int IntellectDM => Tables.DMCalc(Intellect);

    public bool IsDead { get; set; }

    public bool IsMarried
    {
        get
        {
            return UnusedContacts.Contains(ContactType.Husband) || UnusedContacts.Contains(ContactType.Wife)
                || Contacts.Any(c => c.ContactType == ContactType.Husband || c.ContactType == ContactType.Wife);
        }
    }

    public bool IsOutcast { get; set; }

    public CareerHistory? LastCareer { get; set; }

    public int? MaxAge { get; set; }

    public string? Name { get; set; }

    public int? Parole { get; set; }

    public List<string> Personality { get; } = new();

    public string PersonalityList => string.Join(", ", Personality);

    public bool PreviouslyDrafted { get; set; }

    public int PreviousPsiAttempts { get; set; }

    public int? Psi { get; set; }

    public int PsiDM => Psi == null ? -100 : Tables.DMCalc(Psi.Value);

    public string? Race { get; set; }

    public int RiteOfPassageDM { get; set; }

    /// <summary>
    /// Gets or sets the seed used to randomly create the character.
    /// </summary>
    /// <value>The seed.</value>
    public int Seed { get; set; }

    public SkillCollection Skills { get; } = new();

    public int SocialStanding { get; set; }
    public int SocialStandingDM => Tables.DMCalc(SocialStanding);
    public string? Species { get; set; }

    public string? SpeciesUrl { get; set; }

    public int Strength { get; set; }

    public int StrengthDM => Tables.DMCalc(Strength);

    public int? Territory { get; set; }

    public int TerritoryDM => Tables.DMCalc(Territory);

    public string? Title { get; set; }

    public List<string> Trace { get; } = new();

    Queue<ContactType> IContactGroup.UnusedContacts => UnusedContacts;

    public string Upp
    {
        get { return new EHex(Strength).ToString() + new EHex(Dexterity).ToString() + new EHex(Endurance).ToString() + new EHex(Intellect).ToString() + new EHex(Education).ToString() + new EHex(SocialStanding).ToString(); }
    }

    public WeaponCollection Weapons { get; } = new();
    public int? Year { get; set; }

    /// <summary>
    /// These bonuses are lost when used.
    /// </summary>
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

    //public void AddHistory(string text)
    //{
    //    History.Add(new History(CurrentTerm, Age, text));
    //}
    public void AddHistory(string text, int age)
    {
        History.Add(new History(CurrentTerm, age, text));
    }

    public void AddHusband(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Husband);
    }

    public void AddRival(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Rival);
    }

    public void AddWife(int count = 1)
    {
        for (var i = 0; i < count; i++)
            UnusedContacts.Enqueue(ContactType.Wife);
    }

    /// <summary>
    /// Turns one contact into a enemy or one ally into a rival. If none, add a new rival.
    /// </summary>
    public void DowngradeContact()
    {
        var contact = Contacts.FirstOrDefault(c => c.ContactType == ContactType.Ally || c.ContactType == ContactType.Contact);
        if (contact == null)
            AddRival();
        else if (contact.ContactType == ContactType.Contact)
            contact.ContactType = ContactType.Enemy;
        else if (contact.ContactType == ContactType.Ally)
            contact.ContactType = ContactType.Rival;
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
            Name = Name,
            Year = Year,
            Species = Species,
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
            "Fol" or "Following" => FollowingDM,
            "TER" or "Territory" => TerritoryDM,
            _ => throw new ArgumentOutOfRangeException(nameof(attributeName), attributeName, "Unknown attribute " + attributeName),
        };
    }

    public bool RemoveContact(ContactType contactType)
    {
        if (!UnusedContacts.Contains(contactType))
            return false;

        var temp = UnusedContacts.ToList();
        temp.Remove(contactType);
        UnusedContacts.Clear();
        foreach (var item in temp)
            UnusedContacts.Enqueue(item);
        return true;
    }

    internal int GetAdvancementBonus(string career, string? assignment)
    {
        var result = CurrentTermBenefits.AdvancementDM + LongTermBenefits.AdvancementDM;

        if (LongTermBenefits.CareerAdvancementDM.ContainsKey(career))
            result += LongTermBenefits.CareerAdvancementDM[career];

        if (assignment != null)
        {
            if (LongTermBenefits.CareerAdvancementDM.ContainsKey(assignment))
                result += LongTermBenefits.CareerAdvancementDM[assignment];
        }

        return result;
    }

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
}
