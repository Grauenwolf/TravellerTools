namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Army(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : MilitaryCareer("Army", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Traveller Core, page 24";
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
        {
            if (all)
            {
                character.Skills.Add("Drive");
                character.Skills.Add("Vacc Suit");
            }
            else
            {
                var skillList = new SkillTemplateCollection();
                skillList.Add("Drive");
                skillList.Add("Vacc Suit");
                skillList.RemoveOverlap(character.Skills, 0);
                if (skillList.Count > 0)
                    character.Skills.Add(dice.Choose(skillList));
            }
        }

        if (all || roll == 2)
            character.Skills.Add("Athletics");
        if (all || roll == 3)
            character.Skills.Add("Gun Combat");
        if (all || roll == 4)
            character.Skills.Add("Recon");
        if (all || roll == 5)
            character.Skills.Add("Melee");
        if (all || roll == 6)
            character.Skills.Add("Heavy Weapons");
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
                character.AddHistory($"Assigned to a planet with a hostile or wild environment.", dice);
                AddOneSkill(character, dice, "Vacc Suit", "Engineer", "Animals|Handling", "Animals|Training", "Recon");
                return;

            case 4:
                character.AddHistory($"Assigned to an urbanised planet torn by war.", dice);
                AddOneSkill(character, dice, "Stealth", "Streetwise", "Persuade", "Recon");

                return;

            case 5:
                character.AddHistory($"Given a special assignment or duty in {character.Name}'s unit.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                var age = character.AddHistory($"Thrown into a brutal ground war", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    IncreaseOneSkill(character, dice, "Gun Combat", "Leadership");
                else
                    Injury(character, dice, age);
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"Advanced training in a specialist field", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    dice.Choose(character.Skills).Level += 1;
                return;

            case 9:
                character.AddHistory($"Surrounded and outnumbered by the enemy, {character.Name} hold out until relief arrives.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory($"Assigned to a peacekeeping role.", dice);
                AddOneSkill(character, dice, "Admin", "Admin", "Deception", "Recon");
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
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"Unit is slaughtered in a disastrous battle, for which {character.Name} blame {character.Name}'s commander. Gain commander as Enemy.", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"Sent to a very unpleasant region (jungle, swamp, desert, icecap, urban) to battle against guerrilla fighters and rebels. Discharged because of stress, injury or because the government wishes to bury the whole incident.", age);
                character.AddHistory($"Gain rebels as Enemy", age);
                character.AddEnemy();
                IncreaseOneSkill(character, dice, "Recon", "Survival");
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Joined commanding officer is engaged in some illegal activity, such as weapon smuggling. Gain an Ally.", age);
                    character.AddAlly();
                }
                else
                {
                    character.AddHistory($"Forced out after co-operating with military investigation in commanding officer's illegal activity.", age);
                    character.BenefitRolls += 1;
                }
                return;

            case 5:
                character.AddHistory($"{character.Name} is tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.", age);
                character.AddRival();
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EnduranceDM;
        dm += -1 * character.CareerHistory.Count;
        if (character.Age >= 30)
            dm += -2;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 5, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Drive,Vacc Suit", "Athletics", "Gun Combat", "Recon", "Melee", "Heavy Weapons");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        if (careerHistory.CommissionRank == 0)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    careerHistory.Title = "Private";
                    if (allowBonus)
                        character.Skills.Add(dice.Choose(SpecialtiesFor(character, "Gun Combat")), 1);
                    return;

                case 1:
                    careerHistory.Title = "Lance Corporal";
                    if (allowBonus)
                        character.Skills.Add("Recon", 1);
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
                    careerHistory.Title = "Major";
                    if (allowBonus)
                        character.Skills.Add("Tactics", "Military", 1);
                    return;

                case 4:
                    careerHistory.Title = "Lieutenant Colonel";
                    return;

                case 5:
                    careerHistory.Title = "Colonel";
                    return;

                case 6:
                    careerHistory.Title = "General";
                    if (allowBonus)
                    {
                        if (character.SocialStanding < 10)
                            character.SocialStanding = 10;
                        else
                            character.SocialStanding += 1;
                    }
                    return;
            }
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Tactics|Military", "Electronics", "Navigation", "Explosives", "Engineer", "Survival");
    }

    protected override void OfficerTraining(Character character, Dice dice)
    {
        Increase(character, dice, "Tactics|Military", "Leadership", "Advocate", "Diplomat", "Electronics", "Admin");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Gambler", "Medic", "Melee");
    }
}
