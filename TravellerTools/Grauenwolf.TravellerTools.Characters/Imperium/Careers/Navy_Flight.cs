namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Navy_Flight(SpeciesCharacterBuilder speciesCharacterBuilder) : Navy("Flight", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot", "Flyer", "Gunner", "Pilot|Small Craft", "Astrogation", "Electronics");
    }
}
