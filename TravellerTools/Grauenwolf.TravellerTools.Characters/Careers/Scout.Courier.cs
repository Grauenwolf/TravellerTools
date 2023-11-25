namespace Grauenwolf.TravellerTools.Characters.Careers;

class Courier(Book book) : Scout("Courier", book)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 9;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));
                return;

            case 3:
                character.Skills.Increase("Pilot", "Spacecraft");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                return;

            case 6:
                character.Skills.Increase("Astrogation");
                return;
        }
    }
}
