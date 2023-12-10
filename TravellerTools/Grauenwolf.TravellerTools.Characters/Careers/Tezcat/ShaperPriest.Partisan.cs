namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Partisan(CharacterBuilder characterBuilder) : ShaperPriest("Partisan", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Academic");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                return;

            case 4:
                character.Skills.Increase("Tactics", "Military");
                return;

            case 5:
                character.Skills.Increase("Recon");
                return;

            case 6:
                character.Skills.Increase("Stealth");
                return;
        }
    }

    protected override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        base.TitleTable(character, careerHistory, dice, allowBonus);
    }
}
