namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Scout_Surveyor(SpeciesCharacterBuilder speciesCharacterBuilder) : Scout("Surveyor", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Science | CareerTypes.FieldScience | CareerTypes.Military | CareerTypes.Scout | CareerTypes.Violent;
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Persuade", "Flyer", "Navigation", "Diplomat", "Streetwise");
    }
}
