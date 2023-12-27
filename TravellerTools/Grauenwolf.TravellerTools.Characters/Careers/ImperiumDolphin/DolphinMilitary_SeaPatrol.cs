namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinMilitary_SeaPatrol(CharacterBuilder characterBuilder) : DolphinMilitary("Sea Patrol", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Advocate");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;

            case 3:
                character.Skills.Increase("Vacc Suit");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;
        }
    }
}
