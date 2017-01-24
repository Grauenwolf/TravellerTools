using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Army : MilitaryCareer
    {
        private ImmutableArray<NormalCareer> m_Careers;

        public Army(string assignment, Book book) : base("Army", assignment, book)
        {
        }

        protected override int AdvancedEductionMin
        {
            get { return 8; }
        }


        internal override bool Qualify(Character character, Dice dice)
        {
            var dm = character.EnduranceDM;
            dm += -1 * character.CareerHistory.Count;
            if (character.Age >= 30)
                dm += -2;

            dm += character.GetEnlistmentBonus(Name, Assignment);

            return dice.RollHigh(dm, 5);

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
                    character.AddHistory("Assigned to a planet with a hostile or wild environment.");
                    {

                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Vacc Suit");
                        skillList.AddRange(SpecialtiesFor("Engineer"));
                        skillList.Add("Animals", "Riding");
                        skillList.Add("Animals", "Training");
                        skillList.Add("Recon");
                        skillList.RemoveOverlap(character.Skills, 1);
                        character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 4:
                    character.AddHistory("Assigned to an urbanised planet torn by war.");
                    {

                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Stealth");
                        skillList.Add("Streetwise");
                        skillList.Add("Persuade");
                        skillList.Add("Recon");
                        skillList.RemoveOverlap(character.Skills, 1);
                        character.Skills.Add(dice.Choose(skillList), 1);
                    }

                    return;
                case 5:
                    character.AddHistory($"Given a special assignment or duty in your unit.");
                    character.BenefitRollDMs.Add(1);
                    return;
                case 6:
                    character.AddHistory("Thrown into a brutal ground war");
                    if (dice.RollHigh(character.EducationDM, 8))
                    {

                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Gun Combat"));
                        skillList.Add("Leadership");
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    else
                    {
                        Injury(character, dice);
                    }
                    return;
                case 7:
                    LifeEvent(character, dice);
                    return;
                case 8:
                    character.AddHistory("Advanced training in a specialist field");
                    if (dice.RollHigh(character.EducationDM, 8))
                        dice.Choose(character.Skills).Level += 1;
                    return;
                case 9:
                    character.AddHistory("Surrounded and outnumbered by the enemy, you hold out until relief arrives. ");
                    character.CurrentTermBenefits.AdvancementDM += 2;
                    return;
                case 10:
                    character.AddHistory("Assigned to a peacekeeping role.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Admin");
                        skillList.Add("Admin");
                        skillList.Add("Deception");
                        skillList.Add("Recon");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
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
                    character.AddHistory("Display heroism in battle.");

                    character.CurrentTermBenefits.AdvancementDM += 100; //also applies to commission rolls

                    if (character.LastCareer.CommissionRank == 0)
                    {
                        character.CurrentTermBenefits.FreeCommissionRoll = true;
                    }

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
                    character.AddHistory("Unit is slaughtered in a disastrous battle, for which you blame your commander. Gain commander as Enemy.");
                    return;
                case 3:
                    character.AddHistory("Sent to a very unpleasant region (jungle, swamp, desert, icecap, urban) to battle against guerrilla fighters and rebels. Discharged because of stress, injury or because the government wishes to bury the whole incident.");
                    character.AddHistory("Gain rebels as Enemy");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Recon");
                        skillList.Add("Survival");
                        if (skillList.Count > 0)
                            character.Skills.Increase(dice.Choose(skillList));
                    }
                    return;

                case 4:
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Joined commanding officer is engaged in some illegal activity, such as weapon smuggling. Gain an Ally.");
                    }
                    else
                    {
                        character.AddHistory("Forced out after co-operating with military investigation in commanding officer's illegal activity.");
                        character.BenefitRolls += 1;
                    }
                    return;
                case 5:
                    character.AddHistory("You are tormented by or quarrel with an officer or fellow soldier. Gain that officer as a Rival.");
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
                    character.Skills.Increase("Tactics", "Military");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 3:
                    character.Skills.Increase("Navigation");
                    return;
                case 4:
                    character.Skills.Increase("Explosives");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 6:
                    character.Skills.Increase("Survival");
                    return;
            }
        }

        protected override void OfficerTraining(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Tactics", "Military");
                    return;
                case 2:
                    character.Skills.Increase("Leadership");
                    return;
                case 3:
                    character.Skills.Increase("Advocate");
                    return;
                case 4:
                    character.Skills.Increase("Diplomat");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 6:
                    character.Skills.Increase("Admin");
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
                    character.Skills.Increase("Medic");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                    return;
            }
        }

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
            {
                if (all)
                {
                    character.Skills.AddRange(SpecialtiesFor("Drive"));
                    character.Skills.Add("Vacc Suit");
                }
                else
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor("Drive"));
                    skillList.Add("Vacc Suit");
                    character.Skills.Add(dice.Choose(skillList));
                }
            }

            if (all || roll == 2)
                character.Skills.Add(dice.Choose(SpecialtiesFor("Athletics")));
            if (all || roll == 3)
                character.Skills.Add(dice.Choose(SpecialtiesFor("Gun Combat")));
            if (all || roll == 4)
                character.Skills.Add("Recon");
            if (all || roll == 5)
                character.Skills.Add(dice.Choose(SpecialtiesFor("Melee")));
            if (all || roll == 6)
                character.Skills.Add(dice.Choose(SpecialtiesFor("Heavy Weapons")));
        }

        protected override void ServiceSkill(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Drive"));
                        skillList.Add("Vacc Suit");
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                    return;
                case 4:
                    character.Skills.Increase("Recon");
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Heavy Weapons")));
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
                        character.Skills.Add(dice.Choose(SpecialtiesFor("Gun Combat")), 1);
                        return;
                    case 1:
                        careerHistory.Title = "Lance Corporal";
                        character.Skills.Add("Recon", 1);
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
                        careerHistory.Title = "Major";
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
                        if (character.SocialStanding < 10)
                            character.SocialStanding = 10;
                        else
                            character.SocialStanding += 1;
                        return;
                }
            }
        }
    }
}


