namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Inmate : Prisoner
    {
        public Inmate() : base("Inmate") { }

        protected override string AdvancementAttribute
        {
            get { return "Str"; }
        }

        protected override int AdvancementTarget
        {
            get { return 7; }
        }

        protected override string SurvivalAttribute
        {
            get { return "End"; }
        }

        protected override int SurvivalTarget
        {
            get { return 7; }
        }

        protected override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {
            switch (roll)
            {
                case 1:
                    character.Skills.Increase("Stealth");
                    return;
                case 2:
                    character.Skills.Increase("Melee", "Unarmed");
                    return;
                case 3:
                    character.Skills.Increase("Streetwise");
                    return;
                case 4:
                    character.Skills.Increase("Survival");
                    return;
                case 5:
                    character.Skills.Increase("Athletics", "Strength");
                    return;
                case 6:
                    character.Skills.Increase("Mechanic");
                    return;
            }
        }
    }
}

