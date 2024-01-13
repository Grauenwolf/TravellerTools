namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Ceremonial_ClanAgent(CharacterBuilder characterBuilder) : Ceremonial("Clan Agent", characterBuilder)

{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Investigate");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;

            case 3:
                character.Skills.Increase("Streetwise");
                return;

            case 4:
                character.Skills.Increase("Stealth");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 6:
                character.Skills.Increase("Deception");
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Probationary";
                return;

            case 1:
                careerHistory.Title = "Agent";
                character.Skills.Increase("Investigate");
                return;

            case 2:
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Field Agent";
                character.Skills.Increase("Streetwise");
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master Agent";
                character.Skills.Increase("Admin");
                return;
        }
    }
}
