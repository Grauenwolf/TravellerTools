namespace Grauenwolf.TravellerTools.Characters.Careers;

class Surveyor(Book book) : Scout("Surveyor", book)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 2:
                character.Skills.Increase("Persuade");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));
                return;

            case 4:
                character.Skills.Increase("Navigation");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
                return;

            case 6:
                character.Skills.Increase("Streetwise");
                return;
        }
    }
}
