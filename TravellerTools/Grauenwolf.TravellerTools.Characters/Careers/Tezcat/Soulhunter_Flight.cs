namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Soulhunter_Flight(CharacterBuilder characterBuilder) : Soulhunter("Soulhunter Flight", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
                return;

            case 2:
                character.Skills.Increase("Astrogation");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));

                return;

            case 6:
                character.Skills.Increase("Vacc Suit");
                return;
        }
    }
}
