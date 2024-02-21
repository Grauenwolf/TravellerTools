namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Drifter_Barbarian(SpeciesCharacterBuilder speciesCharacterBuilder) : Drifter("Barbarian", speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Outsider | CareerTypes.ViolentNonMilitary | CareerTypes.Violent;
    protected override string AdvancementAttribute => "Str";

    protected override int AdvancementTarget => 7;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 7;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Animals", "Carouse", "Melee|Blade", "Stealth", "Seafarer|Personal,Seafarer|Sails", "Survival");
    }

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Animals", "Carouse", "Melee", "Stealth", "Seafarer", "Survival");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                if (allowBonus)
                    character.Skills.Add("Survival", 1);
                return;

            case 2:
                careerHistory.Title = "Warrior";
                if (allowBonus)
                    character.Skills.Add("Melee", "Blade", 1);
                return;

            case 3:
                return;

            case 4:
                careerHistory.Title = "Chieftain";
                if (allowBonus)
                    character.Skills.Add("Leadership", 1);
                return;

            case 5:
                return;

            case 6:
                careerHistory.Title = "Warlord";
                return;
        }
    }
}
