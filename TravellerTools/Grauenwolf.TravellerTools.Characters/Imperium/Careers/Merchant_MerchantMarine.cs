namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Merchant_MerchantMarine(SpeciesCharacterBuilder speciesCharacterBuilder) : Merchant("Merchant Marine", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot", "Vacc Suit", "Athletics", "Mechanic", "Engineer", "Electronics");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Crewman";
                return;

            case 1:
                careerHistory.Title = "Senior Crewman";
                if (allowBonus)
                    character.Skills.Add("Mechanic", 1);
                return;

            case 2:
                careerHistory.Title = "4th  Officer";
                return;

            case 3:
                careerHistory.Title = "3rd  Officer";
                return;

            case 4:
                careerHistory.Title = "2nd  Officer";
                if (allowBonus)
                    AddOneSkill(character, dice, "Pilot");

                return;

            case 5:
                careerHistory.Title = "1st  Officer";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 6:
                careerHistory.Title = "Captain";
                return;
        }
    }
}
