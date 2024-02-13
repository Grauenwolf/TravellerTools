namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Management_ClanAide(SpeciesCharacterBuilder speciesCharacterBuilder) : Management("Clan Aide", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.StarportEmployee;
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Melee|Natural", "Medic", "Steward", "Tolerance");
    }
}
