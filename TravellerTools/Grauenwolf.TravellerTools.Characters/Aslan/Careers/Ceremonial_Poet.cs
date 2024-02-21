namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Ceremonial_Poet(SpeciesCharacterBuilder speciesCharacterBuilder) : Ceremonial("Poet", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.ArtistOrPerformer | CareerTypes.Civilian;

    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Art", "Art", "Persuade", "Carouse", "Electronics", "Deception");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Supplicant";
                return;

            case 1:
                careerHistory.Title = "Acolyte";
                return;

            case 2:
                careerHistory.Title = "Initiate";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 3:
                careerHistory.Title = "Sojourner";
                return;

            case 4:
                careerHistory.Title = "Practitioner";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 5:
                careerHistory.Title = "Master";
                return;

            case 6:
                careerHistory.Title = "Grand Master";
                if (allowBonus)
                    character.Education += 1;
                return;
        }
    }
}
