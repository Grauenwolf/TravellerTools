namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract public class MilitaryCareer(string name, string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : FullCareer(name, assignment, speciesCharacterBuilder)
{
    public int AdvancementDM { get; set; }
    public int CommissionDM { get; set; }
    internal override bool RankCarryover { get; } = true;
    protected abstract int AdvancedEductionMin { get; }
    protected virtual int CommssionTargetNumber => 8;

    internal override void Run(Character character, Dice dice)
    {
        var careerHistory = SetupAndSkills(character, dice);
        careerHistory.Terms += 1;
        character.LastCareer = careerHistory;

        //Early commission, possibly from military academy.
        if (careerHistory.CommissionRank == 0 && character.CurrentTermBenefits.FreeCommissionRoll)
        {
            AttemptCommission(character, dice, careerHistory);
        }

        var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
        if (survived)
        {
            Event(character, dice);
            FixupSkills(character, dice);

            character.Age += 4;
            character.BenefitRolls += 1;

            var totalTermsInCareer = character.CareerHistory.Where(pc => pc.Career == Career).Sum(c => c.Terms);

            //Not all people will attempt a commission even when possible
            var attemptCommission = (totalTermsInCareer == 1 || CommissionAttribute(character) >= 9) && dice.D(100) < Tables.OddsOfSuccess(CommissionAttributeDM(character), CommssionTargetNumber - character.CurrentTermBenefits.CommissionDM);

            var commissionEarned = false;

            if (careerHistory.CommissionRank == 0 && (attemptCommission || character.CurrentTermBenefits.FreeCommissionRoll))
                commissionEarned = AttemptCommission(character, dice, careerHistory);

            //always try for advancement even if a commission was earned.
            var advancementRoll = dice.D(2, 6);
            if (advancementRoll == 12)
            {
                character.AddHistory($"Forced to continue current assignment", character.Age);
                character.NextTermBenefits.MustEnroll = Assignment;
            }
            advancementRoll += character.GetDM(AdvancementAttribute) + character.GetAdvancementBonus(Career, Assignment) + AdvancementDM;

            if (advancementRoll >= AdvancementTarget)
            {
                Promote(character, dice, careerHistory);
                CareerSkill(character, dice);
                FixupSkills(character, dice);
            }
            if (advancementRoll <= careerHistory.Terms)
            {
                character.AddHistory($"Forced to muster out.", character.Age);
                character.NextTermBenefits.MusterOut = true;
            }
        }
        else
        {
            var mishapAge = character.Age + dice.D(4);

            character.NextTermBenefits.MusterOut = true;
            Mishap(character, dice, mishapAge);
            FixupSkills(character, dice);

            if (character.NextTermBenefits.MusterOut)
                character.Age = mishapAge;
            else
                character.Age += +4; //Complete the term dispite the mishap.
        }
    }

    protected abstract void AdvancedEducation(Character character, Dice dice);

    protected override void CareerSkill(Character character, Dice dice)
    {
        var skillTables = new List<SkillTable>();
        skillTables.Add(PersonalDevelopment);
        skillTables.Add(ServiceSkill);
        skillTables.Add(AssignmentSkills);
        if (character.Education >= AdvancedEductionMin)
            skillTables.Add(AdvancedEducation);
        if (character.LastCareer!.CommissionRank > 0)
            skillTables.Add(OfficerTraining);
        dice.Choose(skillTables)(character, dice);
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
               (1 - careerHistory.Terms)
               + CommissionDM;

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
    int CommissionAttributeDM(Character character) => Tables.DMCalc(CommissionAttribute(character));
}
