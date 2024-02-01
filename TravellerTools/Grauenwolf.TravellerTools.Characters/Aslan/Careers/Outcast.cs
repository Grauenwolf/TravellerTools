namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Outcast(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Outcast", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Aliens of Charted Space 1, page 38";

    protected override int AdvancedEductionMin => int.MaxValue;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Profession", "Streetwise", "Mechanic", "Melee", "Deception", "Survival");
    }

    internal override void Event(Character character, Dice dice)
    {
        try
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    MishapRollAge(character, dice);
                    character.NextTermBenefits.MusterOut = false;
                    return;

                case 3:
                    character.AddHistory("Serve a landowner, but owe a great debt.", dice);
                    character.NextTermBenefits.QualificationDM += 4;

                    return;

                case 4:

                    character.AddHistory($"Pick up a few handy skills", dice);
                    character.Skills.Increase("Jack-of-All-Trades");

                    return;

                case 5:
                    character.AddHistory($"Find working passage on a starship", dice);
                    var skill = IncreaseOneSkill(character, dice, "Mechanic", "Vacc Suit", "Engineer", "Tolerance");
                    return;

                case 6:
                    character.AddHistory("Survive on the edge.Gain a Contact.", dice);
                    character.AddContact();
                    return;

                case 7:
                    LifeEvent(character, dice);
                    return;

                case 8:
                    if (dice.NextBoolean())
                    {
                        if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Melee"), 10))
                        {
                            character.AddHistory($"Fought off thieves.", dice);
                            character.BenefitRolls += 1;
                        }
                        else
                        {
                            character.AddHistory($"Beaten in a fight with thieves.", dice);
                            character.BenefitRolls += -1;
                        }
                    }
                    else
                    {
                        if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Stealth"), 8))
                        {
                            character.AddHistory($"Fled from thieves.", dice);
                            character.BenefitRolls += 1;
                        }
                        else
                        {
                            character.AddHistory($"Caught fleeing from thieves.", dice);
                            character.BenefitRolls += -1;
                        }
                    }
                    return;

                case 9:
                    if (dice.NextBoolean())
                    {
                        character.AddHistory("Join an ihatei heading for frontier worlds. Gain an Ally.", dice);
                        character.NextTermBenefits.MustAttemptCareerGroup = CareerGroup.ImperiumCareer;
                        character.AddAlly();
                    }
                    else
                    {
                        character.AddHistory("Refused to join an ihatei heading for frontier worlds.", dice);
                    }
                    return;

                case 10:
                    if (dice.NextBoolean())
                    {
                        if (dice.NextBoolean())
                        {
                            character.AddHistory("Joined an outlaw band.", dice);
                            character.NextTermBenefits.MustEnroll = "Outlaw";
                        }
                        else
                        {
                            character.AddHistory("Joined a wanderer ship.", dice);
                            character.NextTermBenefits.MustEnroll = "Wanderer";
                        }
                    }
                    else
                    {
                        if (dice.NextBoolean())
                            character.AddHistory("Refused to join an outlaw band.", dice);
                        else
                            character.AddHistory("Refused to join a wanderer ship.", dice);
                    }
                    return;

                case 11:
                    character.AddHistory($"Offered a chance at redemption, but owes a great debt to a clan elder.", dice);
                    character.IsOutcast = false;
                    character.NextTermBenefits.MusterOut = true;
                    character.SocialStanding = 2 + dice.D(6);
                    return;

                case 12:
                    character.CurrentTermBenefits.AdvancementDM += 99;
                    return;
            }
        }
        finally
        {
            character.NextTermBenefits.MusterOut = false;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        try
        {
            switch (dice.D(6))
            {
                case 1:
                    SevereInjury(character, dice, age);
                    return;

                case 2:
                    if (character.RemoveContact(ContactType.Contact))
                        character.AddHistory($"Friends desert you. Lose a Contact.", age);
                    else if (character.RemoveContact(ContactType.Ally))
                        character.AddHistory($"Friends desert you. Lose an Ally.", age);
                    else
                    {
                        character.AddHistory($"Friends desert you.", age);
                        character.BenefitRolls = 0;
                    }
                    return;

                case 3:
                    character.AddHistory($"Attacked by a band of young Aslan thugs. Gain an Enemy.", age);
                    character.AddEnemy();
                    return;

                case 4:
                    character.AddHistory($"Euffer a life-threatening disease.", age);
                    character.Endurance += -1;
                    return;

                case 5:
                    character.AddHistory($"Hunted after stealing from a noble lord.", age);
                    character.BenefitRolls += -1;
                    return;

                case 6:
                    Injury(character, dice, age);
                    return;
            }
        }
        finally
        {
            character.NextTermBenefits.MusterOut = false;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        IncreaseOneSkill(character, dice, "Profession", "Streetwise", "Mechanic", "Melee|Natural", "Deception", "Survival");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                character.Title = "Outcast";
                return;

            case 1:
                if (allowBonus)
                    AddOneSkill(character, dice, "Independence");
                return;

            case 2:
                return;

            case 3:
                character.Title = "Survivor";
                if (allowBonus)
                    AddOneSkill(character, dice, "Streetwise");
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice) => throw new NotImplementedException();

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        return true;
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        if (character.Gender == "M")
            Increase(character, dice, "Independence", "Strength", "Streetwise", "Gambler", "Endurance", "Jack-of-All-Trades");
        else
            Increase(character, dice, "Melee", "Strength", "Streetwise", "Gambler", "Endurance", "Jack-of-All-Trades");
    }
}
