namespace Grauenwolf.TravellerTools.Characters.Careers.Precareers;

class PsionicCommunity(SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase("Psionic Community", null, speciesCharacterBuilder)
{
    public override string? Source => "Traveller Companion, page  33";

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        if (character.CurrentTerm != 1)
            return false;

        var dm = 0;

        if (character.Intellect >= 8)
            dm += 1;
        dm += character.GetEnlistmentBonus(Career, null);
        dm += QualifyDM;

        if (!isPrecheck) //Free Psionic test if you choose this career.
        {
            TestPsionic(character, dice, character.Age);
            dm += character.Psi ?? -3;
        }

        return dice.RollHigh(dm, 8, isPrecheck);
    }

    internal override void Run(Character character, Dice dice)
    {
        character.LongTermBenefits.MayEnrollInSchool = false;
        character.AddHistory($"Entered a psionic community.", character.Age);

        character.EducationHistory = new EducationHistory();
        character.EducationHistory.Name = "Psionic Community";

        PreCareerEvents(character, dice, this, new SkillTemplateCollection());
        FixupSkills(character, dice);

        var graduation = dice.D(2, 6) + character.PsiDM + character.CurrentTermBenefits.GraduationDM;
        if (character.Intellect >= 8)
            graduation += 1;

        if (graduation < 6)
        {
            character.Age = Math.Max(character.Age + dice.D(4), character.History.Max(h => h.Age));
            character.AddHistory($"Dropped out of the psionic community.", character.Age);
            character.EducationHistory.Status = "Failed";
        }
        else
        {
            character.Psi += 1;
            character.Age += 4;
            character.Skills.Add("Science", "Psionicology");
            FixupSkills(character, dice);

            //int bonus;
            if (graduation >= 12)
            {
                foreach (var skill in character.Skills.Where(x => x.IsPsionicTalent && x.Level == 0))
                    skill.Level = 1;

                var skills = character.Skills.Where(x => x.IsPsionicTalent && x.Level == 1).ToList();
                if (skills.Count > 0)
                    dice.Choose(skills).Level = 2;
                FixupSkills(character, dice);

                character.EducationHistory.Status = "Honors";
                character.AddHistory($"Graduated with honors. Gain an enemy who is upset that {character.Name} left the community.", character.Age);
                character.AddEnemy();
            }
            else
            {
                var skills = character.Skills.Where(x => x.IsPsionicTalent && x.Level == 0).ToList();
                if (skills.Count > 0)
                    dice.Choose(skills).Level = 1;
                FixupSkills(character, dice);

                character.EducationHistory.Status = "Graduated";
                character.AddHistory($"Graduated. Gain an rival who is upset that {character.Name} left the community.", character.Age);
                character.AddRival();
            }

            character.LongTermBenefits.EnlistmentDM["Psion"] = 999; //Future enrollment is automatic.
        }
    }
}
