namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

abstract class Soulhunter(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryCareer("Soulhunter", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Aliens of Charted Space Vol. 4, page 228";
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Pilot", "Vacc Suit", "Athletics", "Gunner", "Mechanic", "Gun Combat");
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
                character.AddHistory($"Trapped behind enemy lines.", dice);
                AddOneSkill(character, dice, "Survival", "Stealth", "Deception", "Streetwise");
                return;

            case 4:
                character.AddHistory($"Given a special assignment or duty on board {character.Name}'s ship.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 5:
                character.AddHistory($"Given advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    AddOneRandomSkill(character, dice);
                return;

            case 6:
                character.AddHistory($"{character.Name}'s vessel participates in a notable engagement.", dice);
                AddOneSkill(character, dice, "Electronics", "Engineer", "Gunner", "Pilot");
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"On the front line of an assault and occupation", dice);
                AddOneSkill(character, dice, "Recon", "Leadership", "Gun Combat", "Electronics|Comms");
                return;

            case 9:
                character.AddHistory($"Foiled an attempted crime onboard {character.Name}'s ship or in {character.Name}'s unit, such as mutiny, sabotage, smuggling, or conspiracy. Gain an enemy", dice);
                character.AddEnemy();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory($"Assigned a black ops mission.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 11:
                character.AddHistory($"Commanding officer takes an interest in {character.Name}'s career.", dice);
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Add("Tactics", "Military", 1);
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"Display heroism in battle.", dice);

                character.CurrentTermBenefits.AdvancementDM += 100; //also applies to commission rolls

                if (character.LastCareer!.CommissionRank == 0)
                    character.CurrentTermBenefits.FreeCommissionRoll = true;

                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 1.0M;
        if (roll >= 8)
            return 1.0M;
        if (roll >= 4)
            return 0.75M;
        return 0;
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"A mission goes horribly wrong. {character.Name} and several others are captured and mistreated by the enemy. Gain gaoler as Enemy.", age);
                character.Strength -= 1;
                character.Dexterity -= 1;
                character.AddEnemy();
                return;

            case 3:
                int bestSkill = 0;
                switch (Assignment)
                {
                    case "Commando":
                        bestSkill = character.Skills.BestSkillLevel("Gun Combat", "Stealth");
                        break;

                    case "Flight":
                        bestSkill = character.Skills.BestSkillLevel("Sensors", "Pilot");
                        break;

                    case "Support":
                        bestSkill = character.Skills.BestSkillLevel("Mechanic", "Vacc Suit");
                        break;
                }

                if (dice.D(2, 6) + bestSkill >= 8)
                {
                    character.AddHistory($"During a battle, defeat or victory depends on {character.Name}'s actions. The ship suffers severe damage and {character.Name} is blamed for the disaster, court-martialled, and discharged.", age);
                }
                else
                {
                    character.AddHistory($"During a battle, defeat or victory depends on {character.Name}'s actions. {character.Name}'s efforts ensure {character.Name} is honourably discharged.", age);
                    character.BenefitRolls += 1;
                }
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name} is blamed for an accident which causes the death of several crew members. Guilt drives {character.Name} to exile.", age);
                    character.SocialStanding -= 1;
                }
                else
                {
                    character.AddHistory($"{character.Name} is falsely blamed for an accident which causes the death of several crew members. Gain {character.Name} accusor as an enemy.", age);
                    character.AddEnemy();
                    character.BenefitRolls += 1;
                }
                return;

            case 5:
                character.AddHistory($"{character.Name} is tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.", age);
                character.AddRival();
                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot", "Vacc Suit", "Athletics", "Gunner", "Mechanic", "Gun Combat");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        if (careerHistory.CommissionRank == 0)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    careerHistory.Title = "Least Claw";
                    return;

                case 1:
                    careerHistory.Title = "Third Claw";
                    if (allowBonus)
                        AddOneSkill(character, dice, "Gun Combat", "Mechanic");

                    return;

                case 2:
                    careerHistory.Title = "Second Claw";
                    return;

                case 3:
                    careerHistory.Title = "First Claw";
                    if (allowBonus)
                        character.Skills.Add("Leadership", 1);
                    return;

                case 4:
                    careerHistory.Title = "Third Fang";
                    return;

                case 5:
                    careerHistory.Title = "Second Fang";
                    if (allowBonus)
                        character.Endurance += 1;
                    return;

                case 6:
                    careerHistory.Title = "First Fang";
                    return;
            }
        }
        else
        {
            switch (careerHistory.CommissionRank)
            {
                case 1:
                    careerHistory.Title = "Kaltrhar";
                    if (allowBonus)
                        character.Skills.Add("Melee", "Natural", 1);
                    return;

                case 2:
                    careerHistory.Title = "Shin Kaltrhar";
                    if (allowBonus)
                        character.Skills.Add("Leadership", 1);
                    return;

                case 3:
                    careerHistory.Title = "Shilahn";
                    return;

                case 4:
                    careerHistory.Title = "Shiltrhar";
                    if (allowBonus)
                        AddOneSkill(character, dice, "Tactics");
                    return;

                case 5:
                    careerHistory.Title = "Shil Shintrah";
                    if (allowBonus)
                        character.Skills.Add("Admin", 1);
                    return;

                case 6:
                    careerHistory.Title = "Shinalrhar";
                    if (allowBonus)
                        character.SocialStanding += 1;
                    return;
            }
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Medic", "Survival", "Explosives", "Navigation", "Admin", "Engineer");
    }

    protected override int CommissionAttribute(Character character) => character.Intellect;

    protected override void OfficerTraining(Character character, Dice dice)
    {
        Increase(character, dice, "Leadership", "Electronics", "Pilot", "Melee|Natural", "Admin", "Tactics");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EnduranceDM;
        dm += -1 * character.CareerHistory.Count;
        if (character.Age >= 30)
            dm += -2;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Intellect", "Education", "SocialStanding");
    }
}
