namespace Grauenwolf.TravellerTools.Characters.Careers;

class FreeTrader(CharacterBuilder characterBuilder) : Merchant("Free Trader", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Pilot", "Spacecraft");

                return;

            case 2:
                character.Skills.Increase("Vacc Suit");
                return;

            case 3:
                character.Skills.Increase("Deception");
                return;

            case 4:
                character.Skills.Increase("Mechanic");
                return;

            case 5:
                character.Skills.Increase("Streetwise");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
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
                careerHistory.Title = "Experienced Trader";
                character.Skills.Add("Jack-of-all-Trades", 1);
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
