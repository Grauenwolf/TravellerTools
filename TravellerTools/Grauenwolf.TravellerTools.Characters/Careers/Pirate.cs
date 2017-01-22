
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Pirate : Rogue
    {
        public Pirate() : base("Pirate") { }

        protected override string AdvancementAttribute
        {
            get { return "Int"; }
        }

        protected override int AdvancementTarget
        {
            get { return 6; }
        }

        protected override string SurvivalAttribute
        {
            get { return "Dex"; }
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
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Pilot")));
                    return;
                case 2:
                    character.Skills.Increase("Astrogation");
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Gunner")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Engineer")));
                    return;
                case 5:
                    character.Skills.Increase("Vacc Suit");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Melee")));
                    return;
            }

        }

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    careerHistory.Title = "Lackey";
                    return;
                case 1:
                    careerHistory.Title = "Henchman";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(CharacterBuilder.SpecialtiesFor("Pilot"));
                        skillList.AddRange(CharacterBuilder.SpecialtiesFor("Gunner"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 2:
                    careerHistory.Title = "Corporal";
                    return;
                case 3:
                    careerHistory.Title = "Sergeant";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(CharacterBuilder.SpecialtiesFor("Gun Combat"));
                        skillList.AddRange(CharacterBuilder.SpecialtiesFor("Melee"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }

                    return;
                case 4:
                    careerHistory.Title = "Lieutenant";
                    return;
                case 5:
                    careerHistory.Title = "Leader";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(CharacterBuilder.SpecialtiesFor("Engineer"));
                        skillList.Add("Navigation");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 6:
                    careerHistory.Title = "Captain";
                    return;
            }
        }
    }
}

