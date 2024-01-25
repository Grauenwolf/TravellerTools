namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Rogue_Pirate(SpeciesCharacterBuilder speciesCharacterBuilder) : Rogue("Pirate", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Dex";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Pilot")));
                return;

            case 2:
                character.Skills.Increase("Astrogation");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gunner")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Engineer")));
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Melee")));
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Lackey";
                return;

            case 1:
                careerHistory.Title = "Henchman";
                AddOneSkill(character, dice, "Pilot", "Gunner");
                return;

            case 2:
                careerHistory.Title = "Corporal";
                return;

            case 3:
                careerHistory.Title = "Sergeant";
                AddOneSkill(character, dice, "Gun Combat", "Melee");
                return;

            case 4:
                careerHistory.Title = "Lieutenant";
                return;

            case 5:
                careerHistory.Title = "Leader";
                AddOneSkill(character, dice, "Engineer", "Navigation");
                return;

            case 6:
                careerHistory.Title = "Captain";
                return;
        }
    }
}
