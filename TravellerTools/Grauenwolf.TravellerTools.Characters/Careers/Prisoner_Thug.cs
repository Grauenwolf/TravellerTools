namespace Grauenwolf.TravellerTools.Characters.Careers;

class Prisoner_Thug(SpeciesCharacterBuilder speciesCharacterBuilder) : Prisoner("Thug", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Str";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Persuade", "Melee|Unarmed", "Melee|Unarmed", "Melee|Blade", "Athletics|Strength", "Athletics|Strength");
    }
}
