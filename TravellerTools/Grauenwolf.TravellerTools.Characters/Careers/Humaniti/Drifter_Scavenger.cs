namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Drifter_Scavenger(SpeciesCharacterBuilder speciesCharacterBuilder) : Drifter("Scavenger", speciesCharacterBuilder)
{
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Pilot|Small Craft", "Mechanic", "Astrogation", "Vacc Suit", "Profession", "Gun Combat");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Pilot", "Mechanic", "Astrogation", "Vacc Suit", "Profession", "Gun Combat");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                if (allowBonus)
                    character.Skills.Add("Vacc Suit", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    AddOneSkill(character, dice, "Profession|Belter", "Mechanic");
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
