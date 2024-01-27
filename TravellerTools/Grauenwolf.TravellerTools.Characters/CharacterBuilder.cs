using Grauenwolf.TravellerTools.Names;
using System.Collections.Immutable;
using System.Reflection;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools.Characters;

public class CharacterBuilder
{
    readonly ImmutableDictionary<string, SpeciesCharacterBuilder> m_CharacterBuilders;

    public CharacterBuilder(string dataPath, NameGenerator nameGenerator)
    {
        var builders = new Dictionary<string, SpeciesCharacterBuilder>();

        void Add(SpeciesCharacterBuilder builder)
        {
            foreach (var gender in builder.Genders)
                builders[builder.Species] = builder;
        }

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(SpeciesCharacterBuilder)) && !t.IsAbstract))
        {
            Add((SpeciesCharacterBuilder)Activator.CreateInstance(type, dataPath, this)!);
        }

        //Add(new AelYaelCharacterBuilder(dataPath, this));
        //Add(new AnswerinCharacterBuilder(dataPath, this));
        //Add(new AslanCharacterBuilder(dataPath, this));
        //Add(new AyanshiCharacterBuilder(dataPath, this));
        //Add(new AzhantiCharacterBuilder(dataPath, this));
        //Add(new BwapCharacterBuilder(dataPath, this));
        //Add(new CafadanCharacterBuilder(dataPath, this));
        //Add(new CassilldanCharacterBuilder(dataPath, this));
        //Add(new DarmineCharacterBuilder(dataPath, this));
        //Add(new DarrianImperiumCharacterBuilder(dataPath, this));
        //Add(new DynchiaImperiumCharacterBuilder(dataPath, this));
        //Add(new DynchiaCharacterBuilder(dataPath, this));
        //Add(new DolphinImperiumCharacterBuilder(dataPath, this));
        //Add(new HumanitiCharacterBuilder(dataPath, this));
        //Add(new SolomaniImperiumCharacterBuilder(dataPath, this));
        //Add(new TezcatCharacterBuilder(dataPath, this));
        //Add(new VilaniImperiumCharacterBuilder(dataPath, this));
        //Add(new FlorianCharacterBuilder(dataPath, this));
        //Add(new ZhodaniImperiumCharacterBuilder(dataPath, this));
        //Add(new HapprhaniCharacterBuilder(dataPath, this));
        //Add(new HalkaCharacterBuilder(dataPath, this));
        //Add(new KargolCharacterBuilder(dataPath, this));

        m_CharacterBuilders = builders.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase);

        SpeciesList = m_CharacterBuilders.Keys.OrderBy(x => x).ToImmutableArray();

        CareerNameList = m_CharacterBuilders.Values.SelectMany(x => x.Careers(null)).Select(x => x.Career).Distinct().OrderBy(x => x).ToImmutableArray();

        var skills = new SkillTemplateCollection();
        foreach (var builder in m_CharacterBuilders.Values)
            foreach (var book in builder.Books)
                skills.CopyFrom(book.AllSkills);
        AllSkills = skills.OrderBy(x => x.ToString()).ToImmutableArray();

        var talents = new PsionicSkillTemplateCollection();
        foreach (var builder in m_CharacterBuilders.Values)
            foreach (var book in builder.Books)
                talents.CopyFrom(book.PsionicTalents);
        AllPsionicTalents = talents.OrderBy(x => x.ToString()).ToImmutableArray();

        NameGenerator = nameGenerator;
    }

    public ImmutableArray<PsionicSkillTemplate> AllPsionicTalents { get; }
    public ImmutableArray<SkillTemplate> AllSkills { get; }
    public ImmutableArray<string> CareerNameList { get; }
    public NameGenerator NameGenerator { get; }
    public ImmutableArray<string> SpeciesList { get; }

    public Character Build(CharacterBuilderOptions options)
    {
        var builder = GetCharacterBuilder(options.Species);
        return builder.Build(options);
    }

    public void BuildContacts(Dice dice, IContactGroup character, OddsTable<string> odds)
    {
        while (character.UnusedContacts.Count > 0)
        {
            string? species = null;
            if (odds.Count > 0)
                species = odds.Choose(dice);

            character.Contacts.Add(CreateContact(dice, character.UnusedContacts.Dequeue(), character, species));
        }
    }

    public Character CreateCharacter(Dice dice, string? species = null)
    {
        while (true)
        {
            var options = CreateCharacterStub(dice, species);

            var character = Build(options);
            if (!character.IsDead)
            {
                var skill = character.Skills.BestSkill();
                return character;
            }
        }
    }

    public CharacterBuilderOptions CreateCharacterStub(Dice dice, string? species = null, string? genderCode = null, bool noChildren = false)
    {
        var options = new CharacterBuilderOptions();
        options.Species = species ?? GetRandomSpecies(dice);
        var builder = GetCharacterBuilder(options.Species);

        options.Gender = genderCode ?? dice.Choose(builder.Genders).GenderCode;

        //TODO Species specific name generators
        var temp = NameGenerator.CreateRandomPerson(dice, options.Gender == "M");
        options.Name = temp.FullName;

        options.MaxAge = builder.RandomAge(dice, noChildren);

        options.Seed = dice.Next();
        return options;
    }

    //public CharacterBuilderOptions CreateCharacterStubWithSkill(Dice dice, string targetSkillName, string targetSkillSpeciality, int? targetSkillLevel = null, string? species = null)
    //{
    //    return CreateCharacterWithSkill(dice, targetSkillName, targetSkillSpeciality, targetSkillLevel, species).GetCharacterBuilderOptions();
    //}

    /// <summary>
    /// Creates the character with a desired final career.
    /// </summary>
    /// <param name="careerList">The list of desired careers and/or assignments.</param>
    public Character CreateCharacterWithCareer(Dice dice, string career, string? species = null)
    {
        return CreateCharacterWithCareer(dice, new[] { career }, species);
    }

    /// <summary>
    /// Creates the character with a desired final career.
    /// </summary>
    /// <param name="careerList">The list of desired careers and/or assignments.</param>
    public Character CreateCharacterWithCareer(Dice dice, IReadOnlyList<string> careerList, string? species = null)
    {
        var characters = new List<Character>();

        for (int i = 0; i < 500; i++)
        {
            var character = Build(CreateCharacterStub(dice, species, noChildren: true));
            if (character.IsDead && !character.LongTermBenefits.Retired)
                continue;

            if (careerList.Contains(character.LastCareer?.Career))
                return character;

            if (careerList.Contains(character.LastCareer?.Assignment))
                return character;

            characters.Add(character);
        }

        double Suitability(Character item, bool includeCareers)
        {
            var baseValue = 0.00;

            if (includeCareers)
            {
                baseValue += (item.CareerHistory.Where(ch => careerList.Contains(ch.Career)).Sum(ch => ch.Terms));
                baseValue += (item.CareerHistory.Where(ch => careerList.Contains(ch.Assignment)).Sum(ch => ch.Terms));
            }

            return baseValue;
        }

        {
            //No character's last career was the requested one. Choose the one who spend the most time in the desired career.
            var sortedList = characters.Select(c => new
            {
                Character = c,
                Suitability = Suitability(c, true)
            }).OrderByDescending(x => x.Suitability).ToList();

            return sortedList.First().Character;
        }
    }

    public Character CreateCharacterWithSkill(Dice dice, string targetSkillName, string? targetSkillSpeciality, int? targetSkillLevel = null, string? species = null)
    {
        targetSkillLevel ??= (int)Math.Floor(dice.D(2, 6) / 3.0);

        Character? lastBest = null;
        int lastBestSkillLevel = -3;

        for (var i = 0; i < 500 || lastBest == null; i++)
        {
            var character = Build(CreateCharacterStub(dice, species));
            if (!character.IsDead && !character.LongTermBenefits.Retired)
            {
                int currentSkill = character.Skills.EffectiveSkillLevel(targetSkillName, targetSkillSpeciality);
                if (currentSkill == targetSkillLevel)
                {
                    return character;
                }
                else if (currentSkill > lastBestSkillLevel || lastBest == null)
                {
                    lastBest = character;
                    lastBestSkillLevel = currentSkill;
                }
            }
        }

        if (lastBestSkillLevel < 0)
            lastBest.Skills.Add(targetSkillName, targetSkillSpeciality, 0);
        return lastBest!;
    }

    public Contact CreateContact(Dice dice, ContactType contactType, IContactGroup? character, string? species)
    {
        int PITable(int roll)
        {
            return roll switch
            {
                <= 5 => 0,
                <= 7 => 1,
                8 => 2,
                9 => 3,
                10 => 4,
                11 => 5,
                _ => 6,
            };
        }

        int AffinityTable(int roll)
        {
            return roll switch
            {
                2 => 0,
                <= 4 => 1,
                <= 6 => 2,
                <= 8 => 3,
                <= 10 => 4,
                <= 11 => 5,
                _ => 6,
            };
        }

        var result = new Contact(contactType, CreateCharacterStub(dice, species));
        RollAffinityEnmity(dice, result);
        result.Power = PITable(dice.D(2, 6));
        result.Influence = PITable(dice.D(2, 6));

        if (dice.D(2, 6) >= 8) //special!
        {
            var specialCount = 1;

            while (specialCount > 0)
            {
                specialCount -= 1;

                switch (dice.D66())
                {
                    case 11:
                        result.History.Add("Forgiveness.");
                        result.Affinity += 1;
                        break;

                    case 12:
                        result.History.Add("Relationship soured.");
                        result.Affinity -= 1;
                        result.Enmity += 1;
                        break;

                    case 13:
                        result.History.Add("Relationship altered.");
                        result.Affinity += 1;
                        result.Enmity -= 1;
                        break;

                    case 14:
                        result.History.Add("An incident occured.");
                        result.Enmity += 1;
                        break;

                    case 15:
                        switch (result.ContactType)
                        {
                            case ContactType.Enemy:
                                result.History.Add("Relationship becomes more moderate. Enemy becomes a rival.");
                                result.ContactType = ContactType.Rival;
                                break;

                            case ContactType.Ally:
                                result.History.Add("Relationship becomes more moderate. Ally becomes a contact.");
                                result.ContactType = ContactType.Contact;
                                break;

                            default:
                                result.History.Add("Relationship becomes more moderate.");
                                result.Enmity -= 1;
                                result.Affinity += 1;
                                break;
                        }
                        break;

                    case 16:
                        switch (result.ContactType)
                        {
                            case ContactType.Rival:
                                result.History.Add("Relationship becomes more moderate. Rival becomes an enemy.");
                                result.ContactType = ContactType.Enemy;
                                RollAffinityEnmity(dice, result);
                                break;

                            case ContactType.Contact:
                                result.History.Add("Relationship becomes more moderate. Contact becomes an ally.");
                                result.ContactType = ContactType.Ally;
                                RollAffinityEnmity(dice, result);
                                break;

                            case ContactType.Enemy:
                                result.History.Add("Relationship becomes more intense.");
                                result.Enmity += 1;
                                break;

                            case ContactType.Ally:
                                result.History.Add("Relationship becomes more intense.");
                                result.Affinity += 1;
                                break;
                        }
                        break;

                    case 21:
                        result.History.Add($"{result.CharacterStub.Name} gains in power.");
                        result.Power += 1;
                        break;

                    case 22:
                        result.History.Add($"{result.CharacterStub.Name} loses some of their power base.");
                        result.Power -= 1;
                        break;

                    case 23:
                        result.History.Add($"{result.CharacterStub.Name} gains in influence.");
                        result.Influence += 1;
                        break;

                    case 24:
                        result.History.Add($"{result.CharacterStub.Name}'s influence is diminished.");
                        result.Influence -= 1;
                        break;

                    case 25:
                        result.History.Add($"{result.CharacterStub.Name} gains in power and influence.");
                        result.Power += 1;
                        result.Influence += 1;
                        break;

                    case 26:
                        result.History.Add($"{result.CharacterStub.Name} is diminished in both power and influence.");
                        result.Power -= 1;
                        result.Influence -= 1; break;

                    case 31:
                        result.History.Add($"{result.CharacterStub.Name} belongs to an unusual cultural or religious group.");
                        break;

                    case 32:
                        result.History.Add($"{result.CharacterStub.Name} belongs to an uncommon alien species.");
                        break;

                    case 33:
                        result.History.Add($"{result.CharacterStub.Name} is particularly unusual, such as an artificial intelligence or very alien being.");
                        break;

                    case 34:
                        result.History.Add($"{result.CharacterStub.Name} is actually an organisation such as a political movement or modest sized business.");
                        break;

                    case 35:
                        result.History.Add($"{result.CharacterStub.Name} is a member of an organisation which holds a generally opposite view of the Traveller.");
                        break;

                    case 36:
                        result.History.Add($"{result.CharacterStub.Name} is a questionable figure such as a criminal, pirate or disgraced noble.");
                        break;

                    case 41:
                        result.History.Add("Very bad falling out.");
                        result.Enmity = Math.Max(result.Enmity, dice.D(2, 6));
                        break;

                    case 42:
                        result.History.Add("reconciliation.");
                        result.Affinity = Math.Max(result.Affinity, dice.D(2, 6));
                        break;

                    case 43:
                        result.History.Add($"{result.CharacterStub.Name} fell on hard times.");
                        result.Power -= 1;
                        break;

                    case 44:
                        result.History.Add($"{result.CharacterStub.Name} was ruined by misfortune caused by the character.");
                        result.Power = 0;
                        result.Enmity += 1;
                        break;

                    case 45:
                        result.History.Add($"{result.CharacterStub.Name} gained influence with the character’s assistance.");
                        result.Influence += 1;
                        result.Affinity += 1;
                        break;

                    case 46:
                        result.History.Add($"{result.CharacterStub.Name} gained power at the expense of a third party who now blames the character.");
                        result.Power += 1;
                        character?.AddEnemy();
                        break;

                    case 51:
                        result.History.Add($"{result.CharacterStub.Name} is missing under suspicious circumstances.");
                        break;

                    case 52:
                        result.History.Add($"{result.CharacterStub.Name} is out of contact doing something interesting but not suspicious.");
                        break;

                    case 53:
                        result.History.Add($"{result.CharacterStub.Name} is in desperate trouble and could use the character’s help.");
                        break;

                    case 54:
                        result.History.Add($"{result.CharacterStub.Name} has had an unexpected run of good fortune lately.");
                        break;

                    case 55:
                        result.History.Add($"{result.CharacterStub.Name} is in prison or otherwise trapped somewhere.");
                        break;

                    case 56:
                        result.History.Add($"{result.CharacterStub.Name} is found or reported dead.");
                        break;

                    case 61:
                        result.History.Add($"{result.CharacterStub.Name} has life-changing event that creates new responsibilities.");
                        break;

                    case 62:
                        result.History.Add($"{result.CharacterStub.Name} has negatively life-changing event.");
                        break;

                    case 63:
                        result.History.Add($"{result.CharacterStub.Name}’s relationships have begun to affect the character.");
                        if (result.Affinity > result.Enmity)
                            character?.AddContact();
                        else if (result.Affinity < result.Enmity)
                            character?.AddRival();
                        break;

                    case 64:

                        var temp = result.Affinity;
                        result.Affinity = result.Enmity;
                        result.Enmity = temp;

                        switch (result.ContactType)
                        {
                            case ContactType.Rival:
                                result.History.Add("Relationship redefined. Rival becomes a contact.");
                                result.ContactType = ContactType.Contact;
                                break;

                            case ContactType.Contact:
                                result.History.Add("Relationship redefined. Contact becomes an rival.");
                                result.ContactType = ContactType.Rival;
                                break;

                            case ContactType.Enemy:
                                result.History.Add("Relationship redefined. Enemy becomes an ally.");
                                result.ContactType = ContactType.Ally;
                                break;

                            case ContactType.Ally:
                                result.History.Add("Relationship redefined. Ally becomes an enemy.");
                                result.ContactType = ContactType.Enemy;
                                break;
                        }
                        break;

                    case 65:
                        specialCount += 2;
                        break;

                    case 66:
                        specialCount += 3;
                        break;
                }
            }
        }

        return result;

        void RollAffinityEnmity(Dice dice, Contact result)
        {
            switch (result.ContactType)
            {
                case ContactType.Ally:
                    result.Affinity = AffinityTable(dice.D(2, 6));
                    break;

                case ContactType.Enemy:
                    result.Enmity = AffinityTable(dice.D(2, 6));
                    break;

                case ContactType.Rival:
                    result.Affinity = AffinityTable(dice.D(1, 6) - 1);
                    result.Enmity = AffinityTable(dice.D(1, 6) + 1);
                    break;

                case ContactType.Contact:
                    result.Affinity = AffinityTable(dice.D(1, 6) + 1);
                    result.Enmity = AffinityTable(dice.D(1, 6) - 1);
                    break;
            }
        }
    }

    public List<string> GetAssignmentList(string? species, string career)
    {
        if (species == null)
            return m_CharacterBuilders.Values.SelectMany(x => x.Careers(null)).Where(x => x.Career == career && x.Assignment != null).Select(x => x.Assignment).Distinct().OrderBy(x => x).ToList()!;
        else
            return GetCharacterBuilder(species).Careers(null).Where(x => x.Career == career && x.Assignment != null).Select(x => x.Assignment).Distinct().OrderBy(x => x).ToList()!;
    }

    public SpeciesCharacterBuilder GetCharacterBuilder(string? species)
    {
        if (species != null && m_CharacterBuilders.TryGetValue(species, out var result))
            return result;
        else
            return m_CharacterBuilders["Humaniti"];
    }

    public string GetRandomSpecies(Dice dice)
    {
        return dice.Choose(SpeciesList);
    }

    public string GetRandomSpeciesForCareer(Dice dice, string? career = null, string? career2 = null)
    {
        var builders = new List<SpeciesCharacterBuilder>();
        foreach (var builder in m_CharacterBuilders.Values)
        {
            var valid = true;
            if (!career.IsNullOrEmpty() && !builder.Careers(null).Any(c => c.Career == career))
                valid = false;
            if (!career2.IsNullOrEmpty() && !builder.Careers(null).Any(c => c.Career == career2))
                valid = false;
            if (valid)
                builders.Add(builder);
        }

        if (builders.Count > 0)
            return dice.Choose(builders).Species;
        else
            return GetRandomSpecies(dice); //this shouldn't happen
    }

    public string? GetSpeciesUrl(string? species)
    {
        return GetCharacterBuilder(species)?.SpeciesUrl;
    }
}
