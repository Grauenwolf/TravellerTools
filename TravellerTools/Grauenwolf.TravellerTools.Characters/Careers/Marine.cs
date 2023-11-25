namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class Marine(string assignment, Book book) : MilitaryCareer("Marine", assignment, book)
{
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add(dice.Choose(SpecialtiesFor("Athletics")));
        if (all || roll == 2)
            character.Skills.Add("Vacc Suit");
        if (all || roll == 3)
            character.Skills.Add(dice.Choose(SpecialtiesFor("Tactics")));
        if (all || roll == 4)
            character.Skills.Add(dice.Choose(SpecialtiesFor("Heavy Weapons")));
        if (all || roll == 5)
            character.Skills.Add(dice.Choose(SpecialtiesFor("Gun Combat")));
        if (all || roll == 6)
            character.Skills.Add("Stealth");
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
                character.AddHistory("Assigned to the security staff of a space station.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Vacc Suit");
                    skillList.Add("Athletics", "Dexterity");
                    character.Skills.Increase(dice.Choose(skillList));
                }

                return;

            case 5:
                character.AddHistory($"Advanced training in a specialist field", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(RandomSkills);
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 6:
                character.AddHistory("Assigned to an assault on an enemy fortress.", dice);
                if (dice.RollHigh(character.Skills.BestSkillLevel("Gun Combat", "Melee"), 8))
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Tactics", "Military");
                    skillList.Add("Leadership");
                    character.Skills.Increase(dice.Choose(skillList));
                }
                else
                {
                    character.AddHistory("Injured", dice);
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
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory("On the front lines of a planetary assault and occupation.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Recon");
                    skillList.AddRange(SpecialtiesFor("Gun Combat"));
                    skillList.Add("Leadership");
                    skillList.Add("Electronics", "Comms");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 9:
                var age = character.AddHistory("A mission goes disastrously wrong due to your commander’s error or incompetence, but you survive.", dice);
                if (dice.NextBoolean())
                {
                    character.AddHistory("Report commander and gain an Enemy.", age);
                    character.CurrentTermBenefits.AdvancementDM += 2;
                }
                else
                {
                    character.AddHistory("Cover for the commander and gain an Ally.", age);
                }
                return;

            case 10:
                character.AddHistory("Assigned to a black ops mission.", dice);
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

                if (character.LastCareer.CommissionRank == 0)
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
                character.AddHistory("A mission goes wrong; you and several others are captured and mistreated by the enemy. Gain your jailer as an Enemy.", age);
                character.AddEnemy();
                character.Strength += -1;
                character.Dexterity += -1;
                return;

            case 3:
                character.AddHistory("A mission goes wrong and you are stranded behind enemy lines. Ejected from the service.", age);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Stealth");
                    skillList.Add("Survival");
                    character.Skills.Increase(dice.Choose(skillList));
                }
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory("Refused to take part in a black ops mission that goes against the conscience and ejected from the service.", age);
                }
                else
                {
                    character.AddHistory("You are ordered to take part in a black ops mission that goes against your conscience. Gain the lone survivor as an Enemy.", age);
                    character.AddEnemy();
                    character.CurrentTermBenefits.MusterOut = false;
                }
                return;

            case 5:
                character.AddHistory("You are tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.", age);
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

        return dice.RollHigh(dm, 6);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                return;

            case 2:
                character.Skills.Increase("Vacc Suit");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Tactics")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Heavy Weapons")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;

            case 6:
                character.Skills.Increase("Stealth");
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
                    careerHistory.Title = "Marine";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Gun Combat"));
                        skillList.Add("Melee", "Blade");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;

                case 1:
                    careerHistory.Title = "Lance Corporal";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Gun Combat"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;

                case 2:
                    careerHistory.Title = "Corporal";
                    return;

                case 3:
                    careerHistory.Title = "Lance Sergeant";
                    character.Skills.Add("Leadership", 1);
                    return;

                case 4:
                    careerHistory.Title = "Sergeant";
                    return;

                case 5:
                    careerHistory.Title = "Gunnery Sergeant";
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
                    character.Skills.Add("Leadership", 1);
                    return;

                case 2:
                    careerHistory.Title = "Captain";
                    return;

                case 3:
                    careerHistory.Title = "Force Commander";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Tactics"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;

                case 4:
                    careerHistory.Title = "Lieutenant Colonel";
                    return;

                case 5:
                    careerHistory.Title = "Colonel";
                    if (character.SocialStanding < 10)
                        character.SocialStanding = 10;
                    else
                        character.SocialStanding += 1;
                    return;

                case 6:
                    careerHistory.Title = "Brigadier";
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
                return;

            case 6:
                character.Skills.Increase("Navigation");
                return;
        }
    }

    protected override void OfficerTraining(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Tactics")));
                return;

            case 3:
                character.Skills.Increase("Admin");
                return;

            case 4:
                character.Skills.Increase("Advocate");
                return;

            case 5:
                character.Skills.Increase("Vacc Suit");
                return;

            case 6:
                character.Skills.Increase("Leadership");
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
                character.Skills.Increase("Gambler");
                return;

            case 5:
                character.Skills.Increase("Melee", "Unarmed");
                return;

            case 6:
                character.Skills.Increase("Melee", "Unarmed");
                return;
        }
    }
}
