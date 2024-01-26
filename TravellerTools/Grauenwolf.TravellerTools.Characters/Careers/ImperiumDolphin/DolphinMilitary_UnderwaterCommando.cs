namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinMilitary_UnderwaterCommando(SpeciesCharacterBuilder speciesCharacterBuilder) : DolphinMilitary("Underwater Commando", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Gun Combat", "Heavy Weapon", "Explosives", "Recon", "Vacc Suit", "Athletics");
    }
}
