namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

class MilitaryOfficer_Assassin(SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryOfficer("Assassin", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 8;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Melee", "Stealth", "Streetwise", "Recon", "Deception", "Athletics");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                character.Title = "Hunter";
                if (allowBonus)
                    character.Skills.Add("Stealth", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    character.Skills.Add("Melee", "Natural", 1);
                return;

            case 4:
                character.Title = "Veteran Hunter";
                return;

            case 5:
                return;

            case 6:
                character.Title = "Claw of the Clan";
                if (allowBonus)
                    character.AddHistory("Gain 2 clan shares.", character.Age);
                return;
        }
    }
}
