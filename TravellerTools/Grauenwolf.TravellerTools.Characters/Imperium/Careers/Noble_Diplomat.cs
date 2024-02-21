namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Noble_Diplomat(SpeciesCharacterBuilder speciesCharacterBuilder) : Noble("Diplomat", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.StarportEmployee | CareerTypes.Diplomat | CareerTypes.Government | CareerTypes.Noble | CareerTypes.Spy | CareerTypes.Civilian;

    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Carouse", "Electronics", "Steward", "Diplomat", "Deception");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Intern";
                return;

            case 1:
                careerHistory.Title = "3rd  Secretary";
                if (allowBonus)
                    character.Skills.Add("Admin", 1);
                return;

            case 2:
                careerHistory.Title = "2nd  Secretary";
                return;

            case 3:
                careerHistory.Title = "1st  Secretary";
                if (allowBonus)
                    character.Skills.Add("Advocate", 1);
                return;

            case 4:
                careerHistory.Title = "Counsellor";
                return;

            case 5:
                careerHistory.Title = "Minister";
                if (allowBonus)
                    character.Skills.Add("Diplomat", 1);
                return;

            case 6:
                careerHistory.Title = "Ambassador";
                return;
        }
    }
}
