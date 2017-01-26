namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class GroundAssault : Marine
    {
        public GroundAssault(Book book) : base("Ground Assault", book)
        {

        }

        protected override string AdvancementAttribute
        {
            get { return "Edu"; }
        }

        protected override int AdvancementTarget
        {
            get { return 5; }

        }

        protected override string SurvivalAttribute
        {
            get { return "End"; }

        }

        protected override int SurvivalTarget
        {
            get { return 7; }
        }

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Vacc Suit");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Heavy Weapons")));
                    return;
                case 3:
                    character.Skills.Increase("Recon");
                    return;
                case 4:
                    character.Skills.Increase("Melee", "Blade");
                    return;
                case 5:
                    character.Skills.Increase("Tactics", "Military");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                    return;
            }
        }
    }
}


