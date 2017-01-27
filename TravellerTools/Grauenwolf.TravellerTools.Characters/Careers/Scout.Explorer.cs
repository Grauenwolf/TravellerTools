namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Explorer : Scout
    {
        public Explorer(Book book) : base("Explorer", book) { }

        protected override string AdvancementAttribute => "Edu";

        protected override int AdvancementTarget => 7;

        protected override string SurvivalAttribute => "End";

        protected override int SurvivalTarget => 7;

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
                case 5:
                    character.Skills.Increase("Stealth");
                    return;
                case 6:
                    character.Skills.Increase("Recon");
                    return;
            }

        }


    }
}

