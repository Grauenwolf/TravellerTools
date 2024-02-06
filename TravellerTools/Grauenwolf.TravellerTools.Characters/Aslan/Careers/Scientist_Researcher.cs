namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Scientist_Researcher(SpeciesCharacterBuilder speciesCharacterBuilder) : Scientist("Researcher", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Science", "Electronics", "Engineer", "Investigate", "Admin", "Recon");
    }
}
