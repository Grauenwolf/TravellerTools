using Grauenwolf.TravellerTools.Characters.Careers;

namespace Grauenwolf.TravellerTools.Characters
{
    class Wanderer : Drifter
    {
        public Wanderer() : base("Wanderer") { }

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
            if (level0)
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
                        return;
                    case 2:
                        character.Skills.Add("Deception");
                        return;
                    case 3:
                        character.Skills.Add("Recon");
                        return;
                    case 4:
                        character.Skills.Add("Stealth");
                        return;
                    case 5:
                        character.Skills.Add("Streetwise");
                        return;
                    case 6:
                        character.Skills.Add("Survival");
                        return;
                }
            }
            else
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
                        return;
                    case 2:
                        character.Skills.Increase("Deception");
                        return;
                    case 3:
                        character.Skills.Increase("Recon");
                        return;
                    case 4:
                        character.Skills.Increase("Stealth");
                        return;
                    case 5:
                        character.Skills.Increase("Streetwise");
                        return;
                    case 6:
                        character.Skills.Increase("Survival");
                        return;
                }
            }
        }

        internal override void UpdateTitle(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    character.Skills.Add("Streetwise", 1);
                    return;
                case 2:
                    return;
                case 3:
                    character.Skills.Add("Deception", 1);
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
            }
        }
    }
}

