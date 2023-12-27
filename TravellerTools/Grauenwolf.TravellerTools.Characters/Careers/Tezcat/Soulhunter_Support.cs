namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class Soulhunter_Support(CharacterBuilder characterBuilder) : Soulhunter("Soulhunter Support", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 2:
                character.Skills.Increase("Mechanic");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 4:
                character.Skills.Increase("Medic");
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase("Explosives");
                return;
        }
    }
}
