namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinMilitary_Guardian(SpeciesCharacterBuilder speciesCharacterBuilder) : DolphinMilitary("Guardian", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Melee", "Gun Combat", "Heavy Weapons", "Vacc Suit", "Tactics", "Medic");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        if (careerHistory.CommissionRank == 0)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    if (allowBonus)
                        character.Skills.Add("Melee", "Natural", 1);
                    return;

                case 1:
                    return;

                case 2:
                    if (allowBonus)
                        character.Endurance += 1;
                    return;

                case 3:
                    return;

                case 4:
                    if (allowBonus)
                        character.Strength += 1;
                    return;

                case 5:
                    return;

                case 6:
                    return;
            }
        }
        else
            base.TitleTable(character, careerHistory, dice, allowBonus);
    }
}
