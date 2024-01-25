namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Management_Corporate(SpeciesCharacterBuilder speciesCharacterBuilder) : Management("Corporate", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Admin");
                return;

            case 2:
                character.Skills.Increase("Broker");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Profession")));
                return;

            case 4:
                character.Skills.Increase("Deception");
                return;

            case 5:
                character.Skills.Increase("Streetwise");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;
        }
    }
}
