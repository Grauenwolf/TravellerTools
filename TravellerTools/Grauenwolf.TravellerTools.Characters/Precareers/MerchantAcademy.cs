namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class MerchantAcademy(SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase("Merchant Academy", null, speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Precareer;
    public override string? Source => "Traveller Companion, page  33";

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
            skillChoices.AddRange(SpecialtiesFor(character, "Pilot"));
            skillChoices.Add("Vacc Suit");
            skillChoices.AddRange(SpecialtiesFor(character, "Athletics"));
            skillChoices.Add("Mechanic");
            skillChoices.AddRange(SpecialtiesFor(character, "Engineer"));
            skillChoices.AddRange(SpecialtiesFor(character, "Electronics"));
        }

        //Add basic skills at level 0
        foreach (var skill in skillChoices.Select(x => x.Name).Distinct())
            character.Skills.Add(skill);
        FixupSkills(character, dice);

        character.Education += 1;
        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "Merchant Academy";

        PreCareerEvents(character, dice, this, skillChoices);
        FixupSkills(character, dice);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (character.Education >= 8)
            graduation += 1;
        if (character.SocialStanding >= 8)
            graduation += 1;

        if (graduation < 7)
        {
            character.Age = Math.Max(character.Age + dice.D(4), character.History.Max(h => h.Age));
            character.AddHistory($"Dropped out of the merchant academy.", character.Age);
            character.EducationHistory.Status = "Failed";
        }
        else
        {
            character.Age += 4;
            int bonus;
            if (graduation >= 11)
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
            if (skillChoices.Count > 0)
                character.Skills.Increase(dice.Choose(skillChoices)); //one level 0 becomes level 1
            FixupSkills(character, dice);
            character.Education += 1;

            if (schoolFlag)
                character.NextTermBenefits.MustEnroll = "Broker";
            else
                character.NextTermBenefits.MustEnroll = "Merchant Marine";

            character.LongTermBenefits.CareerAdvancementDM.Add("Merchant", bonus);
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
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

        return dice.RollHigh(dm, 9, isPrecheck);
    }
}
