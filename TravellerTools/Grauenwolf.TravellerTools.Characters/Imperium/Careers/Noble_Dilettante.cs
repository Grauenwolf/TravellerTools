namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Noble_Dilettante(SpeciesCharacterBuilder speciesCharacterBuilder) : Noble("Dilettante", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Noble | CareerTypes.Civilian;
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 3;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Carouse", "Deception", "Flyer", "Streetwise", "Gambler", "Jack-of-All-Trades");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Wastrel";
                return;

            case 1:
                return;

            case 2:
                careerHistory.Title = "Ingrate";
                if (allowBonus)
                    character.Skills.Add("Carouse", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Black Sheep";
                if (allowBonus)
                    character.Skills.Add("Persuade", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Scoundrel";
                if (allowBonus)
                    character.Skills.Add("Jack-of-All-Trades", 1);
                return;
        }
    }
}
