using System;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Entertainer : NormalCareer
    {
        public Entertainer(string assignment, Book book) : base("Entertainer", assignment, book)
        {

        }

        protected override int AdvancedEductionMin => 10;

        protected override bool RankCarryover => false;

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.AddRange(SpecialtiesFor("Art"));
            if (all || roll == 2)
                character.Skills.Add("Carouse");
            if (all || roll == 3)
                character.Skills.AddRange(SpecialtiesFor("Drive"));
            if (all || roll == 4)
                character.Skills.Add("Deception");
            if (all || roll == 5)
                character.Skills.Add("Persuade");
            if (all || roll == 6)
                character.Skills.Add("Steward");
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

                    if (dice.RollHigh(character.Skills.BestSkillLevel("Investigate", "Art"), 8))
                    {
                        character.AddHistory("Invited to take part in a controversial event or exhibition that improves your social standing.");
                        character.SocialStanding += 1;
                    }
                    else
                    {
                        character.AddHistory("Invited to take part in a controversial event or exhibition that injures your social standing.");
                        character.SocialStanding += -1;
                    }
                    return;
                case 4:
                    character.AddHistory("Join homeworld’s celebrity circles");

                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Carouse");
                        skillList.Add("Persuade");
                        skillList.Add("Steward");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                        else
                            character.AddHistory("Gain a contact");
                    }

                    return;
                case 5:
                    character.AddHistory($"Works is especially well received and popular, making you a minor celebrity");
                    character.BenefitRollDMs.Add(1);
                    return;
                case 6:
                    character.AddHistory($"Gain a patron in the arts. Gain an Ally");
                    character.CurrentTermBenefits.AdvancementDM += 2;
                    return;
                case 7:
                    LifeEvent(character, dice);
                    return;
                case 8:

                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Refused to criticise a questionable political leader on your homeworld.");
                    }
                    else
                    {
                        character.AddHistory("Criticised a questionable political leader on your homeworld, causing his downfall. Gain an Enemy");
                        dice.Choose(character.Skills).Level += 1;
                        if (!dice.RollHigh(character.Skills.BestSkillLevel("Art", "Persuade"), 8))
                            Mishap(character, dice);
                    }
                    return;
                case 9:
                    character.AddHistory($"Go on a tour of the sector, visiting several worlds. Gain {dice.D(3)} Contacts.");
                    return;
                case 10:
                    character.AddHistory("One of your pieces of art is stolen, and the investigation brings you into the criminal underworld.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Streetwise");
                        skillList.Add("Investigate");
                        skillList.Add("Recon");
                        skillList.Add("Stealth");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 11:
                    //character.AddHistory("As an artist, you lead a strange and charmed life.");
                    UnusualLifeEvent(character, dice);
                    return;
                case 12:
                    character.AddHistory("Win a prestigious prize.");
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
                    character.AddHistory("Expose or are involved in a scandal of some sort.");
                    return;
                case 3:
                    character.AddHistory("Public opinion turns on you.");
                    character.SocialStanding += -1;
                    return;

                case 4:
                    character.AddHistory("You are betrayed by a peer. One Ally or Contact becomes a Rival or Enemy");
                    return;
                case 5:
                    character.AddHistory("An investigation, tour, project or expedition goes wrong, stranding you far from home.");
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Survival");
                    skillList.AddRange(SpecialtiesFor("Pilot"));
                    skillList.Add("Persuade");
                    skillList.Add("Streetwise");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);

                    return;
                case 6:
                    character.AddHistory("You are forced out because of censorship or controversy. What truth did you get too close to?");
                    character.NextTermBenefits.QualificationDM += 2;
                    return;
            }
        }

        internal override bool Qualify(Character character, Dice dice)
        {
            var dm = Math.Max(character.IntellectDM, character.DexterityDM);
            dm += -1 * character.CareerHistory.Count;

            dm += character.GetEnlistmentBonus(Name, Assignment);

            return dice.RollHigh(dm, 5);
        }
        protected override void AdvancedEducation(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase("Advocate");
                    return;
                case 2:
                    character.Skills.Increase("Broker");
                    return;
                case 3:
                    character.Skills.Increase("Deception");
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
                case 5:
                    character.Skills.Increase("Streetwise");
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
                    character.Dexterity += 1;
                    return;
                case 2:
                    character.Intellect += 1;
                    return;
                case 3:
                    character.SocialStanding += 1;
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                    return;
                case 5:
                    character.Skills.Increase("Carouse");
                    return;
                case 6:
                    character.Skills.Increase("Jack-of-all-Trades");
                    return;
            }
        }
        internal override void ServiceSkill(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
                    return;
                case 2:
                    character.Skills.Increase("Carouse");
                    return;
                case 3:
                    character.Skills.Increase("Deception");
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 5:
                    character.Skills.Increase("Persuade");
                    return;
                case 6:
                    character.Skills.Increase("Steward");
                    return;
            }
        }
    }


}


