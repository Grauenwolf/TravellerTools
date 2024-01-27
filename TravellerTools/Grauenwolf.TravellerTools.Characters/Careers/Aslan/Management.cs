namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Management(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Management", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Aliens of Charted Space 1, page 32";

    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Advocate", "Broker", "Admin", "Gun Combat", "Tolerance", "Admin");
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
                {
                    if (dice.RollHigh(8, character.Skills.BestSkillLevel("Natural", "Stealth", "Gun Combat")))
                    {
                        character.AddHistory($"A rival clan attacks and your place of work is targeted by an assault force.", dice);
                        IncreaseOneRandomSkill(character, dice);
                    }
                    else
                    {
                        var age = character.AddHistory($"A rival clan attacks and your place of work is targeted by an assault force. Suffer an injury.", dice);
                        Injury(character, dice, age);
                    }
                }
                return;

            case 4:
                character.AddHistory($"Pick up some useful skills.", dice);
                AddOneSkill(character, dice, "Pilot", "Mechanic", "Electronics", "Drive");
                return;

            case 5:
                var bet = Math.Min(character.BenefitRolls, 3);
                if (bet > 0)
                {
                    if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Broker"), 8))
                    {
                        character.AddHistory($"Take a risk in business and win {bet} benefits.", dice);
                        character.BenefitRolls += bet;
                    }
                    else
                    {
                        character.AddHistory($"Take a risk in business and lose {bet} benefits.", dice);
                        character.BenefitRolls -= bet;
                    }
                }
                else
                {
                    character.AddHistory($"Miss out on an opportunity to take a risk in business.", dice);
                }
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
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Broker");
                    skillList.AddRange(SpecialtiesFor(character, "Profession"));
                    skillList.Add("Streetwise");
                    skillList.RemoveOverlap(character.Skills, 1);

                    if (skillList.Count > 0 && dice.NextBoolean())
                    {
                        character.AddHistory($"Expand into new territories.", dice);
                        character.Skills.Add(dice.Choose(skillList), 1);
                    }
                    else
                    {
                        character.AddHistory($"Expand into new territories. Gain a Contact.", dice);
                        character.AddContact();
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
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Diplomat", "Admin"), 8))
                    {
                        character.AddHistory($"A foolish decision by a clan member threatens the business. {character.Name} fixes it, but gains a Rival.", dice);
                        character.AddRival();
                    }
                    else
                    {
                        character.AddHistory($"A foolish decision by a clan member threatens the business. {character.Name} is unable to fix it.", dice);
                        character.CurrentTermBenefits.AdvancementDM += -2;
                    }
                }
                return;

            case 10:
                character.AddHistory($"Clan thrives and prospers", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;

                return;

            case 11:
                character.AddHistory($"Trade with aliens and barbarians", dice);
                if (dice.NextBoolean())
                    character.Skills.Increase("Tolerance");
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
                if (dice.NextBoolean())
                {
                    character.AddHistory($"You are caught stealing from employer.", age);
                    character.BenefitRolls += 3;
                    character.IsOutcast = true;
                    character.SocialStanding = 2;
                }
                else
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Advocate"), 8))
                    {
                        character.AddHistory($"Falsely accused of stealing from employer and cleared your name.", age);
                        character.NextTermBenefits.MusterOut = false;
                    }
                    else
                    {
                        character.AddHistory($"Falsely accused of stealing from employer.", age);
                    }
                }
                return;

            case 3:
                character.AddHistory($"A shift in clan politics leaves you shut out in the cold. Gain a Contact who stays in touch despite your new status.", age);
                character.AddContact();
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Clan’s fortunes decline, but stay in anyways.", age);
                    character.BenefitRolls -= 2;
                    character.NextTermBenefits.MusterOut = false;
                }
                else
                {
                    character.AddHistory($"Clan’s fortunes decline.", age);
                }

                return;

            case 5:
                character.AddHistory($"Sent to a border world, where career stagnates.", age);
                AddOneSkill(character, dice, "Survival", "Flyer", "Profession", "Navigation");
                return;

            case 6:
                character.AddHistory($"A clan elder takes a dislike. Gain her as a Rival.", age);
                character.AddRival();
                return;
        }
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 8, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Broker", "Admin", "Gun Combat", "Tolerance", "Admin");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                if (allowBonus)
                    character.Skills.Add("Admin", 1);
                return;

            case 2:
                return;

            case 3:
                if (allowBonus)
                    character.Skills.Add("Tolerance", 1);
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Electronics", "Language", "Science", "Tolerance", "Diplomat");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Intellect", "Strength", "Dexterity", "Endurance", "Tolerance", "Broker");
    }
}
