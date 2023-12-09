namespace Grauenwolf.TravellerTools.Characters.Careers;

class MerchantMarine(CharacterBuilder characterBuilder) : Merchant("Merchant Marine", characterBuilder)
{
    protected override string AdvancementAttribute => "Int";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "Edu";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));

                return;

            case 2:
                character.Skills.Increase("Vacc Suit");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                return;

            case 4:
                character.Skills.Increase("Mechanic");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Crewman";
                return;

            case 1:
                careerHistory.Title = "Senior Crewman";
                character.Skills.Add("Mechanic", 1);
                return;

            case 2:
                careerHistory.Title = "4th  Officer";
                return;

            case 3:
                careerHistory.Title = "3rd  Officer";
                return;

            case 4:
                careerHistory.Title = "2nd  Officer";
                var skillList = new SkillTemplateCollection(SpecialtiesFor("Pilot"));
                skillList.RemoveOverlap(character.Skills, 1);
                if (skillList.Count > 0)
                    character.Skills.Add(dice.Choose(skillList), 1);

                return;

            case 5:
                careerHistory.Title = "1st  Officer";
                character.SocialStanding += 1;
                return;

            case 6:
                careerHistory.Title = "Captain";
                return;
        }
    }
}
