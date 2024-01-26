namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Psion_PsiWarrrior(SpeciesCharacterBuilder speciesCharacterBuilder) : Psion("Psi-Warrrior", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Telepathy", "Awareness", "Teleportation", "Gun Combat", "Vacc Suit", "Recon");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Telepathy", "Telekinesis", "Teleportation", "Gun Combat", "Vacc Suit", "Recon");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Psi-Soldier";
                return;

            case 1:
                if (allowBonus)
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Gun Combat"));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 2:
                careerHistory.Title = "Knight";
                if (allowBonus)
                    character.Skills.Add("Leadership", 1);
                return;

            case 3:
                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Master of Wills";
                if (allowBonus)
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Tactics"));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 6:
                return;
        }
    }
}
