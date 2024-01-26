namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Merchant(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Merchant", assignment, speciesCharacterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Drive", "Vacc Suit", "Broker", "Steward", "Electronics", "Persuade");
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
                    character.AddHistory($"Smuggle illegal items onto a planet.", dice);
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Deception", "Persuade"), 8))
                    {
                        character.Skills.Add("Streetwise", 1);
                        character.BenefitRolls += 1;
                    }
                }
                else
                {
                    character.AddHistory($"Refuse to smuggle illegal items onto a planet. Gain an Enemy.", dice);
                    character.AddEnemy();
                }
                return;

            case 4:
                AddOneSkill(character, dice, "Profession", "Electronics", "Engineer", "Animals", "Science");
                return;

            case 5:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Gambler", "Broker"), 8))
                {
                    character.AddHistory($"Risk {character.Name}'s fortune on a possibility lucrative deal and win.", dice);
                    character.BenefitRolls *= 2;
                }
                else
                    character.BenefitRolls = 0;

                IncreaseOneSkill(character, dice, "Broker", "Gambler");
                return;

            case 6:
                character.AddHistory($"Make an unexpected connection outside {character.Name}'s normal circles. Gain a Contact.", dice);
                character.AddContact();
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"Embroiled in legal trouble.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Advocate");
                    skillList.Add("Admin");
                    skillList.Add("Diplomat");
                    skillList.Add("Investigate");
                }
                if (dice.D(2, 6) == 2)
                {
                    character.NextTermBenefits.MusterOut = true;
                    character.NextTermBenefits.MustEnroll = "Prisoner";
                }
                return;

            case 9:
                character.AddHistory($"Given advanced training in a specialist field", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    dice.Choose(character.Skills).Level += 1;
                return;

            case 10:
                character.AddHistory($"A good deal ensures {character.Name} is living the high life for a few years.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 11:
                character.AddHistory($"Befriend a useful ally in one sphere. Gain an Ally.", dice);
                character.AddAlly();
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Add("Carouse", 1);
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"{character.Name}'s business or ship thrives.", dice);
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 1.0M;
        if (roll >= 8)
            return 0.75M;
        if (roll >= 4)
            return 0.50M;
        return 0;
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"Bankrupted by a rival. Gain the other trader as a Rival.", age);
                character.AddRival();
                character.BenefitRolls = 0;
                return;

            case 3:
                character.AddHistory($"A sudden war destroys {character.Name}'s trade routes and contacts, forcing {character.Name} to flee that region of space.", age);
                character.AddHistory($"Gain rebels as Enemy", age);
                character.AddEnemy();
                AddOneSkill(character, dice, "Gun Combat", "Pilot");
                return;

            case 4:
                character.AddHistory($"{character.Name}'s ship or starport is destroyed by criminals. Gain them as an Enemy.", age);
                character.AddEnemy();
                return;

            case 5:
                character.AddHistory($"Imperial trade restrictions force {character.Name} out of business.", age);
                character.NextTermBenefits.EnlistmentDM.Add("Rogue", 100);
                return;

            case 6:
                character.AddHistory($"A series of bad deals and decisions force {character.Name} into bankruptcy. {character.Name} salvages what {character.Name} can.", age);
                character.BenefitRolls += 1;
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.IntellectDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 4, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Drive", "Vacc Suit", "Broker", "Steward", "Electronics", "Persuade");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Engineer", "Astrogation", "Electronics", "Pilot", "Admin", "Advocate");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Intellect", "Language", "Streetwise");
    }
}
