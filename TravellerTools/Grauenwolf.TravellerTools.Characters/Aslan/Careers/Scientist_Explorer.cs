namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Scientist_Explorer(SpeciesCharacterBuilder speciesCharacterBuilder) : Scientist("Explorer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Navigation", "Pilot", "Science", "Recon", "Survival", "Drive,Flyer");
    }
}
