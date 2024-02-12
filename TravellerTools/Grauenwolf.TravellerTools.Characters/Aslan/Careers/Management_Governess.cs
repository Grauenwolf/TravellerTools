namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Management_Governess(SpeciesCharacterBuilder speciesCharacterBuilder) : Management("Governess", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.StarportEmployee;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Persuade", "Streetwise", "Broker", "Steward", "Steward");
    }
}
