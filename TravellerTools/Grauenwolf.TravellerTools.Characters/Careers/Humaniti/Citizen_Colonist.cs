namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Citizen_Colonist(CharacterBuilder characterBuilder) : Citizen("Colonist", characterBuilder)
{
    protected override string AdvancementAttribute => "End";

    protected override int AdvancementTarget => 5;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Animals")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;

            case 3:
                character.Skills.Increase("Jack-of-all-Trades");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Drive")));
                return;

            case 5:
                character.Skills.Increase("Survival");
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
            character.Skills.Add("Animals");
        if (all || roll == 2)
            character.Skills.Add("Athletics");
        //if (all || roll == 3)
        //character.Skills.Add("Jack-of-all-Trades"); Getting this as level 0 has no effect.
        if (all || roll == 4)
            character.Skills.Add("Drive");
        if (all || roll == 5)
            character.Skills.Add("Survival");
        if (all || roll == 6)
            character.Skills.Add("Recon");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Settler";
                character.Skills.Add("Survival", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Explorer";
                character.Skills.Add("Navigation", 1);
                return;

            case 5:
                return;

            case 6:
                {
                    var skillList = new SkillTemplateCollection(SpecialtiesFor(character, "Gun Combat"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;
        }
    }
}
