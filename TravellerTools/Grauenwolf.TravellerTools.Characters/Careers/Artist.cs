
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Artist : Entertainer
    {
        public Artist(Book book) : base("Artist", book) { }

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
            get { return "Soc"; }
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
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
                    return;
                case 2:
                    character.Skills.Increase("Carouse");
                    return;
                case 3:
                    character.Skills.Increase("Electronics", "Comms");
                    return;
                case 4:
                    character.Skills.Increase("Gambler");
                    return;
                case 5:
                    character.Skills.Increase("Persuade");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Profession")));
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
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Art"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 2:
                    return;
                case 3:
                    character.Skills.Add("Investigate", 1);
                    return;
                case 4:
                    return;
                case 5:
                    careerHistory.Title = "Famous Artist";
                    character.SocialStanding += 1;
                    return;
                case 6:
                    return;
            }
        }
    }
}

