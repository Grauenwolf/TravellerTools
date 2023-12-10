namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Thug(CharacterBuilder characterBuilder) : Prisoner("Thug", characterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Str";

    protected override int SurvivalTarget => 8;

    protected override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Persuade");
                return;

            case 2:
                character.Skills.Increase("Melee", "Unarmed");
                return;

            case 3:
                character.Skills.Increase("Melee", "Unarmed");
                return;

            case 4:
                character.Skills.Increase("Melee", "Blade");
                return;

            case 5:
                character.Skills.Increase("Athletics", "Strength");
                return;

            case 6:
                character.Skills.Increase("Athletics", "Strength");
                return;
        }
    }
}
