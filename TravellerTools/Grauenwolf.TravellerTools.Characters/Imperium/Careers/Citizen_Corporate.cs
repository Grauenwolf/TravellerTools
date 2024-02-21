namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Citizen_Corporate(SpeciesCharacterBuilder speciesCharacterBuilder) : Citizen("Corporate", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.StarportEmployee | CareerTypes.Corporate | CareerTypes.Civilian;

    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Admin", "Broker", "Electronics|Computer", "Diplomat", "Leadership");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Advocate", "Admin", "Broker", "Electronics|Computer", "Diplomat", "Leadership");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Manager";
                if (allowBonus)
                    character.Skills.Add("Admin", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Senior Manager";
                if (allowBonus)
                    character.Skills.Add("Advocate", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Director";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }
}
