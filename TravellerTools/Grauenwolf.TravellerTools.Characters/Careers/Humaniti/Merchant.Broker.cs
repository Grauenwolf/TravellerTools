namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Broker(CharacterBuilder characterBuilder) : Merchant("Broker", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 5;

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
                character.Skills.Increase("Streetwise");
                return;

            case 5:
                character.Skills.Increase("Deception");
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
                return;

            case 1:
                character.Skills.Add("Broker", 1);
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Experienced Broker";
                character.Skills.Add("Streetwise", 1);
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                return;
        }
    }
}
