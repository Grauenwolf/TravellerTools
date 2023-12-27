namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Navy_EngineerGunner(CharacterBuilder characterBuilder) : Navy("Engineer/Gunner", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 2:
                character.Skills.Increase("Mechanic");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));
                return;
        }
    }
}
