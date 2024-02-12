namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Citizen_Colonist(SpeciesCharacterBuilder speciesCharacterBuilder) : Citizen("Colonist", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.StarportEmployee;

    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Animals", "Athletics", "Jack-of-All-Trades", "Drive", "Survival", "Recon");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Animals", "Athletics", "Drive", "Survival", "Recon", null);
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Settler";
                if (allowBonus)
                    character.Skills.Add("Survival", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Explorer";
                if (allowBonus)
                    character.Skills.Add("Navigation", 1);
                return;

            case 5:
                return;

            case 6:
                if (allowBonus)
                    AddOneSkill(character, dice, "Gun Combat");
                return;
        }
    }
}
