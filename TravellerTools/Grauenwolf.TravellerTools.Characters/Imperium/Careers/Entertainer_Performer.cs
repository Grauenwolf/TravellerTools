namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Entertainer_Performer(SpeciesCharacterBuilder speciesCharacterBuilder) : Entertainer("Performer", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Art|Performer,Art|Instrument", "Athletics", "Carouse", "Deception", "Stealth", "Streetwise");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    character.Dexterity += 1;
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    character.Strength += 1;
                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Famous Performer";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 6:
                return;
        }
    }
}
