namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Envoy_Duellist(SpeciesCharacterBuilder speciesCharacterBuilder) : Envoy("Duellist", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Tolerance", "Melee|Natural", "Dexterity", "Strength", "Endurance", "Melee");
    }
}
