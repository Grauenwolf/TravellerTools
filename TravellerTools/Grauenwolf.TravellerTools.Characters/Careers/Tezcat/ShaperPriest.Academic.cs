namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Academic(CharacterBuilder characterBuilder) : ShaperPriest("Academic", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 4;

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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;

            case 5:
                character.Skills.Increase("Electronics", "Computer");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                return;
        }
    }
}
