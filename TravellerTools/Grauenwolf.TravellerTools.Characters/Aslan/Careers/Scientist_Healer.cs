namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Scientist_Healer(SpeciesCharacterBuilder speciesCharacterBuilder) : Scientist("Healer", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Science | CareerTypes.Healer | CareerTypes.Civilian;
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Medic", "Science", "Persuade", "Medic", "Electronics|Sensors", "Admin");
    }
}
