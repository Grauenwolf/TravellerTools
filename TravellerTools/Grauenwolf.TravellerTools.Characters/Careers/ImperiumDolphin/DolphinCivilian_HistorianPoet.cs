namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinCivilian_HistorianPoet(CharacterBuilder characterBuilder) : DolphinCivilian("Historian Poet", characterBuilder)
{
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 8;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Science", "History");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
                return;

            case 4:
                character.Skills.Increase("Advocate");
                return;

            case 5:
                character.Skills.Increase("Persuade");
                return;

            case 6:
                character.Skills.Increase("Diplomat");
                return;
        }
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Drive");
        if (all || roll == 2)
            character.Skills.Add("Deception");
        if (all || roll == 3)
            character.Skills.Add("Recon");
        if (all || roll == 4)
            character.Skills.Add("Stealth");
        if (all || roll == 5)
            character.Skills.Add("Streetwise");
        if (all || roll == 6)
            character.Skills.Add("Survival");
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EducationDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 8, isPrecheck);
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Candidate";
                character.Skills.Add("Art", "Performer", 1);
                return;

            case 1:
                careerHistory.Title = "Apprentice";
                character.Skills.Add("Science", "History", 1);
                return;

            case 2:
                if (character.SocialStanding < 6)
                    character.SocialStanding = 6;
                else
                    character.SocialStanding += 1;
                return;

            case 3:
                careerHistory.Title = "Journeyman";
                return;

            case 4:
                if (character.SocialStanding < 8)
                    character.SocialStanding = 8;
                else
                    character.SocialStanding += 1;
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Master";
                if (character.SocialStanding < 10)
                    character.SocialStanding = 10;
                else
                    character.SocialStanding += 1;
                return;
        }
    }
}
