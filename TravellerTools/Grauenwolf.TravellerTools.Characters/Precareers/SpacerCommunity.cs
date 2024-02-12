namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class SpacerCommunity(SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase("Spacer Community", null, speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Precareer;
    public override string? Source => "Traveller Companion, page  34";

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;

        character.AddHistory($"Entered a spacer community.", character.Age);
        character.Skills.Add("Vacc Suit", 1);

        var skillChoices = new SkillTemplateCollection();
        skillChoices.Add("Astrogation");
        skillChoices.AddRange(SpecialtiesFor(character, "Electronics"));
        skillChoices.AddRange(SpecialtiesFor(character, "Engineer"));
        skillChoices.AddRange(SpecialtiesFor(character, "Profession"));

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
        character.EducationHistory.Name = "Spacer Community";

        PreCareerEvents(character, dice, this, chosenSkills);
        FixupSkills(character, dice);

        var graduation = dice.D(2, 6) + character.IntellectDM + character.CurrentTermBenefits.GraduationDM;
        if (character.Dexterity >= 6)
            graduation += 1;

        if (graduation < 7)
        {
            character.Age += dice.D(4);
            character.AddHistory($"Left the spacer community.", character.Age);
            character.EducationHistory.Status = "Failed";
        }
        else
        {
            character.Age += 4;

            character.SocialStanding -= 1;

            var pilotSkills = new SkillTemplateCollection(SpecialtiesFor(character, "Pilot"));
            pilotSkills.RemoveOverlap(character.Skills, 0);
            if (pilotSkills.Count > 0)
            {
                var pilot = dice.Choose(pilotSkills);
                character.Skills.Add(pilot);
                chosenSkills.Add(pilot);
            }

            skillChoices.RemoveOverlap(character.Skills, 0);
            if (skillChoices.Count > 0)
                foreach (var skill in dice.Pick(skillChoices, Math.Min(skillChoices.Count, 2))) //Remove the ones we used.
                {
                    chosenSkills.Add(skill);
                    character.Skills.Add(skill);
                }
            FixupSkills(character, dice);

            character.Skills.Add(dice.Choose(chosenSkills), 1);
            FixupSkills(character, dice);

            character.Dexterity += 1;
            character.SocialStanding += -2;

            character.LongTermBenefits.CareerAdvancementDM["Free Trader"] = 2;
            character.LongTermBenefits.EnlistmentDM["Free Trader"] = 2;

            if (graduation >= 11)
            {
                character.EducationHistory.Status = "Honors";
                character.AddHistory($"Graduated with honors.", character.Age);

                character.Skills.Add("Jack-of-All-Trades", 1);
                FixupSkills(character, dice);
            }
            else
            {
                character.EducationHistory.Status = "Graduated";
                character.AddHistory($"Graduated.", character.Age);
            }
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        if (character.CurrentTerm != 1)
            return false;
        if (!character.LongTermBenefits.MayEnrollInSchool)
            return false;

        var dm = character.IntellectDM;
        if (character.Dexterity >= 8)
            dm += 1;

        dm += character.GetEnlistmentBonus(Career, null);
        dm += QualifyDM;

        return dice.RollHigh(dm, 7, isPrecheck);
    }
}
