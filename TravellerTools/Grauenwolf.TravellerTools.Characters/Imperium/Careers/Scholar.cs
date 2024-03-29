﻿namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Scholar(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Scholar", assignment, speciesCharacterBuilder)
{
    public override CareerGroup CareerGroup => CareerGroup.ImperiumCareer;
    public override CareerTypes CareerTypes => CareerTypes.Science | CareerTypes.Civilian;
    public override string? Source => "Traveller Core, page 42";
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 10;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Drive", "Electronics", "Diplomat", "Medic", "Investigate", "Science");
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
                    character.AddHistory($"Refused to perform research that goes against {character.Name}'s conscience.", dice);
                }
                else
                {
                    int count = dice.D(3);
                    character.AddHistory($"Agreed to perform research that goes against {character.Name}'s conscience. Gain {count} Enemies.", dice);
                    character.AddEnemy(count);
                    character.BenefitRolls += 1;

                    var skillList = new SkillTemplateCollection(SpecialtiesFor(character, "Science"));
                    character.Skills.Increase(dice.Pick(skillList));
                    character.Skills.Increase(dice.Pick(skillList)); //pick 2
                }
                return;

            case 4:
                character.AddHistory($"Assigned to work on a secret project for a patron or organisation.", dice);
                AddOneSkill(character, dice, "Medic", "Science", "Engineer", "Electronics", "Investigate");
                return;

            case 5:
                character.AddHistory($"Win a prestigious prize for {character.Name}'s work.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                character.AddHistory($"Advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    AddOneRandomSkill(character, dice);
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                {
                    if (dice.NextBoolean())
                    {
                        int age;
                        if (dice.RollHigh(character.Skills.BestSkillLevel("Deception", "Admin"), 8))
                        {
                            age = character.AddHistory($"Cheated in some fashion, advancing {character.Name}'s career and research by stealing another’s work, using an alien device, taking a shortcut and so forth.", dice);
                            character.BenefitRollDMs.Add(2);
                            dice.Choose(character.Skills).Level += 1;
                        }
                        else
                        {
                            age = character.AddHistory($"Caught cheating in some fashion, advancing {character.Name}'s career and research by stealing another’s work, using an alien device, taking a shortcut and so forth.", dice);
                            character.BenefitRolls += -1;
                            Mishap(character, dice, age);
                        }
                        character.AddHistory($"Gain an Enemy", age);
                    }
                    else
                    {
                        character.AddHistory($"Refuse to join a cheat in some fashion.", dice);
                    }
                }
                return;

            case 9:
                character.AddHistory($"Make a breakthrough in {character.Name}'s field.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory($"Entangled in a bureaucratic or legal morass that distracts {character.Name} from {character.Name}'s work.", dice);
                AddOneSkill(character, dice, "Admin", "Advocate", "Persuade", "Diplomat");
                return;

            case 11:
                character.AddHistory($"Work for an eccentric but brilliant mentor, who becomes an Ally.", dice);
                character.AddAlly();
                switch (dice.D(2))
                {
                    case 1:
                        IncreaseOneSkill(character, dice, "Science");
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"Work leads to a considerable breakthrough.", dice);
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
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"A disaster leaves several injured, and others blame {character.Name}, forcing {character.Name} to leave {character.Name}'s career. Gain a Rival.", age);
                character.AddRival();
                Injury(character, dice, age);
                return;

            case 3:
                character.AddHistory($"A disaster or war strikes.", age);
                if (!dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 8))
                    Injury(character, dice, age);

                if (dice.NextBoolean())
                {
                    character.AddHistory($"The planetary government interferes with {character.Name}'s research for political or religious reasons. {character.Name} continue working secretely.", age);
                    character.SocialStanding += -2;
                }
                else
                {
                    character.AddHistory($"The planetary government interferes with {character.Name}'s research for political or religious reasons. {character.Name} continue working openly and gain an Enemy.", age);
                    character.AddEnemy();
                }
                IncreaseOneSkill(character, dice, "Science");
                character.NextTermBenefits.MusterOut = false;
                return;

            case 4:
                character.AddHistory($"An expedition or voyage goes wrong, leaving {character.Name} stranded in the wilderness.", age);
                AddOneSkill(character, dice, "Survival", "Athletics|Dexterity", "Athletics|Endurance");
                return;

            case 5:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name}'s work is sabotaged by unknown parties. {character.Name} may salvage what {character.Name} can and give up.", age);
                    character.BenefitRolls += 1;
                }
                else
                {
                    character.AddHistory($"{character.Name}'s work is sabotaged by unknown parties. {character.Name} start again from scratch.", age);
                    character.BenefitRolls = 0;
                    character.NextTermBenefits.MusterOut = false;
                }
                return;

            case 6:
                character.AddHistory($"A rival researcher blackens {character.Name}'s name or steals {character.Name}'s research. Gain a Rival.", age);
                character.AddRival();
                character.NextTermBenefits.MusterOut = false;
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Drive", "Electronics", "Diplomat", "Medic", "Investigate", "Science");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Art", "Advocate", "Electronics", "Language", "Engineer", "Science");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.IntellectDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 5, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Intellect", "Education", "SocialStanding", "Dexterity", "Endurance", "Language");
    }
}
