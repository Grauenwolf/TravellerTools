namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Rogue_Enforcer(SpeciesCharacterBuilder speciesCharacterBuilder) : Rogue("Enforcer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Melee")));
                return;

            case 3:
                character.Skills.Increase("Streetwise");
                return;

            case 4:
                character.Skills.Increase("Persuade");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Drive")));
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                character.Skills.Add("Persuade", 1);
                return;

            case 2:
                return;

            case 3:
                AddOneSkill(character, dice, "Gun Combat", "Melee");
                return;

            case 4:
                return;

            case 5:
                character.Skills.Add("Streetwise", 1);
                return;

            case 6:
                return;
        }
    }
}
