namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Management_Governess(SpeciesCharacterBuilder speciesCharacterBuilder) : Management("Governess", speciesCharacterBuilder) {



    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Admin");
                return;

            case 2:
                character.Skills.Increase("Persuade");
                return;

            case 3:
                character.Skills.Increase("Streetwise");
                return;

            case 4:
                character.Skills.Increase("Broker");
                return;

            case 5:
                character.Skills.Increase("Steward");
                return;

            case 6:
                character.Skills.Increase("Steward");
                return;
        }
    }



}
