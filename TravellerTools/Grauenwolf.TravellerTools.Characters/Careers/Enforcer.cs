
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Enforcer : Rogue
    {
        public Enforcer() : base("Enforcer") { }

        protected override string AdvancementAttribute
        {
            get { return "Str"; }
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

        internal override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {

            switch (roll)
            {
                case 1:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Gun Combat")));
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Melee")));
                    return;
                case 3:
                    character.Skills.Increase("Streetwise");
                    return;
                case 4:
                    character.Skills.Increase("Persuade");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
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
                    character.Skills.Add("Persuade", 1);
                    return;
                case 2:
                    return;
                case 3:
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(CharacterBuilder.SpecialtiesFor("Gun Combat"));
                    skillList.AddRange(CharacterBuilder.SpecialtiesFor("Melee"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                    return;
                case 4:
                    return;
                case 5:
                    character.Skills.Add("Streetwise", 1);
                    return;
                case 6:
                    return;
            }
        }
    }
}

