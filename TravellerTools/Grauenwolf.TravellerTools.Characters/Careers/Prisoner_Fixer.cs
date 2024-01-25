namespace Grauenwolf.TravellerTools.Characters.Careers;

class Prisoner_Fixer(CharacterBuilder characterBuilder) : Prisoner("Fixer", characterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 9;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Investigate");
                return;

            case 2:
                character.Skills.Increase("Broker");
                return;

            case 3:
                character.Skills.Increase("Deception");
                return;

            case 4:
                character.Skills.Increase("Streetwise");
                return;

            case 5:
                character.Skills.Increase("Stealth");
                return;

            case 6:
                character.Skills.Increase("Admin");
                return;
        }
    }
}
