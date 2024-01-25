namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class ShaperPriest_Ecclesiastic(SpeciesCharacterBuilder speciesCharacterBuilder) : ShaperPriest("Ecclesiastic", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Profession", "Religion");
                return;

            case 2:
                character.Skills.Increase("Science", "Shaper Church");
                return;

            case 3:
                character.Skills.Increase("Persuade");
                return;

            case 4:
                character.Skills.Increase("Admin");
                return;

            case 5:
                character.Skills.Increase("Leadership");
                return;

            case 6:
                character.Skills.Increase("Deception");
                return;
        }
    }
}
