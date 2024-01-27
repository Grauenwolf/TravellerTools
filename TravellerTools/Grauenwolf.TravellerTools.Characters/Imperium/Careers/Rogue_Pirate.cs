namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Rogue_Pirate(SpeciesCharacterBuilder speciesCharacterBuilder) : Rogue("Pirate", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot", "Astrogation", "Gunner", "Engineer", "Vacc Suit", "Melee");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Lackey";
                return;

            case 1:
                careerHistory.Title = "Henchman";
                if (allowBonus)
                    AddOneSkill(character, dice, "Pilot", "Gunner");
                return;

            case 2:
                careerHistory.Title = "Corporal";
                return;

            case 3:
                careerHistory.Title = "Sergeant";
                if (allowBonus)
                    AddOneSkill(character, dice, "Gun Combat", "Melee");
                return;

            case 4:
                careerHistory.Title = "Lieutenant";
                return;

            case 5:
                careerHistory.Title = "Leader";
                if (allowBonus)
                    AddOneSkill(character, dice, "Engineer", "Navigation");
                return;

            case 6:
                careerHistory.Title = "Captain";
                return;
        }
    }
}
