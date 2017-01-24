namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Cavalry : Army
    {
        public Cavalry(Book book) : base("Cavalry", book)
        {

        }

        protected override string AdvancementAttribute
        {
            get { return "Int"; }
        }

        protected override int AdvancementTarget
        {
            get { return 5; }

        }

        protected override string SurvivalAttribute
        {
            get { return "Int"; }

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
                    character.Skills.Increase("Mechanic");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));
                    return;
                case 4:
                    character.Skills.Increase("Recon");
                    return;
                case 5:
                    character.Skills.Increase("Heavy Weapons", "Vehicle");

                    return;
                case 6:
                    character.Skills.Increase("Electronics", "Sensors");
                    return;
            }
        }
    }
}


