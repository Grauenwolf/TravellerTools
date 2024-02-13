namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Envoy_Spy(SpeciesCharacterBuilder speciesCharacterBuilder) : Envoy("Spy", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Spy | CareerTypes.Diplomat | CareerTypes.Government;
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Persuade", "Investigate", "Deception", "Stealth", "Electronics", "Diplomat");
    }
}
