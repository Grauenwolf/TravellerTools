namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Believer_MainstreamBeliever(CharacterBuilder characterBuilder) : Believer("Mainstream Believer", characterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 3;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Profession")));
                return;

            case 2:
                character.Skills.Increase("Profession", "Religion");
                return;

            case 3:
                character.Skills.Increase("Science", "Belief");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Drive")));
                return;

            case 5:
                character.Skills.Increase("Persuade");
                return;

            case 6:
                character.Skills.Increase("Admin");
                return;
        }
    }

    protected override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Lay Person";
                return;

            case 1:
                careerHistory.Title = "Initiate";
                if (allowBonus)
                    character.Skills.Increase("Science", "Belief");
                return;

            case 2:
                careerHistory.Title = "Lay Preacher";
                if (allowBonus)
                    character.Skills.Increase("Persuade");
                return;

            case 3:
                careerHistory.Title = "Priest";
                return;

            case 4:
                careerHistory.Title = "Senior Priest";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 5:
                careerHistory.Title = "Bishop";
                return;

            case 6:
                careerHistory.Title = "Archbishop";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }
}
