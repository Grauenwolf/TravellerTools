using Grauenwolf.TravellerTools;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
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
                character.AddHistory($"Became a {Assignment} at age {character.Age}");
                BasicTrainingSkills(character, dice, character.CareerHistory.Count == 0);

                careerHistory = new CareerHistory(Career, Assignment, 0);
                character.CareerHistory.Add(careerHistory);
                UpdateTitle(character, dice, careerHistory);
            }
            else
            {
                if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
                {
                    character.AddHistory($"Switched to {Assignment} at age {character.Age}");
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
                    character.AddHistory($"Continued as {Assignment} at age {character.Age}");
                    careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                }
                else
                {
                    character.AddHistory($"Returned to {Assignment} at age {character.Age}");
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

                var advancementRoll = dice.D(2, 6);
                if (advancementRoll == 12)
                {
                    character.AddHistory("Forced to continue current assignment");
                    character.NextTermBenefits.MustEnroll = Assignment;
                }
                advancementRoll += character.GetDM(AdvancementAttribute) + character.CurrentTermBenefits.AdvancementDM;

                if (advancementRoll >= AdvancementTarget)
                {

                    careerHistory.Rank += 1;
                    character.AddHistory($"Promoted to rank {careerHistory.Rank}");

                    UpdateTitle(character, dice, careerHistory);

                    //advancement skill
                    var skillTables = new List<SkillTable>();
                    skillTables.Add(PersonalDevelopment);
                    skillTables.Add(ServiceSkill);
                    skillTables.Add(AssignmentSkills);
                    if (character.Education >= AdvancedEductionMin)
                        skillTables.Add(AdvancedEducation);
                    dice.Choose(skillTables)(character, dice);
                }

                if (advancementRoll <= careerHistory.Terms)
                {
                    character.AddHistory("Forced to muster out.");
                    character.NextTermBenefits.MusterOut = true;
                }
            }
            else
            {
                character.NextTermBenefits.MusterOut = true;
                Mishap(character, dice);
            }

            character.LastCareer = careerHistory;
            character.Age += 4;
        }

    }
}

