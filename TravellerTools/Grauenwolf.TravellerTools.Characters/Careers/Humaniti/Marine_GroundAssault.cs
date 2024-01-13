namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Marine_GroundAssault(CharacterBuilder characterBuilder) : Marine("Ground Assault", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Vacc Suit");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Heavy Weapons")));
                return;

            case 3:
                character.Skills.Increase("Recon");
                return;

            case 4:
                character.Skills.Increase("Melee", "Blade");
                return;

            case 5:
                character.Skills.Increase("Tactics", "Military");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;
        }
    }
}
