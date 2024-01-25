namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Envoy_Duellist(CharacterBuilder characterBuilder) : Envoy("Duellist", characterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Tolerance");
                return;

            case 2:
                character.Skills.Increase("Melee", "Natural");
                return;

            case 3:
                character.Dexterity += 1;
                return;

            case 4:
                character.Strength += 1;
                return;

            case 5:
                character.Endurance += 1;
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Melee")));
                return;
        }
    }
}
