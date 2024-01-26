namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Noble(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Noble", assignment, speciesCharacterBuilder)
{
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Admin", "Advocate", "Electronics", "Diplomat", "Investigate", "Persuade");
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
                    character.AddHistory($"Refused a challenge to a duel for {character.Name}'s honour and standing.", dice);
                    character.SocialStanding += -1;
                }
                else
                {
                    if (dice.RollHigh(character.Skills.GetLevel("Melee", "Blade"), 8))
                    {
                        character.SocialStanding += 1;
                    }
                    else
                    {
                        character.SocialStanding += -1;
                        InjuryRollAge(character, dice);
                    }

                    IncreaseOneSkill(character, dice, "Melee|Blade)", "Leadership", "Tactics", "Deception");
                }
                return;

            case 4:
                character.AddHistory($"time as a ruler or playboy gives {character.Name} a wide range of experiences.", dice);
                AddOneSkill(character, dice, "Animals|Handling", "Art", "Carouse", "Streetwise");
                return;

            case 5:
                character.AddHistory($"Inherit a gift from a rich relative.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                character.AddHistory($"Become deeply involved in politics on {character.Name}'s world of residence, becoming a player in the political intrigues of government. Gain a Rival.", dice);
                character.AddRival();
                IncreaseOneSkill(character, dice, "Advocate", "Admin", "Diplomacy", "Persuade");

                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                {
                    if (dice.NextBoolean())
                    {
                        if (dice.RollHigh(character.Skills.BestSkillLevel("Deception", "Persuade"), 8))
                        {
                            character.AddHistory($"Join a successful conspiracy of nobles.", dice);
                            IncreaseOneSkill(character, dice, "Deception", "Persuade", "Tactics", "Carouse");
                        }
                        else
                        {
                            var age = character.AddHistory($"Join a conspiracy of nobles that were caught.", dice);
                            Mishap(character, dice, age);
                        }
                    }
                    else
                    {
                        character.AddHistory($"Refuse to join a conspiracy of nobles. Gain an Enemy.", dice);
                        character.AddEnemy();
                    }
                }
                return;

            case 9:
                character.AddHistory($"{character.Name} reign is acclaimed by all as being fair and wise – or in the case of a dilettante, {character.Name} sponge off {character.Name}'s family’s wealth a while longer. Gain either a jealous relative or an unhappy subject as an Enemy.", dice);
                character.AddEnemy();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory($"{character.Name} manipulate and charm {character.Name}'s way through high society. Gain a Rival and an Ally.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Carouse");
                    skillList.Add("Diplomat");
                    skillList.Add("Persuade");
                    skillList.Add("Steward");
                    character.Skills.Increase(dice.Choose(skillList));
                    character.AddRival();
                    character.AddAlly();
                }
                return;

            case 11:
                character.AddHistory($"{character.Name} make an alliance with a powerful and charismatic noble, who becomes an Ally.", dice);
                character.AddAlly();
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Increase("Leadership");
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"{character.Name} efforts do not go unnoticed by the Imperium.", dice);
                character.CurrentTermBenefits.AdvancementDM += 100;
                return;
        }
    }

    internal override decimal MedicalPaymentPercentage(Character character, Dice dice)
    {
        var roll = dice.D(2, 6) + (character.LastCareer?.Rank ?? 0);
        if (roll >= 12)
            return 1.0M;
        if (roll >= 8)
            return 0.75M;
        if (roll >= 4)
            return 0.50M;
        return 0;
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                Injury(character, dice, true, age);
                return;

            case 2:
                character.AddHistory($"A family scandal forces {character.Name} out of {character.Name}'s position.", age);
                character.SocialStanding += -1;
                return;

            case 3:
                character.AddHistory($"A disaster or war strikes.", age);
                if (!dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 8))
                    Injury(character, dice, age);
                return;

            case 4:
                character.AddHistory($"Political manoeuvrings usurp {character.Name}'s position. Gain a Rival.", age);
                character.AddRival();
                IncreaseOneSkill(character, dice, "Diplomat", "Advocate");
                return;

            case 5:
                character.AddHistory($"An assassin attempts to end {character.Name}'s life.", age);
                if (!dice.RollHigh(character.EnduranceDM, 8))
                    Injury(character, dice, age);
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        if (character.SocialStanding >= 10 && !character.LongTermBenefits.Retired)
            return true;

        var dm = character.SocialStandingDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 10, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Electronics", "Diplomat", "Investigate", "Persuade");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Advocate", "Language", "Leadership", "Diplomat", "Art");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Dexterity", "Endurance", "Gambler", "Gun Combat", "Melee");
    }
}
