namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class MilitaryAcademy(string assignment, Book book) : CareerBase("Military Academy", assignment, book)
{
    protected abstract string QualifyAttribute { get; }

    protected abstract int QualifyTarget { get; }

    /// <summary>
    /// This is a stub for military careers used to get basic skills.
    /// </summary>
    protected abstract MilitaryCareer Stub { get; }

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

        return dice.RollHigh(dm, QualifyTarget);
    }

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        character.AddHistory($"Entered {Assignment}", character.Age);
        Stub.BasicTrainingSkills(character, dice, true);

        var gradDM = character.IntellectDM;
        if (character.Endurance >= 8)
            gradDM += 1;
        if (character.SocialStanding >= 8)
            gradDM += 1;

        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = Assignment;

        Book.PreCareerEvents(character, dice, this, character.Skills.Select(s => s.Name).ToArray());

        var graduation = dice.D(2, 6);
        if (graduation == 2)
        {
            character.Age += dice.D(4);
            character.AddHistory($"Kicked out of military academy.", character.Age);
            character.NextTermBenefits.EnlistmentDM[Stub.Career] = -100;
        }
        else
        {
            graduation += gradDM;
            if (graduation < 8)
            {
                character.Age += dice.D(4);
                character.AddHistory("Dropped out of military academy.", character.Age);
                character.EducationHistory.Status = "Failed";
                character.NextTermBenefits.EnlistmentDM[Stub.Career] = 100;
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
                Stub.ServiceSkill(character, dice);
                Stub.ServiceSkill(character, dice);
                Stub.ServiceSkill(character, dice);
                character.Education += 1;

                character.NextTermBenefits.MustEnroll = Stub.Career;
            }
        }
    }
}
