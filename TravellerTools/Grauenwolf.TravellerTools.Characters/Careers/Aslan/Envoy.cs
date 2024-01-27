namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Envoy(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Envoy", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Aliens of Charted Space 1, page 24";
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Intellect", "Diplomat", "Tolerance", "Carouse", "Survival", "Leadership");
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
                AddOneSkill(character, dice, "Animals|Training", "Survival", "Stealth", "Athletics|Dexterity");
                return;

            case 5:
                character.AddHistory($"Attend a clan council on the homeworld. Gain a Contact there.", dice);
                character.AddContact();
                return;

            case 6:
                character.AddHistory($"{character.Name} is given advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                    IncreaseOneRandomSkill(character, dice);
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
                AddOneSkill(character, dice, "Survival", "Pilot", "Carouse", "Independence");
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

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 10, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Intellect", "Diplomat", "Tolerance", "Carouse", "Survival", "Leadership");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                careerHistory.Title = "Junior Envoy";
                if (allowBonus)
                    character.Skills.Add("Tolerance", 1);
                return;

            case 2:
                careerHistory.Title = "Envoy";
                return;

            case 3:
                careerHistory.Title = "Senior Envoy";
                if (allowBonus)
                    character.Skills.Add("Diplomat", 1);
                return;

            case 4:
                careerHistory.Title = "Respected Envoy";
                return;

            case 5:
                careerHistory.Title = "Honoured Envoy";
                if (allowBonus)
                    character.Skills.Add("Carouse", 1);
                return;

            case 6:
                careerHistory.Title = "Voice of the Clan";
                if (allowBonus)
                    character.Territory += 2;
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Language", "Science", "Electronics", "Diplomat");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Melee|Natural", "Independence", "Independence");
    }
}
