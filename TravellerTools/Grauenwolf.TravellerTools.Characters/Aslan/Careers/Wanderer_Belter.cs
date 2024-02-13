namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Wanderer_Belter(SpeciesCharacterBuilder speciesCharacterBuilder) : Wanderer("Belter", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Belter;

    protected override string AdvancementAttribute => "";

    protected override int AdvancementTarget => ;

    protected override string SurvivalAttribute => "";

    protected override int SurvivalTarget => ;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "", "", "", "", "", "");
    }
}
