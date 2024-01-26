namespace Grauenwolf.TravellerTools.Characters.Careers;

/// <summary>
/// This type of career doesn't have ranks.
/// </summary>
abstract class RanklessCareer(string name, string? assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : FullCareer(name, assignment, speciesCharacterBuilder)
{
    protected sealed override string AdvancementAttribute => throw new NotImplementedException();
    protected sealed override int AdvancementTarget => throw new NotImplementedException();

    internal override void Run(Character character, Dice dice)
    {
        var careerHistory = SetupAndSkills(character, dice);

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

    internal override sealed void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
    }

    protected override void CareerSkill(Character character, Dice dice)
    {
        var skillTables = new List<SkillTable>();
        skillTables.Add(PersonalDevelopment);
        skillTables.Add(ServiceSkill);
        skillTables.Add(AssignmentSkills);
        dice.Choose(skillTables)(character, dice);
    }
}
