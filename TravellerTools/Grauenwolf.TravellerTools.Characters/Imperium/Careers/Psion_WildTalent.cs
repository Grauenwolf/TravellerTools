namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Psion_WildTalent(SpeciesCharacterBuilder speciesCharacterBuilder) : Psion("Wild Talent", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Telepathy", "Telekinesis", "Deception", "Stealth", "Streetwise", "Gun Combat|Melee");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Telepathy", "Telekinesis", "Deception", "Stealth", "Streetwise", "Melee,Gun Combat");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                careerHistory.Title = "Survivor";
                if (allowBonus)
                    AddOneSkill(character, dice, "Survival", "Streetwise");
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Witch";
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
