namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

abstract class Soulhunter(string assignment, CharacterBuilder characterBuilder) : MilitaryCareer("Soulhunter", assignment, characterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Pilot");
        if (all || roll == 2)
            character.Skills.Add("Vacc Suit");
        if (all || roll == 3)
            character.Skills.Add("Athletics");
        if (all || roll == 4)
            character.Skills.Add("Gunner");
        if (all || roll == 5)
            character.Skills.Add("Mechanic");
        if (all || roll == 6)
            character.Skills.Add("Gun Combat");
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
                character.AddHistory("Trapped behind enemy lines.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Survival");
                    skillList.Add("Stealth");
                    skillList.Add("Deception");
                    skillList.Add("Streetwise");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 4:
                character.AddHistory("Given a special assignment or duty on board your ship.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 5:
                character.AddHistory($"Given advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                {
                    var skillList = new SkillTemplateCollection(Book.RandomSkills);
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 6:
                character.AddHistory("Your vessel participates in a notable engagement.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor("Electronics"));
                    skillList.AddRange(SpecialtiesFor("Engineer"));
                    skillList.AddRange(SpecialtiesFor("Gunner"));
                    skillList.AddRange(SpecialtiesFor("Pilot"));
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }

                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory("On the front line of an assault and occupation", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Recon");
                    skillList.Add("Leadership");
                    skillList.AddRange(SpecialtiesFor("Gun Combat"));
                    skillList.Add("Electronics", "Comms");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 9:
                character.AddHistory("Foiled an attempted crime onboard your ship or in your unit, such as mutiny, sabotage, smuggling, or conspiracy. Gain an enemy", dice);
                character.AddEnemy();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory("Assigned a black ops mission.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 11:
                character.AddHistory("Commanding officer takes an interest in your career.", dice);
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
                character.AddHistory("Display heroism in battle.", dice);

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
                character.AddHistory("A mission goes horribly wrong. You and several others are captured and mistreated by the enemy. Gain gaoler as Enemy.", age);
                character.Strength -= 1;
                character.Dexterity -= 1;
                character.AddEnemy();
                return;

            case 3:
                int bestSkill = 0;
                switch (Assignment)
                {
                    case "Commando":
                        bestSkill = character.Skills.BestSkillLevel("Gun Combat", "Stealth");
                        break;

                    case "Flight":
                        bestSkill = character.Skills.BestSkillLevel("Sensors", "Pilot");
                        break;

                    case "Support":
                        bestSkill = character.Skills.BestSkillLevel("Mechanic", "Vacc Suit");
                        break;
                }

                if (dice.D(2, 6) + bestSkill >= 8)
                {
                    character.AddHistory("During a battle, defeat or victory depends on your actions. The ship suffers severe damage and you are blamed for the disaster, court-martialled, and discharged.", age);
                }
                else
                {
                    character.AddHistory("During a battle, defeat or victory depends on your actions. Your efforts ensure you are honourably discharged.", age);
                    character.BenefitRolls += 1;
                }
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory("You are blamed for an accident which causes the death of several crew members. Guilt drives you to exile.", age);
                    character.SocialStanding -= 1;
                }
                else
                {
                    character.AddHistory("You are falsely blamed for an accident which causes the death of several crew members. Gain you accusor as an enemy.", age);
                    character.AddEnemy();
                    character.BenefitRolls += 1;
                }
                return;

            case 5:
                character.AddHistory("You are tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.", age);
                character.AddRival();
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice)
    {
        var dm = character.EnduranceDM;
        dm += -1 * character.CareerHistory.Count;
        if (character.Age >= 30)
            dm += -2;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6);
    }

    internal override void ServiceSkill(Character character, Dice dice)
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Mechanic")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        if (careerHistory.CommissionRank == 0)
        {
            switch (careerHistory.Rank)
            {
                case 0:
                    careerHistory.Title = "Least Claw";
                    return;

                case 1:
                    careerHistory.Title = "Third Claw";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Gun Combat"));
                        skillList.Add("Mechanic");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }

                    return;

                case 2:
                    careerHistory.Title = "Second Claw";
                    return;

                case 3:
                    careerHistory.Title = "First Claw";
                    character.Skills.Add("Leadership", 1);
                    return;

                case 4:
                    careerHistory.Title = "Third Fang";
                    return;

                case 5:
                    careerHistory.Title = "Second Fang";
                    character.Endurance += 1;
                    return;

                case 6:
                    careerHistory.Title = "First Fang";
                    return;
            }
        }
        else
        {
            switch (careerHistory.CommissionRank)
            {
                case 1:
                    careerHistory.Title = "Kaltrhar";
                    character.Skills.Add("Melee", "Natural", 1);
                    return;

                case 2:
                    careerHistory.Title = "Shin Kaltrhar";
                    character.Skills.Add("Leadership", 1);
                    return;

                case 3:
                    careerHistory.Title = "Shilahn";
                    return;

                case 4:
                    careerHistory.Title = "Shiltrhar";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Tactics"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;

                case 5:
                    careerHistory.Title = "Shil Shintrah";
                    character.Skills.Add("Admin", 1);
                    return;

                case 6:
                    careerHistory.Title = "Shinalrhar";
                    character.SocialStanding += 1;
                    return;
            }
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
                character.Skills.Increase("Survival");
                return;

            case 3:
                character.Skills.Increase("Explosives");
                return;

            case 4:
                character.Skills.Increase("Navigation");
                return;

            case 5:
                character.Skills.Increase("Admin");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;
        }
    }

    protected override int CommissionAttribute(Character character) => character.Intellect;

    protected override void OfficerTraining(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Leadership");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));

                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
                return;

            case 4:
                character.Skills.Increase("Melee", "Natural");
                return;

            case 5:
                character.Skills.Increase("Admin");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Tactics")));
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
                character.SocialStanding += 1;
                return;
        }
    }
}
