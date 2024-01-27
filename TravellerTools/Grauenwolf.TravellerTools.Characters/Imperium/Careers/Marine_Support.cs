namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Marine_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Marine("Marine Support", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Mechanic", "Drive|Flyer", "Medic", "Heavy Weapons", "Gun Combat");
    }
}
