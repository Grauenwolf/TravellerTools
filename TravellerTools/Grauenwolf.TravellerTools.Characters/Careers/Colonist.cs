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

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.AddRange(CharacterBuilder.SpecialtiesFor("Animals"));
            if (all || roll == 2)
                character.Skills.AddRange(CharacterBuilder.SpecialtiesFor("Athletics"));
            //if (all || roll == 3)
            //character.Skills.Add("Jack-of-all-Trades");
            if (all || roll == 4)
                character.Skills.AddRange(CharacterBuilder.SpecialtiesFor("Drive"));
            if (all || roll == 5)
                character.Skills.Add("Survival");
            if (all || roll == 6)
                character.Skills.Add("Recon");
        }


        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
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

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    return;
                case 2:
                    careerHistory.Title = "Settler";
                    character.Skills.Add("Survival", 1);
                    return;
                case 3:
                    return;
                case 4:
                    careerHistory.Title = "Explorer";
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

