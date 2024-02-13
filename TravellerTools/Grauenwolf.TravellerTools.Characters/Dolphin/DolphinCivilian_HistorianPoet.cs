namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinCivilian_HistorianPoet(SpeciesCharacterBuilder speciesCharacterBuilder) : DolphinCivilian("Historian Poet", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Science;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Science|History", "Art", "Art", "Advocate", "Persuade", "Diplomat");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Candidate";
                if (allowBonus)
                    character.Skills.Add("Art", "Performer", 1);
                return;

            case 1:
                careerHistory.Title = "Apprentice";
                if (allowBonus)
                    character.Skills.Add("Science", "History", 1);
                return;

            case 2:
                if (allowBonus)
                {
                    if (character.SocialStanding < 6)
                        character.SocialStanding = 6;
                    else
                        character.SocialStanding += 1;
                }
                return;

            case 3:
                careerHistory.Title = "Journeyman";
                return;

            case 4:
                if (allowBonus)
                {
                    if (character.SocialStanding < 8)
                        character.SocialStanding = 8;
                    else
                        character.SocialStanding += 1;
                }
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master";
                if (allowBonus)
                {
                    if (character.SocialStanding < 10)
                        character.SocialStanding = 10;
                    else
                        character.SocialStanding += 1;
                }
                return;
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EducationDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 8, isPrecheck);
    }
}
