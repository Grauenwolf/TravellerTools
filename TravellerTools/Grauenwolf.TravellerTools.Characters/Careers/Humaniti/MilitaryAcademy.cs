namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class MilitaryAcademy(string assignment, CharacterBuilder characterBuilder) : CareerBase("Military Academy", assignment, characterBuilder)
{
    protected abstract string Branch { get; }
    protected abstract string QualifyAttribute { get; }
    protected abstract int QualifyTarget { get; }

    ///// <summary>
    ///// This is a stub for military careers used to get basic skills.
    ///// </summary>
    //protected abstract MilitaryCareer Stub { get; }

    internal override bool Qualify(Character character, Dice dice)
    {
        if (!character.LongTermBenefits.MayEnrollInSchool)
            return false;
        if (character.CurrentTerm > 3)
            return false;

        var dm = character.GetDM(QualifyAttribute);
        if (character.CurrentTerm == 2)
            dm += -2;
        if (character.CurrentTerm == 3)
            dm += -4;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, QualifyTarget);
    }

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        character.AddHistory($"Entered {Career}({Assignment})", character.Age);

        var skillChoices = GetBasicSkills();
        //Add basic skills at level 0
        foreach (var skill in skillChoices.Select(x => x.Name).Distinct())
            character.Skills.Add(skill);
        FixupSkills(character);

        var gradDM = character.IntellectDM;
        if (character.Endurance >= 8)
            gradDM += 1;
        if (character.SocialStanding >= 8)
            gradDM += 1;

        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = Assignment;

        Book.PreCareerEvents(character, dice, this, skillChoices);
        FixupSkills(character);

        var graduation = dice.D(2, 6);
        if (graduation == 2)
        {
            character.Age += dice.D(4);
            character.AddHistory($"Kicked out of military academy.", character.Age);
            character.NextTermBenefits.EnlistmentDM[Branch] = -100;
        }
        else
        {
            graduation += gradDM;
            if (graduation < 8)
            {
                character.Age += dice.D(4);
                character.AddHistory("Dropped out of military academy.", character.Age);
                character.EducationHistory.Status = "Failed";
                character.NextTermBenefits.EnlistmentDM[Branch] = 100;
                character.NextTermBenefits.CommissionDM = -100;
            }
            else
            {
                character.Age += 4;
                if (graduation >= 11)
                {
                    character.EducationHistory.Status = "Honors";
                    character.AddHistory($"Graduated with honors.", character.Age);
                    character.SocialStanding += 1;
                    character.NextTermBenefits.FreeCommissionRoll = true;
                    character.NextTermBenefits.CommissionDM += 100;
                }
                else
                {
                    character.EducationHistory.Status = "Graduated";
                    character.AddHistory($"Graduated.", character.Age);
                    character.NextTermBenefits.CommissionDM += 2;
                }

                for (var i = 1; i <= 3; i++)
                    character.Skills.Increase(dice.Pick(skillChoices)); //Use pick so we don't get duplicates
                FixupSkills(character);

                character.Education += 1;

                character.NextTermBenefits.MustEnroll = Branch;
            }
        }
    }

    /// <summary>
    /// Gets a list of skills you can choose from for pre-career events.
    /// </summary>
    protected abstract SkillTemplateCollection GetBasicSkills();
}
