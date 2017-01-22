
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Barbarian : Drifter
    {
        public Barbarian() : base("Barbarian") { }

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

        internal override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {
            if (level0)
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Animals")));
                        return;
                    case 2:
                        character.Skills.Add("Carouse");
                        return;
                    case 3:
                        character.Skills.Add("Melee", "Blade");
                        return;
                    case 4:
                        character.Skills.Add("Stealth");
                        return;
                    case 5:
                        {
                            var skillList = new SkillTemplateCollection() { new SkillTemplate("Seafarer", "Personal"), new SkillTemplate("Seafarer", "Sails") };
                            character.Skills.Add(dice.Choose(skillList));
                        }
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
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Animals")));
                        return;
                    case 2:
                        character.Skills.Increase("Carouse");
                        return;
                    case 3:
                        character.Skills.Increase("Melee", "Blade");
                        return;
                    case 4:
                        character.Skills.Increase("Stealth");
                        return;
                    case 5:
                        {
                            var skillList = new SkillTemplateCollection() { new SkillTemplate("Seafarer", "Personal"), new SkillTemplate("Seafarer", "Sails") };
                            character.Skills.Increase(dice.Choose(skillList));
                        }
                        return;
                    case 6:
                        character.Skills.Increase("Survival");
                        return;
                }
            }
        }

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    character.Skills.Add("Survival", 1);
                    return;
                case 2:
                    careerHistory.Title = "Warrior";
                    character.Skills.Add("Melee", "Blade", 1);
                    return;
                case 3:
                    return;
                case 4:
                    careerHistory.Title = "Chieftain";
                    character.Skills.Add("Leadership", 1);
                    return;
                case 5:
                    return;
                case 6:
                    careerHistory.Title = "Warlord";
                    return;
            }
        }
    }
}

