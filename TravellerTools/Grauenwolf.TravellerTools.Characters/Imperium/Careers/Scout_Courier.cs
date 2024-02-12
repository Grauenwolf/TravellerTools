﻿namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

class Scout_Courier(SpeciesCharacterBuilder speciesCharacterBuilder) : Scout("Courier", speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Military;
    protected override string AdvancementAttribute => "Edu";

    protected override int AdvancementTarget => 9;

    protected override string SurvivalAttribute => "End";

    protected override int SurvivalTarget => 5;

    internal override void AssignmentSkills(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Flyer", "Pilot|Spacecraft", "Engineer", "Athletics", "Astrogation");
    }
}