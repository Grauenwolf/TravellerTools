namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Citizen_Worker(SpeciesCharacterBuilder speciesCharacterBuilder) : Citizen("Worker", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.StarportEmployee;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Drive", "Mechanic", "Electronics", "Engineer", "Profession", "Science");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Drive", "Mechanic", "Electronics", "Engineer", "Profession", "Science");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Technician";
                if (allowBonus)
                    AddOneSkill(character, dice, "Profession");
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Craftsman";
                if (allowBonus)
                    character.Skills.Add("Mechanic", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master Technician";
                if (allowBonus)
                    AddOneSkill(character, dice, "Engineer");
                return;
        }
    }
}
