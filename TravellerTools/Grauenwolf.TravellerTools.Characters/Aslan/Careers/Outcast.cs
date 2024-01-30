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
                    character.AddHistory("Fight against an alien race.", dice);
                    IncreaseOneSkill(character, dice, "Gun Combat", "Language", "Melee", "Recon", "Suvival");
                    return;

                case 9:
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

                case 10:

                    if (dice.RollHigh(character.RiteOfPassageDM + character.LastCareer!.Terms, 10))
                    {
                        character.AddHistory($"Promoted to the officer caste.", dice);
                        character.NextTermBenefits.MustEnroll = "Military Officer";
                    }
                    else
                    {
                        character.AddHistory($"Considered for promotion in the officer caste but didn't make the cut.", dice);
                    }

                    return;

                case 11:
                    character.AddHistory($"Serve under a hero of the clan.", dice);
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
                    Injury(character, dice, true, age);
                    return;

                case 2:
                    character.AddHistory($"Drumed out of the service by a superior officer. Gain a Rival.", age);
                    character.AddRival();
                    return;

                case 3:
                    character.AddHistory($"Lost behind enemy lines.", age);
                    IncreaseOneSkill(character, dice, "Stealth", "Survival", "Streetwise", "Gun Combat");
                    return;

                case 4:
                    character.AddHistory($"Captured and ransomed back to the clan.", age);
                    character.SocialStanding += -1;
                    return;

                case 5:
                    if (dice.NextBoolean())
                    {
                        if (dice.RollHigh(character.Skills.BestSkillLevel("Gun Combat", "Athletics"), 8))
                        {
                            character.AddHistory("Fight bravely in a dangerous skirmish.", dice);
                            character.NextTermBenefits.MusterOut = false;
                        }
                        else
                        {
                            character.AddHistory("Injured in a dangerous skirmish.", dice);
                        }
                    }
                    else
                    {
                        character.AddHistory("Refused to fight in a dangerous skirmish.", dice);
                    }
                    return;

                case 6:
                    Injury(character, dice, true, age);
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
                character.Title = "Recruit";
                return;

            case 1:
                character.Title = "Soldier";
                if (allowBonus)
                    character.Skills.Add("Melee", "Natural", 1);
                return;

            case 2:
                character.Title = "Veteran Soldier";
                return;

            case 3:
                character.Title = "Warrior";
                if (allowBonus)
                    character.Endurance += 1;
                return;

            case 4:
                character.Title = "Veteran Warrior";
                return;

            case 5:
                character.Title = "Leader of Warriors";
                return;

            case 6:
                character.Title = "Honoured Warrior Leader";
                if (allowBonus)
                    character.AddHistory("Gain 3 clan shares.", character.Age);
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