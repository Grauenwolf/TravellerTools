namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Psion_Adept(SpeciesCharacterBuilder speciesCharacterBuilder) : Psion("Adept", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Telepathy", "Clairvoyance", "Awareness", "Medic", "Persuade", "Science");
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

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                careerHistory.Title = "Initiate";
                if (allowBonus)
                    character.Skills.Add("Science", "Psionicology");
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Acolyte";
                if (allowBonus)
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(PsionicTalents(character));
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
                if (allowBonus)
                {
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(PsionicTalents(character));
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                return;
        }
    }
}
