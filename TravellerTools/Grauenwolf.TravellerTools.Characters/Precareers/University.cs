namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class University(SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase("University", null, speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Precareer;
    public override string? Source => "Traveller Core, page 16";

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        character.AddHistory($"Entered University.", character.Age);

        var skillChoices = new SkillTemplateCollection();
        skillChoices.Add("Admin");
        skillChoices.Add("Advocate");
        skillChoices.Add("Animals", "Training");
        skillChoices.Add("Animals", "Veterinary");
        skillChoices.AddRange(SpecialtiesFor(character, "Art"));
        skillChoices.Add("Astrogation");
        skillChoices.AddRange(SpecialtiesFor(character, "Electronics"));
        skillChoices.AddRange(SpecialtiesFor(character, "Engineer"));
        skillChoices.AddRange(SpecialtiesFor(character, "Language"));
        skillChoices.Add("Medic");
        skillChoices.Add("Navigation");
        skillChoices.AddRange(SpecialtiesFor(character, "Profession"));
        skillChoices.AddRange(SpecialtiesFor(character, "Science"));

        //Remove skills we already have at level 1
        skillChoices.RemoveOverlap(character.Skills, 1);

        var skillA = dice.Pick(skillChoices);
        character.Skills.Add(skillA, 1);
        FixupSkills(character, dice);

        //Remove skills we already have at level 0
        skillChoices.RemoveOverlap(character.Skills, 0);

        var skillB = dice.Pick(skillChoices);
        character.Skills.Add(skillB);
        FixupSkills(character, dice);

        character.Education += 1;
        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "University";

        PreCareerEvents(character, dice, this, new() { skillA, skillB });
        FixupSkills(character, dice);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (graduation < 5)
        {
            character.Age = Math.Max(character.Age + dice.D(4), character.History.Max(h => h.Age));
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
            FixupSkills(character, dice);

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

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
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

        dm += character.GetEnlistmentBonus(Career, null);
        dm += QualifyDM;

        return dice.RollHigh(dm, 7, isPrecheck);
    }
}
