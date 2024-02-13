namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Agent_LawEnforcement(SpeciesCharacterBuilder speciesCharacterBuilder) : Agent("Law Enforcement", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Police | CareerTypes.StarportOfficer | CareerTypes.StarportEmployee | CareerTypes.Government;

    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Investigate", "Recon", "Streetwise", "Stealth", "Melee", "Advocate");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:

                careerHistory.Title = "Rookie";
                return;

            case 1:
                careerHistory.Title = "Corporal";
                if (allowBonus)
                    character.Skills.Add("Streetwise", 1);
                return;

            case 2:
                careerHistory.Title = "Sergeant";
                return;

            case 3:
                careerHistory.Title = "Detective";
                return;

            case 4:
                careerHistory.Title = "Lieutenant";
                if (allowBonus)
                    character.Skills.Add("Investigate", 1);
                return;

            case 5:
                careerHistory.Title = "Chief";
                if (allowBonus)
                    character.Skills.Add("Admin", 1);
                return;

            case 6:
                careerHistory.Title = "Commissioner";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }
}
