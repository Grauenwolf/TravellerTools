namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Soulhunter_Flight(SpeciesCharacterBuilder speciesCharacterBuilder) : Soulhunter("Soulhunter Flight", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot", "Astrogation", "Gunner", "Electronics", "Flyer", "Vacc Suit");
    }
}
