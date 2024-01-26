namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Marine_StarMarine(SpeciesCharacterBuilder speciesCharacterBuilder) : Marine("Star Marine", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Vacc Suit", "Athletics", "Gunner", "Melee|Blade", "Electronics", "Gun Combat");
    }
}
