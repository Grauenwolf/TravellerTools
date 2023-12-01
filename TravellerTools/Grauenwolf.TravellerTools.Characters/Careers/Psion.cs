namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class Psion : NormalCareer
{
    protected Psion(string assignment, Book book) : base("Psion", assignment, book)
    {
    }

    protected override int AdvancedEductionMin => 8;

    protected override bool RankCarryover => false;

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                MishapRollAge(character, dice);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                character.AddHistory("Psionic abilities make you uncomfortable to be around. One contact or ally becomes a rival.", dice);
                //TODO: Change contact type
                return;

            case 4:
                character.AddHistory("Spent time mastering mind and body.", dice);
                {
                    var skills = new SkillTemplateCollection();
                    skills.Add("Stealth");
                    skills.Add("Survival");
                    skills.AddRange(SpecialtiesFor("Athletics"));
                    skills.AddRange(SpecialtiesFor("Art"));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 5:
                if (dice.NextBoolean())
                {
                    character.AddHistory("Refuse to misuse powers for personal gain.", dice);
                    return;
                }
                else
                {
                    if (dice.RollHigh(character.PsiDM, 8))
                    {
                        character.AddHistory("Use powers for personal gain.", dice);
                        if (dice.NextBoolean())
                            character.BenefitRolls += 1;
                        else
                            character.SocialStanding += 1;
                    }
                    else
                    {
                        character.AddHistory("Attempt to use powers for personal gain, but it backfires.", dice);
                        character.SocialStanding -= 1;
                    }
                    return;
                }

            case 6:
                character.AddHistory("Gain a contact outside of normal circles.", dice);
                character.AddContact();
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory("Psionic strength increases.", dice);
                character.Psi += 1;
                return;

            case 9:
                character.AddHistory("Advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                {
                    var skills = new SkillTemplateCollection(Book.RandomSkills);
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 10:
                character.AddHistory("Pick up potentially useful information using your psychic powers/", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 11:
                character.AddHistory("Gain a mentor/", dice);
                character.CurrentTermBenefits.AdvancementDM += 4;
                return;

            case 12:
                character.AddHistory("Achieve a new level of discipline in your powers/", dice);
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory("Telepathically contact something dangerous. Suffer from persistent and terrifying nightmares.", age);
                character.Psi -= 1;
                return;

            case 3:
                character.AddHistory("An anti-psi cult or gang attempts to expose or attack you.", age);
                switch (dice.D(3))
                {
                    case 1:
                        Injury(character, dice, age);
                        return;

                    case 2:
                        character.SocialStanding -= 1;
                        return;

                    case 3:
                        //muster out only
                        return;
                }
                return;

            case 4:

                if (dice.NextBoolean())
                {
                    character.AddHistory("Use your psionic powers in an unethical fashion. Gain an enemy.", age);
                    character.AddEnemy();
                    character.NextTermBenefits.MusterOut = false;
                }
                else
                {
                    character.AddHistory("Refused to use your psionic powers in an unethical fashion.", age);
                }
                return;

            case 5:
                character.AddHistory("Experimented on by a corporation, government, or other organisation", age);
                return;

            case 6:
                character.AddHistory("Gift causes a former ally to turn on you and betray you. One Ally or Contact becomes an Enemy.", age);
                //TODO: Change contact type
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice)
    {
        if ((character.Psi ?? 0) <= 0)
            return false; //not possible

        var dm = character.PsiDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);

        return dice.RollHigh(dm, 6);
    }

    internal override void Run(Character character, Dice dice)
    {
        //Force Psionic Testing. This can only happen if the character is forced into this career.
        if (character.Psi == null)
            Book.TestPsionic(character, dice, character.Age);

        base.Run(character, dice);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                IncreaseTalent(character, dice, "Telepathy");
                return;

            case 2:
                IncreaseTalent(character, dice, "Clairvoyance");
                return;

            case 3:
                IncreaseTalent(character, dice, "Telekinesis");
                return;

            case 4:
                IncreaseTalent(character, dice, "Awareness");
                return;

            case 5:
                IncreaseTalent(character, dice, "Teleportation");
                return;

            case 6:
                {
                    var skills = new SkillTemplateCollection(Book.PsionicTalents.Where(st => character.Skills.Contains(st.Name)));
                    if (skills.Count > 0)
                        character.Skills.Increase(dice.Choose(skills));
                    else //no psionic talents? that is very unlikely
                        IncreaseTalent(character, dice, dice.Choose(Book.PsionicTalents).Name);
                }
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 4:
                character.Skills.Increase("Medic");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;

            case 6:
                character.Skills.Increase("Mechanic");
                return;
        }
    }

    protected void AttemptTalent(Character character, Dice dice, string name)
    {
        if (character.Skills.Contains(name))
            return;

        var nextSkill = Book.PsionicTalents.Single(s => s.Name == name);
        if ((dice.D(2, 6) + nextSkill.LearningDM + character.PsiDM - character.PreviousPsiAttempts) >= 8)
        {
            character.AddHistory("Learned " + name, character.Age);
            character.Skills.Add(nextSkill);
        }
        character.PreviousPsiAttempts += 1;
    }

    protected void IncreaseTalent(Character character, Dice dice, string name)
    {
        if (character.Skills.Contains(name))
            character.Skills.Increase(name);
        else
            AttemptTalent(character, dice, name);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Education += 1;
                return;

            case 2:
                character.Intellect += 1;
                return;

            case 3:
                character.Strength += 1;
                return;

            case 4:
                character.Dexterity += 1;
                return;

            case 5:
                character.Endurance += 1;
                return;

            case 6:
                character.Psi += 1;
                return;
        }
    }
}
