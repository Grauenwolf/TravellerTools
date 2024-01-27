namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class SchoolOfHardKnocks(SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase("School of Hard Knocks", null, speciesCharacterBuilder)
{
    public override string? Source => "Traveller Companion, page  34";

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        if (character.CurrentTerm != 1)
            return false;
        if (!character.LongTermBenefits.MayEnrollInSchool)
            return false;

        return character.SocialStanding <= 6;
    }

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        character.AddHistory($"Entered the school of hard knocks.", character.Age);
        character.Skills.Add("Streetwise", 1);

        var skillChoices = new SkillTemplateCollection();
        skillChoices.AddRange(SpecialtiesFor(character, "Athletics"));
        skillChoices.Add("Deception");
        skillChoices.AddRange(SpecialtiesFor(character, "Drive"));
        skillChoices.Add("Gambler");
        skillChoices.AddRange(SpecialtiesFor(character, "Melee"));
        skillChoices.Add("Persuade");
        skillChoices.Add("Stealth");

        var chosenSkills = new SkillTemplateCollection() { "Streetwise" };

        //Add basic skills at level 0
        skillChoices.RemoveOverlap(character.Skills, 0);
        foreach (var skill in dice.Pick(skillChoices, 2)) //Remove the ones we used.
        {
            chosenSkills.Add(skill);
            character.Skills.Add(skill);
        }
        FixupSkills(character, dice);

        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "School of Hard Knocks";

        PreCareerEvents(character, dice, this, chosenSkills);
        FixupSkills(character, dice);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (character.Endurance >= 9)
            graduation += 1;

        character.Age += 4;

        if (graduation < 7)
        {
            character.AddHistory($"Failed in the school of hard knocks.", character.Age);
            character.EducationHistory.Status = "Failed";
        }
        else
        {
            character.SocialStanding -= 1;

            var gunSkills = new SkillTemplateCollection(SpecialtiesFor(character, "Gun Combat"));
            gunSkills.RemoveOverlap(character.Skills, 0);
            if (gunSkills.Count > 0)
            {
                var guns = dice.Choose(gunSkills);
                character.Skills.Add(guns);
                chosenSkills.Add(guns);
            }

            skillChoices.RemoveOverlap(character.Skills, 0);
            foreach (var skill in dice.Pick(skillChoices, 3)) //Remove the ones we used.
            {
                chosenSkills.Add(skill);
                character.Skills.Add(skill);
            }
            FixupSkills(character, dice);

            //Not exactly right. It should be for the whole first career
            character.NextTermBenefits.AdvancementDM -= 2;
            character.NextTermBenefits.CommissionDM -= 2;

            if (graduation >= 11)
            {
                character.EducationHistory.Status = "Honors";
                character.AddHistory($"Graduated with honors.", character.Age);
                character.NextTermBenefits.MinRank = 2;

                character.Skills.Add("Carouse", 1);

                chosenSkills.RemoveOverlap(character.Skills, 1);
                character.Skills.Add(dice.Choose(chosenSkills), 1);
                FixupSkills(character, dice);
            }
            else
            {
                character.EducationHistory.Status = "Graduated";
                character.AddHistory($"Graduated.", character.Age);
                character.NextTermBenefits.MinRank = 1;
            }
        }
    }
}
