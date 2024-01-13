namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Soulhunter_Commando(CharacterBuilder characterBuilder) : Soulhunter("Commando", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Vacc Suit");
                return;

            case 2:
                character.Skills.Increase("Stealth");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gunner")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Melee")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));

                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;
        }
    }
}
