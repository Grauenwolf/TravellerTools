namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Outlaw_Thief(SpeciesCharacterBuilder speciesCharacterBuilder) : Outlaw("Outlaw Thief", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Broker", "Stealth", "Streetwise", "Deception", "Electronics", "Mechanic");
    }
}
