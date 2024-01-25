namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Citizen_Worker(SpeciesCharacterBuilder speciesCharacterBuilder) : Citizen("Worker", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Drive")));
                return;

            case 2:
                character.Skills.Increase("Mechanic");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Engineer")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Profession")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
                return;
        }
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Drive");
        if (all || roll == 2)
            character.Skills.Add("Mechanic");
        if (all || roll == 3)
            character.Skills.Add("Electronics");
        if (all || roll == 4)
            character.Skills.Add("Engineer");
        if (all || roll == 5)
            character.Skills.Add("Profession");
        if (all || roll == 6)
            character.Skills.Add("Science");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Technician";
                {
                    var skillList = new SkillTemplateCollection(SpecialtiesFor(character, "Profession"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Craftsman";
                character.Skills.Add("Mechanic", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master Technician";
                {
                    var skillList = new SkillTemplateCollection(SpecialtiesFor(character, "Engineer"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;
        }
    }
}
