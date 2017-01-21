
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Corporate : Citizen
    {
        public Corporate() : base("Corporate") { }

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
            get { return "Soc"; }
        }

        protected override int SurvivalTarget
        {
            get { return 6; }
        }

        protected override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {
            switch (roll)
            {
                case 1:
                    character.Skills.Increase("Advocate");
                    return;
                case 2:
                    character.Skills.Increase("Admin");
                    return;
                case 3:
                    character.Skills.Increase("Broker");
                    return;
                case 4:
                    character.Skills.Increase("Electronics", "Computer");
                    return;
                case 5:
                    character.Skills.Increase("Diplomat");
                    return;
                case 6:
                    character.Skills.Increase("Leadership");
                    return;
            }
        }

        internal override void UpdateTitle(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    return;
                case 2:
                    character.Title = "Manager";
                    character.Skills.Add("Admin", 1);
                    return;
                case 3:
                    return;
                case 4:
                    character.Title = "Senior Manager";
                    character.Skills.Add("Advocate", 1);
                    return;
                case 5:
                    return;
                case 6:
                    character.Title = "Director";
                    character.SocialStanding += 1;
                    return;
            }
        }
    }
}

