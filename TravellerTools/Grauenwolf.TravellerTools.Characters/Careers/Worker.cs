
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Worker : Citizen
    {
        public Worker() : base("Worker") { }

        protected override string AdvancementAttribute
        {
            get { return "Edu"; }
        }

        protected override int AdvancementTarget
        {
            get { return 8; }
        }

        protected override string SurvivalAttribute
        {
            get { return "End"; }
        }

        protected override int SurvivalTarget
        {
            get { return 4; }
        }

        internal override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {
            if (level0)
            {
                switch (roll)
                {
                    case 1:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
                        return;
                    case 2:
                        character.Skills.Add("Mechanic");
                        return;
                    case 3:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Electronics")));
                        return;
                    case 4:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Engineer")));
                        return;
                    case 5:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Profession")));
                        return;
                    case 6:
                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Science")));
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
                        character.Skills.Increase("Mechanic");
                        return;
                    case 3:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Electronics")));
                        return;
                    case 4:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Engineer")));
                        return;
                    case 5:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Profession")));
                        return;
                    case 6:
                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Science")));
                        return;
                }
            }
        }

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    return;
                case 2:
                    careerHistory.Title = "Technician";
                    {
                        var skillList = new SkillTemplateCollection(CharacterBuilder.SpecialtiesFor("Profession"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 3:
                    return;
                case 4:
                    careerHistory.Title = "Craftsman";
                    character.Skills.Add("Mechanic", 1);
                    return;
                case 5:
                    return;
                case 6:
                    careerHistory.Title = "Master Technician";
                    {
                        var skillList = new SkillTemplateCollection(CharacterBuilder.SpecialtiesFor("Engineer"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
            }
        }
    }
}

