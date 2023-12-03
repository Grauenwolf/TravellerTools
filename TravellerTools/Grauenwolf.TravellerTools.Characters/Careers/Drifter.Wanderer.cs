using Grauenwolf.TravellerTools.Characters.Careers;

namespace Grauenwolf.TravellerTools.Characters;

class Wanderer(Book book) : Drifter("Wanderer", book)
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                return;

            case 2:
                character.Skills.Increase("Deception");
                return;

            case 3:
                character.Skills.Increase("Recon");
                return;

            case 4:
                character.Skills.Increase("Stealth");
                return;

            case 5:
                character.Skills.Increase("Streetwise");
                return;

            case 6:
                character.Skills.Increase("Survival");
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

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                character.Skills.Add("Streetwise", 1);
                return;

            case 2:
                return;

            case 3:
                character.Skills.Add("Deception", 1);
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
