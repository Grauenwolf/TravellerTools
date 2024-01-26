namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Envoy_Negotiator(SpeciesCharacterBuilder speciesCharacterBuilder) : Envoy("Negotiator", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Persuade", "Tolerance", "Diplomat", "Deception", "Diplomat", "Art");
    }
}
