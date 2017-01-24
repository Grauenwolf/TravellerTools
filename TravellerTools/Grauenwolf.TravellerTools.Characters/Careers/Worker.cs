
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Worker : Citizen
    {
        public Worker(Book book) : base("Worker", book) { }

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

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.AddRange(SpecialtiesFor("Drive"));
            if (all || roll == 2)
                character.Skills.Add("Mechanic");
            if (all || roll == 3)
                character.Skills.AddRange(SpecialtiesFor("Electronics"));
            if (all || roll == 4)
                character.Skills.AddRange(SpecialtiesFor("Engineer"));
            if (all || roll == 5)
                character.Skills.AddRange(SpecialtiesFor("Profession"));
            if (all || roll == 6)
                character.Skills.AddRange(SpecialtiesFor("Science"));
        }

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 2:
                    character.Skills.Increase("Mechanic");
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Profession")));
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
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
                        var skillList = new SkillTemplateCollection(SpecialtiesFor("Profession"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
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
                        var skillList = new SkillTemplateCollection(SpecialtiesFor("Engineer"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
            }
        }
    }
}

