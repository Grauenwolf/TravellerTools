namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class MilitaryOfficer(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Military Officer", assignment, speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Military;
    public override string? Source => "Aliens of Charted Space 1, page 30";
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Tactics", "Drive", "Gun Combat", "Melee", "Leadership", "Tolerance");
    }

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                MishapRollAge(character, dice);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                character.AddHistory($"Fight a brutal ground war.", dice);
                IncreaseOneSkill(character, dice, "Stealth", "Heavy Weapons", "Vacc Suit", "Drive");

                return;

            case 4:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Persuade", "Natural"), 8))
                {
                    character.AddHistory($"Disciplined a disobedient junior officer. Gain an Ally.", dice);
                    character.AddAlly();
                }
                else
                {
                    character.AddHistory($"Disciplined a disobedient junior officer. Gain an Rival.", dice);
                    character.AddRival();
                    character.SocialStanding += -1;
                }

                return;

            case 5:
                character.AddHistory($"Garrison one of the richest Aslan worlds.", dice);
                IncreaseOneSkill(character, dice, "Carouse", "Streetwise", "Independence", "Survival");
                return;

            case 6:
                character.AddHistory($"{character.Name} is given advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    character.Skills.Increase(dice.Choose(RandomSkills(character)));
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory("Opportunity to establish a landhold in your name.", dice);
                character.Territory += 2;
                return;

            case 9:
                if (dice.NextBoolean())
                {
                    character.AddHistory("Captured an enemy commander and ransomed for land.", dice);
                    character.Territory += 2;
                }
                else
                {
                    character.AddHistory("Freed a captured an enemy commander. Gain an Ally.", dice);
                    character.AddAlly();
                }

                return;

            case 10:
                if (dice.NextBoolean())
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Melee"), 8))
                    {
                        character.AddHistory($"Win a duel with a rival.", dice);
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    else
                        character.BenefitRolls += -1;
                }
                else
                {
                    character.AddHistory($"Refuse to duel with a rival.", dice);
                    character.SocialStanding += -dice.D(6);
                }
                return;

            case 11:
                character.AddHistory($"Your deeds are legend among the cubs of your clan.", dice);
                if (dice.NextBoolean())
                    character.Skills.Increase("Tactics|Military");
                else
                    character.CurrentTermBenefits.AdvancementDM += 4;
                return;

            case 12:
                character.AddHistory($"Efforts strike a great blow for the clan.", dice);
                character.CurrentTermBenefits.AdvancementDM += 99;
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"Failing on your part causes a catastrophic loss for your clan. Become a Yohai (outcast).", age);
                character.IsOutcast = true;
                character.SocialStanding = 2;
                return;

            case 3:
                character.AddHistory($"Shift in clan politics ruins your career", age);
                character.SocialStanding += -2;
                return;

            case 4:
                character.AddHistory($"Defeated in battle. Gain the foe who defeated you as a Rival.", age);
                character.AddRival();
                return;

            case 5:
                character.AddHistory($"Captured and ransomed back to the clan, ending career in disgrace.Gain a Contact in the \r\nrival clan.", age);
                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Tactics|Military", "Drive", "Gun Combat", "Melee|Natural", "Leadership", "Tolerance");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Navigation", "Electronics", "Melee", "Science", "Engineer", "Diplomat");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 10, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        if (character.Gender == "M")
            Increase(character, dice, "Independence", "Strength", "Dexterity", "Endurance", "Intellect", "Jack-of-All-Trades");
        else
            Increase(character, dice, "Admin", "Strength", "Dexterity", "Endurance", "Intellect", "Jack-of-All-Trades");
    }
}
