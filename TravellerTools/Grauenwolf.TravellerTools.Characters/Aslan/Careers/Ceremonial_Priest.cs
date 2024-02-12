namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Ceremonial_Priest(SpeciesCharacterBuilder speciesCharacterBuilder) : Ceremonial("Priest", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Religious;

    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Diplomat", "Persuade", "Tolerance", "Melee|Natural");
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
