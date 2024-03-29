﻿namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Drifter(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Drifter", assignment, speciesCharacterBuilder)
{
    public override CareerGroup CareerGroup => CareerGroup.ImperiumCareer;
    public override CareerTypes CareerTypes => CareerTypes.Outsider | CareerTypes.Civilian;

    public override string? Source => "Traveller Core, page 28";
    protected override int AdvancedEductionMin => int.MaxValue;

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                MishapRollAge(character, dice);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                character.AddHistory($"Offered job by a patron. Now owe him a favor.", dice);
                character.NextTermBenefits.MusterOut = true;
                character.NextTermBenefits.QualificationDM += 4;
                return;

            case 4:
                IncreaseOneSkill(character, dice, "Jack-of-All-Trades", "Survival", "Streetwise");
                return;

            case 5:
                character.AddHistory($"Find valuable salvage.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                UnusualLifeEvent(character, dice);
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                {
                    int age;
                    var bestSkill = character.Skills.BestSkillLevel("Melee", "Gun Combat", "Stealth");
                    if (dice.RollHigh(bestSkill, 8))
                    {
                        age = character.AddHistory($"Attacked by enemies that {character.Name} easily defeat.", dice);
                    }
                    else
                    {
                        age = character.AddHistory($"Attacked by enemies and injured.", dice);
                        Injury(character, dice, age);
                    }
                    character.AddHistory($"Gain Enemy if {character.Name} don't already have one.", age);
                    return;
                }
            case 9:
                {
                    var roll = dice.D(6);
                    if (roll == 1)
                    {
                        var age = character.AddHistory($"Attempted a risky adventure and was injured.", dice);
                        Injury(character, dice, age);
                    }
                    else if (roll == 2)
                    {
                        character.AddHistory($"Attempted a risky adventure and was sent to prison.", dice);
                        character.NextTermBenefits.MustEnroll = "Prisoner";
                    }
                    else if (roll < 5)
                    {
                        character.AddHistory($"Survived a risky adventure but gained nothing.", dice);
                    }
                    else
                    {
                        character.AddHistory($"Attempted a risky adventure and was wildly successful.", dice);
                        character.BenefitRollDMs.Add(4);
                    }
                }
                return;

            case 10:
                dice.Choose(character.Skills).Level += 1;
                return;

            case 11:
                character.NextTermBenefits.MusterOut = true;
                character.NextTermBenefits.MustEnroll = RollDraft(character, dice).ShortName;
                character.AddHistory($"Drafted into " + character.NextTermBenefits.MustEnroll, dice);
                return;

            case 12:
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 0.75M;
        if (roll >= 8)
            return 0.50M;
        if (roll >= 4)
            return 0.00M;
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
                Injury(character, dice, age);
                return;

            case 3:
                character.AddHistory($"{character.Name} run afoul of a criminal gang, corrupt bureaucrat or other foe. Gain an Enemy.", age);
                return;

            case 4:
                character.AddHistory($"Suffer from a life-threatening illness.", age);
                character.Endurance -= 1;
                return;

            case 5:
                character.AddHistory($"Betrayed by a friend. One of {character.Name}'s Contacts or Allies betrays {character.Name}, ending {character.Name}'s career. That Contact or Ally becomes a Rival or Enemy.", age);
                if (dice.D(2, 12) == 2)
                    character.NextTermBenefits.MustEnroll = "Prisoner";
                return;

            case 6:
                character.AddHistory($"{character.Name} do not know what happened to {character.Name}. There is a gap in {character.Name}'s memory.", age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Athletics", "Melee|Unarmed", "Recon", "Streetwise", "Stealth", "Survival");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        throw new NotImplementedException();
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        return !character.LongTermBenefits.Retired;
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Endurance", "Dexterity", "Language", "Profession", "Jack-of-All-Trades");
    }
}
