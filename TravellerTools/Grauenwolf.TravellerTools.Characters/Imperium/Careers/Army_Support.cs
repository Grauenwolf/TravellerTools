namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Army_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Army("Army Support", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Mechanic", "Drive|Flyer", "Profession", "Explosives", "Electronics|Comms", "Medic");
    }
}
