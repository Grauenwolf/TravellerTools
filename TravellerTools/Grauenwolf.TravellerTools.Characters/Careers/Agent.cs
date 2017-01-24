using System.Collections.Generic;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Agent : NormalCareer
    {
        private ImmutableArray<NormalCareer> m_Careers;

        public Agent(string assignment, Book book) : base("Agent", assignment, book)
        {
            var careers = new List<NormalCareer>();
            careers.Add(new Corporate(book));
            careers.Add(new Worker(book));
            careers.Add(new Colonist(book));
            careers.Add(new Thief(book));
            careers.Add(new Enforcer(book));
            careers.Add(new Pirate(book));
            m_Careers = careers.ToImmutableArray();
        }

        protected override int AdvancedEductionMin
        {
            get { return 8; }
        }

        protected override bool RankCarryover
        {
            get { return false; }
        }

        internal override bool Qualify(Character character, Dice dice)
        {
            var dm = character.IntellectDM;
            dm += -1 * character.CareerHistory.Count;

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
                    character.AddHistory("An investigation takes on a dangerous turn.");

                    if (dice.RollHigh(character.Skills.BestSkillLevel("Investigate", "Streetwise"), 8))
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Deception");
                        skillList.Add("Jack-of-All-Trades");
                        skillList.Add("Persuade");
                        skillList.Add("Tactics");
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    else
                    {
                        Mishap(character, dice);
                    }
                    return;
                case 4:
                    character.AddHistory("Rewarded for a successful mission.");
                    character.BenefitRollDMs.Add(1);
                    return;
                case 5:
                    character.AddHistory($"Established a network of contacts. Gain {dice.D(3)} contacts.");
                    return;
                case 6:
                    character.AddHistory("Advanced training in a specialist field.");
                    if (dice.RollHigh(character.EducationDM, 8))
                    {
                        dice.Choose(character.Skills).Level += 1;
                    }
                    return;
                case 7:
                    LifeEvent(character, dice);
                    return;
                case 8:
                    {
                        character.AddHistory("Go undercover to investigate an enemy.");

                        var career = dice.Choose(m_Careers);

                        if (dice.RollHigh(character.Skills.GetLevel("Deception"), 8))
                        {
                            career.Event(character, dice);
                            career.AssignmentSkills(character, dice);
                        }
                        else
                        {
                            career.Mishap(character, dice);
                        }
                    }
                    return;
                case 9:
                    character.AddHistory("You go above and beyond the call of duty.");
                    character.CurrentTermBenefits.AdvancementDM += 2;
                    return;
                case 10:
                    character.AddHistory("Given specialist training in vehicles.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.AddRange(SpecialtiesFor("Drive"));
                        skillList.AddRange(SpecialtiesFor("Flyer"));
                        skillList.AddRange(SpecialtiesFor("Pilot"));
                        skillList.AddRange(SpecialtiesFor("Gunner"));
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 11:
                    character.AddHistory("Befriended by a senior agent.");
                    switch (dice.D(2))
                    {
                        case 1:
                            character.Skills.Increase("Investigate");
                            return;
                        case 2:
                            character.CurrentTermBenefits.AdvancementDM += 4;
                            return;
                    }
                    return;
                case 12:
                    character.AddHistory("Uncover a major conspiracy against your employers.");
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
                    character.AddHistory("Life ruined by a criminal gang. Gain the gang as an Enemy");
                    return;
                case 3:
                    character.AddHistory("Hard times caused by a lack of interstellar trade costs you your job.");
                    character.SocialStanding += -1;
                    return;

                case 4:
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Accepted a deal with criminal or other figure under investigation.");
                    }
                    else
                    {
                        character.AddHistory("Refused a deal with criminal or other figure under investigation. Gain an Enemy");
                        character.Skills.Increase(dice.Choose(RandomSkills));
                    }
                    return;
                case 5:
                    character.AddHistory("Your work ends up coming home with you, and someone gets hurt. Contact or ally takes the worst of 2 injury rolls.");
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
                    character.Skills.Increase("Advocate");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                    return;
                case 3:
                    character.Skills.Increase("Explosives");
                    return;
                case 4:
                    character.Skills.Increase("Medic");
                    return;
                case 5:
                    character.Skills.Increase("Vacc Suit");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
            }
        }

        protected override void PersonalDevelopment(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                    return;
                case 2:
                    character.Dexterity += 1;
                    return;
                case 3:
                    character.Endurance += 1;
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                    return;
                case 5:
                    character.Intellect += 1;
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Athletics")));
                    return;
            }
        }

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.Add("Streetwise");
            if (all || roll == 2)
                character.Skills.AddRange(SpecialtiesFor("Drive"));
            if (all || roll == 3)
                character.Skills.Add("Investigate");
            if (all || roll == 4)
                character.Skills.AddRange(SpecialtiesFor("Flyer"));
            if (all || roll == 5)
                character.Skills.Add("Recon");
            if (all || roll == 6)
                character.Skills.AddRange(SpecialtiesFor("Gun Combat"));
        }


        protected override void ServiceSkill(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Streetwise");
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 3:
                    character.Skills.Increase("Investigate");
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Flyer")));
                    return;
                case 5:
                    character.Skills.Increase("Recon");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                    return;
            }
        }
    }
}


