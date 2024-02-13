namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Wanderer_Nomad(SpeciesCharacterBuilder speciesCharacterBuilder) : Wanderer("Nomad", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.None;

    protected override string AdvancementAttribute => "";

    protected override int AdvancementTarget => ;

    protected override string SurvivalAttribute => "";

    protected override int SurvivalTarget => ;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "", "", "", "", "", "");
    }
}
