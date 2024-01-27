namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinMilitary_SeaPatrol(SpeciesCharacterBuilder speciesCharacterBuilder) : DolphinMilitary("Sea Patrol", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Gun Combat", "Vacc Suit", "Athletics", "Electronics", "Gun Combat");
    }
}
