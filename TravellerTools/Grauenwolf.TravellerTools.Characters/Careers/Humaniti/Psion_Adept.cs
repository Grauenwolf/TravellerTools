namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Psion_Adept(CharacterBuilder characterBuilder) : Psion("Adept", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                IncreaseTalent(character, dice, "Telepathy");
                return;

            case 2:
                IncreaseTalent(character, dice, "Clairvoyance");
                return;

            case 3:
                IncreaseTalent(character, dice, "Awareness");
                return;

            case 4:
                character.Skills.Increase("Medic");
                return;

            case 5:
                character.Skills.Increase("Persuade");
                return;

            case 6:
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor("Science"));
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
                careerHistory.Title = "Initiate";
                character.Skills.Add("Science", "Psionicology");
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Acolyte";
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(Book.PsionicTalents);
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master";
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(Book.PsionicTalents);
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;
        }
    }
}
