namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Ceremonial_Poet(CharacterBuilder characterBuilder) : Ceremonial("Poet", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Art")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Art")));
                return;

            case 3:
                character.Skills.Increase("Persuade");
                return;

            case 4:
                character.Skills.Increase("Carouse");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 6:
                character.Skills.Increase("Deception");
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Supplicant";
                return;

            case 1:
                careerHistory.Title = "Acolyte";
                return;

            case 2:
                careerHistory.Title = "Initiate";
                character.SocialStanding += 1;
                return;

            case 3:
                careerHistory.Title = "Sojourner";
                return;

            case 4:
                careerHistory.Title = "Practitioner";
                character.SocialStanding += 1;
                return;

            case 5:
                careerHistory.Title = "Master";
                return;

            case 6:
                careerHistory.Title = "Grand Master";
                character.Education += 1;
                return;
        }
    }
}
