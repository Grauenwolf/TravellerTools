namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Noble_Diplomat(CharacterBuilder characterBuilder) : Noble("Diplomat", characterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Advocate");
                return;

            case 2:
                character.Skills.Increase("Carouse");
                return;

            case 3:
                character.Skills.Increase("Electronics");
                return;

            case 4:
                character.Skills.Increase("Steward");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
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
                careerHistory.Title = "Intern";
                return;

            case 1:
                careerHistory.Title = "3rd  Secretary";
                character.Skills.Add("Admin", 1);
                return;

            case 2:
                careerHistory.Title = "2nd  Secretary";
                return;

            case 3:
                careerHistory.Title = "1st  Secretary";
                character.Skills.Add("Advocate", 1);
                return;

            case 4:
                careerHistory.Title = "Counsellor";
                return;

            case 5:
                careerHistory.Title = "Minister";
                character.Skills.Add("Diplomat", 1);
                return;

            case 6:
                careerHistory.Title = "Ambassador";
                return;
        }
    }
}
