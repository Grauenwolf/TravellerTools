
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Thief : Rogue
    {
        public Thief() : base("Thief") { }

        protected override string AdvancementAttribute
        {
            get { return "Dex"; }
        }

        protected override int AdvancementTarget
        {
            get { return 6; }
        }

        protected override string SurvivalAttribute
        {
            get { return "Int"; }
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
                    character.Skills.Increase("Stealth");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Electronics")));
                    return;
                case 3:
                    character.Skills.Increase("Recon");
                    return;
                case 4:
                    character.Skills.Increase("Streetwise");
                    return;
                case 5:
                    character.Skills.Increase("Deception");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
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
                    character.Skills.Add("Stealth", 1);
                    return;
                case 2:
                    return;
                case 3:
                    character.Skills.Add("Streetwise", 1);
                    return;
                case 4:
                    return;
                case 5:
                    character.Skills.Add("Recon", 1);
                    return;
                case 6:
                    return;
            }
        }
    }
}

