namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Soulhunter_Commando(SpeciesCharacterBuilder speciesCharacterBuilder) : Soulhunter("Commando", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.StarportOfficer | CareerType.Military;

    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Vacc Suit", "Stealth", "Gunner", "Melee", "Electronics", "Gun Combat");
    }
}
