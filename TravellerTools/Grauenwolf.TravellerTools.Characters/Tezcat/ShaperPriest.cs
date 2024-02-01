namespace Grauenwolf.TravellerTools.Characters.Careers.Tezcat;

abstract class ShaperPriest(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Shaper Priest", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Aliens of Charted Space Vol. 4, page 226";
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Profession|Religion", "Science|Shaper Church", "Admin", "Diplomat", "Persuade", "Electronics|Computer");
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
                character.AddHistory($"{character.Name} is involved in good works in the community gaining the respect of a large segment of society. Gain an alley", dice);
                character.SocialStanding += 1;
                character.AddAlly();
                return;

            case 4:
                character.AddHistory($"A notable academic consults with {character.Name} about a publication or documentary they are working on. Gain a Contact in academia.", dice);
                character.AddContact();
                return;

            case 5:
                character.AddHistory($"{character.Name} is sent to a new community or parish to preach Shaper doctrine among the poor.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Streetwise");
                    skillList.Add("Persuade");
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }

                return;

            case 6:
                character.AddHistory($"{character.Name} retreat from the mundane world for a time in the hope of a revelation, although this affects {character.Name}'s work and relationships.", dice);
                character.SocialStanding -= dice.D(3);
                if (character.SocialStanding < 1)
                    character.SocialStanding = 1;
                character.BenefitRolls += dice.D(3);
                character.BenefitRollsPermanentDM += 1;

                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"{character.Name} is chosen to represent the Shaper Church in a vid show or other highly public forum.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Carouse");
                    skillList.Add("Persuade");
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 9:
                if (dice.NextBoolean())
                {
                    var enemies = dice.D(6);
                    character.AddHistory($"{character.Name} refuse an offer of inducements to betray the Shaper Church. Gain {enemies} enemies.", dice);
                    character.AddEnemy(enemies);
                }
                else
                {
                    var cash = 10000 * dice.D(character.BenefitRolls * 2, 6);
                    character.BenefitRolls = 0;
                    character.AddHistory($"{character.Name} accept an offer of inducements to betray the Shaper Church. Gain {cash.ToString("N0")} in cash.", dice);
                    character.NextTermBenefits.MusterOut = true;
                }
                return;

            case 10:
                {
                    if (dice.NextBoolean())
                    {
                        character.AddHistory($"A leader or other important personage confides in {character.Name} about their highly damaging indiscretions. Gain an ally", dice);
                        character.AddAlly();
                    }
                    else
                    {
                        character.AddHistory($"{character.Name} betray a leader or other important personage who confided in {character.Name} about their highly damaging indiscretions. Gain an enemy", dice);
                        character.BenefitRolls += dice.D(3);
                        character.AddEnemy();
                    }
                    return;
                }
            case 11:
                character.AddHistory($"Someone more charismatic but less devout than {character.Name} has become {character.Name}'s superior.", dice);
                character.LongTermBenefits.AdvancementDM = -1;
                return;

            case 12:
                var age = character.AddHistory($"{character.Name}'s faith enjoys an explosion of popularity largely thanks to {character.Name}'s efforts.", dice);
                Promote(character, dice, character.LastCareer!, age);
                character.LongTermBenefits.AdvancementDM = 1;
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                character.AddHistory($"Opponents of {character.Name}'s belief system ambush {character.Name}.", age);
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"{character.Name} aid a follower back to the True Path but this angers a friend or relative. Gain an Enemy.", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"{character.Name} is caught in a scandal.", age);
                Demote(character, dice, character.LastCareer!, age);
                character.BenefitRolls -= 1;
                character.NextTermBenefits.MusterOut = false;
                return;

            case 4:
                character.AddHistory($"{character.Name} have been following false teachings!", age);

                var skills = new List<Skill>();
                var skillA = character.Skills["Profession", "Religion"];
                if (skillA?.Level > 0)
                    skills.Add(skillA);
                var skillB = character.Skills["Science", "Shaper Church"];
                if (skillB?.Level > 0)
                    skills.Add(skillB);

                if (skills.Count > 0)
                    dice.Choose(skills).Level -= 1;
                return;

            case 5:
                character.AddHistory($"{character.Name}'s faith is shaken.", age);
                character.BenefitRolls -= 1;
                character.NextTermBenefits.MusterOut = false;
                return;

            case 6:
                var rivals = dice.D(3);
                character.AddHistory($"{character.Name} come into conflict with a splinter group of the Shaper Church which maintains {character.Name}'s version is the wrong one. Gain {rivals} Rivals.", age);
                character.AddRival(3);

                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Profession|Religion", "Science|Shaper Church", "Admin", "Diplomat", "Persuade", "Electronics|Computer");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                careerHistory.Title = "Novice";
                return;

            case 1:
                careerHistory.Title = "Initiate";
                if (allowBonus)
                    character.Skills.Increase("Science", "Shaper Church");
                return;

            case 2:
                careerHistory.Title = "Acolyte";
                if (allowBonus)
                    character.Skills.Increase("Persuade");
                return;

            case 3:
                careerHistory.Title = "Priest";
                return;

            case 4:
                careerHistory.Title = "High Priest";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;

            case 5:
                careerHistory.Title = "Prelate";
                return;

            case 6:
                careerHistory.Title = "Primate";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Profession|Religion", "Science", "Medic", "Investigate", "Advocate", "Electronics|Computer");
    }

    protected void Demote(Character character, Dice dice, CareerHistory careerHistory, int? age)
    {
        string historyMessage;
        if (careerHistory.CommissionRank > 0)
        {
            careerHistory.CommissionRank -= 1;
            historyMessage = $"Demoted to {careerHistory.LongName} officer rank {careerHistory.CommissionRank}";
        }
        else
        {
            careerHistory.Rank -= 1;
            historyMessage = $"Demoted to {careerHistory.LongName} rank {careerHistory.Rank}";
        }

        var oldTitle = character.Title;
        TitleTable(character, careerHistory, dice, allowBonus: false);
        var newTitle = careerHistory.Title;
        if (oldTitle != newTitle && newTitle != null)
        {
            historyMessage += $" with the new title {newTitle}";
            character.Title = newTitle;
        }
        historyMessage += ".";
        character.AddHistory(historyMessage, age ?? character.Age);
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.IntellectDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Intellect", "Education", "SocialStanding", "Science|Shaper Church", "Profession|Religion", "Persuade");
    }
}
