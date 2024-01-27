namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Army_Infantry(SpeciesCharacterBuilder speciesCharacterBuilder) : Army("Infantry", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Str";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Gun Combat", "Melee", "Heavy Weapons", "Stealth", "Athletics", "Recon");
    }
}
