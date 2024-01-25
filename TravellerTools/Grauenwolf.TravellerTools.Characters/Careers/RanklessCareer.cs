using Grauenwolf.TravellerTools;

namespace Grauenwolf.TravellerTools.Characters.Careers;

/// <summary>
/// This type of career doesn't have ranks.
/// </summary>
abstract class RanklessCareer(string name, string? assignment, CharacterBuilder characterBuilder) : CareerBase(name, assignment, characterBuilder)
{
    protected abstract int AdvancedEductionMin { get; }

    /// <summary>
    /// Override this if the career has multiple assignments and you don't get basic skills for changing assignments.
    /// </summary>
    protected virtual bool RankCarryover => false;

    protected abstract string SurvivalAttribute { get; }

    protected abstract int SurvivalTarget { get; }

    internal abstract void AssignmentSkills(Character character, Dice dice);

    internal abstract void BasicTrainingSkills(Character character, Dice dice, bool all);

    internal abstract void Event(Character character, Dice dice);

    internal abstract void Mishap(Character character, Dice dice, int age);

    internal void MishapRollAge(Character character, Dice dice)
    {
        Mishap(character, dice, character.Age + dice.D(4));
    }

    internal override void Run(Character character, Dice dice)
    {
        CareerHistory careerHistory;
        if (!character.CareerHistory.Any(pc => pc.Career == Career))
        {
            careerHistory = new CareerHistory(character.Age, Career, Assignment, 0);
            character.AddHistory($"Became a {careerHistory.LongName}.", character.Age);
            BasicTrainingSkills(character, dice, character.CareerHistory.Count == 0);
            FixupSkills(character, dice);
            character.CareerHistory.Add(careerHistory);
        }
        else
        {
            if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
            {
                careerHistory = new CareerHistory(character.Age, Career, Assignment, 0);
                character.AddHistory($"Switched to {careerHistory.LongName}.", character.Age);
                character.CareerHistory.Add(careerHistory);

                if (!RankCarryover) //then this is a new career path
                {
                    character.AddHistory($"Switched to {careerHistory.LongName}.", character.Age);
                    BasicTrainingSkills(character, dice, false);
                    FixupSkills(character, dice);
                }
            }
            else if (character.LastCareer?.Assignment == Assignment)
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Continued as {careerHistory.LongName}.", character.Age);
                careerHistory.LastTermAge = character.Age;
            }
            else
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Returned to {careerHistory.LongName}.", character.Age);
                careerHistory.LastTermAge = character.Age;
            }

            var skillTables = new List<SkillTable>();
            skillTables.Add(PersonalDevelopment);
            skillTables.Add(ServiceSkill);
            skillTables.Add(AssignmentSkills);
            if (character.Education >= AdvancedEductionMin)
                skillTables.Add(AdvancedEducation);

            dice.Choose(skillTables)(character, dice);
            FixupSkills(character, dice);
        }
        careerHistory.Terms += 1;
        character.LastCareer = careerHistory;

        var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
        if (survived)
        {
            character.BenefitRolls += 1;

            Event(character, dice);
            FixupSkills(character, dice);

            character.Age += 4;
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

    internal abstract void ServiceSkill(Character character, Dice dice);

    protected abstract void AdvancedEducation(Character character, Dice dice);

    protected abstract void PersonalDevelopment(Character character, Dice dice);
}
