using Grauenwolf.TravellerTools;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class NormalCareer : Career
    {
        protected NormalCareer(string name, string assignment) : base(name, assignment)
        {
        }

        protected abstract string SurvivalAttribute { get; }
        protected abstract int SurvivalTarget { get; }
        protected abstract string AdvancementAttribute { get; }
        protected abstract int AdvancementTarget { get; }

        protected abstract int AdvancedEductionMin { get; }

        public override void Run(Character character, Dice dice)
        {
            CareerHistory careerHistory;
            if (!character.CareerHistory.Any(pc => pc.Name == Name))
            {
                character.AddHistory($"Became a {Assignment} at age {character.Age}");
                BasicTraining(character, dice, character.CareerHistory.Count == 0);

                careerHistory = new CareerHistory(Name, Assignment, 0);
                character.CareerHistory.Add(careerHistory);
            }
            else
            {
                if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
                {
                    character.AddHistory($"Switched to {Assignment} at age {character.Age}");
                    careerHistory = new CareerHistory(Name, Assignment, 0); //TODO: Carry-over rank
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

                dice.Choose(skillTables)(character, dice, dice.D(1, 6), false);
            }
            careerHistory.Terms += 1;

            var survived = dice.RollHigh(character.GetDM(SurvivalAttribute), SurvivalTarget);
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

                if (advancementRoll <= careerHistory.Terms)
                {
                    character.AddHistory("Forced to muster out.");
                    character.NextTermBenefits.MusterOut = true;
                }
                if (advancementRoll > AdvancementTarget)
                {
                    if (careerHistory.CommissionRank > 0)
                    {
                        careerHistory.CommissionRank += 1;
                        character.AddHistory($"Promoted to officer rank {careerHistory.CommissionRank}");
                    }
                    else
                    {
                        careerHistory.Rank += 1;
                        character.AddHistory($"Promoted to rank {careerHistory.Rank}");
                    }
                    var oldTitle = character.Title;
                    UpdateTitle(character, careerHistory, dice);
                    var newTitle = character.Title;
                    if (oldTitle != newTitle)
                    {
                        character.AddHistory($"Is now a {newTitle}");
                        careerHistory.Title = newTitle;
                    }

                    //advancement skill
                    var skillTables = new List<SkillTable>();
                    skillTables.Add(PersonalDevelopment);
                    skillTables.Add(ServiceSkill);
                    skillTables.Add(AssignmentSkills);
                    if (character.Education >= AdvancedEductionMin)
                        skillTables.Add(AdvancedEducation);
                    if (careerHistory.CommissionRank > 0)
                        skillTables.Add(OfficerTraining);
                    dice.Choose(skillTables)(character, dice, dice.D(1, 6), false);
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

        internal abstract void UpdateTitle(Character character, CareerHistory careerHistory, Dice dice);
        internal abstract void Mishap(Character character, Dice dice);
        internal abstract void Event(Character character, Dice dice);
        protected abstract void ServiceSkill(Character character, Dice dice, int roll, bool level0);
        protected abstract void PersonalDevelopment(Character character, Dice dice, int roll, bool level0);
        protected abstract void AdvancedEducation(Character character, Dice dice, int roll, bool level0);
        protected abstract void OfficerTraining(Character character, Dice dice, int roll, bool level0);
        protected abstract void AssignmentSkills(Character character, Dice dice, int roll, bool level0);



        protected virtual void BasicTraining(Character character, Dice dice, bool firstCareer)
        {
            if (firstCareer)
                for (var i = 1; i < 7; i++)
                    ServiceSkill(character, dice, i, true);
            else
                ServiceSkill(character, dice, dice.D(6), true);

        }
    }
}

