namespace Grauenwolf.TravellerTools.Characters.Careers;

class Performer(CharacterBuilder characterBuilder) : Entertainer("Performer", characterBuilder)
{
    protected override string AdvancementAttribute => "Dex";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Art", "Performer");
                    skillList.Add("Art", "Instrument");
                    character.Skills.Increase(dice.Choose(skillList));
                }
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                return;

            case 3:
                character.Skills.Increase("Carouse");
                return;

            case 4:
                character.Skills.Increase("Deception");
                return;

            case 5:
                character.Skills.Increase("Stealth");
                return;

            case 6:
                character.Skills.Increase("Streetwise");
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                character.Dexterity += 1;
                return;

            case 2:
                return;

            case 3:
                character.Strength += 1;
                return;

            case 4:
                return;

            case 5:
                careerHistory.Title = "Famous Performer";
                character.SocialStanding += 1;
                return;

            case 6:
                return;
        }
    }
}
