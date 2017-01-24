
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class LawEnforcement : Agent
    {
        public LawEnforcement() : base("Law Enforcement") { }

        protected override string AdvancementAttribute
        {
            get { return "Int"; }
        }

        protected override int AdvancementTarget
        {
            get { return 6; }
        }

        protected override string SurvivalAttribute
        {
            get { return "End"; }
        }

        protected override int SurvivalTarget
        {
            get { return 6; }
        }

        internal override void AssignmentSkills(Character character, Dice dice)
        {

            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Investigate");
                    return;
                case 2:
                    character.Skills.Increase("Recon");
                    return;
                case 3:
                    character.Skills.Increase("Streetwise");
                    return;
                case 4:
                    character.Skills.Increase("Stealth");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Melee")));
                    return;
                case 6:
                    character.Skills.Increase("Advocate");
                    return;
            }

        }

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 0:

                    careerHistory.Title = "Rookie";
                    return;
                case 1:
                    careerHistory.Title = "Corporal";
                    character.Skills.Add("Streetwise", 1);
                    return;
                case 2:
                    careerHistory.Title = "Sergeant";
                    return;
                case 3:
                    careerHistory.Title = "Detective";
                    return;
                case 4:
                    careerHistory.Title = "Lieutenant";
                    character.Skills.Add("Investigate", 1);
                    return;
                case 5:
                    careerHistory.Title = "Chief";
                    character.Skills.Add("Admin", 1);
                    return;
                case 6:
                    careerHistory.Title = "Commissioner";
                    character.SocialStanding += 1;
                    return;
            }
        }
    }
}

