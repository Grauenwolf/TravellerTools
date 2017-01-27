namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Scientist : Scholar
    {
        public Scientist(Book book) : base("Scientist", book) { }

        protected override string AdvancementAttribute => "Int";

        protected override int AdvancementTarget => 8;

        protected override string SurvivalAttribute => "Edu";

        protected override int SurvivalTarget => 4;

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Admin");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
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
                case 0:
                    return;
                case 1:
                    {
                        var skillList = new SkillTemplateCollection(SpecialtiesFor("Science"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 2:
                    character.Skills.Add("Electronics", "Computers", 1);
                    return;
                case 3:
                    character.Skills.Add("Investigate", 1);
                    return;
                case 4:
                    return;
                case 5:
                    {
                        var skillList = new SkillTemplateCollection(SpecialtiesFor("Science"));
                        //look for a level 0 to increase
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 2);
                        else
                        {
                            //look for a level 1 to increase
                            skillList = new SkillTemplateCollection(SpecialtiesFor("Science"));
                            skillList.RemoveOverlap(character.Skills, 2);
                            if (skillList.Count > 0)
                                character.Skills.Add(dice.Choose(skillList), 2);
                        }
                    }
                    return;
                case 6:
                    return;
            }
        }
    }
}

