namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class StarMarine : Marine
    {
        public StarMarine(Book book) : base("Star Marine", book)
        {

        }

        protected override string AdvancementAttribute
        {
            get { return "Edu"; }
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
                    character.Skills.Increase("Vacc Suit");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
                    return;
                case 4:
                    character.Skills.Increase("Melee", "Blade");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                    return;
            }
        }
    }
}


