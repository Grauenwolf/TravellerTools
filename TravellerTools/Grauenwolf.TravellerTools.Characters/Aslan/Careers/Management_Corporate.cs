namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Management_Corporate(SpeciesCharacterBuilder speciesCharacterBuilder) : Management("Corporate", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.StarportEmployee | CareerTypes.Corporate;

    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Broker", "Profession", "Deception", "Streetwise", "Electronics");
    }
}
