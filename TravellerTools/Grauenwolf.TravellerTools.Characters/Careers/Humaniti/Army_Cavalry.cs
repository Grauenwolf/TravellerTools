namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Army_Cavalry(SpeciesCharacterBuilder speciesCharacterBuilder) : Army("Cavalry", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Mechanic", "Drive", "Flyer", "Recon", "Heavy Weapons|Vehicle", "Electronics|Sensors");
    }
}
