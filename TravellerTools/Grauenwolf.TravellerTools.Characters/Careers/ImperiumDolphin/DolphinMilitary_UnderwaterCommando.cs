namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinMilitary_UnderwaterCommando(CharacterBuilder characterBuilder) : DolphinMilitary("Underwater Commando", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Heavy Weapon")));
                return;

            case 3:
                character.Skills.Increase("Explosives");
                return;

            case 4:
                character.Skills.Increase("Recon");
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;
        }
    }
}
