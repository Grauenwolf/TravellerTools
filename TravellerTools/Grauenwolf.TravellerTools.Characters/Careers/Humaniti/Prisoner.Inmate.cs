namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Inmate(CharacterBuilder characterBuilder) : Prisoner("Inmate", characterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    protected override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Stealth");
                return;

            case 2:
                character.Skills.Increase("Melee", "Unarmed");
                return;

            case 3:
                character.Skills.Increase("Streetwise");
                return;

            case 4:
                character.Skills.Increase("Survival");
                return;

            case 5:
                character.Skills.Increase("Athletics", "Strength");
                return;

            case 6:
                character.Skills.Increase("Mechanic");
                return;
        }
    }
}
