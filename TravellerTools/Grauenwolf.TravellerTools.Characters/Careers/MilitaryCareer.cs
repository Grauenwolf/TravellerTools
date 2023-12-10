using Grauenwolf.TravellerTools;

namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class MilitaryCareer : FullCareer
{
    protected MilitaryCareer(string name, string assignment, CharacterBuilder characterBuilder) : base(name, assignment, characterBuilder)
    {
    }

    protected virtual int CommssionTargetNumber => 8;

    internal override void Run(Character character, Dice dice)
    {
        CareerHistory careerHistory;
        if (!character.CareerHistory.Any(pc => pc.Career == Career))
        {
            careerHistory = new CareerHistory(Career, Assignment, 0);
            ChangeCareer(character, dice, careerHistory);
            BasicTraining(character, dice, character.CareerHistory.Count == 0);
            FixupSkills(character);

            character.CareerHistory.Add(careerHistory); //add this after basic training so the prior count isn't wrong.
        }
        else
        {
            if (!character.CareerHistory.Any(pc => pc.Assignment == Assignment))
            {
                careerHistory = new CareerHistory(Career, Assignment, 0);
                ChangeAssignment(character, dice, careerHistory);
                character.CareerHistory.Add(careerHistory);
            }
            else if (character.LastCareer?.Assignment == Assignment)
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Continued as {careerHistory.LongName}.", character.Age);
            }
            else
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Assignment == Assignment);
                character.AddHistory($"Returned to {careerHistory.LongName}.", character.Age);
            }

            var skillTables = new List<SkillTable>();
            skillTables.Add(PersonalDevelopment);
            skillTables.Add(ServiceSkill);
            skillTables.Add(AssignmentSkills);
            if (character.Education >= AdvancedEductionMin)
                skillTables.Add(AdvancedEducation);
            if (careerHistory.CommissionRank > 0)
                skillTables.Add(OfficerTraining);

            dice.Choose(skillTables)(character, dice);
            FixupSkills(character);
        }
        careerHistory.Terms += 1;
        character.LastCareer = careerHistory;

        //rank carry-over
        careerHistory.Rank = character.CareerHistory.Where(c => c.Career == Career).Max(c => c.Rank);
        careerHistory.CommissionRank = character.CareerHistory.Where(c => c.Career == Career).Max(c => c.CommissionRank);

        //Early commission, possibly from military academy.
        if (careerHistory.CommissionRank == 0 && character.CurrentTermBenefits.FreeCommissionRoll)
        {
            AttemptCommission(character, dice, careerHistory);
        }

        var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
        if (survived)
        {
            Event(character, dice);
            FixupSkills(character);

            character.Age += 4;
            character.BenefitRolls += 1;

            var totalTermsInCareer = character.CareerHistory.Where(pc => pc.Career == Career).Sum(c => c.Terms);

            //Not all people will attempt a commission even when possible
            var attemptCommission = (totalTermsInCareer == 1 || CommissionAttribute(character) >= 9) && dice.D(100) < Book.OddsOfSuccess(character, CommissionAttributeDM(character), CommssionTargetNumber - character.CurrentTermBenefits.CommissionDM);

            var commissionEarned = false;

            if (careerHistory.CommissionRank == 0 && (attemptCommission || character.CurrentTermBenefits.FreeCommissionRoll))
                commissionEarned = AttemptCommission(character, dice, careerHistory);

            //always try for advancement even if a commission was earned.
            var advancementRoll = dice.D(2, 6);
            if (advancementRoll == 12)
            {
                character.AddHistory("Forced to continue current assignment", character.Age);
                character.NextTermBenefits.MustEnroll = Assignment;
            }
            advancementRoll += character.GetDM(AdvancementAttribute) + character.CurrentTermBenefits.AdvancementDM + character.LongTermBenefits.AdvancementDM;

            if (advancementRoll >= AdvancementTarget)
            {
                Promote(character, dice, careerHistory);

                //advancement skill
                var skillTables = new List<SkillTable>();
                skillTables.Add(PersonalDevelopment);
                skillTables.Add(ServiceSkill);
                skillTables.Add(AssignmentSkills);
                if (character.Education >= AdvancedEductionMin)
                    skillTables.Add(AdvancedEducation);
                if (careerHistory.CommissionRank > 0)
                    skillTables.Add(OfficerTraining);
                dice.Choose(skillTables)(character, dice);
                FixupSkills(character);
            }
            if (advancementRoll <= careerHistory.Terms)
            {
                character.AddHistory("Forced to muster out.", character.Age);
                character.NextTermBenefits.MusterOut = true;
            }
        }
        else
        {
            var mishapAge = character.Age + dice.D(4);

            character.NextTermBenefits.MusterOut = true;
            Mishap(character, dice, mishapAge);
            FixupSkills(character);

            if (character.NextTermBenefits.MusterOut)
                character.Age = mishapAge;
            else
                character.Age += +4; //Complete the term dispite the mishap.
        }
    }

    protected virtual void BasicTraining(Character character, Dice dice, bool firstCareer)
    {
        if (firstCareer)
            for (var i = 1; i < 7; i++)
                ServiceSkill(character, dice);
        else
            ServiceSkill(character, dice);
    }

    /// <summary>
    /// This is used when determining if a commission will be attempted.
    /// </summary>
    protected virtual int CommissionAttribute(Character character) => character.SocialStanding;

    protected abstract void OfficerTraining(Character character, Dice dice);

    bool AttemptCommission(Character character, Dice dice, CareerHistory careerHistory)
    {
        var commissionDM =
               CommissionAttributeDM(character) +
               character.CurrentTermBenefits.AdvancementDM +
               character.CurrentTermBenefits.CommissionDM +
               character.LongTermBenefits.CommissionDM +
               (1 - careerHistory.Terms);

        character.CurrentTermBenefits.FreeCommissionRoll = false;
        character.CurrentTermBenefits.CommissionDM = 0;

        if (dice.RollHigh(commissionDM, CommssionTargetNumber))
        {
            Comissioned(character, dice, careerHistory);
            return true;
        }
        else
        {
            character.AddHistory($"Attempt at commissioned failed.", character.Age);
            return false;
        }
    }

    /// <summary>
    /// This is the DM used for the actual commission roll.
    /// </summary>
    int CommissionAttributeDM(Character character) => Character.DMCalc(CommissionAttribute(character));
}
