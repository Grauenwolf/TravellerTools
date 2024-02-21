namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Scholar_Physician(SpeciesCharacterBuilder speciesCharacterBuilder) : Scholar("Physician", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Science | CareerTypes.Healer | CareerTypes.Civilian;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;
    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 4;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Medic", "Electronics", "Investigate", "Medic", "Persuade", "Science");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    character.Skills.Add("Medic", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    AddOneSkill(character, dice, "Science");
                return;

            case 4:
                return;

            case 5:
                if (allowBonus)
                {
                    var skillList = new SkillTemplateCollection(SpecialtiesFor(character, "Science"));
                    //look for a level 0 to increase
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 2);
                    else
                    {
                        //look for a level 1 to increase
                        skillList = new SkillTemplateCollection(SpecialtiesFor(character, "Science"));
                        skillList.RemoveOverlap(character.Skills, 2);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 2);
                    }
                }
                return;

            case 6:
                return;
        }
    }
}
