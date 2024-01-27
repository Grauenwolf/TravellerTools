namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Citizen(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Citizen", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Traveller Core, page 26";
    protected override int AdvancedEductionMin => 10;

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                MishapRollAge(character, dice);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                character.AddHistory($"Political upheaval strikes {character.Name}'s homeworld, and {character.Name} is caught up in the revolution.", dice);
                AddOneSkill(character, dice, "Advocate", "Persuade", "Explosives", "Streetwise");

                if (dice.RollHigh(8))
                    character.CurrentTermBenefits.AdvancementDM += 2;
                else
                    character.NextTermBenefits.SurvivalDM += -2;
                return;

            case 4:
                character.AddHistory($"Spent time maintaining and using heavy vehicles.", dice);
                IncreaseOneSkill(character, dice, "Drive", "Electronics", "Flyer", "Engineer");
                return;

            case 5:
                character.AddHistory($"{character.Name}'s business expands, {character.Name}'s corporation grows, or the colony thrives.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                character.AddHistory($"Advanced training in a specialist field.", dice);
                AddOneSkill(character, dice, "Gun Combat");

                if (dice.RollHigh(character.EducationDM, 10))
                    AddOneRandomSkill(character, dice);
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                var age = character.AddHistory($"{character.Name} learn something {character.Name} should not have – a corporate secret, a political scandal – which {character.Name} can profit from illegally.", dice);
                character.BenefitRollDMs.Add(1);
                switch (dice.D(3))
                {
                    case 1:
                        character.Skills.Add("Streetwise", 1);
                        return;

                    case 2:
                        character.Skills.Add("Deception", 1);
                        return;

                    case 3:
                        character.AddHistory($"Gain a criminal contact.", age);
                        character.AddContact();
                        return;
                }
                return;

            case 9:
                character.AddHistory($"{character.Name} is rewarded for {character.Name}'s diligence or cunning.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory($"{character.Name} gain experience in a technical field as a computer operator or surveyor.", dice);
                IncreaseOneSkill(character, dice, "Electronics", "Engineer");
                return;

            case 11:
                character.AddHistory($"{character.Name} befriend a superior in the corporation or the colony.", dice);
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Add("Diplomat", 1);
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"{character.Name} rise to a position of power in {character.Name}'s colony or corporation.", dice);
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 1.0M;
        if (roll >= 8)
            return 0.75M;
        if (roll >= 4)
            return 0.50M;
        return 0;
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"Life ruined by a criminal gang. Gain the gang as an Enemy", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"Hard times caused by a lack of interstellar trade costs {character.Name} {character.Name}'s job.", age);
                character.SocialStanding += -1;
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Co-operate with investigation by the planetary authorities. The business or colony is shut down.", age);
                    character.NextTermBenefits.QualificationDM += 2;
                }
                else
                {
                    character.AddHistory($"Refused to co-operate with investigation by the planetary authorities. Gain an Ally", age);
                }
                return;

            case 5:
                character.AddHistory($"A revolution, attack or other unusual event throws {character.Name}'s life into chaos, forcing {character.Name} to leave the planet.", age);
                if (dice.RollHigh(character.Skills.BestSkillLevel("Streetwise"), 8))
                    dice.Choose(character.Skills).Level += 1;
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EducationDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 5, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Drive", "Flyer", "Streetwise", "Melee", "Steward", "Profession");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Art", "Advocate", "Diplomat", "Language", "Electronics|Computers", "Medic");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Education", "Intellect", "Carouse", "Gambler", "Drive", "Jack-of-All-Trades");
    }
}
