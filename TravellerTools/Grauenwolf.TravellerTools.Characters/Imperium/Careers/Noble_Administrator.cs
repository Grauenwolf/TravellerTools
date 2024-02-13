namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Noble_Administrator(SpeciesCharacterBuilder speciesCharacterBuilder) : Noble("Administrator", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.StarportEmployee | CareerTypes.Government | CareerTypes.Noble;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Broker", "Diplomat", "Leadership", "Persuade");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Assistant";
                return;

            case 1:
                careerHistory.Title = "Clerk";
                if (allowBonus)
                    character.Skills.Add("Admin", 1);
                return;

            case 2:
                careerHistory.Title = "Supervisor";
                return;

            case 3:
                careerHistory.Title = "Manager";
                if (allowBonus)
                    character.Skills.Add("Advocate", 1);
                return;

            case 4:
                careerHistory.Title = "Chief";
                return;

            case 5:
                careerHistory.Title = "Director";
                if (allowBonus)
                    character.Skills.Add("Leadership", 1);
                return;

            case 6:
                careerHistory.Title = "Minister";
                return;
        }
    }
}
