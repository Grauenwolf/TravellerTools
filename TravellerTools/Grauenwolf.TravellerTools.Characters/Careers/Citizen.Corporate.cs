namespace Grauenwolf.TravellerTools.Characters.Careers;

class Corporate(Book book) : Citizen("Corporate", book)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Advocate");
                return;

            case 2:
                character.Skills.Increase("Admin");
                return;

            case 3:
                character.Skills.Increase("Broker");
                return;

            case 4:
                character.Skills.Increase("Electronics", "Computer");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
                return;

            case 6:
                character.Skills.Increase("Leadership");
                return;
        }
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Advocate");
        if (all || roll == 2)
            character.Skills.Add("Admin");
        if (all || roll == 3)
            character.Skills.Add("Broker");
        if (all || roll == 4)
            character.Skills.Add("Electronics");
        if (all || roll == 5)
            character.Skills.Add("Diplomat");
        if (all || roll == 6)
            character.Skills.Add("Leadership");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Manager";
                character.Skills.Add("Admin", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Senior Manager";
                character.Skills.Add("Advocate", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Director";
                character.SocialStanding += 1;
                return;
        }
    }
}
