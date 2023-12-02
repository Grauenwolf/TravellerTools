using Grauenwolf.TravellerTools;

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
            careerHistory = new CareerHistory(Career, Assignment, 0);
            ChangeCareer(character, dice, careerHistory);
            BasicTrainingSkills(character, dice, character.CareerHistory.Count == 0);

            character.CareerHistory.Add(careerHistory);
        }
        else
        {
            if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
            {
                careerHistory = new CareerHistory(Career, Assignment, 0);
                character.AddHistory($"Switched to {careerHistory.LongName}.", character.Age);
                character.CareerHistory.Add(careerHistory);

                if (!RankCarryover) //then this is a new career path
                {
                    ChangeAssignment(character, dice, careerHistory);
                    BasicTrainingSkills(character, dice, false);
                }
            }
            else if (character.LastCareer?.Assignment == Assignment)
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Continued as {careerHistory.LongName}.", character.Age);
            }
            else
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Returned to {careerHistory.LongName}.", character.Age);
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
                character.AddHistory("Forced to continue current assignment.", character.Age);
                character.NextTermBenefits.MustEnroll = Assignment;
            }
            advancementRoll += character.GetDM(AdvancementAttribute) + character.CurrentTermBenefits.AdvancementDM + character.LongTermBenefits.AdvancementDM;

            if (advancementRoll >= AdvancementTarget)
            {
                Promote(character, dice, careerHistory);

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
