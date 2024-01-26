namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Rogue_Enforcer(SpeciesCharacterBuilder speciesCharacterBuilder) : Rogue("Enforcer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Gun Combat", "Melee", "Streetwise", "Persuade", "Athletics", "Drive");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    character.Skills.Add("Persuade", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    AddOneSkill(character, dice, "Gun Combat", "Melee");
                return;

            case 4:
                return;

            case 5:
                if (allowBonus)
                    character.Skills.Add("Streetwise", 1);
                return;

            case 6:
                return;
        }
    }
}
