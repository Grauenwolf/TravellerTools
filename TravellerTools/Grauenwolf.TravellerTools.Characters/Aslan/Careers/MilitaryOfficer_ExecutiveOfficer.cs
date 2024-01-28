namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class MilitaryOfficer_ExecutiveOfficer(SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryOfficer("Executive Officer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Medic", "Electronics", "Electronics", "Advocate", "Navigation");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                character.Title = "Probationary Lieutenant";
                return;

            case 1:
                character.Title = "Junior Lieutenant";
                if (allowBonus)
                    character.Skills.Add("Electronics", 1);
                return;

            case 2:
                character.Title = "Lieutenant";
                return;

            case 3:
                character.Title = "Captain";
                if (allowBonus)
                    character.Skills.Add("Admin", 1);
                return;

            case 4:
                character.Title = "Executive";
                return;

            case 5:
                character.Title = "Division Chief of Staff";
                return;

            case 6:
                character.Title = "Chief of Staff";
                if (allowBonus)
                    character.AddHistory("Gain 2 clan shares.", character.Age);
                return;
        }
    }
}
