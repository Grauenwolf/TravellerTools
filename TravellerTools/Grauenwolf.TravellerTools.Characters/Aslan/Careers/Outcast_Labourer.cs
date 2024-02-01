namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Outcast_Labourer(SpeciesCharacterBuilder speciesCharacterBuilder) : Outcast("Labourer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Str";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Athletics", "Drive", "Streetwise", "Gun Combat", "Endurance", "Strength");
    }
}
