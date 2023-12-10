namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Infantry(CharacterBuilder characterBuilder) : Army("Infantry", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Str";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Heavy Weapons")));
                return;

            case 4:
                character.Skills.Increase("Stealth");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));

                return;

            case 6:
                character.Skills.Increase("Recon");
                return;
        }
    }
}
