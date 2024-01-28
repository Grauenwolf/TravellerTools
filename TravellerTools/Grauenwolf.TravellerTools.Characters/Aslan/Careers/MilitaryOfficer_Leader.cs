namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class MilitaryOfficer_Leader(SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryOfficer("Leader", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Tactics|Military", "Recon", "Melee|Natural", "Heavy Weapons", "Gun Combat", "Electronics");
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
                    character.Skills.Add("Vacc Suit", 1);
                return;

            case 2:
                character.Title = "Lieutenant";
                return;

            case 3:
                character.Title = "Captain";
                if (allowBonus)
                    character.Skills.Add("Leadership", 1);
                return;

            case 4:
                character.Title = "Commandant";
                return;

            case 5:
                character.Title = "Division General";
                return;

            case 6:
                character.Title = "General";
                if (allowBonus)
                    character.Territory += 2;
                return;
        }
    }
}
