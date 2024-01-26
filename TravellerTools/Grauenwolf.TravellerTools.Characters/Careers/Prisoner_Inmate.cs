namespace Grauenwolf.TravellerTools.Characters.Careers;

class Prisoner_Inmate(SpeciesCharacterBuilder speciesCharacterBuilder) : Prisoner("Inmate", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Stealth", "Melee|Unarmed", "Streetwise", "Survival", "Athletics|Strength", "Mechanic");
    }
}
