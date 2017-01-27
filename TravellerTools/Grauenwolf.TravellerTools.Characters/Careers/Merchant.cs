namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Merchant : NormalCareer
    {
        public Merchant(string assignment, Book book) : base("Merchant", assignment, book)
        {
        }

        protected override int AdvancedEductionMin => 8;

        protected override bool RankCarryover => false;

        internal override bool Qualify(Character character, Dice dice)
        {
            var dm = character.IntellectDM;
            dm += -1 * character.CareerHistory.Count;

            dm += character.GetEnlistmentBonus(Name, Assignment);

            return dice.RollHigh(dm, 4);

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
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Smuggle illegal items onto a planet.");
                        if (dice.RollHigh(character.Skills.BestSkillLevel("Deception", "Persuade"), 8))
                        {
                            character.Skills.Add("Streetwise", 1);
                            character.BenefitRolls += 1;
                        }
                    }
                    else
                        character.AddHistory("Refuse to smuggle illegal items onto a planet. Gain an Enemy.");
                    return;
                case 4:
                    {

                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Profession"));
                        skillList.AddRange(SpecialtiesFor("Electronics"));
                        skillList.AddRange(SpecialtiesFor("Engineer"));
                        skillList.AddRange(SpecialtiesFor("Animals"));
                        skillList.AddRange(SpecialtiesFor("Science"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }

                    return;
                case 5:
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Gambler", "Broker"), 8))
                    {
                        character.AddHistory($"Risk your fortune on a possibility lucrative deal and win.");
                        character.BenefitRolls *= 2;
                    }
                    else
                    {
                        character.BenefitRolls = 0;
                    }
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Broker");
                        skillList.Add("Gambler");
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    return;
                case 6:
                    character.AddHistory("Make an unexpected connection outside your normal circles. Gain a Contact.");
                    return;
                case 7:
                    LifeEvent(character, dice);
                    return;
                case 8:
                    character.AddHistory("Embroiled in legal trouble.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Advocate");
                        skillList.Add("Admin");
                        skillList.Add("Diplomat");
                        skillList.Add("Investigate");
                    }
                    if (dice.D(2, 6) == 2)
                    {
                        character.NextTermBenefits.MusterOut = true;
                        character.NextTermBenefits.MustEnroll = "Prisoner";
                    }
                    return;
                case 9:
                    character.AddHistory("Given advanced training in a specialist field");
                    if (dice.RollHigh(character.EducationDM, 8))
                        dice.Choose(character.Skills).Level += 1;
                    return;
                case 10:
                    character.AddHistory("A good deal ensures you are living the high life for a few years.");
                    character.BenefitRollDMs.Add(1);
                    return;
                case 11:
                    character.AddHistory("Befriend a useful ally in one sphere. Gain an Ally.");
                    switch (dice.D(2))
                    {
                        case 1:
                            character.Skills.Add("Carouse", 1);
                            return;
                        case 2:
                            character.CurrentTermBenefits.AdvancementDM += 4;
                            return;
                    }
                    return;
                case 12:
                    character.AddHistory("Your business or ship thrives.");
                    character.CurrentTermBenefits.AdvancementDM += 100;
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
                    character.AddHistory("Bankrupted by a rival. Gain the other trader as a Rival.");
                    character.BenefitRolls = 0;
                    return;
                case 3:
                    character.AddHistory("A sudden war destroys your trade routes and contacts, forcing you to flee that region of space.");
                    character.AddHistory("Gain rebels as Enemy");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Gun Combat"));
                        skillList.AddRange(SpecialtiesFor("Pilot"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;

                case 4:
                    character.AddHistory("Your ship or starport is destroyed by criminals. Gain them as an Enemy.");
                    return;
                case 5:
                    character.AddHistory("Imperial trade restrictions force you out of business.");
                    character.NextTermBenefits.EnlistmentDM.Add("Rogue", 100);
                    return;
                case 6:
                    character.AddHistory("A series of bad deals and decisions force you into bankruptcy. You salvage what you can.");
                    character.BenefitRolls += 1;
                    return;
            }
        }

        protected override void AdvancedEducation(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 2:
                    character.Skills.Increase("Astrogation");
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Pilot")));
                    return;
                case 5:
                    character.Skills.Increase("Admin");
                    return;
                case 6:
                    character.Skills.Increase("Advocate");
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
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                    return;
                case 6:
                    character.Skills.Increase("Streetwise");
                    return;
            }
        }

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.AddRange(SpecialtiesFor("Drive"));
            if (all || roll == 2)
                character.Skills.Add("Vacc Suit");
            if (all || roll == 3)
                character.Skills.Add("Broker");
            if (all || roll == 4)
                character.Skills.Add("Steward");
            if (all || roll == 5)
                character.Skills.AddRange(SpecialtiesFor("Electronics"));
            if (all || roll == 6)
                character.Skills.Add("Persuade");
        }

        protected override void ServiceSkill(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 2:
                    character.Skills.Increase("Vacc Suit");
                    return;
                case 3:
                    character.Skills.Increase("Broker");
                    return;
                case 4:
                    character.Skills.Increase("Steward");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 6:
                    character.Skills.Increase("Persuade");
                    return;
            }
        }


    }
}


