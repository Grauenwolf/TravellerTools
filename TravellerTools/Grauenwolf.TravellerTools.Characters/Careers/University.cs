namespace Grauenwolf.TravellerTools.Characters.Careers;

class University(Book book) : CareerBase("University", null, book)
{
    internal override bool Qualify(Character character, Dice dice)
    {
        if (!character.LongTermBenefits.MayEnrollInSchool)
            return false;
        if (character.CurrentTerm > 3)
            return false;

        var dm = character.EducationDM;
        if (character.CurrentTerm == 2)
            dm += -1;
        if (character.CurrentTerm == 3)
            dm += -2;
        if (character.SocialStanding >= 9)
            dm += 1;

        dm += character.GetEnlistmentBonus("University", null);

        return dice.RollHigh(dm, 7);
    }

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        character.AddHistory($"Entered University.", character.Age);

        var skillChoices = new SkillTemplateCollection();
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

        var skillA = dice.Pick(skillChoices);
        character.Skills.Add(skillA, 1);

        //Remove skills we already have at level 0
        skillChoices.RemoveOverlap(character.Skills, 0);

        var skillB = dice.Pick(skillChoices);
        character.Skills.Add(skillB);

        character.Education += 1;
        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "University";

        Book.PreCareerEvents(character, dice, this, skillA.Name, skillB.Name);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (graduation < 5)
        {
            character.Age += dice.D(4);
            character.AddHistory($"Dropped out of university.", character.Age);
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
                bonus = 2;
            }
            else
            {
                character.EducationHistory.Status = "Graduated";
                character.AddHistory($"Graduated.", character.Age);
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
