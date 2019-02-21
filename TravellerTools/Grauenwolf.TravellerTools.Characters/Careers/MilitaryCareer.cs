using Grauenwolf.TravellerTools;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class MilitaryCareer : FullCareer
    {
        protected MilitaryCareer(string name, string assignment, Book book) : base(name, assignment, book)
        {
        }

        internal override void Run(Character character, Dice dice)
        {
            CareerHistory careerHistory;
            if (!character.CareerHistory.Any(pc => pc.Career == Career))
            {
                character.AddHistory($"Became a {Assignment} at age {character.Age}");
                BasicTraining(character, dice, character.CareerHistory.Count == 0);

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
                if (careerHistory.CommissionRank > 0)
                    skillTables.Add(OfficerTraining);

                dice.Choose(skillTables)(character, dice);
            }
            careerHistory.Terms += 1;
            character.LastCareer = careerHistory;

            //rank carry-over
            careerHistory.Rank = character.CareerHistory.Where(c => c.Career == Career).Max(c => c.Rank);
            careerHistory.CommissionRank = character.CareerHistory.Where(c => c.Career == Career).Max(c => c.CommissionRank);

            //Early commission, possibly from military academy.
            if (careerHistory.CommissionRank == 0 && character.CurrentTermBenefits.FreeCommissionRoll)
            {
                AttemptCommission(character, dice, careerHistory);
            }

            var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
            if (survived)
            {
                character.BenefitRolls += 1;

                Event(character, dice);

                var totalTermsInCareer = character.CareerHistory.Where(pc => pc.Career == Career).Sum(c => c.Terms);

                //Not all people will attempt a commission even when possible
                var attemptCommission = (totalTermsInCareer == 1 || character.SocialStanding >= 9) && dice.D(100) < Book.OddsOfSuccess(character, "Soc", 8 - character.CurrentTermBenefits.CommissionDM);

                var commissionEarned = false;

                if (careerHistory.CommissionRank == 0 && (attemptCommission || character.CurrentTermBenefits.FreeCommissionRoll))
                    commissionEarned = AttemptCommission(character, dice, careerHistory);

                //always try for advancement even if a commission was earned.
                var advancementRoll = dice.D(2, 6);
                if (advancementRoll == 12)
                {
                    character.AddHistory("Forced to continue current assignment");
                    character.NextTermBenefits.MustEnroll = Assignment;
                }
                advancementRoll += character.GetDM(AdvancementAttribute) + character.CurrentTermBenefits.AdvancementDM;

                if (advancementRoll >= AdvancementTarget)
                {
                    if (careerHistory.CommissionRank > 0)
                    {
                        careerHistory.CommissionRank += 1;
                        character.AddHistory($"Promoted to {careerHistory.Assignment} officer rank {careerHistory.CommissionRank}");
                    }
                    else
                    {
                        careerHistory.Rank += 1;
                        character.AddHistory($"Promoted to {careerHistory.Assignment} rank {careerHistory.Rank}");
                    }
                    UpdateTitle(character, dice, careerHistory);

                    //advancement skill
                    var skillTables = new List<SkillTable>();
                    skillTables.Add(PersonalDevelopment);
                    skillTables.Add(ServiceSkill);
                    skillTables.Add(AssignmentSkills);
                    if (character.Education >= AdvancedEductionMin)
                        skillTables.Add(AdvancedEducation);
                    if (careerHistory.CommissionRank > 0)
                        skillTables.Add(OfficerTraining);
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

            character.Age += 4;
        }

        protected virtual void BasicTraining(Character character, Dice dice, bool firstCareer)
        {
            if (firstCareer)
                for (var i = 1; i < 7; i++)
                    ServiceSkill(character, dice);
            else
                ServiceSkill(character, dice);
        }

        protected abstract void OfficerTraining(Character character, Dice dice);

        private bool AttemptCommission(Character character, Dice dice, CareerHistory careerHistory)
        {
            var commissionDM =
                   character.SocialStandingDM +
                   character.CurrentTermBenefits.AdvancementDM +
                   character.CurrentTermBenefits.CommissionDM +
                   (1 - careerHistory.Terms);

            character.CurrentTermBenefits.FreeCommissionRoll = false;
            character.CurrentTermBenefits.CommissionDM = 0;

            if (dice.RollHigh(commissionDM, 8))
            {
                character.AddHistory($"Commissioned in {Career}/{Assignment}");
                careerHistory.CommissionRank = 1;

                UpdateTitle(character, dice, careerHistory);
                return true;
            }
            else
            {
                character.AddHistory($"Attempt at commissioned failed.");
                return false;
            }
        }
    }
}