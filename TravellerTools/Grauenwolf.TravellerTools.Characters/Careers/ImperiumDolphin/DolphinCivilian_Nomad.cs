namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinCivilian_Nomad(CharacterBuilder characterBuilder) : DolphinCivilian("Nomad", characterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;

            case 2:
                character.Skills.Increase("Melee", "Natural");
                return;

            case 3:
                character.Skills.Increase("Leadership");
                return;

            case 4:
                character.Skills.Increase("Recon");
                return;

            case 5:
                character.Skills.Increase("Stealth");
                return;

            case 6:
                character.Skills.Increase("Survival");
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Pod Leader";
                character.Skills.Add("Leadership", 1);
                return;

            case 3:
                if (character.SocialStanding < 6)
                    character.SocialStanding = 6;
                else
                    character.SocialStanding += 1;

                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Superpod Leader";
                if (character.SocialStanding < 8)
                    character.SocialStanding = 8;
                else
                    character.SocialStanding += 1;
                return;

            case 6:
                return;
        }
    }
}
