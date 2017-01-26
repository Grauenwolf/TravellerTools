namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class ArmySupport : Army
    {
        public ArmySupport(Book book) : base("Army Support", book)
        {

        }

        protected override string AdvancementAttribute
        {
            get { return "Edu"; }
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
            get { return 5; }
        }

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Mechanic");

                    return;
                case 2:
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Drive"));
                        skillList.AddRange(SpecialtiesFor("Flyer"));
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Profession")));
                    return;
                case 4:
                    character.Skills.Increase("Explosives");
                    return;
                case 5:
                    character.Skills.Increase("Electronics", "Comms");
                    return;
                case 6:
                    character.Skills.Increase("Medic");
                    return;
            }
        }
    }
}


