namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Merchant_Broker(SpeciesCharacterBuilder speciesCharacterBuilder) : Merchant("Broker", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Broker", "Streetwise", "Deception", "Persuade");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    character.Skills.Add("Broker", 1);
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Experienced Broker";
                if (allowBonus)
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
