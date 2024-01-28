namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Military_Cavalry(SpeciesCharacterBuilder speciesCharacterBuilder) : Military("Cavalry", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Drive", "Gunner|Tturret ", "Heavy Weapons", "Mechanic", "Gun Combat", "Drive");
    }
}
