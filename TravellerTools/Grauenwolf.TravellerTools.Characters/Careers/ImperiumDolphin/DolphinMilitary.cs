namespace Grauenwolf.TravellerTools.Characters.Careers.ImperiumDolphin;

abstract class DolphinMilitary(string assignment, CharacterBuilder characterBuilder) : MilitaryCareer("Dolphin Military", assignment, characterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    protected override int CommssionTargetNumber => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Athletics");
        if (all || roll == 2)
            character.Skills.Add("Vacc Suit");
        if (all || roll == 3)
            character.Skills.Add("Electronics");
        if (all || roll == 4)
            character.Skills.Add("Electronics");
        if (all || roll == 5)
            character.Skills.Add("Gun Combat");
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
                character.AddHistory($"{character.Name} perform well during a black ops commando mission. Gain a Contact in an elite military unit.", dice);
                character.AddContact();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 4:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Vacc Suit"), 4))
                {
                    character.AddHistory($"Receive special training in underwater mine disposal.", dice);
                    character.Skills.Increase("Explosives");
                }
                else
                {
                    var age = character.AddHistory($"Injured while training in underwater mine disposal.", dice);
                    Injury(character, dice, age);
                }
                return;

            case 5:
                character.AddHistory($"Spend a lengthy period serving as an auxiliary aboard a spacecraft operating underwater, such \r\nas a system defence boat.", dice);
                if (dice.RollHigh(character.EducationDM, 7))
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Pilot", "Small Craft");
                    skillList.Add("Pilot", "Spacecraft");
                    skillList.Add("Electronics", "Sensors");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 6:
                character.AddHistory($"A brutal amphibious warfare campaign forces {character.Name} to spend as much time fighting out of water as in.", dice);
                if (dice.RollHigh(character.Skills.BestSkillLevel("Vacc Suit"), 7))
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));
                    skillList.Add("Leadership");
                    skillList.Add("Tactics", "Military");
                    character.Skills.Increase(dice.Choose(skillList), 1);
                }
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                {
                    var age = character.AddHistory($"Participate in a major sea battle", dice);
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Vacc Suit");
                    skillList.AddRange(SpecialtiesFor(character, "Melee"));
                    skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));
                    skillList.Add("Tactics", "Military");
                    character.Skills.Increase(dice.Choose(skillList), 1);
                }
                return;

            case 9:
                character.AddHistory($"{character.Name} is assigned to a joint mission with an intelligence agency to catch spies or insurgents operating near underwater base facilities. Gain an intelligence agent as a Contact.", dice);
                character.AddContact();
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Recon");
                    skillList.Add("Stealth");
                    character.Skills.Increase(dice.Choose(skillList), 1);
                }
                return;

            case 10:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"A mission goes badly wrong when a human leader disregards {character.Name}'s advice because {character.Name} is ‘just \r\na Dolphin’ and then blames {character.Name} for their mistake. {character.Name} quitely accept it.", dice);
                    character.CurrentTermBenefits.AdvancementDM += -1;
                }
                else
                {
                    var dm = Math.Max(character.SocialStandingDM, character.Skills.BestSkillLevel("Advocate"));
                    if (dice.RollHigh(dm, 8))
                    {
                        character.AddHistory($"A mission goes badly wrong when a human leader disregards {character.Name}'s advice because {character.Name} is ‘just a Dolphin’ and then blames {character.Name} for their mistake. {character.Name} prove {character.Name}'s innocence and they are dismissed.", dice);

                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Leadership");
                        skillList.Add("Advocate");
                        character.Skills.Increase(dice.Choose(skillList), 1);
                        character.CurrentTermBenefits.AdvancementDM += 1;
                    }
                    else
                    {
                        character.AddHistory($"A mission goes badly wrong when a human leader disregards {character.Name}'s advice because {character.Name} is ‘just a Dolphin’ and then blames {character.Name} for their mistake. {character.Name} try and fail to prove {character.Name}'s innocence.", dice);
                        character.CurrentTermBenefits.AdvancementDM += -2;
                    }
                }
                return;

            case 11:
                character.AddHistory($"Commanding officer takes an interest in {character.Name}'s career.", dice);
                switch (dice.D(2))
                {
                    case 1:
                        {
                            var skillList = new SkillTemplateCollection();
                            skillList.Add("Admin");
                            skillList.Add("Tactics", "Military");
                            skillList.RemoveOverlap(character.Skills, 1);
                            if (skillList.Count > 0)
                                character.Skills.Add(dice.Choose(skillList), 1);
                        }
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 2;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"Display heroism in battle.", dice);
                character.CurrentTermBenefits.AdvancementDM += 100; //also applies to commission rolls

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
                character.AddHistory($"{character.Name} encounter anti-Dolphin prejudice from the force {character.Name} is working with. {character.Name} resign in disgust.", age);
                return;

            case 3:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name}'s unit is involved in a savage campaign against primitive indigenous aquatic aliens that troubles {character.Name}'s conscience. If {character.Name} refuse to participate {character.Name} is ejected from this career.", age);
                }
                else
                {
                    character.AddHistory($"{character.Name}'s unit is involved in a savage campaign against primitive indigenous aquatic aliens that troubles \r\nyour conscience. If {character.Name} refuse to participate {character.Name} is ejected from this career. {character.Name} participate in the massacre and gain an Enemy.", age);
                    character.NextTermBenefits.MusterOut = false;
                }
                return;

            case 4:
                character.AddHistory($"An ambitious planetary operation goes badly wrong leaving {character.Name} beached ashore without functional protective gear. Dehydration and skin damage requires lengthy recovery.", age);
                character.Endurance -= 2;

                return;

            case 5:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"An investigation uncovers links between {character.Name}'s unit’s commander and a radical faction.", age);
                }
                else
                {
                    character.AddHistory($"An investigation uncovers links between {character.Name}'s unit’s commander and a radical faction. {character.Name} denounce {character.Name}'s commander. Gain one of his allies as an Enemy.", age);
                    character.NextTermBenefits.MusterOut = false;
                    character.AddEnemy();
                }
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.EducationDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Athletics")));
                return;

            case 2:
                character.Skills.Increase("Vacc Suit");
                return;

            case 3:
                character.Skills.Increase("Electronics", "Comms");
                return;

            case 4:
                character.Skills.Increase("Electronics", "Sensors");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Gun Combat")));
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
                    careerHistory.Title = "Private";
                    character.Skills.Add("Vacc Suit", 1);
                    return;

                case 1:
                    careerHistory.Title = "Lance Corporal";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor(character, "Gun Combat"));
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
                    character.Skills.Add("Electronics", "Computers");
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
                    character.Skills.Add("Leadership", 1);
                    return;

                case 2:
                    careerHistory.Title = "Captain";
                    character.Skills.Add("Melee", "Blade", 1);
                    return;

                case 3:
                    careerHistory.Title = "Major";
                    return;

                case 4:
                    careerHistory.Title = "Lt. Colonel";
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor(character, "Tactics"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;

                case 5:
                    careerHistory.Title = "Colonel";
                    return;

                case 6:
                    careerHistory.Title = "General";
                    if (character.SocialStanding < 10)
                        character.SocialStanding = 10;
                    else
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Engineer")));
                return;

            case 5:
                character.Skills.Increase("Mechanic");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Pilot")));
                return;
        }
    }

    protected override int CommissionAttribute(Character character) => character.SocialStanding;

    protected override void OfficerTraining(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Leadership");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Pilot")));
                return;

            case 4:
                character.Skills.Increase("Admin");
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Tactics")));
                return;

            case 6:
                character.Skills.Increase("Diplomat");
                return;
        }
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Carouse");
                return;

            case 2:
                character.Dexterity += 1;
                return;

            case 3:
                character.Endurance += 1;
                return;

            case 4:
                character.Strength += 1;
                return;

            case 5:
                character.Skills.Increase("Survival");
                return;

            case 6:
                character.Skills.Increase("Carouse");
                return;
        }
    }
}
