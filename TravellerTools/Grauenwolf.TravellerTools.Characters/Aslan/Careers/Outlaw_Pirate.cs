namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Outlaw_Pirate(SpeciesCharacterBuilder speciesCharacterBuilder) : Outlaw("Pirate", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot", "Engineer", "Gunner", "Mechanic", "Athletics|Dexterity", "Vacc Suit");
    }
}
