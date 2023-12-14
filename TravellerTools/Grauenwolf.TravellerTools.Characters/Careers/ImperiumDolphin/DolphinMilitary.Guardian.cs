namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class Guardian(CharacterBuilder characterBuilder) : DolphinMilitary("Guardian", characterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Heavy Weapons")));
                return;

            case 4:
                character.Skills.Increase("Vacc Suit");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Tactics")));
                return;

            case 6:
                character.Skills.Increase("Medic");
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        if (careerHistory.CommissionRank == 0)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    character.Skills.Add("Melee", "Natural", 1);
                    return;

                case 1:
                    return;

                case 2:
                    character.Endurance += 1;
                    return;

                case 3:
                    return;

                case 4:
                    character.Strength += 1;
                    return;

                case 5:
                    return;

                case 6:
                    return;
            }
        }
        else
            base.TitleTable(character, careerHistory, dice);
    }
}
