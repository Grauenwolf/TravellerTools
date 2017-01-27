using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Navy : MilitaryCareer
    {
        public Navy(string assignment, Book book) : base("Navy", assignment, book)
        {
        }

        protected override int AdvancedEductionMin => 8;


        internal override bool Qualify(Character character, Dice dice)
        {
            var dm = character.Intellect;
            dm += -1 * character.CareerHistory.Count;
            if (character.Age >= 34)
                dm += -2;

            dm += character.GetEnlistmentBonus(Name, Assignment);

            return dice.RollHigh(dm, 6);

        }

        internal override void Event(Character character, Dice dice)
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    Mishap(character, dice);
                    character.NextTermBenefits.MusterOut = false;
                    return;
                case 3:
                    character.AddHistory("You join a gambling circle on board.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Gambler");
                        skillList.Add("Deception");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);

                        if (dice.RollHigh(character.Skills.GetLevel("Gambler"), 8))
                            character.BenefitRolls += 1;
                        else
                            character.BenefitRolls += -1;
                    }

                    return;
                case 4:
                    character.AddHistory("Special assignment or duty on board ship.");
                    character.BenefitRollDMs.Add(1);
                    return;
                case 5:
                    character.AddHistory($"Advanced training in a specialist field");
                    if (dice.RollHigh(character.EducationDM, 8))
                        dice.Choose(character.Skills).Level += 1;
                    return;
                case 6:
                    character.AddHistory("Vessel participates in a notable military engagement.");
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
                    character.AddHistory("Vessel participates in a diplomatic mission.");
                    if (dice.D(2) == 1)
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Recon");
                        skillList.Add("Diplomat");
                        skillList.Add("Steward");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    else
                        character.AddHistory("Gain a contact.");

                    return;
                case 9:
                    character.AddHistory("You foil an attempted crime on board, such as mutiny, sabotage, smuggling or conspiracy. Gain an Enemy.");
                    character.CurrentTermBenefits.AdvancementDM += 2;
                    return;
                case 10:
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Abuse your position for profit");
                        character.BenefitRolls += 1;
                    }
                    else
                    {
                        character.AddHistory("Refuse to abuse your position for profit");
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    return;
                case 11:
                    character.AddHistory("Commanding officer takes an interest in your career.");
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
                    character.AddHistory("Display heroism in battle, saving the whole ship.");

                    character.CurrentTermBenefits.AdvancementDM += 100; //also applies to commission rolls

                    if (character.LastCareer.CommissionRank == 0)
                        character.CurrentTermBenefits.FreeCommissionRoll = true;

                    return;
            }
        }

        internal override void Mishap(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    Injury(character, dice, true);
                    return;
                case 2:
                    character.AddHistory("Placed in the frozen watch (cryogenically stored on board ship) and revived improperly.");
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
                    character.NextTermBenefits.MusterOut = false;
                    return;
                case 3:
                    var checkPassed = false;

                    switch (character.LastCareer.Assignment)
                    {
                        case "Line/Crew":
                            checkPassed = (dice.RollHigh(character.Skills.BestSkillLevel("Electronics", "Gunner"), 8));
                            break;
                        case "Engineer/Gunner":
                            checkPassed = (dice.RollHigh(character.Skills.BestSkillLevel("Mechanic", "Vacc Suit"), 8));
                            break;
                        case "Flight":
                            checkPassed = (dice.RollHigh(character.Skills.BestSkillLevel("Pilot", "Tactics"), 8));
                            break;
                    }
                    if (checkPassed)
                    {
                        character.AddHistory("During a battle, defeat or victory depends on your actions. You actions lead to an honorabe discharge.");
                        character.BenefitRolls += 1;
                    }
                    else
                    {
                        character.AddHistory("During a battle, defeat or victory depends on your actions. The ship suffers severe damage and you are blamed for the disaster. You are court - martialled and discharged.");
                    }

                    return;

                case 4:
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("You are blamed for an accident that causes the death of several crew members. Your guilt drives you to excel.");

                        var skillTables = new List<SkillTable>();
                        skillTables.Add(PersonalDevelopment);
                        skillTables.Add(ServiceSkill);
                        skillTables.Add(AssignmentSkills);
                        if (character.Education >= AdvancedEductionMin)
                            skillTables.Add(AdvancedEducation);
                        if (character.LastCareer.CommissionRank > 0)
                            skillTables.Add(OfficerTraining);

                        dice.Choose(skillTables)(character, dice);
                    }
                    else
                    {
                        character.AddHistory("You are falsed accused for an accident that causes the death of several crew members. Gain the officer who blamed you as an Enemy.");
                        character.BenefitRolls += 1;
                    }
                    return;
                case 5:
                    character.AddHistory("You are tormented by or quarrel with an officer or fellow crewman. Gain that character as a Rival.");
                    return;
                case 6:
                    Injury(character, dice, false);
                    return;
            }
        }

        protected override void AdvancedEducation(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 2:
                    character.Skills.Increase("Astrogation");
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 5:
                    character.Skills.Increase("Navigation");
                    return;
                case 6:
                    character.Skills.Increase("Admin");
                    return;
            }
        }

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
                    character.Skills.Increase("Melee", "Blade");
                    return;
                case 5:
                    character.Skills.Increase("Admin");
                    return;
                case 6:
                    character.Skills.Increase("Tactics", "Naval");
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

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
            if (all || roll == 2)
                character.Skills.Increase("Vacc Suit");
            if (all || roll == 3)
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
            if (all || roll == 4)
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gunner")));
            if (all || roll == 5)
                character.Skills.Increase("Mechanic");
            if (all || roll == 6)
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat ")));
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
                    character.Skills.Increase("Mechanic");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat ")));
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
                        careerHistory.Title = "Crewman";
                        return;
                    case 1:
                        careerHistory.Title = "Able Spacehand";
                        character.Skills.Add("Mechanic", 1);
                        return;
                    case 2:
                        careerHistory.Title = "Petty Officer, 3rd  class";
                        character.Skills.Add("Vacc Suit", 1);
                        return;
                    case 3:
                        careerHistory.Title = "Petty Officer, 2nd  class";
                        return;
                    case 4:
                        careerHistory.Title = "Petty Officer, 1st  class";
                        character.Endurance += 1;
                        return;
                    case 5:
                        careerHistory.Title = "Chief Petty Officer";
                        return;
                    case 6:
                        careerHistory.Title = "Master Chief";
                        return;
                }
            }
            else
            {
                switch (careerHistory.CommissionRank)
                {
                    case 1:
                        careerHistory.Title = "Ensign";
                        character.Skills.Add("Melee", "Blade", 1);
                        return;
                    case 2:
                        careerHistory.Title = "Sublieutenant";
                        character.Skills.Add("Leadership", 1);
                        return;
                    case 3:
                        careerHistory.Title = "Lieutenant";
                        return;
                    case 4:
                        careerHistory.Title = "Commander";
                        character.Skills.Add("Tactics", "Military", 1);
                        return;
                    case 5:
                        careerHistory.Title = "Captain";
                        if (character.SocialStanding < 10)
                            character.SocialStanding = 10;
                        else
                            character.SocialStanding += 1;
                        return;
                    case 6:
                        careerHistory.Title = "Admiral";
                        if (character.SocialStanding < 12)
                            character.SocialStanding = 12;
                        else
                            character.SocialStanding += 1;
                        return;
                }
            }
        }
    }
}


