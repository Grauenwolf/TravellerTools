namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Dilettante : Noble
    {
        public Dilettante(Book book) : base("Dilettante", book) { }

        protected override string AdvancementAttribute => "Int";

        protected override int AdvancementTarget => 8;

        protected override string SurvivalAttribute => "Soc";

        protected override int SurvivalTarget => 3;

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Carouse");
                    return;
                case 2:
                    character.Skills.Increase("Deception");
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));
                    return;
                case 4:
                    character.Skills.Increase("Streetwise");
                    return;
                case 5:
                    character.Skills.Increase("Gambler");
                    return;
                case 6:
                    character.Skills.Increase("Jack-of-all-Trades");
                    return;
            }

        }

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    careerHistory.Title = "Wastrel";
                    return;
                case 1:
                    return;
                case 2:
                    careerHistory.Title = "Ingrate";
                    character.Skills.Add("Carouse", 1);
                    return;
                case 3:
                    return;
                case 4:
                    careerHistory.Title = "Black Sheep";
                    character.Skills.Add("Persuade", 1);
                    return;
                case 5:
                    return;
                case 6:
                    careerHistory.Title = "Scoundrel";
                    character.Skills.Add("Jack-of-all-Trades", 1);
                    return;
            }
        }
    }
}

