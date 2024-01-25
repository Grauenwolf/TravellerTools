namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Scout_Courier(SpeciesCharacterBuilder speciesCharacterBuilder) : Scout("Courier", speciesCharacterBuilder)
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Flyer")));
                return;

            case 3:
                character.Skills.Increase("Pilot", "Spacecraft");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Engineer")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;

            case 6:
                character.Skills.Increase("Astrogation");
                return;
        }
    }
}
