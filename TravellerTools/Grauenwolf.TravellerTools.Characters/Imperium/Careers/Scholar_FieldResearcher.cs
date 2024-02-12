namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Scholar_FieldResearcher(SpeciesCharacterBuilder speciesCharacterBuilder) : Scholar("Field Researcher", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Science | CareerType.FieldScience;

    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Vacc Suit", "Navigation", "Survival", "Investigate", "Science");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    AddOneSkill(character, dice, "Science");
                return;

            case 2:
                if (allowBonus)
                    character.Skills.Add("Electronics", "Computers", 1);
                return;

            case 3:
                if (allowBonus)
                    character.Skills.Add("Investigate", 1);
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
