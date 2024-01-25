namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Envoy(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Envoy", assignment, characterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(5);

        if (all || roll == 1)
            character.Skills.Add("Diplomat");
        if (all || roll == 2)
            character.Skills.Add("Tolerance");
        if (all || roll == 3)
            character.Skills.Add("Carouse");
        if (all || roll == 4)
            character.Skills.Add("Survival");
        if (all || roll == 5)
            character.Skills.Add("Leadership");
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
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name}' clan places {character.Name} in a difficult situation and {character.Name} fled.", dice);
                    character.SocialStanding += -1;
                }
                else
                {
                    var bestSkill = character.Skills.BestSkillLevel("Diplomat", "Investigate", "Stealth");
                    if (dice.RollHigh(8, bestSkill))
                    {
                        character.AddHistory($"{character.Name}' clan places {character.Name} in a difficult situation. {character.Name} fought and failed.", dice);
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    else
                    {
                        character.AddHistory($"{character.Name}' clan places {character.Name} in a difficult situation. {character.Name} fought and lost.", dice);
                        character.SocialStanding += -1;
                        character.CurrentTermBenefits.AdvancementDM += -2;
                    }
                }
                return;

            case 4:
                character.AddHistory($"Develop a taste for hunting.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Animals", "Training");
                    skillList.Add("Survival");
                    skillList.Add("Stealth");
                    skillList.Add("Athletics", "Dexterity");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 5:
                character.AddHistory($"Attend a clan council on the homeworld. Gain a Contact there.", dice);
                character.AddContact();
                return;

            case 6:
                character.AddHistory($"{character.Name} is given advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    character.Skills.Increase(dice.Choose(RandomSkills(character)));
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                {
                    var bestSkill = character.Skills.BestSkillLevel("Carouse", "Persuade");
                    if (dice.RollHigh(8, bestSkill))
                    {
                        character.AddHistory($"Spend time in diplomatic circles. Gain an Ally.", dice);
                        character.AddAlly();
                    }
                    else
                    {
                        character.AddHistory($"Spend time in diplomatic circles. Gain an Rival.", dice);
                        character.AddRival();
                    }
                }
                return;

            case 9:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Insulted by a noble of a rival clan and did nothing.", dice);
                    character.SocialStanding += -2;
                }
                else
                {
                    if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Melee", "Natural"), 9))
                    {
                        character.AddHistory($"Insulted by a noble of a rival clan and win a duel.", dice);
                        character.SocialStanding += 1;
                        character.CurrentTermBenefits.AdvancementDM += 2;
                    }
                    else
                    {
                        character.AddHistory($"Insulted by a noble of a rival clan and lose a duel.", dice);
                        character.SocialStanding += -2;
                        character.CurrentTermBenefits.AdvancementDM += -2;
                    }
                }
                return;

            case 10:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Offered membership of a conspiracy in the upper echelons of the clan. Refused and gain the conspiracy as an Enemy.", dice);
                    character.AddEnemy();
                }
                else
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Deception", "Persuade"), 8))
                    {
                        character.AddHistory($"Succeeded in a conspiracy with the upper echelons of the clan.", dice);
                        switch (dice.D(4))
                        {
                            case 1:
                                character.Skills.Increase("Deception");
                                break;

                            case 2:
                                character.Skills.Increase("Persuade");
                                break;

                            case 3:
                                character.SocialStanding += 1;
                                break;

                            case 4:
                                character.Territory += 1;
                                break;
                        }
                        character.AddEnemy();
                    }
                    else
                    {
                        var age = character.AddHistory($"Failed in a conspiracy with the upper echelons of the clan.", dice);
                        Mishap(character, dice, age);
                        character.NextTermBenefits.MusterOut = false;
                    }
                }
                return;

            case 11:
                character.AddHistory($"Trusted by the great lords of the clan", dice);
                if (dice.NextBoolean())
                    character.Territory += 2;
                else
                    character.CurrentTermBenefits.AdvancementDM += 4;
                return;

            case 12:
                character.AddHistory($"Excel in {character.Name}'s role.", dice);
                character.CurrentTermBenefits.AdvancementDM += 99;
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"A blunder in a negotiation brings shame to the clan. {character.Name} is now an Outcast.", age);
                character.SocialStanding = 2;
                character.IsOutcast = true;
                return;

            case 3:
                character.AddHistory($"Fail in a difficult assignment because of the manipulations of another Envoy. Gain him as a Rival.", age);
                character.AddRival();
                return;

            case 4:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Natural", "Recon"), 8))
                {
                    character.AddHistory($"Defeat an assassin.", age);
                    character.NextTermBenefits.MusterOut = false;
                }
                else
                {
                    character.AddHistory($"Injured by an assassin.", age);
                    Injury(character, dice, true, age);
                }
                return;

            case 5:
                character.AddHistory($"{character.Name} is dispatched to a distant world for a long period; by the time {character.Name} returns, {character.Name}'s position has been taken by someone younger and more ambitious.", age);

                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Survival");
                    skillList.Add("Pilot");
                    skillList.Add("Carouse");
                    skillList.Add("Independence");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 6:
                if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Tolerance"), 8))
                {
                    character.AddHistory($"Ignore an injusting human ambassador.", age);
                    character.NextTermBenefits.MusterOut = false;
                }
                else
                {
                    character.AddHistory($"Ate an injusting human ambassador. Gain an enemy.", age);
                    character.AddEnemy();
                }

                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 10, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Intellect += 1;
                return;

            case 2:
                character.Skills.Increase("Diplomat");
                return;

            case 3:
                character.Skills.Increase("Tolerance");
                return;

            case 4:
                character.Skills.Increase("Carouse");
                return;

            case 5:
                character.Skills.Increase("Survival");
                return;

            case 6:
                character.Skills.Increase("Leadership");
                return;
        }
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                careerHistory.Title = "Junior Envoy";
                character.Skills.Add("Tolerance", 1);
                return;

            case 2:
                careerHistory.Title = "Envoy";
                return;

            case 3:
                careerHistory.Title = "Senior Envoy";
                character.Skills.Add("Diplomat", 1);
                return;

            case 4:
                careerHistory.Title = "Respected Envoy";
                return;

            case 5:
                careerHistory.Title = "Honoured Envoy";
                character.Skills.Add("Carouse", 1);
                return;

            case 6:
                careerHistory.Title = "Voice of the Clan";
                character.Territory += 2;
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Admin");
                return;

            case 2:
                character.Skills.Increase("Advocate");
                return;

            case 3:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Language")));
                return;

            case 4:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Science")));
                return;

            case 5:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 6:
                character.Skills.Increase("Diplomat");
                return;
        }
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        switch (character.Gender == "M" ? dice.D(6) : dice.D(4))
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
                character.Skills.Increase("Melee", "Nnatural");
                return;

            case 5:
                character.Skills.Increase("Independence");
                return;

            case 6:
                character.Skills.Increase("Independence");
                return;
        }
    }
}
