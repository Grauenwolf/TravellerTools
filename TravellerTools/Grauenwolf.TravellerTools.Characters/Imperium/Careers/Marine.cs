namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Marine(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryCareer("Marine", assignment, speciesCharacterBuilder)
{
    public override CareerGroup CareerGroup => CareerGroup.ImperiumCareer;
    public override CareerType CareerTypes => CareerType.StarportOfficer | CareerType.Military;
    public override string? Source => "Traveller Core, page 32";
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Athletics", "Vacc Suit", "Tactics", "Heavy Weapons", "Gun Combat", "Stealth");
    }

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                MishapRollAge(character, dice);
                character.NextTermBenefits.MusterOut = false;
                return;

            case 3:
                character.AddHistory($"Trapped behind enemy lines.", dice);
                AddOneSkill(character, dice, "Survival", "Stealth", "Deception", "Streetwise");
                return;

            case 4:
                character.AddHistory($"Assigned to the security staff of a space station.", dice);
                IncreaseOneSkill(character, dice, "Vacc Suit", "Athletics|Dexterity");

                return;

            case 5:
                character.AddHistory($"Advanced training in a specialist field", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    AddOneRandomSkill(character, dice);
                return;

            case 6:
                {
                    var age = character.AddHistory($"Assigned to an assault on an enemy fortress.", dice);
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Gun Combat", "Melee"), 8))
                    {
                        IncreaseOneSkill(character, dice, "Tactics|Military", "Leadership");
                    }
                    else
                    {
                        character.AddHistory($"Injured", age);
                        switch (dice.D(3))
                        {
                            case 1:
                                character.Strength -= 1;
                                return;

                            case 2:
                                character.Dexterity -= 1;
                                return;

                            case 3:
                                character.Endurance -= 1;
                                return;
                        }
                    }
                }
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"On the front lines of a planetary assault and occupation.", dice);
                AddOneSkill(character, dice, "Recon", "Gun Combat", "Leadership", "Electronics|Comms");
                return;

            case 9:
                {
                    var age = character.AddHistory($"A mission goes disastrously wrong due to {character.Name}'s commander’s error or incompetence, but {character.Name} survive.", dice);
                    if (dice.NextBoolean())
                    {
                        character.AddHistory($"Report commander and gain an Enemy.", age);
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    else
                    {
                        character.AddHistory($"Cover for the commander and gain an Ally.", age);
                    }
                }
                return;

            case 10:
                character.AddHistory($"Assigned to a black ops mission.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;

                return;

            case 11:
                character.AddHistory($"Commanding officer takes an interest in {character.Name}'s career.", dice);
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Add("Tactics", "Military", 1);
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"Display heroism in battle.", dice);

                character.CurrentTermBenefits.AdvancementDM += 100; //also applies to commission rolls

                if (character.LastCareer!.CommissionRank == 0)
                    character.CurrentTermBenefits.FreeCommissionRoll = true;

                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 1.0M;
        if (roll >= 8)
            return 1.0M;
        if (roll >= 4)
            return 0.75M;
        return 0;
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"A mission goes wrong; {character.Name} and several others are captured and mistreated by the enemy. Gain {character.Name}'s jailer as an Enemy.", age);
                character.AddEnemy();
                character.Strength += -1;
                character.Dexterity += -1;
                return;

            case 3:
                character.AddHistory($"A mission goes wrong and {character.Name} is stranded behind enemy lines. Ejected from the service.", age);

                IncreaseOneSkill(character, dice, "Stealth", "Survival");
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Refused to take part in a black ops mission that goes against the conscience and ejected from the service.", age);
                }
                else
                {
                    character.AddHistory($"{character.Name} is ordered to take part in a black ops mission that goes against {character.Name}'s conscience. Gain the lone survivor as an Enemy.", age);
                    character.AddEnemy();
                    character.CurrentTermBenefits.MusterOut = false;
                }
                return;

            case 5:
                character.AddHistory($"{character.Name} is tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.", age);
                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Athletics", "Vacc Suit", "Tactics", "Heavy Weapons", "Gun Combat", "Stealth");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        if (careerHistory.CommissionRank == 0)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    careerHistory.Title = "Marine";
                    if (allowBonus)
                        AddOneSkill(character, dice, "Gun Combat", "Melee|Blade");
                    return;

                case 1:
                    careerHistory.Title = "Lance Corporal";
                    if (allowBonus)
                        AddOneSkill(character, dice, "Gun Combat");
                    return;

                case 2:
                    careerHistory.Title = "Corporal";
                    return;

                case 3:
                    careerHistory.Title = "Lance Sergeant";
                    if (allowBonus)
                        character.Skills.Add("Leadership", 1);
                    return;

                case 4:
                    careerHistory.Title = "Sergeant";
                    return;

                case 5:
                    careerHistory.Title = "Gunnery Sergeant";
                    if (allowBonus)
                        character.Endurance += 1;
                    return;

                case 6:
                    careerHistory.Title = "Sergeant Major";
                    return;
            }
        }
        else
        {
            switch (careerHistory.CommissionRank)
            {
                case 1:
                    careerHistory.Title = "Lieutenant";
                    if (allowBonus)
                        character.Skills.Add("Leadership", 1);
                    return;

                case 2:
                    careerHistory.Title = "Captain";
                    return;

                case 3:
                    careerHistory.Title = "Force Commander";
                    if (allowBonus)
                        AddOneSkill(character, dice, "Tactics");
                    return;

                case 4:
                    careerHistory.Title = "Lieutenant Colonel";
                    return;

                case 5:
                    careerHistory.Title = "Colonel";
                    if (allowBonus)
                    {
                        if (character.SocialStanding < 10)
                            character.SocialStanding = 10;
                        else
                            character.SocialStanding += 1;
                    }
                    return;

                case 6:
                    careerHistory.Title = "Brigadier";
                    return;
            }
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Medic", "Survival", "Explosives", "Engineer", "Pilot", "Navigation");
    }

    protected override void OfficerTraining(Character character, Dice dice)
    {
        Increase(character, dice, "Electronics", "Tactics", "Admin", "Advocate", "Vacc Suit", "Leadership");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EnduranceDM;
        dm += -1 * character.CareerHistory.Count;
        if (character.Age >= 30)
            dm += -2;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Gambler", "Melee|Unarmed", "Melee|Unarmed");
    }
}
