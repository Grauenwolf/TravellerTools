namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Envoy_Spy(SpeciesCharacterBuilder speciesCharacterBuilder) : Envoy("Spy", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Persuade");
                return;

            case 2:
                character.Skills.Increase("Investigate");
                return;

            case 3:
                character.Skills.Increase("Deception");
                return;

            case 4:
                character.Skills.Increase("Stealth");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 6:
                character.Skills.Increase("Diplomat");
                return;
        }
    }


}
