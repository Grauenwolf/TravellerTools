namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Spacer_Pilot(SpeciesCharacterBuilder speciesCharacterBuilder) : Spacer("Pilot", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "";

    protected override int AdvancementTarget => ;

    protected override string SurvivalAttribute => "";

    protected override int SurvivalTarget => ;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "", "", "", "", "", "");
    }
}
