namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Entertainer_Artist(SpeciesCharacterBuilder speciesCharacterBuilder) : Entertainer("Artist", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Art", "Carouse", "Electronics|Comms", "Gambler", "Persuade", "Profession");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    AddOneSkill(character, dice, "Art");
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    character.Skills.Add("Investigate", 1);
                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Famous Artist";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 6:
                return;
        }
    }
}
