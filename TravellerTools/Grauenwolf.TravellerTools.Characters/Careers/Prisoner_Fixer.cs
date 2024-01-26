namespace Grauenwolf.TravellerTools.Characters.Careers;

class Prisoner_Fixer(SpeciesCharacterBuilder speciesCharacterBuilder) : Prisoner("Fixer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 9;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Investigate", "Broker", "Deception", "Streetwise", "Stealth", "Admin");
    }
}
