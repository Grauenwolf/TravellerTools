namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class ShaperPriest_Ecclesiastic(SpeciesCharacterBuilder speciesCharacterBuilder) : ShaperPriest("Ecclesiastic", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Profession|Religion", "Science|Shaper Church", "Persuade", "Admin", "Leadership", "Deception");
    }
}
