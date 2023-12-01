using Grauenwolf.TravellerTools.Characters.Careers;
using Grauenwolf.TravellerTools.Names;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Characters;

public class CharacterBuilder
{
    static readonly ImmutableList<string> s_BackgroundSkills = ImmutableList.Create("Admin", "Animals", "Art", "Athletics", "Carouse", "Drive", "Science", "Seafarer", "Streetwise", "Survival", "Vacc Suit", "Electronics", "Flyer", "Language", "Mechanic", "Medic", "Profession");

    NameGenerator m_NameGenerator;
    ImmutableArray<string> m_Personalities;

    public CharacterBuilder(string dataPath, NameGenerator nameGenerator)
    {
        m_NameGenerator = nameGenerator;
        var file = new FileInfo(Path.Combine(dataPath, "CharacterBuilder.xml"));

        var converter = new XmlSerializer(typeof(CharacterTemplates));

        using (var stream = file.OpenRead())
            Book = new Book((CharacterTemplates)converter.Deserialize(stream)!);

        var careers = new List<CareerBase>
        {
            new ArmyAcademy(Book),
            new MarineAcademy(Book),
            new NavalAcademy(Book),
            new ArmySupport(Book),
            new Administrator(Book),
            new Artist(Book),
            new Barbarian(Book),
            new Broker(Book),
            new Cavalry(Book),
            new Colonist(Book),
            new Corporate(Book),
            new CorporateAgent(Book),
            new Enforcer(Book),
            new Fixer(Book),
            new FreeTrader(Book),
            new GroundAssault(Book),
            new Infantry(Book),
            new Inmate(Book),
            new Intelligence(Book),
            new Journalist(Book),
            new LawEnforcement(Book),
            new MarineSupport(Book),
            new MerchantMarine(Book),
            new Performer(Book),
            new Pirate(Book),
            new Retired(Book),
            new Scavenger(Book),
            new StarMarine(Book),
            new Thief(Book),
            new Thug(Book),
            new University(Book),
            new ColonialUpbringing(Book),
            new Wanderer(Book),
            new Worker(Book),
            new Flight(Book),
            new EngineerGunner(Book),
            new LineCrew(Book),
            new Dilettante(Book),
            new Diplomat(Book),
            new FieldResearcher(Book),
            new Scientist(Book),
            new Physician(Book),
            new Courier(Book),
            new Surveyor(Book),
            new Explorer(Book),
            new WildTalent(Book),
            new Adept(Book),
            new PsiWarrrior(Book)
        };

        Careers = careers.ToImmutableArray();

        var personalityFile = new FileInfo(Path.Combine(dataPath, "personality.txt"));
        m_Personalities = File.ReadAllLines(personalityFile.FullName).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToImmutableArray();
    }

    //    PITable(dice.D(2, 6)

    //PITable(dice.D(2, 6)

    //RollAffinityEnmity(dice, result);

    //    RollAffinityEnmity(dice, result);

    //    RollAffinityEnmity(dice, result);

    public Book Book { get; }
    public ImmutableArray<CareerBase> Careers { get; }

    public Character Build(CharacterBuilderOptions options)
    {
        var seed = options.Seed ?? (new Random()).Next();
        var dice = new Dice(seed);
        var character = new Character();

        character.Seed = seed;
        character.FirstAssignment = options.FirstAssignment;
        character.FirstCareer = options.FirstCareer;
        character.Name = options.Name;
        character.Gender = options.Gender;
        character.MaxAge = options.MaxAge;

        character.Strength = dice.D(2, 6);
        character.Dexterity = dice.D(2, 6);
        character.Endurance = dice.D(2, 6);
        character.Intellect = dice.D(2, 6);
        character.Education = dice.D(2, 6);
        character.SocialStanding = dice.D(2, 6);

        if (character.EducationDM + 3 > 0)
        {
            var backgroundSKills = dice.Choose(s_BackgroundSkills, character.EducationDM + 3, allowDuplicates: false);
            foreach (var skill in backgroundSKills)
                character.Skills.Add(skill); //all skills added at level 0
        }

        character.CurrentTerm = 1;

        if (!string.IsNullOrEmpty(options.FirstAssignment))
            character.NextTermBenefits.MustEnroll = options.FirstAssignment;
        else if (!string.IsNullOrEmpty(options.FirstCareer))
            character.NextTermBenefits.MustEnroll = options.FirstCareer;

        while (!IsDone(options, character))
        {
            var nextCareer = PickNextCareer(character, dice);
            character.CurrentTermBenefits = character.NextTermBenefits;
            character.NextTermBenefits = new NextTermBenefits();
            nextCareer.Run(character, dice);

            if (character.LongTermBenefits.MayTestPsi && dice.RollHigh(10))
                Book.TestPsionic(character, dice, character.Age);

            character.CurrentTerm += 1;

            if (character.CurrentTerm >= 4)
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

        character.Skills.Collapse();

        BuildContacts(dice, character);

        return character;
    }

    public Contact CreateContact(Dice dice, ContactType contactType, Character? character)
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

        var user = m_NameGenerator.CreateRandomPerson(dice);

        var options = new CharacterBuilderOptions() { MaxAge = 22 + dice.D(1, 50), Gender = user.Gender, Name = $"{user.FirstName} {user.LastName}", Seed = dice.Next() };

        var result = new Contact(contactType, options);
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
                        result.History.Add($"{result.CharacterStub.Name} is particularly unusual, such as an artificial intelligence or very alien being. ");
                        break;

                    case 34:
                        result.History.Add("This individual is actually an organisation such as a political movement or modest sized business.");
                        break;

                    case 35:
                        result.History.Add("This individual is a member of an organisation which holds a generally opposite view of the Traveller.");
                        break;

                    case 36:
                        result.History.Add("This individual is a questionable figure such as a criminal, pirate or disgraced noble.");
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
                        result.History.Add("This individual fell on hard times.");
                        result.Power -= 1;
                        break;

                    case 44:
                        result.History.Add("This individual was ruined by misfortune caused by the character.");
                        result.Power = 0;
                        result.Enmity += 1;
                        break;

                    case 45:
                        result.History.Add("This individual gained influence with the character’s assistance.");
                        result.Influence += 1;
                        result.Affinity += 1;
                        break;

                    case 46:
                        result.History.Add("This individual gained power at the expense of a third party who now blames the character.");
                        result.Power += 1;
                        character?.AddEnemy();
                        break;

                    case 51:
                        result.History.Add("This individual is missing under suspicious circumstances.");
                        break;

                    case 52:
                        result.History.Add("This individual is out of contact doing something interesting but not suspicious.");
                        break;

                    case 53:
                        result.History.Add("This individual is in desperate trouble and could use the character’s help.");
                        break;

                    case 54:
                        result.History.Add("This individual has had an unexpected run of good fortune lately.");
                        break;

                    case 55:
                        result.History.Add("This individual is in prison or otherwise trapped somewhere.");
                        break;

                    case 56:
                        result.History.Add("This individual is found or reported dead.");
                        break;

                    case 61:
                        result.History.Add("This individual has life-changing event that creates new responsibilities.");
                        break;

                    case 62:
                        result.History.Add("This individual has negatively life-changing event.");
                        break;

                    case 63:
                        result.History.Add("This individual’s relationships have begun to affect the character.");
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

    static void AgingRoll(Character character, Dice dice)
    {
        //TODO: Anagathics page 47

        var roll = dice.D(2, 6) - character.CurrentTerm;
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

    void BuildContacts(Dice dice, Character character)
    {
        while (character.UnusedContacts.Count > 0)
            character.Contacts.Add(CreateContact(dice, character.UnusedContacts.Dequeue(), character));
    }

    CareerBase PickNextCareer(Character character, Dice dice)
    {
        var careers = new List<CareerBase>();

        //Forced picks (e.g. Draft)
        if (character.NextTermBenefits?.MustEnroll != null)
        {
            foreach (var career in Careers)
            {
                if (string.Equals(character.NextTermBenefits.MustEnroll, career.Career, StringComparison.OrdinalIgnoreCase) || string.Equals(character.NextTermBenefits.MustEnroll, career.Assignment, StringComparison.OrdinalIgnoreCase))
                {
                    careers.Add(career);
                }
            }
        }

        if (!character.NextTermBenefits.MusterOut && careers.Count == 0 && character.LastCareer != null)
        {
            if (dice.D(10) > 1) //continue career
            {
                foreach (var career in Careers)
                {
                    if (character.LastCareer.Career == career.Career && character.LastCareer.Assignment == career.Assignment)
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
                if (character.NextTermBenefits.MusterOut && character.LastCareer.Career == career.Career && character.LastCareer.Assignment == career.Assignment)
                    continue;

                if (career.Qualify(character, dice))
                {
                    careers.Add(career);
                    character.Trace.Add("Qualified for " + career.Career + " at age " + character.Age);
                }
            }
        }

        var result = dice.Choose(careers);
        character.Trace.Add("Selected " + result.Career + " at age " + character.Age);
        return result;
    }
}
