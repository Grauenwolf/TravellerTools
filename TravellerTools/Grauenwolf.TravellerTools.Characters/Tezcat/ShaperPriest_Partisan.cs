namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

class ShaperPriest_Partisan(SpeciesCharacterBuilder speciesCharacterBuilder) : ShaperPriest("Partisan", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Academic", "Gun Combat", "Melee", "Tactics|Military", "Recon", "Stealth");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Least Claw";
                return;

            case 1:
                careerHistory.Title = "Third Claw";
                if (allowBonus)
                    IncreaseOneSkill(character, dice, "Gun Combat");
                return;

            case 2:
                careerHistory.Title = "Second Claw";
                if (allowBonus)
                    character.Skills.Increase("Leadership");
                return;

            case 3:
                careerHistory.Title = "First Claw";
                return;

            case 4:
                careerHistory.Title = "Kaltrhar";
                if (allowBonus)
                    character.Skills.Increase("Tactics", "Military");
                return;

            case 5:
                careerHistory.Title = "Shilahn";
                return;

            case 6:
                careerHistory.Title = "Shil Shintrah";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }
}
