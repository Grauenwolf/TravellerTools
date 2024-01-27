namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Navy_EngineerGunner(SpeciesCharacterBuilder speciesCharacterBuilder) : Navy("Engineer/Gunner", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Engineer", "Mechanic", "Electronics", "Engineer", "Gunner", "Flyer");
    }
}
