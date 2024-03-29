﻿namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Navy(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryCareer("Navy", assignment, speciesCharacterBuilder)
{
    public override CareerGroup CareerGroup => CareerGroup.ImperiumCareer;
    public override CareerTypes CareerTypes => CareerTypes.Military | CareerTypes.MilitaryNavy | CareerTypes.Violent;
    public override string? Source => "Traveller Core, page 36";
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
                character.AddHistory($"{character.Name} join a gambling circle on board.", dice);
                AddOneSkill(character, dice, "Gambler", "Deception");

                if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Gambler"), 8))
                    character.BenefitRolls += 1;
                else
                    character.BenefitRolls += -1;

                return;

            case 4:
                character.AddHistory($"Special assignment or duty on board ship.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 5:
                character.AddHistory($"Advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    dice.Choose(character.Skills).Level += 1;
                return;

            case 6:
                character.AddHistory($"Vessel participates in a notable military engagement.", dice);
                AddOneSkill(character, dice, "Electronics", "Engineer", "Gunner", "Pilot");
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                var age = character.AddHistory($"Vessel participates in a diplomatic mission.", dice);
                if (dice.NextBoolean())
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Recon");
                    skillList.Add("Diplomat");
                    skillList.Add("Steward");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                else
                {
                    character.AddHistory($"Gain a contact.", age);
                    character.AddContact();
                }

                return;

            case 9:
                character.AddHistory($"{character.Name} foil an attempted crime on board, such as mutiny, sabotage, smuggling or conspiracy. Gain an Enemy.", dice);
                character.AddEnemy();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Abuse {character.Name}'s position for profit.", dice);
                    character.BenefitRolls += 1;
                }
                else
                {
                    character.AddHistory($"Refuse to abuse {character.Name}'s position for profit.", dice);
                    character.CurrentTermBenefits.AdvancementDM += 2;
                }
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
                character.AddHistory($"Display heroism in battle, saving the whole ship.", dice);

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
                character.AddHistory($"Placed in the frozen watch (cryogenically stored on board ship) and revived improperly.", age);
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
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                var checkPassed = false;

                switch (character.LastCareer!.Assignment)
                {
                    case "Line/Crew":
                        checkPassed = dice.RollHigh(character.Skills.BestSkillLevel("Electronics", "Gunner"), 8);
                        break;

                    case "Engineer/Gunner":
                        checkPassed = dice.RollHigh(character.Skills.BestSkillLevel("Mechanic", "Vacc Suit"), 8);
                        break;

                    case "Flight":
                        checkPassed = dice.RollHigh(character.Skills.BestSkillLevel("Pilot", "Tactics"), 8);
                        break;
                }
                if (checkPassed)
                {
                    character.AddHistory($"During a battle, defeat or victory depends on {character.Name}'s actions. {character.Name} actions lead to an honorabe discharge.", age);
                    character.BenefitRolls += 1;
                }
                else
                {
                    character.AddHistory($"During a battle, defeat or victory depends on {character.Name}'s actions. The ship suffers severe damage and {character.Name} is blamed for the disaster. {character.Name} is court - martialled and discharged.", age);
                }

                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name} is blamed for an accident that causes the death of several crew members. {character.Name}'s guilt drives {character.Name} to excel.", age);

                    var skillTables = new List<SkillTable>();
                    skillTables.Add(PersonalDevelopment);
                    skillTables.Add(ServiceSkill);
                    skillTables.Add(AssignmentSkills);
                    if (character.Education >= AdvancedEductionMin)
                        skillTables.Add(AdvancedEducation);
                    if (character.LastCareer!.CommissionRank > 0)
                        skillTables.Add(OfficerTraining);

                    dice.Choose(skillTables)(character, dice);
                }
                else
                {
                    character.AddHistory($"{character.Name} is falsely accused for an accident that causes the death of several crew members. Gain the officer who blamed {character.Name} as an Enemy.", age);
                    character.AddEnemy();
                    character.BenefitRolls += 1;
                }
                return;

            case 5:
                character.AddHistory($"{character.Name} is tormented by or quarrel with an officer or fellow crewman. Gain that character as a Rival.", age);
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
                    careerHistory.Title = "Crewman";
                    return;

                case 1:
                    careerHistory.Title = "Able Spacehand";
                    if (allowBonus)
                        character.Skills.Add("Mechanic", 1);
                    return;

                case 2:
                    careerHistory.Title = "Petty Officer, 3rd  class";
                    if (allowBonus)
                        character.Skills.Add("Vacc Suit", 1);
                    return;

                case 3:
                    careerHistory.Title = "Petty Officer, 2nd  class";
                    return;

                case 4:
                    careerHistory.Title = "Petty Officer, 1st  class";
                    character.Endurance += 1;
                    return;

                case 5:
                    careerHistory.Title = "Chief Petty Officer";
                    return;

                case 6:
                    careerHistory.Title = "Master Chief";
                    return;
            }
        }
        else
        {
            switch (careerHistory.CommissionRank)
            {
                case 1:
                    careerHistory.Title = "Ensign";
                    if (allowBonus)
                        character.Skills.Add("Melee", "Blade", 1);
                    return;

                case 2:
                    careerHistory.Title = "Sublieutenant";
                    if (allowBonus)
                        character.Skills.Add("Leadership", 1);
                    return;

                case 3:
                    careerHistory.Title = "Lieutenant";
                    return;

                case 4:
                    careerHistory.Title = "Commander";
                    if (allowBonus)
                        character.Skills.Add("Tactics", "Military", 1);
                    return;

                case 5:
                    careerHistory.Title = "Captain";
                    if (allowBonus)
                    {
                        if (character.SocialStanding < 10)
                            character.SocialStanding = 10;
                        else
                            character.SocialStanding += 1;
                    }
                    return;

                case 6:
                    careerHistory.Title = "Admiral";
                    if (allowBonus)
                    {
                        if (character.SocialStanding < 12)
                            character.SocialStanding = 12;
                        else
                            character.SocialStanding += 1;
                    }
                    return;
            }
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Astrogation", "Engineer", "Drive", "Navigation", "Admin");
    }

    protected override void OfficerTraining(Character character, Dice dice)
    {
        Increase(character, dice, "Leadership", "Electronics", "Pilot", "Melee|Blade", "Admin", "Tactics|Naval");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.IntellectDM;
        dm += -1 * character.CareerHistory.Count;
        if (character.Age >= 34)
            dm += -2;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Intellect", "Education", "SocialStanding");
    }
}
