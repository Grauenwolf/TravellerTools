namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Scout_Explorer(SpeciesCharacterBuilder speciesCharacterBuilder) : Scout("Explorer", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Science | CareerType.FieldScience | CareerType.Military;

    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Pilot", "Engineer", "Science", "Stealth", "Recon");
    }
}
