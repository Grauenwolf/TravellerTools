namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Envoy_Negotiator(CharacterBuilder characterBuilder) : Envoy("Negotiator", characterBuilder)
{

    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Persuade");
                return;

            case 2:
                character.Skills.Increase("Tolerance");
                return;

            case 3:
                character.Skills.Increase("Diplomat");
                return;

            case 4:
                character.Skills.Increase("Deception");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Art")));
                return;
        }
    }


}
