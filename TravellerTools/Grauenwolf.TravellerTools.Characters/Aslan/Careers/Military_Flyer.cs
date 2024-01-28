namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Military_Flyer(SpeciesCharacterBuilder speciesCharacterBuilder) : Military("Flyer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Flyer", "Gunner|Turret", "Electronics", "Pilot|Small Craft", "Gun Combat", "Flyer");
    }
}
