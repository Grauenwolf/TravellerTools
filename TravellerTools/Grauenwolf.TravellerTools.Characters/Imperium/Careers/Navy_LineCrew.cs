namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Navy_LineCrew(SpeciesCharacterBuilder speciesCharacterBuilder) : Navy("Line/Crew", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Mechanic", "Gun Combat", "Flyer", "Melee", "Vacc Suit");
    }
}
