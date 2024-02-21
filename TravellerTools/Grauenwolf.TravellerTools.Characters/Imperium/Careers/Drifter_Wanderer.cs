namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Drifter_Wanderer(SpeciesCharacterBuilder speciesCharacterBuilder) : Drifter("Wanderer", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Outsider | CareerTypes.Belter | CareerTypes.Civilian;
    protected override string AdvancementAttribute => "Str";
    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Drive", "Deception", "Recon", "Stealth", "Streetwise", "Survival");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Drive", "Deception", "Recon", "Stealth", "Streetwise", "Survival");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                if (allowBonus)
                    character.Skills.Add("Streetwise", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    character.Skills.Add("Deception", 1);
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                return;
        }
    }
}
