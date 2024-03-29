﻿namespace Grauenwolf.TravellerTools.Characters.Careers;

public abstract class NormalCareer(string name, string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : FullCareer(name, assignment, speciesCharacterBuilder)
{
    public int AdvancementDM { get; set; }
    internal override bool RankCarryover { get; } = false;

    protected abstract int AdvancedEductionMin { get; }

    internal override void Run(Character character, Dice dice)
    {
        var careerHistory = SetupAndSkills(character, dice);
        careerHistory.Terms += 1;
        character.LastCareer = careerHistory;

        if (character.CurrentTermBenefits.MinRank.HasValue)
            while (careerHistory.Rank < character.CurrentTermBenefits.MinRank)
                Promote(character, dice, careerHistory);

        var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
        if (survived)
        {
            character.BenefitRolls += 1;

            Event(character, dice);
            FixupSkills(character, dice);

            character.Age += 4;

            var advancementRoll = dice.D(2, 6);
            if (advancementRoll == 12)
            {
                character.AddHistory($"Forced to continue current assignment.", character.Age);
                character.NextTermBenefits.MustEnroll = Assignment;
            }
            advancementRoll += character.GetDM(AdvancementAttribute) + character.GetAdvancementBonus(Career, Assignment) + AdvancementDM;

            if (advancementRoll >= AdvancementTarget)
            {
                Promote(character, dice, careerHistory);
                FixupSkills(character, dice);

                CareerSkill(character, dice);
                FixupSkills(character, dice);
            }

            if (advancementRoll <= careerHistory.Terms)
            {
                character.AddHistory($"Forced to muster out.", character.Age);
                character.NextTermBenefits.MusterOut = true;
            }
        }
        else
        {
            var mishapAge = character.Age + dice.D(4);

            character.NextTermBenefits.MusterOut = true;
            Mishap(character, dice, mishapAge);
            FixupSkills(character, dice);

            if (character.NextTermBenefits.MusterOut)
                character.Age = mishapAge;
            else
                character.Age += +4; //Complete the term dispite the mishap.
        }
    }

    protected abstract void AdvancedEducation(Character character, Dice dice);

    protected override void CareerSkill(Character character, Dice dice)
    {
        var skillTables = new List<SkillTable>
                {
                    PersonalDevelopment,
                    ServiceSkill,
                    AssignmentSkills
                };
        if (character.Education >= AdvancedEductionMin)
            skillTables.Add(AdvancedEducation);

        dice.Choose(skillTables)(character, dice); //Choose a skill table and execute it.
    }
}
