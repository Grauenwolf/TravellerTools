namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Outcast_Scavenger(SpeciesCharacterBuilder speciesCharacterBuilder) : Outcast("Outcast Scavenger", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.LegalGoodsTrader | CareerType.ShadyGoodsTrader;

    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Mechanic", "Streetwise", "Drive,Flyer", "Engineer", "Independence", "Endurance");
    }
}
