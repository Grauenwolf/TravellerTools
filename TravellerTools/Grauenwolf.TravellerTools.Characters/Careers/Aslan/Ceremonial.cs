namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Ceremonial(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Ceremonial", assignment, speciesCharacterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Diplomat", "Investigate", "Advocate", "Melee|Natural", "Science", "Persuade");
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
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name} uncover an embarrassing secret related to {character.Name}'s clan or family.", dice);
                }
                else
                {
                    character.AddHistory($"{character.Name} uncover an embarrassing secret related to {character.Name}'s clan or family. Trade it for {dice.D(3)} clan shares and an enemy.", dice);
                    character.AddEnemy();
                }
                return;

            case 4:
                character.AddHistory($"{character.Name} witnessed one of the great duellists in action.", dice);
                AddOneSkill(character, dice, "Melee|Natural", "Athletics|Strength", "Carouse", "Medic");
                return;

            case 5:
                var bestSkill = character.Skills.BestSkill("Art", "Investigate", "Persuade");
                if (bestSkill != null)
                {
                    if (dice.RollHigh(bestSkill.Level, 8))
                    {
                        character.AddHistory($"{character.Name} was assigned a challenging task using {bestSkill.Name} and succeeded.", dice);
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    else
                    {
                        character.AddHistory($"{character.Name} was assigned a challenging task using {bestSkill.Name} and failed.", dice);
                        character.CurrentTermBenefits.AdvancementDM += -2;
                    }
                }
                else
                {
                    if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Art"), 8)) //this will be -3 without Jack-of-All-Trades
                    {
                        character.AddHistory($"{character.Name} was assigned a challenging task using and succeeded.", dice);
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    else
                    {
                        character.AddHistory($"{character.Name} was assigned a challenging task using and failed.", dice);
                        character.CurrentTermBenefits.AdvancementDM += -2;
                    }
                }
                return;

            case 6:
                character.AddHistory($"{character.Name} is given advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    IncreaseOneRandomSkill(character, dice);
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"{character.Name}'s clan prospers and so do {character.Name}.", dice);
                AddOneSkill(character, dice, "Carouse", "Survival", "Admin", "Independence");
                return;

            case 9:
                character.AddHistory($"{character.Name} rise in influence in {character.Name}'s clan.", dice);
                character.Territory += 1;
                IncreaseOneRandomSkill(character, dice);
                return;

            case 10:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name} discover that one of {character.Name}'s kinfolk has acted dishonourably. If {character.Name} cover up his failing, gain him as an Ally.", dice);
                    character.AddAlly();
                }
                else
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Melee"), 8))
                    {
                        character.AddHistory($"{character.Name} discover that one of {character.Name}'s kinfolk has acted dishonourably. {character.Name} expose him and win a duel.", dice);
                        character.Territory += 2;
                        character.AddEnemy();
                    }
                    else
                    {
                        character.AddHistory($"{character.Name} discover that one of {character.Name}'s kinfolk has acted dishonourably. {character.Name} expose him and lose a duel.", dice);
                        character.SocialStanding += -2;
                        character.AddRival();
                    }
                }
                return;

            case 11:
                character.AddHistory($"{character.Name} is trusted by the great lords of {character.Name}'s clan.", dice);
                if (dice.NextBoolean())
                    character.Territory += 2;
                else
                    character.CurrentTermBenefits.AdvancementDM += 4;
                return;

            case 12:
                character.AddHistory($"{character.Name} excel in {character.Name}'s role.", dice);
                character.CurrentTermBenefits.AdvancementDM += 99;
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
                character.AddHistory($"{character.Name} commited a grievous breach of protocol and are Outcast.", age);
                character.SocialStanding = 2;
                character.IsOutcast = true;
                return;

            case 3:
                character.AddHistory($"{character.Name} is exiled because of some political scandal.", age);
                AddOneSkill(character, dice, "Survival", "Pilot", "Streetwise", "Independence");
                return;

            case 4:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Melee"), 8))
                {
                    character.AddHistory($"{character.Name} is wounded in a duel that {character.Name} won.", age);
                    character.SocialStanding += 1;
                }
                else
                    character.AddHistory($"{character.Name} is wounded in a duel that {character.Name} lost.", age);
                return;

            case 5:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Advocate"), 8))
                {
                    character.AddHistory($"{character.Name} is accused of a crime {character.Name} did not commit and proven innocent.", age);
                    character.NextTermBenefits.MusterOut = false;
                }
                else
                    character.AddHistory($"{character.Name} is accused of a crime {character.Name} did not commit.", age);
                return;

            case 6:
                character.AddHistory($"{character.Name} is embroiled in a rivalry with another official, who ends {character.Name}'s career. Gain him as a Rival.", age);
                character.AddRival();
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        if (character.SocialStanding >= 9)
            dm += 2;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 10, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Diplomat", "Investigate", "Advocate", "Melee|Natural", "Science", "Persuade");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Art", "Electronics", "Language", "Tolerance", "Admin", "Science");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        if (character.Gender == "M")
            Increase(character, dice, "Strength", "Dexterity", "Intellect", "Education", "SocialStanding", "Independence");
        else
            Increase(character, dice, "Strength", "Dexterity", "Intellect", "Education", "SocialStanding", null);
    }
}
