namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class University : Career
    {
        public University() : base("University", null)
        {

        }

        public override bool Qualify(Character character, Dice dice)
        {
            if (!character.LongTermBenefits.MayEnrollInSchool)
                return false;
            if (character.CurrentTerm > 3)
                return false;

            var dm = character.GetDM("Edu");
            if (character.CurrentTerm == 2)
                dm += -1;
            if (character.CurrentTerm == 3)
                dm += -2;
            if (character.SocialStanding >= 9)
                dm += 1;

            dm += character.GetEnlistmentBonus("University", null);

            return dice.RollHigh(7);

        }

        public override void Run(Character character, Dice dice)
        {
            character.LongTermBenefits.MayEnrollInSchool = false;

            character.AddHistory($"Entered University at age {character.Age}");

            var skillChoices = new SkillTemplateCollection();
            skillChoices.Add("Admin");
            skillChoices.Add("Advocate");
            skillChoices.Add("Animals", "Training");
            skillChoices.Add("Animals", "Veterinary");
            skillChoices.AddRange(CharacterBuilder.SpecialtiesFor("Art"));
            skillChoices.Add("Astrogation");
            skillChoices.AddRange(CharacterBuilder.SpecialtiesFor("Electronics"));
            skillChoices.AddRange(CharacterBuilder.SpecialtiesFor("Engineer"));
            skillChoices.AddRange(CharacterBuilder.SpecialtiesFor("Language"));
            skillChoices.Add("Medic");
            skillChoices.Add("Navigation");
            skillChoices.AddRange(CharacterBuilder.SpecialtiesFor("Profession"));
            skillChoices.AddRange(CharacterBuilder.SpecialtiesFor("Science"));


            //Remove skills we already have at level 1
            skillChoices.RemoveOverlap(character.Skills, 1);

            var skillA = dice.Pick(skillChoices);
            character.Skills.Add(skillA, 1);


            //Remove skills we already have at level 0
            skillChoices.RemoveOverlap(character.Skills, 0);

            var skillB = dice.Pick(skillChoices);
            character.Skills.Add(skillB);


            character.Age += 4;
            character.Education += 1;

            CharacterBuilder.PreCareerEvents(character, dice, skillA, skillB);

            var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
            if (graduation < 7)
            {
                character.AddHistory("Dropped out of university.");
            }
            else
            {
                int bonus;
                if (graduation >= 11)
                {
                    character.AddHistory($"Graduated with honors at age {character.Age}.");
                    bonus = 2;
                }
                else
                {
                    character.AddHistory($"Graduated at age {character.Age}.");
                    bonus = 1;
                }

                character.Education += 2;

                character.Skills.Increase(skillA, 1);
                character.Skills.Increase(skillB, 1);

                character.NextTermBenefits.FreeCommissionRoll = true;
                character.NextTermBenefits.CommissionDM = bonus;
                character.LongTermBenefits.EnlistmentDM.Add("Agent", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Army", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Corporate", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Journalist", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Marines", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Navy", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Scholar", bonus);
                character.LongTermBenefits.EnlistmentDM.Add("Scouts", bonus);
            }


        }
    }
}
