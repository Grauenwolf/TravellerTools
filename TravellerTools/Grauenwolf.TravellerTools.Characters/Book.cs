using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters
{
    class Book
    {
        internal Book(CharacterTemplates templates)
        {
            m_Templates = templates;


            var skills = new List<SkillTemplate>();
            foreach (var skill in m_Templates.Skills)
            {
                if (skill.Name == "Jack-of-All-Trades")
                    continue;

                if (skill.Specialty?.Length > 0)
                    foreach (var specialty in skill.Specialty)
                        skills.Add(new SkillTemplate(skill.Name, specialty.Name));
                else
                    skills.Add(new SkillTemplate(skill.Name));

            }
            m_RandomSkills = ImmutableArray.CreateRange(skills);
        }


        ImmutableArray<SkillTemplate> m_RandomSkills { get; }
        CharacterTemplates m_Templates { get; }

        internal List<SkillTemplate> SpecialtiesFor(string skillName)
        {
            var skill = m_Templates.Skills.FirstOrDefault(s => s.Name == skillName);
            if (skill != null && skill.Specialty != null)
                return skill.Specialty.Select(s => new SkillTemplate(skillName, s.Name)).ToList();
            else
                return new List<SkillTemplate>() { new SkillTemplate(skillName) };
        }

        public static int OddsOfSuccess(Character character, string attributeName, int target)

        {
            var dm = character.GetDM(attributeName);
            return OddsOfSuccess(target - dm);
        }

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

        public void UnusualLifeEvent(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.AddHistory("Encounter a Psionic institute.");
                    TestPsionic(character, dice);
                    return;
                case 2:
                    character.AddHistory("Spend time with an alien race. Gain a contact.");
                    var skillList = new SkillTemplateCollection(SpecialtiesFor("Science"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                    return;
                case 3:
                    character.AddHistory("Find an Alien Artifact.");
                    return;
                case 4:
                    character.AddHistory("Amnesia.");
                    return;
                case 5:
                    character.AddHistory("Contact with Government.");
                    return;
                case 6:
                    character.AddHistory("Find Ancient Technology.");
                    return;
            }

        }

        public void LifeEvent(Character character, Dice dice)
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    Injury(character, dice);
                    return;
                case 3:
                    character.AddHistory("Birth or Death involving a family member or close friend.");
                    return;
                case 4:
                    character.AddHistory("A romantic relationship ends badly. Gain a Rival or Enemy.");
                    return;
                case 5:
                    character.AddHistory("A romantic relationship deepens, possibly leading to marriage. Gain an Ally.");
                    return;
                case 6:
                    character.AddHistory("A new romantic starts. Gain an Ally.");
                    return;
                case 7:
                    character.AddHistory("Gained a contact.");
                    return;
                case 8:
                    character.AddHistory("Betrayal. Convert an Ally into a Rival or Enemy.");
                    return;
                case 9:
                    character.AddHistory("Moved to a new world.");
                    character.NextTermBenefits.QualificationDM += 1;
                    return;
                case 10:
                    character.AddHistory("Good fortune");
                    character.BenefitRollDMs.Add(2);
                    return;
                case 11:
                    if (dice.D(2) == 1)
                    {
                        character.BenefitRolls -= 1;
                        character.AddHistory("Victim of a crime");
                    }
                    else
                    {
                        character.AddHistory("Accused of a crime");
                        character.NextTermBenefits.MustEnroll = "Prisoner";
                    }
                    return;
                case 12:
                    UnusualLifeEvent(character, dice);
                    return;
            }
        }

        public void PreCareerEvents(Character character, Dice dice, params SkillTemplate[] skills)
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    character.AddHistory("Contacted by an underground psionic group");
                    character.LongTermBenefits.MayTestPsi = true;
                    return;
                case 3:
                    character.AddHistory("Suffered a deep tragedy.");
                    character.CurrentTermBenefits.GraduationDM = -100;
                    return;
                case 4:
                    character.AddHistory("A prank goes horribly wrong.");
                    var roll = dice.D(2, 6) + character.SocialStandingDM;

                    if (roll >= 8)
                        character.AddHistory("Gain a Rival.");
                    else if (roll > 2)
                        character.AddHistory("Gain an Enemy.");
                    else
                        character.NextTermBenefits.MustEnroll = "Prisoner";
                    return;
                case 5:
                    character.AddHistory("Spent the college years partying.");
                    character.Skills.Add("Carouse", 1);
                    return;
                case 6:
                    character.AddHistory($"Made lifelong friends. Gain {dice.D(3)} Allies.");
                    return;
                case 7:
                    LifeEvent(character, dice);
                    return;
                case 8:
                    if (dice.RollHigh(character.SocialStandingDM, 8))
                    {
                        character.AddHistory("Become leader in social movement.");
                        character.AddHistory("Gain an Ally and an Enemy.");
                    }
                    else
                        character.AddHistory("Join a social movement.");
                    return;
                case 9:
                    {
                        var skillList = new SkillTemplateCollection(m_RandomSkills);
                        skillList.RemoveOverlap(character.Skills, 0);

                        if (skillList.Count > 0)
                        {
                            var skill = dice.Choose(skillList);
                            character.Skills.Add(skill);
                            character.AddHistory($"Study {skill} as a hobby.");
                        }
                    }
                    return;
                case 10:

                    if (dice.RollHigh(9))
                    {
                        var skill = dice.Choose(skills);
                        character.Skills.Increase(skill, 1);
                        character.AddHistory($"Expand the field of {skill}, but gain a Rival in your former tutor.");
                    }
                    return;
                case 11:
                    character.AddHistory("War breaks out, triggering a mandatory draft.");
                    if (dice.RollHigh(character.SocialStandingDM, 9))
                        character.AddHistory("Used social standing to avoid the draft.");
                    else
                    {
                        character.CurrentTermBenefits.GraduationDM -= 100;
                        if (dice.D(2) == 1)
                        {
                            character.AddHistory("Fled from the draft.");
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
                    character.AddHistory("Widely recognized.");
                    character.SocialStanding += 1;
                    return;
            }
        }

        public ImmutableArray<SkillTemplate> RandomSkills
        {
            get { return m_RandomSkills; }
        }

        public static void Injury(Character character, Dice dice, bool severe = false)
        {
            //TODO: Add medical bills support. Page 47

            var roll = dice.D(6);
            if (severe)
                roll = Math.Min(roll, dice.D(6));

            switch (roll)
            {
                case 1:
                    character.AddHistory("Nearly killed");
                    switch (dice.D(3))
                    {
                        case 1:
                            character.Strength -= dice.D(6);
                            character.Dexterity -= 2;
                            character.Endurance -= 2;
                            return;
                        case 2:
                            character.Strength -= 2;
                            character.Dexterity -= dice.D(6);
                            character.Endurance -= 2;
                            return;
                        case 3:
                            character.Strength -= 2;
                            character.Dexterity -= 2;
                            character.Endurance -= dice.D(6);
                            return;
                    }
                    return;
                case 2:
                    character.AddHistory("Severely injured");
                    switch (dice.D(3))
                    {
                        case 1:
                            character.Strength -= dice.D(6);
                            return;
                        case 2:
                            character.Dexterity -= dice.D(6);
                            return;
                        case 3:
                            character.Endurance -= dice.D(6);
                            return;
                    }
                    return;
                case 3:
                    character.AddHistory("Lost eye or limb");
                    switch (dice.D(2))
                    {
                        case 1:
                            character.Strength -= 2;
                            return;
                        case 2:
                            character.Dexterity -= 2;
                            return;
                    }
                    return;
                case 4:
                    character.AddHistory("Scarred");
                    switch (dice.D(3))
                    {
                        case 1:
                            character.Strength -= 2;
                            return;
                        case 2:
                            character.Dexterity -= 2;
                            return;
                        case 3:
                            character.Endurance -= 2;
                            return;
                    }
                    return;
                case 5:
                    character.AddHistory("Injured");
                    switch (dice.D(3))
                    {
                        case 1:
                            character.Strength -= 1;
                            return;
                        case 2:
                            character.Dexterity -= 1;
                            return;
                        case 3:
                            character.Endurance -= 1;
                            return;
                    }
                    return;

                case 6:
                    character.AddHistory("Lightly injured, no permanent damage,");
                    return;
            }
        }

        public static string RollDraft(Dice dice)
        {
            switch (dice.D(6))
            {
                case 1: return "Navy";
                case 2: return "Army";
                case 3: return "Marine";
                case 4: return "Merchant Marine";
                case 5: return "Scout";
                case 6: return "Law Enforcement";
            }
            return null;
        }

        static void TestPsionic(Character character, Dice dice)
        {
            //TODO
        }
    }
}
