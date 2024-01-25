namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Management_ClanAide(SpeciesCharacterBuilder speciesCharacterBuilder) : Management("Clan Aide", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Admin");
                return;

            case 2:
                character.Skills.Increase("Advocate");
                return;

            case 3:
                character.Skills.Increase("Melee", "Natural");
                return;

            case 4:
                character.Skills.Increase("Medic");
                return;

            case 5:
                character.Skills.Increase("Steward");
                return;

            case 6:
                character.Skills.Increase("Tolerance");
                return;
        }
    }
}
