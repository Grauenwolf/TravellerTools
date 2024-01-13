namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Believer(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Believer", assignment, characterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    protected override bool RankCarryover => false;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Profession", "Religion");
        if (all || roll == 2)
            character.Skills.Add("Science", "Belief");
        if (all || roll == 3)
            character.Skills.Add("Admin");
        if (all || roll == 4)
            character.Skills.Add("Electronics", "Computers");
        if (all || roll == 5)
            character.Skills.Add("Diplomat");
        if (all || roll == 6)
            character.Skills.Add("Persuade");
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
                character.AddHistory($"{character.Name} is involved in good works in the community, gaining the respect of a large segment of \r\nsociety. Gain SOC +1 and an Ally. ", dice);
                character.AddAlly();
                character.SocialStanding += 1;
                return;

            case 4:
                character.AddHistory($"A notable academic consults with {character.Name} about a publication or documentary he is working on. Gain a Contact in academia.", dice);
                return;

            case 5:
                character.AddHistory($"{character.Name} is sent to a new community or parish to preach The Word among the poor.", dice);
                if (dice.NextBoolean())
                    character.Skills.Increase("Streetwise");
                else
                    character.Skills.Increase("Persuade");
                return;

            case 6:
                character.AddHistory($"{character.Name} retreat from the mundane world for a time in the hope of a revelation, although this affects \r\nyour work and relationships.", dice);
                character.SocialStanding -= dice.D(3);
                character.BenefitRolls += dice.D(3);
                character.BenefitRollsPermanentDM += 1;
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory($"{character.Name} is chosen to represent {character.Name}'s faith in a vid show or other highly public forum.", dice);
                if (dice.NextBoolean())
                    character.Skills.Increase("Carouse");
                else
                    character.Skills.Increase("Persuade");

                return;

            case 9:
                if (dice.NextBoolean())
                {
                    var enemies = dice.D(6);
                    character.AddHistory($"{character.Name} refused an offer of inducements to betray {character.Name}'s faith. Gain {enemies} enemies.", dice);
                    character.AddEnemy(enemies);
                }
                else
                {
                    var cash = 10000 * dice.D(character.BenefitRolls * 2, 6);
                    character.BenefitRolls = 0;
                    character.AddHistory($"{character.Name} accepted an offer of inducements to betray {character.Name}'s faith. Gain {cash.ToString("N0")} in cash.", dice);
                    character.NextTermBenefits.MusterOut = true;
                }
                return;

            case 10:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"{character.Name} secretly provide religious rites for a dying leader or noble, although they do not share {character.Name}'s faith. Gain an Ally in the family and a Rival who disagrees with {character.Name}'s choice.", dice);
                    character.AddAlly();
                    character.AddRival();
                }
                else
                {
                    character.AddHistory($"{character.Name} refuse to secretly provide religious rites for a dying leader or noble, because they do not share {character.Name}'s faith.", dice);
                }
                return;

            case 11:
                character.AddHistory($"Someone more charismatic but less devout than {character.Name} has become {character.Name}'s superior.", dice);
                character.LongTermBenefits.AdvancementDM = -1;
                return;

            case 12:
                var age = character.AddHistory($"{character.Name} faith enjoys an explosion of popularity largely thanks to {character.Name}'s efforts.", dice);
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
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"{character.Name} aid a follower back to the True Path but this angers a friend or relative. Gain an Enemy.", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"{character.Name} is caught in the periphery of a scandal.", age);
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
                character.AddHistory($"{character.Name} faith is shaken.", age);
                character.BenefitRolls = 0;
                character.NextTermBenefits.MusterOut = false;
                return;

            case 6:
                var rivals = dice.D(3);
                character.AddHistory($"{character.Name} come into conflict with a splinter group {character.Name}'s own religion which maintains {character.Name}'s version is the wrong one. Gain {rivals} Rivals.", age);
                character.AddRival(3);

                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck) => true;

    internal override void ServiceSkill(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Profession", "Religion");
                return;

            case 2:
                character.Skills.Increase("Science", "Belief");
                return;

            case 3:
                character.Skills.Increase("Admin");
                return;

            case 4:
                character.Skills.Increase("Electronics", "Computers");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
                return;

            case 6:
                character.Skills.Increase("Persuade");
                return;
        }
    }

    internal sealed override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
    {
        TitleTable(character, careerHistory, dice, true);
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        switch (dice.D(6))
        {
            case 1:
                character.Skills.Increase("Profession", "Religion");
                return;

            case 2:
                character.Skills.Increase(dice.Choose(SpecialtiesFor(character, "Electronics")));
                return;

            case 3:
                character.Skills.Increase("Medic");
                return;

            case 4:
                character.Skills.Increase("Investigate");
                return;

            case 5:
                character.Skills.Increase("Electronics", "Computers");
                return;

            case 6:
                character.Skills.Increase("Advocate");
                return;
        }
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
                character.Skills.Increase("Science", "Belief");
                return;

            case 5:
                character.Skills.Increase("Profession", "Religion");
                return;

            case 6:
                character.Skills.Increase("Persuade");
                return;
        }
    }

    protected abstract void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus);
}
