﻿namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Entertainer_Journalist(SpeciesCharacterBuilder speciesCharacterBuilder) : Entertainer("Journalist", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Art|Holography,Art|Write", "Electronics", "Drive", "Investigate", "Recon", "Streetwise");
    }

    /// <summary>
    /// Titles the table.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="careerHistory">The career history.</param>
    /// <param name="dice">The dice.</param>
    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                careerHistory.Title = "Freelancer";
                if (allowBonus)
                    character.Skills.Add("Electronics", "Comms", 1);
                return;

            case 2:
                careerHistory.Title = "Staff Writer";
                if (allowBonus)
                    character.Skills.Add("Investigate", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Correspondent";
                if (allowBonus)
                    character.Skills.Add("Persuade", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Senior Correspondent";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }
}
