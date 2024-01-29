using Grauenwolf.TravellerTools.Characters.Careers;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Shared.Names;
using System.Collections.Immutable;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Characters;

public abstract class SpeciesCharacterBuilder
{
    static readonly ImmutableList<string> s_BackgroundSkills = ImmutableList.Create("Admin", "Animals", "Art", "Athletics", "Carouse", "Drive", "Science", "Seafarer", "Streetwise", "Survival", "Vacc Suit", "Electronics", "Flyer", "Language", "Mechanic", "Medic", "Profession");

    readonly OddsTable<int> m_AgeTable;
    readonly CharacterBuilder m_CharacterBuilder;
    readonly ImmutableArray<string> m_Personalities;
    ImmutableArray<CareerBase> m_Careers;
    ImmutableArray<CareerBase> m_DefaultCareers;
    ImmutableArray<CareerBase> m_DraftCareers;

    public SpeciesCharacterBuilder(string dataPath, CharacterBuilder characterBuilder)
    {
        m_CharacterBuilder = characterBuilder;

        var converter = new XmlSerializer(typeof(CharacterTemplates));

        if (CharacterBuilderFilename != null)
        {
            var file = new FileInfo(Path.Combine(dataPath, CharacterBuilderFilename));
            using (var stream = file.OpenRead())
                Books = ImmutableArray.Create(new Book((CharacterTemplates)converter.Deserialize(stream)!));
        }

        (m_DefaultCareers, m_DraftCareers, m_Careers) = CreateCareerList();

        var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
        m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();

        //Age Table
        var ages = new OddsTable<int>();
        var age = StartingAge - 4; //Pre-adults
        const int baseOdds = 20;

        while (true)
        {
            int odds;

            if (age < StartingAge)
                odds = baseOdds - (2 * (StartingAge - age)); //-2 per year under starting age
            else if (age < AgingRollMinAge)
                odds = baseOdds;
            else
            {
                var currentTerm = (int)Math.Floor((age - StartingAge) / 4.0);
                odds = baseOdds + AgingRollDM(currentTerm); //aging DM is a negative number
            }
            if (odds > 0)
                ages.Add(age, odds);
            else
                break;

            age += 1;
        }

        m_AgeTable = ages;
    }

    public virtual ImmutableArray<Book> Books { get; protected set; }
    public abstract string Faction { get; }
    public abstract ImmutableArray<Gender> Genders { get; }
    public virtual string? Remarks { get; }
    public virtual string? Source { get; }
    public abstract string Species { get; }
    public virtual string SpeciesGroup => Species;
    public abstract string SpeciesUrl { get; }
    public virtual int StartingAge => 18;
    protected virtual int AgingRollMinAge => 34;
    protected abstract bool AllowPsionics { get; }
    protected virtual string? CharacterBuilderFilename => "CharacterBuilder.xml";
    protected virtual LanguageType LanguageTypeForNames => LanguageType.Humaniti;
    protected NameGenerator NameGenerator => m_CharacterBuilder.NameGenerator;

    public virtual Book Book(Character character) => Books[0];

    public Character Build(CharacterBuilderOptions options)
    {
        //Copy the values out of `options`, but don't capture it. The `options` object may be reused with different values.

        var seed = options.Seed ?? (new Random()).Next();
        var dice = new Dice(seed);

        var character = new Character
        {
            Seed = seed,
            Species = Species,
            SpeciesUrl = SpeciesUrl,
            FirstAssignment = options.FirstAssignment,
            FirstCareer = options.FirstCareer,
            Name = options.Name,
            Gender = options.Gender,
            MaxAge = options.MaxAge,
            Year = options.Year
        };

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

        SetFinalTitle(character);

        //Fix skills that should have had specialities. This shouldn't happen, but there are bugs elsewhere that cause it.
        var needsSpecials = character.Skills.Where(s => s.Level > 0 && s.Specialty == null && Book(character).RequiresSpeciality(s.Name)).ToList();
        foreach (var skill in needsSpecials)
        {
            character.Skills.Increase(dice.Choose(Book(character).SpecialtiesFor(skill.Name)), skill.Level);
            character.Skills.Remove(skill);
        }

        //Add the skill groups [Art, Profession, Science]
        foreach (var skill in character.Skills.Where(s => s.Specialty != null))
        {
            var template = Book(character).RandomSkills.FirstOrDefault(s => s.Name == skill.Name && s.Specialty == skill.Specialty && s.Group != null);
            if (template != null)
                skill.Group = template.Group;
        }
        //Remove redundant level 0 skills
        character.Skills.Collapse();

        //Add specialties for remaining level 0 broad skills [Art, Profession, Science]
        foreach (var skill in character.Skills.Where(s => s.Level == 0 && s.Specialty is null))
        {
            var groups = Book(character).RandomSkills.Where(s => s.Name == skill.Name && s.Group != null).Select(s => s.Group).Distinct().ToList();
            if (groups.Count > 0)
                skill.Group = dice.Choose(groups);
        }

        //Half of all contacts should be the same species.
        var odds = new OddsTable<string>();
        foreach (var species in m_CharacterBuilder.SpeciesList)
            if (species == character.Species)
                odds.Add(species, m_CharacterBuilder.SpeciesList.Length - 1);
            else
                odds.Add(species, 1);

        m_CharacterBuilder.BuildContacts(dice, character, odds);

        return character;
    }

    public ImmutableArray<CareerBase> Careers() => Careers(null);

    public virtual ImmutableArray<CareerBase> Careers(Character? character)
    {
        return m_Careers;
    }

    public virtual ImmutableArray<CareerBase> DefaultCareers(Character? character)
    {
        return m_DefaultCareers;
    }

    public virtual ImmutableArray<CareerBase> DraftCareers(Character? character)
    {
        return m_DraftCareers;
    }

    public virtual string GenerateName(Dice dice, string genderCode)
    {
        return NameGenerator.GenerateName(LanguageTypeForNames, dice, genderCode);
    }

    public virtual void Injury(Character character, Dice dice, CareerBase career, bool severe, int age)
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

    public virtual void LifeEvent(Character character, Dice dice, CareerBase career)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                Injury(character, dice, career, false, character.Age + dice.D(4));
                return;

            case 3:
                character.AddHistory($"Birth or Death involving a family member or close friend.", dice);
                return;

            case 4:
                character.AddHistory($"A romantic relationship ends badly. Gain a Rival or Enemy.", dice);
                if (dice.NextBoolean())
                    character.AddRival();
                else
                    character.AddEnemy();
                return;

            case 5:
                character.AddHistory($"A romantic relationship deepens, possibly leading to marriage. Gain an Ally.", dice);
                character.AddAlly();
                return;

            case 6:
                character.AddHistory($"A new romantic starts. Gain an Ally.", dice);
                character.AddAlly();
                return;

            case 7:
                character.AddHistory($"Gained a contact.", dice);
                return;

            case 8:
                character.AddHistory($"Betrayal. Convert an Ally into a Rival or Contact into an Enemy.", dice);
                character.DowngradeContact();
                return;

            case 9:
                character.AddHistory($"Moved to a new world.", dice);
                character.NextTermBenefits.QualificationDM += 1;
                return;

            case 10:
                character.AddHistory($"Good fortune", dice);
                character.BenefitRollDMs.Add(2);
                return;

            case 11:
                if (dice.NextBoolean())
                {
                    character.BenefitRolls -= 1;
                    character.AddHistory($"Victim of a crime", dice);
                }
                else
                {
                    character.AddHistory($"Accused of a crime", dice);
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
                character.AddHistory($"Contacted by an underground psionic group.", dice);
                character.LongTermBenefits.MayTestPsi = true;
                return;

            case 3:
                character.AddHistory($"Suffered a deep tragedy.", dice);
                character.CurrentTermBenefits.GraduationDM = -100;
                return;

            case 4:

                var roll = dice.D(2, 6) + character.SocialStandingDM;

                if (roll >= 8)
                {
                    character.AddHistory($"A prank goes horribly wrong. Gain a Rival.", dice);
                    character.AddRival();
                }
                else if (roll > 2)
                {
                    character.AddHistory($"A prank goes horribly wrong. Gain an Enemy", dice);
                    character.AddEnemy();
                }
                else
                {
                    character.AddHistory($"A prank goes horribly wrong and {character.Name} is sent to prison.", dice);
                    character.NextTermBenefits.MustEnroll = "Prisoner";
                }
                return;

            case 5:
                character.AddHistory($"Spent the college years partying.", dice);
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
                    var age = character.AddHistory($"Become leader in social movement.", dice);
                    character.AddHistory($"Gain an Ally and an Enemy.", age);
                    character.AddAlly();
                    character.AddEnemy();
                }
                else
                    character.AddHistory($"Join a social movement.", dice);
                return;

            case 9:
                {
                    var skillList = new SkillTemplateCollection(Book(character).RandomSkills);
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
                        character.AddHistory($"Expand the field of {skill}, but gain a Rival in {character.Name}'s former tutor.", dice);
                        character.AddRival();
                    }
                }
                return;

            case 11:
                {
                    var age = character.AddHistory($"War breaks out, triggering a mandatory draft.", dice);
                    if (dice.RollHigh(character.SocialStandingDM, 9))
                        character.AddHistory($"Used social standing to avoid the draft.", age);
                    else
                    {
                        character.CurrentTermBenefits.GraduationDM -= 100;
                        if (dice.NextBoolean())
                        {
                            character.AddHistory($"Fled from the draft.", age);
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
                character.AddHistory($"Widely recognized.", dice);
                character.SocialStanding += 1;
                return;
        }
    }

    public CareerBase RollDraft(Character character, Dice dice)
    {
        return dice.Choose(DraftCareers(character));
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
        character.AddHistory($"Tested for psionic powers", age);

        character.Psi = RollForPsi(character, dice);

        if (character.Psi <= 0)
        {
            character.Psi = 0;
            return false;
        }

        var availableSkills = new PsionicSkillTemplateCollection(Book(character).PsionicTalents);

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
                var age = character.AddHistory($"Encounter a Psionic institute.", dice);
                if (AllowPsionics)
                    if (TestPsionic(character, dice, age))
                        character.NextTermBenefits.MustEnroll = "Psion";
                return;

            case 2:
                character.AddHistory($"Spend time with an alien race. Gain a contact.", dice);
                var skillList = new SkillTemplateCollection(Book(character).SpecialtiesFor("Science"));
                skillList.RemoveOverlap(character.Skills, 1);
                if (skillList.Count > 0)
                    character.Skills.Add(dice.Choose(skillList), 1);
                return;

            case 3:
                character.AddHistory($"Find an Alien Artifact.", dice);
                return;

            case 4:
                character.AddHistory($"Amnesia.", dice);
                return;

            case 5:
                character.AddHistory($"Contact with Government.", dice);
                return;

            case 6:
                character.AddHistory($"Find Ancient Technology.", dice);
                return;
        }
    }

    internal virtual void FixupSkills(Character character, Dice dice)
    {
    }

    internal int RandomAge(Dice dice, bool noChildren = false)
    {
        if (noChildren) //keep trying until we get an adult
            while (true)
            {
                int age = m_AgeTable.Choose(dice);
                if (age >= StartingAge + 4)
                    return age;
            }
        else
            return m_AgeTable.Choose(dice);
    }

    protected virtual void AddBackgroundSkills(Dice dice, Character character)
    {
        var skillPicks = character.EducationDM + 3;
        if (character.MaxAge < StartingAge)
            skillPicks = (int)Math.Ceiling(skillPicks / 2.0);

        if (skillPicks > 0)
        {
            var backgroundSKills = dice.Choose(s_BackgroundSkills, skillPicks, allowDuplicates: false);
            foreach (var skill in backgroundSKills)
                character.Skills.Add(skill); //all skills added at level 0
        }
    }

    protected virtual int AgingRollDM(int currentTerm) => -1 * currentTerm;

    /// <summary>
    /// Creates the career list.
    /// </summary>
    /// <returns>If career lists needs special handling based on the character, return empty lists.</returns>
    protected abstract CareerLists CreateCareerList();

    /// <summary>
    /// Forces the first term for some species.
    /// </summary>
    protected virtual void ForceFirstTerm(Character character, Dice dice) { }

    protected virtual void InitialCharacterStats(Dice dice, Character character)
    {
        character.Age = StartingAge;
        character.Strength = dice.D(2, 6);
        character.Dexterity = dice.D(2, 6);
        character.Endurance = dice.D(2, 6);
        character.Intellect = dice.D(2, 6);
        character.Education = dice.D(2, 6);
        character.SocialStanding = dice.D(2, 6);
    }

    protected virtual CareerBase PickNextCareer(Character character, Dice dice)
    {
        CareerBase? previousAssignment = null;
        if (character.LastCareer != null)
            previousAssignment = Careers(character).SingleOrDefault(c => character.LastCareer.ShortName == c.ShortName);

        bool noRoll = false;
        var careerOptions = new List<CareerBase>();

        //Forced picks (e.g. Draft)
        if (character.NextTermBenefits.MustEnroll != null)
        {
            foreach (var career in Careers(character))
            {
                noRoll = true; //Don't need to roll if forced to enroll
                if (string.Equals(character.NextTermBenefits.MustEnroll, career.Career, StringComparison.OrdinalIgnoreCase) || string.Equals(character.NextTermBenefits.MustEnroll, career.Assignment, StringComparison.OrdinalIgnoreCase))
                {
                    careerOptions.Add(career);
                }
            }
        }

        //Normal career progression
        if (!character.NextTermBenefits.MusterOut && careerOptions.Count == 0 && character.LastCareer != null)
        {
            //1: New career.
            //2-3: New assignment in same career, if RankCarryover.
            //4+: Same assignment.

            var continueRoll = dice.D(10);

            if (continueRoll == 1)
            {
                character.NextTermBenefits.MusterOut = true;
                character.AddHistory($"Voluntarily left " + character.LastCareer.ShortName, character.Age);
            }
            else if (continueRoll <= 3 && previousAssignment!.RankCarryover)
            {
                character.AddHistory($"Attempted to change assignments.", character.Age);
                foreach (var career in Careers(character))
                    if (previousAssignment != career && previousAssignment.Career == career.Career)
                        careerOptions.Add(career);
            }
            else
            {
                noRoll = true; //Don't need to roll if continuing a career
                careerOptions.Add(previousAssignment!);
            }
        }

        //Random picks
        if (careerOptions.Count == 0)
        {
            foreach (var career in Careers(character))
            {
                if (character.NextTermBenefits.MusterOut && character.LastCareer!.Career == career.Career)
                    continue; //No assignments from previous career allowed

                if (career.Qualify(character, dice, true))
                    careerOptions.Add(career);
            }
        }

        //Random picks when not qualified for anything
        if (careerOptions.Count == 0)
        {
            foreach (var career in Careers(character))
            {
                if (character.NextTermBenefits.MusterOut && character.LastCareer!.Career == career.Career)
                    continue; //No assignments from previous career allowed

                careerOptions.Add(career);
            }
        }

        var result = dice.Choose(careerOptions);

        if (result.Qualify(character, dice, false) || noRoll) //Force a Qualify roll so we can get special behavior for Psionic Community
            return result;
        else
        {
            character.AddHistory($"Failed to qualify for {result}.", character.Age);

            if (previousAssignment?.Career == result.Career && previousAssignment.RankCarryover)
            {
                return previousAssignment;
            }
            else if (character.PreviouslyDrafted || dice.NextBoolean())
            {
                return dice.Choose(DefaultCareers(character));
            }
            else
            {
                character.AddHistory($"Submitted to the draft.", character.Age);
                character.PreviouslyDrafted = true;
                return RollDraft(character, dice);
            }
        }
    }

    protected abstract int RollForPsi(Character character, Dice dice);

    protected virtual void SetFinalTitle(Character character)
    {
        if (character.LongTermBenefits.Retired)
        {
            var title = character.CareerHistory.Where(c => c.Title != null).OrderByDescending(c => c.LastTermAge + (100 * c.Rank) + (1000 * c.CommissionRank)).Select(c => c.Title).FirstOrDefault();
            if (title != null)
                character.Title = "Retired " + title;
        }
        else
            character.Title = character.LastCareer?.Title;
    }

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

        var roll = dice.D(2, 6) + AgingRollDM(character.CurrentTerm);
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

            if (!character.LongTermBenefits.Retired)
            {
                character.LongTermBenefits.Retired = true;
                character.AddHistory($"Retired at age {character.Age}.", character.Age);
            }
        }
    }
}
