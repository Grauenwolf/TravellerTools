using Grauenwolf.TravellerTools.Characters.Careers;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters;

public class Book
{
    readonly CharacterTemplates m_Templates;

    internal Book(CharacterTemplates templates)
    {
        m_Templates = templates;

        var skills = new List<SkillTemplate>();
        var allSkills = new List<SkillTemplate>();
        foreach (var skill in m_Templates.Skills)
        {
            if (skill.Name == "Jack-of-All-Trades")
                continue;

            allSkills.Add(new SkillTemplate(skill.Name));

            if (skill.Specialty?.Length > 0)
                foreach (var specialty in skill.Specialty)
                {
                    var template = new SkillTemplate(skill.Name, specialty.Name, specialty.Group);
                    skills.Add(template);
                    allSkills.Add(template);
                }
            else
                skills.Add(new SkillTemplate(skill.Name));
        }
        RandomSkills = ImmutableArray.CreateRange(skills);
        AllSkills = ImmutableArray.CreateRange(allSkills);

        PsionicTalents = ImmutableArray.Create(
            new PsionicSkillTemplate("Telepathy", 4),
            new PsionicSkillTemplate("Clairvoyance", 3),
            new PsionicSkillTemplate("Telekinesis", 2),
            new PsionicSkillTemplate("Awareness", 1),
            new PsionicSkillTemplate("Teleportation", 0)
            );
    }

    /// <summary>
    /// Gets all of the skills, including unspecialized skills. For example, both "Art" and "Art (Performer)" will be included.
    /// </summary>
    /// <value>All skills.</value>
    public ImmutableArray<SkillTemplate> AllSkills { get; }

    public ImmutableArray<PsionicSkillTemplate> PsionicTalents { get; }

    /// <summary>
    /// Gets the list of random skills. Skills needing specialization will be excluded. For example, "Art (Performer)" will be included but just "Art" will not.
    /// </summary>
    /// <value>The random skills.</value>
    public ImmutableArray<SkillTemplate> RandomSkills { get; }

    public static void Injury(Character character, Dice dice, CareerBase career, bool severe, int age)
    {
        //TODO: Add medical bills support. Page 47

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

    public static int OddsOfSuccess(Character character, int dm, int target)
    {
        return OddsOfSuccess(target - dm);
    }

    //public static int OddsOfSuccess(Character character, string attributeName, int target)

    //{
    //    var dm = character.GetDM(attributeName);
    //    return OddsOfSuccess(target - dm);
    //}

    public static int OddsOfSuccess(int target)
    {
        if (target <= 2) return 100;
        if (target == 3) return 97;
        if (target == 4) return 92;
        if (target == 5) return 83;
        if (target == 6) return 72;
        if (target == 7) return 58;
        if (target == 8) return 42;
        if (target == 9) return 28;
        if (target == 10) return 17;
        if (target == 11) return 8;
        if (target == 12) return 3;
        return 0;
    }

    public static string RollDraft(Dice dice)
    {
        //TODO: Move this to the character builder.
        switch (dice.D(6))
        {
            case 1: return "Navy";
            case 2: return "Army";
            case 3: return "Marine";
            case 4: return "Merchant Marine";
            case 5: return "Scout";
            case 6: return "Law Enforcement";
        }
        return null!;
    }

    public void LifeEvent(Character character, Dice dice, CareerBase career)
    {
        //TODO: Move this to the character builder.
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

    public void PreCareerEvents(Character character, Dice dice, CareerBase career, params string[] skills)
    {
        //TODO: Move this to the character builder.
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
                character.AddHistory("A prank goes horribly wrong.", dice);
                var roll = dice.D(2, 6) + character.SocialStandingDM;

                if (roll >= 8)
                    character.AddHistory("Gain a Rival.", dice);
                else if (roll > 2)
                    character.AddHistory("Gain an Enemy.", dice);
                else
                    character.NextTermBenefits.MustEnroll = "Prisoner";
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
                    var skillList = new SkillTemplateCollection(RandomSkills);
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
                    var skill = dice.Choose(skills);
                    character.Skills.Increase(skill, 1);
                    character.AddHistory($"Expand the field of {skill}, but gain a Rival in your former tutor.", dice);
                }
                return;

            case 11:
                character.AddHistory("War breaks out, triggering a mandatory draft.", dice);
                if (dice.RollHigh(character.SocialStandingDM, 9))
                    character.AddHistory("Used social standing to avoid the draft.", dice);
                else
                {
                    character.CurrentTermBenefits.GraduationDM -= 100;
                    if (dice.NextBoolean())
                    {
                        character.AddHistory("Fled from the draft.", dice);
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
                return;

            case 12:
                character.AddHistory("Widely recognized.", dice);
                character.SocialStanding += 1;
                return;
        }
    }

    public void TestPsionic(Character character, Dice dice, int age)
    {
        if (character.Psi.HasValue)
            return; //already tested

        character.LongTermBenefits.MayTestPsi = false;
        character.AddHistory("Tested for psionic powers", age);

        character.Psi = dice.D(2, 6) - character.CurrentTerm;

        if (character.Psi <= 0)
        {
            character.Psi = 0;
            return;
        }

        var availableSkills = new PsionicSkillTemplateCollection(PsionicTalents);

        character.PreviousPsiAttempts = 0;

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
                }
            }

            character.PreviousPsiAttempts += 1;
        }
    }

    public void UnusualLifeEvent(Character character, Dice dice)
    {
        //TODO: Move this to the character builder.
        switch (dice.D(6))
        {
            case 1:
                var age = character.AddHistory("Encounter a Psionic institute.", dice);
                TestPsionic(character, dice, age);
                return;

            case 2:
                character.AddHistory("Spend time with an alien race. Gain a contact.", dice);
                var skillList = new SkillTemplateCollection(SpecialtiesFor("Science"));
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

    internal List<SkillTemplate> SpecialtiesFor(string skillName)
    {
        var skill = m_Templates.Skills.FirstOrDefault(s => s.Name == skillName);
        if (skill != null && skill.Specialty != null)
            return skill.Specialty.Select(s => new SkillTemplate(skillName, s.Name)).ToList();
        else
            return new List<SkillTemplate>() { new SkillTemplate(skillName) };
    }
}
