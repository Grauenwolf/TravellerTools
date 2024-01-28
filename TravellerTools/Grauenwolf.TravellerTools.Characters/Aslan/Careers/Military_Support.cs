namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class Military_Support(SpeciesCharacterBuilder speciesCharacterBuilder) : Military("Support", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Medic", "Mechanic", "Electronics", "Navigation", "Admin", "Gun Combat");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                character.Title = "Recruit-Technician";
                return;

            case 1:
                character.Title = "Soldier-Technician";
                if (allowBonus)
                    character.Skills.Add("Mechanic", 1);
                return;

            case 2:
                character.Title = "Blooded Soldier-Technician";
                return;

            case 3:
                character.Title = "Warsister";
                if (allowBonus)
                    character.Education += 1;
                return;

            case 4:
                character.Title = "Veteran Warsister";
                return;

            case 5:
                character.Title = "Master Technician";
                return;

            case 6:
                character.Title = "Honoured Master Technician";
                if (allowBonus)
                    character.AddHistory("Gain 2 clan shares.", character.Age);
                return;
        }
    }
}
