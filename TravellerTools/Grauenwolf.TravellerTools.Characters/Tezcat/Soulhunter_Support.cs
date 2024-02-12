namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Soulhunter_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Soulhunter("Soulhunter Support", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.StarportOfficer | CareerType.Military;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Mechanic", "Engineer", "Medic", "Vacc Suit", "Explosives");
    }
}
