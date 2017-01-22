//using System;

//namespace Grauenwolf.TravellerTools.Characters.Careers
//{
//    public class Rogue : NormalCareer
//    {
//        public Rogue(string assignment) : base("Rogue", assignment)
//        {

//        }

//        protected override void BasicTraining(Character character, Dice dice, bool firstCareer)
//        {
//            if (firstCareer)
//                for (var i = 1; i < 7; i++)
//                    ServiceSkill(character, dice, i, true);
//            else
//                ServiceSkill(character, dice, dice.D(6), true);
//        }

//        protected override int AdvancedEductionMin
//        {
//            get { return 10; }
//        }

//        public override bool Qualify(Character character, Dice dice)
//        {
//            var dm = character.DexterityDM;
//            dm += -1 * character.CareerHistory.Count;

//            dm += character.GetEnlistmentBonus(Name, Assignment);

//            return dice.RollHigh(dm, 6);

//        }

//        protected override void AdvancedEducation(Character character, Dice dice, int roll, bool level0)
//        {
//            switch (roll)
//            {
//                case 1:
//                    character.Skills.Increase("Advocate");
//                    return;
//                case 2:
//                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Language")));
//                    return;
//                case 3:
//                    character.Skills.Increase("Explosives");
//                    return;
//                case 4:
//                    character.Skills.Increase("Medic");
//                    return;
//                case 5:
//                    character.Skills.Increase("Vacc Suit");
//                    return;
//                case 6:
//                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Electronics")));
//                    return;
//            }
//        }

//        protected override bool RankCarryover
//        {
//            get { return false; }
//        }

//        protected override void PersonalDevelopment(Character character, Dice dice, int roll, bool level0)
//        {
//            switch (roll)
//            {
//                case 1:
//                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Gun Combat")));
//                    return;
//                case 2:
//                    character.Dexterity += 1;
//                    return;
//                case 3:
//                    character.Endurance += 1;
//                    return;
//                case 4:
//                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Melee")));
//                    return;
//                case 5:
//                    character.Intellect += 1;
//                    return;
//                case 6:
//                    character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Athletics")));
//                    return;
//            }
//        }

//        protected override void ServiceSkill(Character character, Dice dice, int roll, bool level0)
//        {
//            if (level0)
//            {
//                switch (roll)
//                {
//                    case 1:
//                        character.Skills.Add("Streetwise");
//                        return;
//                    case 2:
//                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
//                        return;
//                    case 3:
//                        character.Skills.Add("Investigate");
//                        return;
//                    case 4:
//                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Flyer")));
//                        return;
//                    case 5:
//                        character.Skills.Add("Recon");
//                        return;
//                    case 6:
//                        character.Skills.Add(dice.Choose(CharacterBuilder.SpecialtiesFor("Gun Combat")));
//                        return;
//                }
//            }
//            else
//            {
//                switch (roll)
//                {
//                    case 1:
//                        character.Skills.Increase("Streetwise");
//                        return;
//                    case 2:
//                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Drive")));
//                        return;
//                    case 3:
//                        character.Skills.Increase("Investigate");
//                        return;
//                    case 4:
//                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Flyer")));
//                        return;
//                    case 5:
//                        character.Skills.Increase("Recon");
//                        return;
//                    case 6:
//                        character.Skills.Increase(dice.Choose(CharacterBuilder.SpecialtiesFor("Gun Combat")));
//                        return;
//                }
//            }
//        }

//        internal override void Event(Character character, Dice dice)
//        {
//            switch (dice.D(2, 6))
//            {
//                case 2:
//                    Mishap(character, dice);
//                    character.NextTermBenefits.MusterOut = false;
//                    return;
//                case 3:
//                    character.AddHistory("An investigation takes on a dangerous turn.");

//                    if (dice.RollHigh(character.Skills.BestSkillLevel("Investigate", "Streetwise"), 8))
//                    {
//                        var skillList = new SkillTemplateCollection();
//                        skillList.Add("Deception");
//                        skillList.Add("Jack-of-All-Trades");
//                        skillList.Add("Persuade");
//                        skillList.Add("Tactics");
//                        character.Skills.Increase(dice.Choose(skillList));
//                    }
//                    else
//                    {
//                        Mishap(character, dice);
//                    }
//                    return;
//                case 4:
//                    character.AddHistory("Rewarded for a successful mission.");
//                    character.BenefitRollDMs.Add(1);
//                    return;
//                case 5:
//                    character.AddHistory($"Established a network of contacts. Gain {dice.D(3)} contacts.");
//                    return;
//                case 6:
//                    character.AddHistory("Advanced training in a specialist field.");
//                    if (dice.RollHigh(character.EducationDM, 8))
//                    {
//                        dice.Choose(character.Skills).Level += 1;
//                    }
//                    return;
//                case 7:
//                    CharacterBuilder.LifeEvent(character, dice);
//                    return;
//                case 8:
//                    {
//         
//                    }
//                    return;
//                case 9:
//                    character.AddHistory("Involved in a feud with a rival criminal organization.");
//                    if (dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Gun Combat"), 8))
//                        character.BenefitRolls += 1;
//                    else
//                        CharacterBuilder.Injury(character, dice);
//                    return;
//                case 10:
//                    character.AddHistory("Involved in a gambling ring. ");
//                    character.Skills.Add("Gambler", 1);
//                    if (character.BenefitRolls > 0)
//                    {
//                        if (dice.RollHigh(character.Skills.GetLevel("Gambler"), 8))
//                            character.BenefitRolls = (int)Math.Ceiling(1.5 * character.BenefitRolls);
//                        else
//                            character.BenefitRolls = 0;
//                    }
//                    return;
//                case 11:
//                    character.AddHistory("A crime lord considers you his protégé.");
//                    switch (dice.D(2))
//                    {
//                        case 1:
//                            character.Skills.Add("Tactics", "Military", 1);
//                            return;
//                        case 2:
//                            character.CurrentTermBenefits.AdvancementDM += 4;
//                            return;
//                    }
//                    return;
//                case 12:
//                    character.AddHistory("You commit a legendary crime.");
//                    character.CurrentTermBenefits.AdvancementDM += 100;
//                    return;
//            }
//        }

//        internal override void Mishap(Character character, Dice dice)
//        {
//            switch (dice.D(6))
//            {
//                case 1:
//                    CharacterBuilder.Injury(character, dice, true);
//                    return;
//                case 2:
//                    character.AddHistory("Life ruined by a criminal gang. Gain the gang as an Enemy");
//                    return;
//                case 3:
//                    character.AddHistory("Hard times caused by a lack of interstellar trade costs you your job.");
//                    character.SocialStanding += -1;
//                    return;

//                case 4:
//                    if (dice.D(2) == 1)
//                    {
//                        character.AddHistory("Co-operate with investigation by the planetary authorities. The business or colony is shut down.");
//                        character.NextTermBenefits.QualificationDM += 2;
//                    }
//                    else
//                    {
//                        character.AddHistory("Refused to co-operate with investigation by the planetary authorities. Gain an Ally");
//                    }
//                    return;
//                case 5:
//                    character.AddHistory("A revolution, attack or other unusual event throws your life into chaos, forcing you to leave the planet.");
//                    if (dice.RollHigh(character.Skills.BestSkillLevel("Streetwise"), 8))
//                        dice.Choose(character.Skills).Level += 1;
//                    return;
//                case 6:
//                    CharacterBuilder.Injury(character, dice, false);
//                    return;
//            }
//        }
//    }
//}


