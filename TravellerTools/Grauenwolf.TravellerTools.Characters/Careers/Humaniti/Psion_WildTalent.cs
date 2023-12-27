namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Psion_WildTalent(CharacterBuilder characterBuilder) : Psion("Wild Talent", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Soc";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                IncreaseTalent(character, dice, "Telepathy");
                return;

            case 2:
                IncreaseTalent(character, dice, "Telekinesis");
                return;

            case 3:
                character.Skills.Increase("Deception");
                return;

            case 4:
                character.Skills.Increase("Stealth");
                return;

            case 5:
                character.Skills.Increase("Streetwise");
                return;

            case 6:
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Gun Combat"));
                    skills.AddRange(SpecialtiesFor("Melee"));
                    character.Skills.Increase(dice.Choose(skills));
                }
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
            character.Skills.Add("Deception");
        if (all || roll == 4)
            character.Skills.Add("Stealth");
        if (all || roll == 5)
            character.Skills.Add("Streetwise");
        if (all || roll == 6)
        {
            if (dice.NextBoolean())
                character.Skills.Add("Melee");
            else
                character.Skills.Add("Gun Combat");
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                careerHistory.Title = "Survivor";
                {
                    var skillList = new SkillTemplateCollection("Survival", "Streetwise");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Witch";
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
