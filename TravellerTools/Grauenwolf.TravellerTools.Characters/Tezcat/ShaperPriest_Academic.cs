namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class ShaperPriest_Academic(SpeciesCharacterBuilder speciesCharacterBuilder) : ShaperPriest("Academic", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Science", "Science", "Electronics|Computer", "Science");
    }
}
