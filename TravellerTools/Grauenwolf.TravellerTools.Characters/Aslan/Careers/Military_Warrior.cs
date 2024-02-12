namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Military_Warrior(SpeciesCharacterBuilder speciesCharacterBuilder) : Military("Warrior", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.StarportOfficer | CareerType.Military;

    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Vacc Suit", "Gun Combat", "Heavy Weapons", "Recon", "Stealth", "Athletics");
    }
}
