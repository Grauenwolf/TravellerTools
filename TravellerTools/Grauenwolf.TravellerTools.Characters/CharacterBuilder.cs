using Grauenwolf.TravellerTools.Characters.Careers;
using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Characters;

public abstract class CharacterBuilder
{
    static readonly ImmutableList<string> s_BackgroundSkills = ImmutableList.Create("Admin", "Animals", "Art", "Athletics", "Carouse", "Drive", "Science", "Seafarer", "Streetwise", "Survival", "Vacc Suit", "Electronics", "Flyer", "Language", "Mechanic", "Medic", "Profession");

    CharacterBuilderLocator m_CharacterBuilderLocator;
    NameGenerator m_NameGenerator;
    ImmutableArray<string> m_Personalities;

    public CharacterBuilder(string dataPath, NameGenerator nameGenerator, CharacterBuilderLocator characterBuilderLocator)
    {
        m_NameGenerator = nameGenerator;
        m_CharacterBuilderLocator = characterBuilderLocator;
        var file = new FileInfo(Path.Combine(dataPath, CharacterBuilderFilename));

        var converter = new XmlSerializer(typeof(CharacterTemplates));

        using (var stream = file.OpenRead())
            Book = new Book((CharacterTemplates)converter.Deserialize(stream)!);

        (DefaultCareers, DraftCareers, Careers) = CreateCareerList();

        var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
        m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();
    }

    public Book Book { get; }

    public ImmutableArray<CareerBase> Careers { get; }
    public ImmutableArray<CareerBase> DefaultCareers { get; }
    public ImmutableArray<CareerBase> DraftCareers { get; }
    public abstract string Species { get; }

    protected virtual int AgingRollMinAge => 34;

    protected abstract bool AllowPsionics { get; }
    protected virtual string CharacterBuilderFilename => "CharacterBuilder.xml";

    public Character Build(CharacterBuilderOptions options)
    {
        var seed = options.Seed ?? (new Random()).Next();
        var dice = new Dice(seed);
        var character = new Character();

        character.Seed = seed;
        character.Species = Species;
        character.FirstAssignment = options.FirstAssignment;
        character.FirstCareer = options.FirstCareer;
        character.Name = options.Name;
        character.Gender = options.Gender;
        character.MaxAge = options.MaxAge;
        character.Year = options.Year;

        InitialCharacterStats(dice, character);

        AddBackgroundSkills(dice, character);
        FixupSkills(character, dice);

        character.CurrentTerm = 1;

        if (!string.IsNullOrEmpty(options.FirstAssignment))
            character.NextTermBenefits.MustEnroll = options.FirstAssignment;
        else if (!string.IsNullOrEmpty(options.FirstCareer))
            character.NextTermBenefits.MustEnroll = options.FirstCareer;
        else
            ForceFirstTerm(character, dice);

        while (!IsDone(options, character))
        {
            var nextCareer = PickNextCareer(character, dice);
            character.CurrentTermBenefits = character.NextTermBenefits;
            character.NextTermBenefits = new NextTermBenefits();
            nextCareer.Run(character, dice);

            if (character.LongTermBenefits.MayTestPsi && dice.RollHigh(10))
                TestPsionic(character, dice, character.Age);

            character.CurrentTerm += 1;

            if (character.Age >= AgingRollMinAge)
                AgingRoll(character, dice);
        }

        //Add personality
        int personalityTraits = dice.D(3);
        for (var i = 0; i < personalityTraits; i++)
            character.Personality.Add(dice.Choose(m_Personalities));

        //Fixups
        if (options.MaxAge.HasValue && !character.IsDead)
            character.Age = options.MaxAge.Value;

        character.Title = character.CareerHistory.Where(c => c.Title != null).OrderByDescending(c => c.Rank + c.CommissionRank).Select(c => c.Title).FirstOrDefault();

        //Add the skill groups [Art, Profession, Science]
        foreach (var skill in character.Skills.Where(s => s.Specialty != null))
        {
            var template = Book.RandomSkills.FirstOrDefault(s => s.Name == skill.Name && s.Specialty == skill.Specialty && s.Group != null);
            if (template != null)
                skill.Group = template.Group;
        }
        //Remove redundant level 0 skills
        character.Skills.Collapse();

        //Add specialties for remaining level 0 broad skills [Art, Profession, Science]
        foreach (var skill in character.Skills.Where(s => s.Level == 0 && s.Specialty is null))
        {
            var groups = Book.RandomSkills.Where(s => s.Name == skill.Name && s.Group != null).Select(s => s.Group).Distinct().ToList();
            if (groups.Count > 0)
                skill.Group = dice.Choose(groups);
        }

        //Half of all contacts should be the same species.
        var odds = new OddsTable<string>();
        foreach (var species in m_CharacterBuilderLocator.SpeciesList)
            if (species == character.Species)
                odds.Add(species, m_CharacterBuilderLocator.SpeciesList.Length - 1);
            else
                odds.Add(species, 1);

        m_CharacterBuilderLocator.BuildContacts(dice, character, odds);

        return character;
    }

    public void Injury(Character character, Dice dice, CareerBase career, bool severe, int age)
    {
        var medCovered = career.MedicalPaymentPercentage(character, dice);
        var medCostPerPoint = (1.0M - medCovered) * 5000;

        var roll = dice.D(6);
        if (severe)
            roll = Math.Min(roll, dice.D(6));

        int strengthPoints = 0;
        int dexterityPoints = 0;
        int endurancePoints = 0;
        int strengthPointsLost = 0;
        int dexterityPointsLost = 0;
        int endurancePointsLost = 0;
        string logMessage = "";

        switch (roll)
        {
            case 1:
                logMessage = "Nearly killed.";
                switch (dice.D(3))
                {
                    case 1:
                        strengthPoints = dice.D(6);
                        dexterityPoints = 2;
                        endurancePoints = 2;
                        break;

                    case 2:
                        strengthPoints = 2;
                        dexterityPoints = dice.D(6);
                        endurancePoints = 2;
                        break;

                    case 3:
                        strengthPoints = 2;
                        dexterityPoints = 2;
                        endurancePoints = dice.D(6);
                        break;
                }
                break;

            case 2:
                logMessage = "Severely injured.";
                switch (dice.D(3))
                {
                    case 1:
                        strengthPoints = dice.D(6);
                        break;

                    case 2:
                        dexterityPoints = dice.D(6);
                        break;

                    case 3:
                        endurancePoints = dice.D(6);
                        break;
                }
                break;

            case 3:
                logMessage = "Lost eye or limb.";
                switch (dice.D(2))
                {
                    case 1:
                        strengthPoints = 2;
                        break;

                    case 2:
                        dexterityPoints = 2;
                        break;
                }
                break;

            case 4:
                logMessage = "Scarred.";
                switch (dice.D(3))
                {
                    case 1:
                        strengthPoints = 2;
                        break;

                    case 2:
                        dexterityPoints = 2;
                        break;

                    case 3:
                        endurancePoints = 2;
                        break;
                }
                break;

            case 5:
                logMessage = "Injured.";
                switch (dice.D(3))
                {
                    case 1:
                        strengthPoints = 1;
                        break;

                    case 2:
                        dexterityPoints = 1;
                        break;

                    case 3:
                        endurancePoints = 1;
                        break;
                }
                break;

            case 6:
                logMessage = "Lightly injured, no permanent damage.";
                break;
        }

        var medicalBills = 0M;
        for (int i = 0; i < strengthPoints; i++)
        {
            if (dice.D(10) > 1) //90% chance of healing
                medicalBills += medCostPerPoint;
            else
            {
                character.Strength -= 1;
                strengthPointsLost += 1;
            }
        }
        for (int i = 0; i < dexterityPoints; i++)
        {
            if (dice.D(10) > 1) //90% chance of healing
                medicalBills += medCostPerPoint;
            else
            {
                character.Dexterity -= 1;
                dexterityPointsLost += 1;
            }
        }
        for (int i = 0; i < endurancePoints; i++)
        {
            if (dice.D(10) > 1) //90% chance of healing
                medicalBills += medCostPerPoint;
            else
            {
                character.Endurance -= 1;
                endurancePointsLost += 1;
            }
        }

        if (strengthPointsLost == 0 && dexterityPointsLost == 0 && endurancePointsLost == 0)
            logMessage += " Fully recovered.";

        if (medicalBills > 0)
            logMessage += $" Owe {medicalBills.ToString("N0")} for medical bills.";

        character.AddHistory(logMessage, age);
    }

    public void LifeEvent(Character character, Dice dice, CareerBase career)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                Injury(character, dice, career, false, character.Age + dice.D(4));
                return;

            case 3:
                character.AddHistory("Birth or Death involving a family member or close friend.", dice);
                return;

            case 4:
                character.AddHistory("A romantic relationship ends badly. Gain a Rival or Enemy.", dice);
                if (dice.NextBoolean())
                    character.AddRival();
                else
                    character.AddEnemy();
                return;

            case 5:
                character.AddHistory("A romantic relationship deepens, possibly leading to marriage. Gain an Ally.", dice);
                character.AddAlly();
                return;

            case 6:
                character.AddHistory("A new romantic starts. Gain an Ally.", dice);
                character.AddAlly();
                return;

            case 7:
                character.AddHistory("Gained a contact.", dice);
                return;

            case 8:
                character.AddHistory("Betrayal. Convert an Ally into a Rival or Contact into an Enemy.", dice);
                character.DowngradeContact();
                return;

            case 9:
                character.AddHistory("Moved to a new world.", dice);
                character.NextTermBenefits.QualificationDM += 1;
                return;

            case 10:
                character.AddHistory("Good fortune", dice);
                character.BenefitRollDMs.Add(2);
                return;

            case 11:
                if (dice.NextBoolean())
                {
                    character.BenefitRolls -= 1;
                    character.AddHistory("Victim of a crime", dice);
                }
                else
                {
                    character.AddHistory("Accused of a crime", dice);
                    character.NextTermBenefits.MustEnroll = "Prisoner";
                }
                return;

            case 12:
                UnusualLifeEvent(character, dice);
                return;
        }
    }

    public void PreCareerEvents(Character character, Dice dice, CareerBase career, SkillTemplateCollection skills)
    {
        var skillSet = skills switch
        {
            [] => [.. character.Skills.Select(s => s.ToSkillTemplate())],
            _ => skills
        };

        switch (dice.D(2, 6))
        {
            case 2:
                character.AddHistory("Contacted by an underground psionic group.", dice);
                character.LongTermBenefits.MayTestPsi = true;
                return;

            case 3:
                character.AddHistory("Suffered a deep tragedy.", dice);
                character.CurrentTermBenefits.GraduationDM = -100;
                return;

            case 4:

                var roll = dice.D(2, 6) + character.SocialStandingDM;

                if (roll >= 8)
                {
                    character.AddHistory("A prank goes horribly wrong. Gain a Rival.", dice);
                    character.AddRival();
                }
                else if (roll > 2)
                {
                    character.AddHistory("A prank goes horribly wrong. Gain an Enemy", dice);
                    character.AddEnemy();
                }
                else
                {
                    character.AddHistory("A prank goes horribly wrong and you are sent to prison.", dice);
                    character.NextTermBenefits.MustEnroll = "Prisoner";
                }
                return;

            case 5:
                character.AddHistory("Spent the college years partying.", dice);
                character.Skills.Add("Carouse", 1);
                return;

            case 6:
                int count = dice.D(3);
                character.AddHistory($"Made lifelong friends. Gain {count} Allies.", dice);
                character.AddAlly(count);
                return;

            case 7:
                LifeEvent(character, dice, career);
                return;

            case 8:
                if (dice.RollHigh(character.SocialStandingDM, 8))
                {
                    var age = character.AddHistory("Become leader in social movement.", dice);
                    character.AddHistory("Gain an Ally and an Enemy.", age);
                    character.AddAlly();
                    character.AddEnemy();
                }
                else
                    character.AddHistory("Join a social movement.", dice);
                return;

            case 9:
                {
                    var skillList = new SkillTemplateCollection(Book.RandomSkills);
                    skillList.RemoveOverlap(character.Skills, 0);

                    if (skillList.Count > 0)
                    {
                        var skill = dice.Choose(skillList);
                        character.Skills.Add(skill);
                        character.AddHistory($"Study {skill} as a hobby.", dice);
                    }
                }
                return;

            case 10:

                if (dice.RollHigh(9))
                {
                    if (skillSet.Count > 0)
                    {
                        var skill = dice.Choose(skillSet);
                        character.Skills.Increase(skill);
                        character.AddHistory($"Expand the field of {skill}, but gain a Rival in your former tutor.", dice);
                        character.AddRival();
                    }
                }
                return;

            case 11:
                {
                    var age = character.AddHistory("War breaks out, triggering a mandatory draft.", dice);
                    if (dice.RollHigh(character.SocialStandingDM, 9))
                        character.AddHistory("Used social standing to avoid the draft.", age);
                    else
                    {
                        character.CurrentTermBenefits.GraduationDM -= 100;
                        if (dice.NextBoolean())
                        {
                            character.AddHistory("Fled from the draft.", age);
                            character.NextTermBenefits.MustEnroll = "Drifter";
                        }
                        else
                        {
                            var roll2 = dice.D(6);
                            if (roll2 <= 3)
                                character.NextTermBenefits.MustEnroll = "Army";
                            else if (roll2 <= 5)
                                character.NextTermBenefits.MustEnroll = "Marine";
                            else
                                character.NextTermBenefits.MustEnroll = "Navy";
                        }
                    }
                }
                return;

            case 12:
                character.AddHistory("Widely recognized.", dice);
                character.SocialStanding += 1;
                return;
        }
    }

    public CareerBase RollDraft(Dice dice)
    {
        return dice.Choose(DraftCareers);
        //switch (dice.D(6))
        //{
        //    case 1: return "Navy";
        //    case 2: return "Army";
        //    case 3: return "Marine";
        //    case 4: return "Merchant Marine";
        //    case 5: return "Scout";
        //    case 6: return "Law Enforcement";
        //}
        //return null!;
    }

    /// <summary>
    /// Tests for psionic talents.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="dice">The dice.</param>
    /// <param name="age">The age.</param>
    /// <returns>Returns true is at least one psionic skill was gained.</returns>
    public bool TestPsionic(Character character, Dice dice, int age)
    {
        if (!AllowPsionics)
            return false;  //not allowed
        if (character.Psi.HasValue)
            return false; //already tested

        character.LongTermBenefits.MayTestPsi = false;
        character.AddHistory("Tested for psionic powers", age);

        character.Psi = RollForPsi(character, dice);

        if (character.Psi <= 0)
        {
            character.Psi = 0;
            return false;
        }

        var availableSkills = new PsionicSkillTemplateCollection(Book.PsionicTalents);

        character.PreviousPsiAttempts = 0;

        bool result = false;
        //roll for every skill
        while (availableSkills.Count > 0)
        {
            var nextSkill = dice.Pick(availableSkills);
            if (nextSkill.Name == "Telepathy" && character.PreviousPsiAttempts == 0)
            {
                character.Skills.Add(nextSkill);
            }
            else
            {
                if ((dice.D(2, 6) + nextSkill.LearningDM + character.PsiDM - character.PreviousPsiAttempts) >= 8)
                {
                    character.Skills.Add(nextSkill);
                    result = true;
                }
            }

            character.PreviousPsiAttempts += 1;
        }
        return result;
    }

    public void UnusualLifeEvent(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                var age = character.AddHistory("Encounter a Psionic institute.", dice);
                if (AllowPsionics)
                    if (TestPsionic(character, dice, age))
                        character.NextTermBenefits.MustEnroll = "Psion";
                return;

            case 2:
                character.AddHistory("Spend time with an alien race. Gain a contact.", dice);
                var skillList = new SkillTemplateCollection(Book.SpecialtiesFor("Science"));
                skillList.RemoveOverlap(character.Skills, 1);
                if (skillList.Count > 0)
                    character.Skills.Add(dice.Choose(skillList), 1);
                return;

            case 3:
                character.AddHistory("Find an Alien Artifact.", dice);
                return;

            case 4:
                character.AddHistory("Amnesia.", dice);
                return;

            case 5:
                character.AddHistory("Contact with Government.", dice);
                return;

            case 6:
                character.AddHistory("Find Ancient Technology.", dice);
                return;
        }
    }

    internal virtual void FixupSkills(Character character, Dice dice)
    {
    }

    protected virtual void AddBackgroundSkills(Dice dice, Character character)
    {
        if (character.EducationDM + 3 > 0)
        {
            var backgroundSKills = dice.Choose(s_BackgroundSkills, character.EducationDM + 3, allowDuplicates: false);
            foreach (var skill in backgroundSKills)
                character.Skills.Add(skill); //all skills added at level 0
        }
    }

    protected virtual int AgingRollDM(Character character)
    {
        return -1 * character.CurrentTerm;
    }

    protected abstract CareerLists CreateCareerList();

    /// <summary>
    /// Forces the first term for some species.
    /// </summary>
    protected virtual void ForceFirstTerm(Character character, Dice dice) { }

    protected virtual void InitialCharacterStats(Dice dice, Character character)
    {
        character.Age = 18;
        character.Strength = dice.D(2, 6);
        character.Dexterity = dice.D(2, 6);
        character.Endurance = dice.D(2, 6);
        character.Intellect = dice.D(2, 6);
        character.Education = dice.D(2, 6);
        character.SocialStanding = dice.D(2, 6);
    }

    protected abstract int RollForPsi(Character character, Dice dice);

    static bool IsDone(CharacterBuilderOptions options, Character character)
    {
        if (character.Strength <= 0 ||
            character.Dexterity <= 0 ||
            character.Endurance <= 0 ||
            character.Intellect <= 0 ||
            character.Education <= 0 ||
            character.SocialStanding <= 0)
        {
            character.AddHistory($"Died at age {character.Age}", character.Age);
            character.IsDead = true;
            return true;
        }

        if ((character.Age + 3) >= options.MaxAge) //+3 because terms are 4 years long
            return true;

        return false;
    }

    void AgingRoll(Character character, Dice dice)
    {
        //TODO: Anagathics page 47

        var roll = dice.D(2, 6) + AgingRollDM(character);
        if (roll <= -6)
        {
            character.Strength += -2;
            character.Dexterity += -2;
            character.Endurance += -2;
            switch (dice.D(3))
            {
                case 1: character.Intellect += -1; break;
                case 2: character.Education += -1; break;
                case 3: character.SocialStanding += -1; break;
            }
        }
        else if (roll == -5)
        {
            character.Strength += -2;
            character.Dexterity += -2;
            character.Endurance += -2;
        }
        else if (roll == -4)
        {
            switch (dice.D(3))
            {
                case 1:
                    character.Strength += -2;
                    character.Dexterity += -2;
                    character.Endurance += -1;
                    break;

                case 2:
                    character.Strength += -1;
                    character.Dexterity += -2;
                    character.Endurance += -2;
                    break;

                case 3:
                    character.Strength += -2;
                    character.Dexterity += -1;
                    character.Endurance += -2;
                    break;
            }
        }
        else if (roll == -3)
        {
            switch (dice.D(3))
            {
                case 1:
                    character.Strength += -2;
                    character.Dexterity += -1;
                    character.Endurance += -1;
                    break;

                case 2:
                    character.Strength += -1;
                    character.Dexterity += -2;
                    character.Endurance += -1;
                    break;

                case 3:
                    character.Strength += -1;
                    character.Dexterity += -1;
                    character.Endurance += -2;
                    break;
            }
        }
        else if (roll == -2)
        {
            character.Strength += -1;
            character.Dexterity += -1;
            character.Endurance += -1;
        }
        else if (roll == -1)
        {
            switch (dice.D(3))
            {
                case 1:
                    character.Strength += -1;
                    character.Dexterity += -1;
                    break;

                case 2:
                    character.Strength += -1;
                    character.Endurance += -1;
                    break;

                case 3:
                    character.Dexterity += -1;
                    character.Endurance += -1;
                    break;
            }
        }
        else if (roll == 0)
        {
            switch (dice.D(3))
            {
                case 1:
                    character.Strength += -1;
                    break;

                case 2:
                    character.Dexterity += -1;
                    break;

                case 3:
                    character.Endurance += -1;
                    break;
            }
        }
        else
        {
            return; //no again crisis possible.
        }

        if (character.Strength <= 0 ||
                character.Dexterity <= 0 ||
                character.Endurance <= 0 ||
                character.Intellect <= 0 ||
                character.Education <= 0 ||
                character.SocialStanding <= 0)
        {
            var bills = dice.D(6) * 10000;
            character.Debt += bills;
            character.AddHistory($"Aging Crisis. Owe {bills:N0} for medical bills.", character.Age);

            if (character.Strength < 1) character.Strength = 1;
            if (character.Dexterity < 1) character.Dexterity = 1;
            if (character.Endurance < 1) character.Endurance = 1;
            if (character.Intellect < 1) character.Intellect = 1;
            if (character.Education < 1) character.Education = 1;
            if (character.SocialStanding < 1) character.SocialStanding = 1;
            character.LongTermBenefits.QualificationDM = -100;
            character.LongTermBenefits.Retired = true;
        }
    }

    CareerBase PickNextCareer(Character character, Dice dice)
    {
        bool noRoll = false;
        var careers = new List<CareerBase>();

        //Forced picks (e.g. Draft)
        if (character.NextTermBenefits.MustEnroll != null)
        {
            foreach (var career in Careers)
            {
                noRoll = true; //Don't need to roll if forced to enroll
                if (string.Equals(character.NextTermBenefits.MustEnroll, career.Career, StringComparison.OrdinalIgnoreCase) || string.Equals(character.NextTermBenefits.MustEnroll, career.Assignment, StringComparison.OrdinalIgnoreCase))
                {
                    careers.Add(career);
                }
            }
        }

        //Normal career progression
        if (!character.NextTermBenefits.MusterOut && careers.Count == 0 && character.LastCareer != null)
        {
            if (dice.D(10) > 1) //continue career
            {
                foreach (var career in Careers)
                {
                    noRoll = true; //Don't need to roll if continuing a career
                    if (character.LastCareer.ShortName == career.ShortName)
                    {
                        careers.Add(career);
                    }
                }
            }
            else
            {
                character.NextTermBenefits.MusterOut = true;
                character.AddHistory("Voluntarily left " + character.LastCareer.ShortName, character.Age);
            }
        }

        //Random picks
        if (careers.Count == 0)
        {
            foreach (var career in Careers)
            {
                if (character.NextTermBenefits.MusterOut && character.LastCareer!.Career == career.Career)
                    continue; //No assignments from previous career allowed

                if (career.Qualify(character, dice, true))
                    careers.Add(career);
            }
        }

        //Random picks when not qualified for anything
        if (careers.Count == 0)
        {
            foreach (var career in Careers)
            {
                if (character.NextTermBenefits.MusterOut && character.LastCareer!.Career == career.Career)
                    continue; //No assignments from previous career allowed

                careers.Add(career);
            }
        }

        var result = dice.Choose(careers);

        if (result.Qualify(character, dice, false) || noRoll) //Force a Qualify roll so we can get special behavior for Psionic Community
            return result;
        else
        {
            character.AddHistory($"Failed to qualify for {result}.", character.Age);
            if (character.PreviouslyDrafted || dice.NextBoolean())
            {
                return dice.Choose(DefaultCareers);
            }
            else
            {
                character.AddHistory($"Submitted to the draft.", character.Age);
                character.PreviouslyDrafted = true;
                return RollDraft(dice);
            }
        }
    }
}
