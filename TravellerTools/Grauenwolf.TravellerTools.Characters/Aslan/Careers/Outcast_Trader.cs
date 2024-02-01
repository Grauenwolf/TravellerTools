namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Outcast_Trader(SpeciesCharacterBuilder speciesCharacterBuilder) : Outcast("Trader", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Broker", "Streetwise", "Admin", "Profession", "Electronics", "Intellect");
    }
}
