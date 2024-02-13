namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Agent_Intelligence(SpeciesCharacterBuilder speciesCharacterBuilder) : Agent("Intelligence", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Spy | CareerTypes.Government;
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Investigate", "Recon", "Electronics|Comms", "Stealth", "Persuade", "Deception");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                careerHistory.Title = "Agent";
                if (allowBonus)
                    character.Skills.Add("Deception", 1);
                return;

            case 2:
                careerHistory.Title = "Field Agent";
                if (allowBonus)
                    character.Skills.Add("Investigate", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Special Agent";
                if (allowBonus)
                    AddOneSkill(character, dice, "Gun Combat");

                return;

            case 5:
                careerHistory.Title = "Assistant Director";
                return;

            case 6:
                careerHistory.Title = "Director";
                return;
        }
    }
}
