namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Merchant_FreeTrader(SpeciesCharacterBuilder speciesCharacterBuilder) : Merchant("Free Trader", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.LegalGoodsTrader | CareerTypes.ShadyGoodsTrader | CareerTypes.FreeTrader;
    protected override string AdvancementAttribute => "Int";
    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot|Spacecraft", "Vacc Suit", "Deception", "Mechanic", "Streetwise", "Gunner");
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
                careerHistory.Title = "Experienced Trader";
                if (allowBonus)
                    character.Skills.Add("Jack-of-All-Trades", 1);
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
