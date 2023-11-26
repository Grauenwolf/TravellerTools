using Grauenwolf.TravellerTools;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class NormalCareer : FullCareer
{
    protected NormalCareer(string name, string assignment, Book book) : base(name, assignment, book)
    {
    }

    protected abstract bool RankCarryover { get; }

    internal override void Run(Character character, Dice dice)
    {
        CareerHistory careerHistory;
        if (!character.CareerHistory.Any(pc => pc.Career == Career))
        {
            character.AddHistory($"Became a {Assignment}.", character.Age);
            BasicTrainingSkills(character, dice, character.CareerHistory.Count == 0);

            careerHistory = new CareerHistory(Career, Assignment, 0);
            character.CareerHistory.Add(careerHistory);
            UpdateTitle(character, dice, careerHistory);
        }
        else
        {
            if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
            {
                character.AddHistory($"Switched to {Assignment}.", character.Age);
                careerHistory = new CareerHistory(Career, Assignment, 0);
                character.CareerHistory.Add(careerHistory);

                if (!RankCarryover) //then this is a new career
                {
                    UpdateTitle(character, dice, careerHistory);
                    BasicTrainingSkills(character, dice, false);
                }
            }
            else if (character.LastCareer?.Assignment == Assignment)
            {
                character.AddHistory($"Continued as {Assignment}.", character.Age);
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
            }
            else
            {
                character.AddHistory($"Returned to {Assignment}.", character.Age);
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
            }

            var skillTables = new List<SkillTable>();
            skillTables.Add(PersonalDevelopment);
            skillTables.Add(ServiceSkill);
            skillTables.Add(AssignmentSkills);
            if (character.Education >= AdvancedEductionMin)
                skillTables.Add(AdvancedEducation);

            dice.Choose(skillTables)(character, dice);
        }
        careerHistory.Terms += 1;

        if (RankCarryover)
        {
            careerHistory.Rank = character.CareerHistory.Where(c => c.Career == Career).Max(c => c.Rank);
            careerHistory.CommissionRank = character.CareerHistory.Where(c => c.Career == Career).Max(c => c.CommissionRank);
        }

        var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
        if (survived)
        {
            character.BenefitRolls += 1;

            Event(character, dice);

            character.Age += 4;

            var advancementRoll = dice.D(2, 6);
            if (advancementRoll == 12)
            {
                character.AddHistory("Forced to continue current assignment", character.Age);
                character.NextTermBenefits.MustEnroll = Assignment;
            }
            advancementRoll += character.GetDM(AdvancementAttribute) + character.CurrentTermBenefits.AdvancementDM + character.LongTermBenefits.AdvancementDM;

            if (advancementRoll >= AdvancementTarget)
            {
                careerHistory.Rank += 1;
                character.AddHistory($"Promoted to {careerHistory.ShortName} rank {careerHistory.Rank}", character.Age);

                UpdateTitle(character, dice, careerHistory);

                //advancement skill
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

            if (advancementRoll <= careerHistory.Terms)
            {
                character.AddHistory("Forced to muster out.", character.Age);
                character.NextTermBenefits.MusterOut = true;
            }
        }
        else
        {
            var mishapAge = character.Age + dice.D(4);

            character.NextTermBenefits.MusterOut = true;
            Mishap(character, dice, mishapAge);

            if (character.NextTermBenefits.MusterOut)
                character.Age = mishapAge;
            else
                character.Age += +4; //Complete the term dispite the mishap.
        }

        character.LastCareer = careerHistory;
    }
}