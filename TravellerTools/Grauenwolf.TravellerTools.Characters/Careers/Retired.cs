namespace Grauenwolf.TravellerTools.Characters.Careers;

class Retired(SpeciesCharacterBuilder speciesCharacterBuilder) : CareerBase("Retired", null, speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.None;
    public override string? Source => null;

    public void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                InjuryRollAge(character, dice);
                return;

            case 3:
                character.AddHistory($"Birth or Death involving a family member or close friend.", dice);
                return;

            case 4:
                character.AddHistory($"A romantic relationship ends badly. Gain a Rival or Enemy.", dice);
                return;

            case 5:
                character.AddHistory($"A romantic relationship deepens, possibly leading to marriage. Gain an Ally.", dice);
                character.AddAlly();
                return;

            case 6:
                character.AddHistory($"A new romantic starts. Gain an Ally.", dice);
                character.AddAlly();
                return;

            case 7:
                character.AddHistory($"Gained a contact.", dice);
                character.AddContact();
                return;

            case 8:
                character.AddHistory($"Betrayal. Convert an Ally into a Rival or Enemy.", dice);
                //TODO: Change contact type
                return;

            case 9:
                character.AddHistory($"Moved to a new world.", dice);
                character.NextTermBenefits.QualificationDM += 1;
                return;

            case 10:
                character.AddHistory($"Good fortune.", dice);
                character.BenefitRollDMs.Add(2);
                return;

            case 11:
                if (dice.NextBoolean())
                {
                    character.BenefitRolls -= 1;
                    character.AddHistory($"Victim of a crime.", dice);
                }
                else
                {
                    character.AddHistory($"Accused of a crime.", dice);
                    character.NextTermBenefits.MustEnroll = "Prisoner";
                }
                return;

            case 12:
                UnusualLifeEvent(character, dice);
                return;
        }
    }

    internal override void Run(Character character, Dice dice)
    {
        CareerHistory careerHistory;
        if (!character.CareerHistory.Any(pc => pc.Career == "Retired"))
        {
            character.AddHistory($"Retired.", character.Age);
            careerHistory = new CareerHistory(character.Age, "Retired", null, CareerTypes, 0);
            character.CareerHistory.Add(careerHistory);
        }
        else
        {
            careerHistory = character.CareerHistory.Single(pc => pc.Career == "Retired");
            careerHistory.LastTermAge = character.Age;
        }
        careerHistory.Terms += 1;

        Event(character, dice);

        character.LastCareer = careerHistory;
        character.Age += 4;
        character.NextTermBenefits.MustEnroll = "Retired";
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        return character.LongTermBenefits.Retired;
    }
}
