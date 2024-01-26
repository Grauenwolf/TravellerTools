namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Believer_Missionary(SpeciesCharacterBuilder speciesCharacterBuilder) : Believer("Missionary/Humanitarian", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Jack-of-All-Trades", "Medic", "Persuade", "Diplomacy", "Carouse", "Leadership");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Junior Project Worker";
                return;

            case 1:
                careerHistory.Title = "Project Worker";
                if (allowBonus)
                    character.Skills.Increase("Jack-of-All-Trades");
                return;

            case 2:
                careerHistory.Title = "Team Leader";
                if (allowBonus)
                    character.Skills.Increase("Leadership");
                return;

            case 3:
                careerHistory.Title = "Project Leader";
                return;

            case 4:
                careerHistory.Title = "Project Coordinator";
                if (allowBonus)
                    character.Skills.Increase("Admin");
                return;

            case 5:
                careerHistory.Title = "Department Director";
                return;

            case 6:
                careerHistory.Title = "Director";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }
}
