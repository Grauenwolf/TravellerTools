
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Scavenger : Drifter
    {
        public Scavenger() : base("Scavenger") { }

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
                    character.Skills.Increase("Pilot", "Small Craft");
                    return;
                case 2:
                    character.Skills.Increase("Mechanic");
                    return;
                case 3:
                    character.Skills.Increase("Astrogation");
                    return;
                case 4:
                    character.Skills.Increase("Vacc Suit");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Profession")));

                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Gun Combat")));
                    return;
            }
        }

        internal override void UpdateTitle(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    character.Skills.Add("Vacc Suit", 1);
                    return;
                case 2:
                    return;
                case 3:
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Profession", "Belter");
                    skillList.Add("Mechanic");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
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

