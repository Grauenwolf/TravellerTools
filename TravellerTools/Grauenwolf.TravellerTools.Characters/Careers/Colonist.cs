
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Colonist : Citizen
    {
        public Colonist() : base("Colonist") { }

        protected override string AdvancementAttribute
        {
            get { return "End"; }
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

        protected override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {
            if (level0)
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Animals")));
                        return;
                    case 2:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
                        return;
                    case 3:
                        //character.Skills.Add("Jack-of-all-Trades");
                        return;
                    case 4:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
                        return;
                    case 5:
                        character.Skills.Add("Survival");
                        return;
                    case 6:
                        character.Skills.Add("Recon");
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
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
                        return;
                    case 3:
                        character.Skills.Increase("Jack-of-all-Trades");
                        return;
                    case 4:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
                        return;
                    case 5:
                        character.Skills.Increase("Survival");
                        return;
                    case 6:
                        character.Skills.Increase("Recon");
                        return;
                }
            }
        }

        internal override void UpdateTitle(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    return;
                case 2:
                    character.Title = "Settler";
                    character.Skills.Add("Survival", 1);
                    return;
                case 3:
                    return;
                case 4:
                    character.Title = "Explorer";
                    character.Skills.Add("Navigation", 1);
                    return;
                case 5:
                    return;
                case 6:
                    {
                        var skillList = new SkillTemplateCollection(CharacterBuilder.SpecialtiesFor("Gun Combat"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
            }
        }
    }
}

