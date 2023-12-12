namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class PsiWarrrior(CharacterBuilder characterBuilder) : Psion("Psi-Warrrior", characterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                IncreaseTalent(character, dice, "Telepathy");
                return;

            case 2:
                IncreaseTalent(character, dice, "Awareness");
                return;

            case 3:
                IncreaseTalent(character, dice, "Teleportation");
                return;

            case 4:
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Gun Combat"));
                    character.Skills.Increase(dice.Choose(skills));
                }
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase("Recon");
                return;
        }
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            AttemptTalent(character, dice, "Telepathy");
        if (all || roll == 2)
            AttemptTalent(character, dice, "Telekinesis");
        if (all || roll == 3)
            AttemptTalent(character, dice, "Teleportation");
        if (all || roll == 4)
            character.Skills.Add("Gun Combat");
        if (all || roll == 5)
            character.Skills.Add("Vacc Suit");
        if (all || roll == 6)
            character.Skills.Add("Recon");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Psi-Soldier";
                return;

            case 1:
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Gun Combat"));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 2:
                careerHistory.Title = "Knight";
                character.Skills.Add("Leadership", 1);
                return;

            case 3:
                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Master of Wills";
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Tactics"));
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
