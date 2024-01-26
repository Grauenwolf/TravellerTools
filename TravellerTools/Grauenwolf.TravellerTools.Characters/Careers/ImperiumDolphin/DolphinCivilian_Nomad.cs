namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinCivilian_Nomad(SpeciesCharacterBuilder speciesCharacterBuilder) : DolphinCivilian("Nomad", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Athletics", "Melee|Natural", "Leadership", "Recon", "Stealth", "Survival");
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Pod Leader";
                if (allowBonus)
                    character.Skills.Add("Leadership", 1);
                return;

            case 3:
                if (allowBonus)
                {
                    if (character.SocialStanding < 6)
                        character.SocialStanding = 6;
                    else
                        character.SocialStanding += 1;
                }
                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Superpod Leader";
                if (allowBonus)
                {
                    if (character.SocialStanding < 8)
                        character.SocialStanding = 8;
                    else
                        character.SocialStanding += 1;
                }
                return;

            case 6:
                return;
        }
    }
}
