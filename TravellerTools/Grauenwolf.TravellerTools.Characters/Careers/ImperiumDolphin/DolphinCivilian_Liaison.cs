namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

class DolphinCivilian_Liaison(CharacterBuilder characterBuilder) : DolphinCivilian("Liaison", characterBuilder)
{
    protected override string AdvancementAttribute => "Soc";

    protected override int AdvancementTarget => 6;

    protected override string SurvivalAttribute => "Int";

    protected override int SurvivalTarget => 6;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Vacc Suit");
                return;

            case 2:
                character.Skills.Increase("Vacc Suit");
                return;

            case 3:
                character.Skills.Increase("Carouse");
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 5:
                character.Skills.Increase("Diplomat");

                return;

            case 6:
                character.Skills.Increase("Persuade");
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                careerHistory.Title = "Representative";
                character.Skills.Add("Diplomat", 1);
                return;

            case 3:
                character.SocialStanding += 1;
                return;

            case 4:
                careerHistory.Title = "Ambassador";
                character.Skills.Add("Vacc Suit", 1);
                return;

            case 5:
                character.SocialStanding += 1;
                return;

            case 6:
                return;
        }
    }
}
