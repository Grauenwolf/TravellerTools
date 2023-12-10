namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class MerchantAcademy(CharacterBuilder characterBuilder) : CareerBase("Merchant Academy", null, characterBuilder)
{
    internal override bool Qualify(Character character, Dice dice)
    {
        if (!character.LongTermBenefits.MayEnrollInSchool)
            return false;
        //if (character.CurrentTerm > 3)
        //    return false;

        var dm = character.IntellectDM;
        if (character.SocialStanding >= 8)
            dm += 1;
        dm += character.GetEnlistmentBonus(Career, null);
        dm += QualifyDM;

        return dice.RollHigh(dm, 9);
    }

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        var schoolFlag = dice.NextBoolean();
        var school = schoolFlag ? "Business" : "Shipboard";

        character.AddHistory($"Entered Merchant Academy with the {school} curriculum.", character.Age);

        var skillChoices = new SkillTemplateCollection();
        if (schoolFlag)
        {
            skillChoices.Add("Admin");
            skillChoices.Add("Advocate");
            skillChoices.Add("Broker");
            skillChoices.Add("Streetwise");
            skillChoices.Add("Deception");
            skillChoices.Add("Persuade");
        }
        else
        {
            skillChoices.AddRange(SpecialtiesFor("Pilot"));
            skillChoices.Add("Vacc Suit");
            skillChoices.AddRange(SpecialtiesFor("Athletics"));
            skillChoices.Add("Mechanic");
            skillChoices.AddRange(SpecialtiesFor("Engineer"));
            skillChoices.AddRange(SpecialtiesFor("Electronics"));
        }

        //Add basic skills at level 0
        foreach (var skill in skillChoices.Select(x => x.Name).Distinct())
            character.Skills.Add(skill);
        FixupSkills(character);

        /*
            skillChoices.Add("Admin");
            skillChoices.Add("Advocate");
            skillChoices.Add("Animals", "Training");
            skillChoices.Add("Animals", "Veterinary");
            skillChoices.AddRange(SpecialtiesFor("Art"));
            skillChoices.Add("Astrogation");
            skillChoices.AddRange(SpecialtiesFor("Electronics"));
            skillChoices.AddRange(SpecialtiesFor("Engineer"));
            skillChoices.AddRange(SpecialtiesFor("Language"));
            skillChoices.Add("Medic");
            skillChoices.Add("Navigation");
            skillChoices.AddRange(SpecialtiesFor("Profession"));
            skillChoices.AddRange(SpecialtiesFor("Science"));

            //Remove skills we already have at level 1
            skillChoices.RemoveOverlap(character.Skills, 1);
        */

        character.Education += 1;
        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "Merchant Academy";

        Book.PreCareerEvents(character, dice, this, skillChoices);
        FixupSkills(character);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (graduation < 5)
        {
            character.Age += dice.D(4);
            character.AddHistory($"Dropped out of the merchant academy.", character.Age);
            character.EducationHistory.Status = "Failed";
        }
        else
        {
            character.Age += 4;
            int bonus;
            if (graduation >= 10)
            {
                character.EducationHistory.Status = "Honors";
                character.AddHistory($"Graduated with honors.", character.Age);
                character.NextTermBenefits.MinRank = 2;
                bonus = 2;
            }
            else
            {
                character.EducationHistory.Status = "Graduated";
                character.AddHistory($"Graduated.", character.Age);
                character.NextTermBenefits.MinRank = 1;
                bonus = 1;
            }
            skillChoices.RemoveOverlap(character.Skills, 1);
            character.Skills.Increase(dice.Choose(skillChoices));
            FixupSkills(character);
            character.Education += 1;

            if (schoolFlag)
                character.NextTermBenefits.MustEnroll = "Broker";
            else
                character.NextTermBenefits.MustEnroll = "Merchant Marine";

            character.LongTermBenefits.CareerAdvancementDM.Add("Merchant", bonus);
        }
    }
}
