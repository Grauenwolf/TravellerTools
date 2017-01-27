namespace Grauenwolf.TravellerTools.Characters.Careers
{
    abstract class Scholar : NormalCareer
    {

        public Scholar(string assignment, Book book) : base("Scholar", assignment, book)
        {
        }

        protected override int AdvancedEductionMin => 10;

        protected override bool RankCarryover => false;

        internal override bool Qualify(Character character, Dice dice)
        {
            var dm = character.IntellectDM;
            dm += -1 * character.CareerHistory.Count;

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
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Refused to perform research that goes against your conscience.");
                    }
                    else
                    {
                        character.AddHistory($"Agreed to perform research that goes against your conscience. Gain {dice.D(3)} Enemies.");
                        character.BenefitRolls += 1;

                        var skillList = new SkillTemplateCollection(SpecialtiesFor("Science"));
                        character.Skills.Increase(dice.Pick(skillList));
                        character.Skills.Increase(dice.Pick(skillList)); //pick 2

                    }
                    return;
                case 4:
                    character.AddHistory("Assigned to work on a secret project for a patron or organisation.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Medic");
                        skillList.AddRange(SpecialtiesFor("Science"));
                        skillList.AddRange(SpecialtiesFor("Engineer"));
                        skillList.AddRange(SpecialtiesFor("Electronics"));
                        skillList.Add("Investigate");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 5:
                    character.AddHistory($"Win a prestigious prize for your work.");
                    character.BenefitRollDMs.Add(1);
                    return;
                case 6:
                    character.AddHistory("Advanced training in a specialist field.");
                    if (dice.RollHigh(character.EducationDM, 8))
                    {
                        var skillList = new SkillTemplateCollection(RandomSkills);
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }

                    return;
                case 7:
                    LifeEvent(character, dice);
                    return;
                case 8:
                    {
                        if (dice.D(2) == 1)
                        {
                            if (dice.RollHigh(character.Skills.BestSkillLevel("Deception", "Admin"), 8))
                            {
                                character.AddHistory("Cheated in some fashion, advancing your career and research by stealing another’s work, using an alien device, taking a shortcut and so forth.");
                                character.BenefitRollDMs.Add(2);
                                dice.Choose(character.Skills).Level += 1;
                            }
                            else
                            {
                                character.AddHistory("Caught cheating in some fashion, advancing your career and research by stealing another’s work, using an alien device, taking a shortcut and so forth.");
                                character.BenefitRolls += -1;
                                Mishap(character, dice);
                            }
                            character.AddHistory("Gain an Enemy");
                        }
                        else
                        {
                            character.AddHistory("Refuse to join a cheat in some fashion.");
                        }

                    }
                    return;
                case 9:
                    character.AddHistory("Make a breakthrough in your field.");
                    character.CurrentTermBenefits.AdvancementDM += 2;
                    return;
                case 10:
                    character.AddHistory("Entangled in a bureaucratic or legal morass that distracts you from your work.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Admin");
                        skillList.Add("Advocate");
                        skillList.Add("Persuade");
                        skillList.Add("Diplomat");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 11:
                    character.AddHistory("Work for an eccentric but brilliant mentor, who becomes an Ally.");
                    switch (dice.D(2))
                    {
                        case 1:
                            character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                            return;
                        case 2:
                            character.CurrentTermBenefits.AdvancementDM += 4;
                            return;
                    }
                    return;
                case 12:
                    character.AddHistory("Work leads to a considerable breakthrough.");
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
                    character.AddHistory("A disaster leaves several injured, and others blame you, forcing you to leave your career. Gain a Rival.");
                    Injury(character, dice);
                    return;
                case 3:
                    character.AddHistory("A disaster or war strikes.");
                    if (!dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 8))
                        Injury(character, dice);

                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("The planetary government interferes with your research for political or religious reasons. You continue working secretely.");
                        character.SocialStanding += -2;
                    }
                    else
                    {
                        character.AddHistory("The planetary government interferes with your research for political or religious reasons. You continue working openly and gain an Enemy.");
                    }
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    character.NextTermBenefits.MusterOut = false;
                    return;

                case 4:
                    character.AddHistory("An expedition or voyage goes wrong, leaving you stranded in the wilderness.");
                    {
                        var skillList = new SkillTemplateCollection();
                        skillList.Add("Survival");
                        skillList.Add("Athletics", "Dexterity");
                        skillList.Add("Athletics", "Endurance");
                        skillList.RemoveOverlap(character.Skills, 1);
                        if (skillList.Count > 0)
                            character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    return;
                case 5:
                    if (dice.D(2) == 1)
                    {
                        character.AddHistory("Your work is sabotaged by unknown parties. You may salvage what you can and give up.");
                        character.BenefitRolls += 1;
                    }
                    else
                    {
                        character.AddHistory("Your work is sabotaged by unknown parties. You start again from scratch.");
                        character.BenefitRolls = 0;
                        character.NextTermBenefits.MusterOut = false;
                    }
                    return;
                case 6:
                    character.AddHistory("A rival researcher blackens your name or steals your research. Gain a Rival.");
                    character.NextTermBenefits.MusterOut = false;
                    return;
            }
        }


        protected override void AdvancedEducation(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
                    return;
                case 2:
                    character.Skills.Increase("Advocate");
                    return;
                case 3:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 4:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                    return;
                case 5:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Engineer")));
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
            }
        }

        protected override void PersonalDevelopment(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Intellect += 1;
                    return;
                case 2:
                    character.Education += 1;
                    return;
                case 3:
                    character.SocialStanding += 1;
                    return;
                case 4:
                    character.Dexterity += 1;
                    return;
                case 5:
                    character.Endurance += 1;
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                    return;
            }
        }

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.AddRange(SpecialtiesFor("Drive"));
            if (all || roll == 2)
                character.Skills.AddRange(SpecialtiesFor("Electronics"));
            if (all || roll == 3)
                character.Skills.Add("Diplomat");
            if (all || roll == 4)
                character.Skills.Add("Medic");
            if (all || roll == 5)
                character.Skills.Add("Investigate");
            if (all || roll == 6)
                character.Skills.AddRange(SpecialtiesFor("Science"));

        }


        internal override void ServiceSkill(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Drive")));
                    return;
                case 2:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                    return;
                case 3:
                    character.Skills.Increase("Diplomat");
                    return;
                case 4:
                    character.Skills.Increase("Medic");
                    return;
                case 5:
                    character.Skills.Increase("Investigate");
                    return;
                case 6:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Science")));
                    return;
            }
        }
    }
}


