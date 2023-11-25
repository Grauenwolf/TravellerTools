namespace Grauenwolf.TravellerTools.Characters.Careers;

class Scavenger(Book book) : Drifter("Scavenger", book)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Pilot", "Small Craft");
                return;

            case 2:
                character.Skills.Increase("Mechanic");
                return;

            case 3:
                character.Skills.Increase("Astrogation");
                return;

            case 4:
                character.Skills.Increase("Vacc Suit");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Profession")));

                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;
        }
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Pilot", "Small Craft");
        if (all || roll == 2)
            character.Skills.Add("Mechanic");
        if (all || roll == 3)
            character.Skills.Add("Astrogation");
        if (all || roll == 4)
            character.Skills.Add("Vacc Suit");
        if (all || roll == 5)
            character.Skills.AddRange(SpecialtiesFor("Profession"));
        if (all || roll == 6)
            character.Skills.AddRange(SpecialtiesFor("Gun Combat"));
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                character.Skills.Add("Vacc Suit", 1);
                return;

            case 2:
                return;

            case 3:
                var skillList = new SkillTemplateCollection();
                skillList.Add("Profession", "Belter");
                skillList.Add("Mechanic");
                skillList.RemoveOverlap(character.Skills, 1);
                if (skillList.Count > 0)
                    character.Skills.Add(dice.Choose(skillList), 1);
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                return;
        }
    }
}
