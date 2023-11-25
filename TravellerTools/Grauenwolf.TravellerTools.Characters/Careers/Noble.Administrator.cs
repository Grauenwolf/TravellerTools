namespace Grauenwolf.TravellerTools.Characters.Careers;

class Administrator(Book book) : Noble("Administrator", book)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Admin");
                return;

            case 2:
                character.Skills.Increase("Advocate");
                return;

            case 3:
                character.Skills.Increase("Broker");
                return;

            case 4:
                character.Skills.Increase("Diplomat");
                return;

            case 5:
                character.Skills.Increase("Leadership");
                return;

            case 6:
                character.Skills.Increase("Persuade");
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Assistant";
                return;

            case 1:
                careerHistory.Title = "Clerk";
                character.Skills.Add("Admin", 1);
                return;

            case 2:
                careerHistory.Title = "Supervisor";
                return;

            case 3:
                careerHistory.Title = "Manager";
                character.Skills.Add("Advocate", 1);
                return;

            case 4:
                careerHistory.Title = "Chief";
                return;

            case 5:
                careerHistory.Title = "Director";
                character.Skills.Add("Leadership", 1);
                return;

            case 6:
                careerHistory.Title = "Minister";
                return;
        }
    }
}
