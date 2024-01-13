namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Believer_HolyWarrior(CharacterBuilder characterBuilder) : Believer("Holy Warrior", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Melee")));
                return;

            case 3:
                character.Skills.Increase("Explosives");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Heavy Weapons")));
                return;

            case 5:
                character.Skills.Increase("Tactics", "Military");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;
        }
    }

    protected override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Hopeful";
                return;

            case 1:
                careerHistory.Title = "Fighter";
                if (allowBonus)
                    character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;

            case 2:
                careerHistory.Title = "Combat Leader";
                if (allowBonus)
                    character.Skills.Increase("Leadership");
                return;

            case 3:
                careerHistory.Title = "Force Commander";
                return;

            case 4:
                careerHistory.Title = "Area Commander";
                if (allowBonus)
                    character.Skills.Increase("Tactics", "Military");
                return;

            case 5:
                careerHistory.Title = "Movement Sub-Leader";
                return;

            case 6:
                careerHistory.Title = "Movement Leader";
                return;
        }
    }
}
