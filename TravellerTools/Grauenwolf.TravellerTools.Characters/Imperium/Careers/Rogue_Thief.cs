namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Rogue_Thief(SpeciesCharacterBuilder speciesCharacterBuilder) : Rogue("Thief", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Stealth", "Electronics", "Recon", "Streetwise", "Deception", "Athletics");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    character.Skills.Add("Stealth", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    character.Skills.Add("Streetwise", 1);
                return;

            case 4:
                return;

            case 5:
                if (allowBonus)
                    character.Skills.Add("Recon", 1);
                return;

            case 6:
                return;
        }
    }
}
