namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Marine_GroundAssault(SpeciesCharacterBuilder speciesCharacterBuilder) : Marine("Ground Assault", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Vacc Suit", "Heavy Weapons", "Recon", "Melee|Blade", "Tactics|Military", "Gun Combat");
    }
}
