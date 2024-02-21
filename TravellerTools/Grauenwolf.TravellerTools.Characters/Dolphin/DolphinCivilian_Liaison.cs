namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinCivilian_Liaison(SpeciesCharacterBuilder speciesCharacterBuilder) : DolphinCivilian("Liaison", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Spy | CareerTypes.Government | CareerTypes.Civilian;
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Vacc Suit", "Vacc Suit", "Carouse", "Electronics", "Diplomat", "Persuade");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Representative";
                if (allowBonus)
                    character.Skills.Add("Diplomat", 1);
                return;

            case 3:
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 4:
                careerHistory.Title = "Ambassador";
                if (allowBonus)
                    character.Skills.Add("Vacc Suit", 1);
                return;

            case 5:
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 6:
                return;
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck) => true;
}
