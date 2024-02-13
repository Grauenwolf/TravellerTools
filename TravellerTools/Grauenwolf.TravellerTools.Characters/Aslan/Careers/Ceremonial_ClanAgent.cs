namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Ceremonial_ClanAgent(SpeciesCharacterBuilder speciesCharacterBuilder) : Ceremonial("Clan Agent", speciesCharacterBuilder)

{
    public override CareerTypes CareerTypes => CareerTypes.Religious | CareerTypes.StarportEmployee | CareerTypes.Government;
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Investigate", "Gun Combat", "Streetwise", "Stealth", "Electronics", "Deception");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Probationary";
                return;

            case 1:
                careerHistory.Title = "Agent";
                if (allowBonus)
                    character.Skills.Increase("Investigate");
                return;

            case 2:
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Field Agent";
                if (allowBonus)
                    character.Skills.Increase("Streetwise");
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master Agent";
                if (allowBonus)
                    character.Skills.Increase("Admin");
                return;
        }
    }
}
