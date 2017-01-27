
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class CorporateAgent : Agent
    {
        public CorporateAgent(Book book) : base("Corporate Agent", book) { }

        protected override string AdvancementAttribute => "Int";

        protected override int AdvancementTarget => 7;

        protected override string SurvivalAttribute => "Int";

        protected override int SurvivalTarget => 5;

        internal override void AssignmentSkills(Character character, Dice dice)
        {

            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Investigate");
                    return;
                case 2:
                    character.Skills.Increase("Electronics", "Computers");
                    return;
                case 3:
                    character.Skills.Increase("Stealth");
                    return;
                case 4:
                    character.Skills.Increase("Carouse");
                    return;
                case 5:
                    character.Skills.Increase("Deception");
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
                    careerHistory.Title = "Agent";
                    character.Skills.Add("Deception", 1);
                    return;
                case 2:
                    careerHistory.Title = "Field Agent";
                    character.Skills.Add("Investigate", 1);
                    return;
                case 3:
                    return;
                case 4:
                    careerHistory.Title = "Special Agent";
                    var skillList = new SkillTemplateCollection(SpecialtiesFor("Gun Combat"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);

                    return;
                case 5:
                    careerHistory.Title = "Assistant Director";
                    return;
                case 6:
                    careerHistory.Title = "Director";
                    return;
            }
        }
    }
}

