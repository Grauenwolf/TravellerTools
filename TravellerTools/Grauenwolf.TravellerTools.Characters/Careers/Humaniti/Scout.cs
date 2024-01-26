namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Scout(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Scout", assignment, speciesCharacterBuilder)
{
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Pilot", "Survival", "Mechanic", "Astrogation", "Vacc Suit", "Gun Combat");
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
                if (dice.NextBoolean())
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Pilot"), 8))
                    {
                        character.AddHistory($"{character.Name}'s ship is ambushed by enemy vessels. {character.Name} successfully ran to the jump point.", dice);
                        character.Skills.Add("Electronics", "Sensors", 1);
                    }
                    else
                    {
                        character.AddHistory($"{character.Name}'s ship is ambushed by enemy vessels. Attempted to run but caught and ship is destroyed.", dice);
                        character.NextTermBenefits.MusterOut = true;
                    }
                }
                else
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Persuade"), 10))
                    {
                        character.AddHistory($"{character.Name}'s ship is ambushed by enemy vessels. {character.Name} successfully bargain with them.", dice);
                        character.Skills.Add("Electronics", "Sensors", 1);
                    }
                    else
                    {
                        character.AddHistory($"{character.Name}'s ship is ambushed by enemy vessels. Attempted to bargain with them but fail and the ship is destroyed.", dice);
                        character.NextTermBenefits.MusterOut = true;
                    }
                }
                return;

            case 4:
                character.AddHistory($"{character.Name} survey an alien world.", dice);
                AddOneSkill(character, dice, "Animals|Handling", "Animals|Training", "Survival", "Recon");
                return;

            case 5:
                character.AddHistory($"Perform an exemplary service for the scouts.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                character.AddHistory($"Spend several years jumping from world to world in {character.Name}'s scout ship.", dice);
                AddOneSkill(character, dice, "Astrogation", "Electronics", "Navigation", "Pilot|Small Craft", "Mechanic");
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Electronics", "Deception"), 8))
                {
                    character.AddHistory($"When dealing with an alien race, {character.Name} have an opportunity to gather extra intelligence about them. Gain an Ally in the Imperium", dice);
                    character.AddAlly();
                    character.BenefitRollDMs.Add(2);
                }
                else
                {
                    var age = character.AddHistory($"When dealing with an alien race, {character.Name} botch an opportunity to gather extra intelligence about them.", dice);
                    Mishap(character, dice, age);
                    character.NextTermBenefits.MusterOut = false;
                }
                return;

            case 9:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Medic", "Engineer"), 8))
                {
                    character.AddHistory($"{character.Name}'s scout ship is one of the first on the scene to rescue the survivors of a disaster. Gain a Contact.", dice);
                    character.AddContact();
                    character.BenefitRollDMs.Add(2);
                }
                else
                {
                    var age = character.AddHistory($"{character.Name}'s scout ship is one of the first on the scene to rescue the survivors of a disaster but {character.Name} fail to help. Gain an Enemy.", dice);
                    character.AddEnemy();
                    Mishap(character, dice, age);
                    character.NextTermBenefits.MusterOut = false;
                }
                return;

            case 10:
                {
                    var age = character.AddHistory($"{character.Name} spend a great deal of time on the fringes of Charted Space.", dice);
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Survival", "Pilot"), 8))
                    {
                        character.AddHistory($"Gain a contact in an alien race.", age);
                        character.AddContact();
                        dice.Choose(character.Skills).Level += 1;
                    }
                    else
                    {
                        Mishap(character, dice, age);
                        character.NextTermBenefits.MusterOut = false;
                    }
                    return;
                }
            case 11:
                character.AddHistory($"Serve as the courier for an important message from the Imperium.", dice);
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Increase("Diplomat");
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"{character.Name} discover a world, item or information of worth to the Imperium.", dice);
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 0.75M;
        if (roll >= 8)
            return 0.50M;
        if (roll >= 4)
            return 0.00M;
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
                character.AddHistory($"Psychologically damaged by {character.Name}'s time in the scouts.", age);
                if (dice.NextBoolean())
                    character.Intellect += -1;
                else
                    character.SocialStanding += -1;
                return;

            case 3:
                character.AddHistory($"{character.Name}'s ship is damaged, and {character.Name} have to hitch-hike {character.Name}'s way back across the stars to the nearest scout base.", age);
                int countC = dice.D(6);
                int countE = dice.D(3);
                character.AddHistory($"Gain {countC} Contacts. Gain {countE} Enemies.", age);
                character.AddContact(countC);
                character.AddEnemy(countE);
                return;

            case 4:
                character.AddHistory($"{character.Name} inadvertently cause a conflict between the Imperium and a minor world or race. Gain a Rival.", age);
                character.AddRival();
                character.Skills.Add("Diplomat", 1);
                return;

            case 5:
                character.AddHistory($"{character.Name} have no idea what happened to {character.Name} – they found {character.Name}'s ship drifting on the fringes of friendly space.", age);
                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.IntellectDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 5, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                if (dice.NextBoolean())
                    character.Skills.Increase("Pilot", "Small Craft");
                else
                    character.Skills.Increase("Pilot", "Spacecraft");
                return;

            case 2:
                character.Skills.Increase("Survival");
                return;

            case 3:
                character.Skills.Increase("Mechanic");
                return;

            case 4:
                character.Skills.Increase("Astrogation");
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                careerHistory.Title = "Scout";
                if (allowBonus)
                    character.Skills.Add("Vacc Suit", 1);
                return;

            case 2:
                return;

            case 3:
                careerHistory.Title = "Senior Scout";
                if (allowBonus)
                    AddOneSkill(character, dice, "Pilot");
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Medic");
                return;

            case 2:
                character.Skills.Increase("Navigation");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Seafarer")));
                return;

            case 4:
                character.Skills.Increase("Explosives");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
                return;

            case 6:
                character.Skills.Increase("Jack-of-All-Trades");
                return;
        }
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Strength += 1;
                return;

            case 2:
                character.Dexterity += 1;
                return;

            case 3:
                character.Endurance += 1;
                return;

            case 4:
                character.Intellect += 1;
                return;

            case 5:
                character.Education += 1;
                return;

            case 6:
                character.Skills.Increase("Jack-of-All-Trades");
                return;
        }
    }
}
