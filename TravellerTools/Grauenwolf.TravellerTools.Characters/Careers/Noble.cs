namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class Noble(string assignment, CharacterBuilder characterBuilder) : NormalCareer("Noble", assignment, characterBuilder)
{
    protected override int AdvancedEductionMin => 8;

    protected override bool RankCarryover => false;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        var roll = dice.D(6);

        if (all || roll == 1)
            character.Skills.Add("Admin");
        if (all || roll == 2)
            character.Skills.Add("Advocate");
        if (all || roll == 3)
            character.Skills.Add("Electronics");
        if (all || roll == 4)
            character.Skills.Add("Diplomat");
        if (all || roll == 5)
            character.Skills.Add("Investigate");
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
                if (dice.NextBoolean())
                {
                    character.AddHistory("Refused a challenge to a duel for your honour and standing.", dice);
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

                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Melee", "Blade)");
                    skillList.Add("Leadership");
                    skillList.AddRange(SpecialtiesFor("Tactics"));
                    skillList.Add("Deception");
                    character.Skills.Increase(dice.Choose(skillList));
                }
                return;

            case 4:
                character.AddHistory("time as a ruler or playboy gives you a wide range of experiences.", dice);
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Animals", "Handling");
                    skillList.AddRange(SpecialtiesFor("Art"));
                    skillList.Add("Carouse");
                    skillList.Add("Streetwise");
                    skillList.RemoveOverlap(character.Skills, 1);
                    if (skillList.Count > 0)
                        character.Skills.Add(dice.Choose(skillList), 1);
                }
                return;

            case 5:
                character.AddHistory($"Inherit a gift from a rich relative.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 6:
                character.AddHistory("Become deeply involved in politics on your world of residence, becoming a player in the political intrigues of government. Gain a Rival.", dice);
                character.AddRival();
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.Add("Advocate");
                    skillList.Add("Admin");
                    skillList.Add("Diplomacy");
                    skillList.Add("Persuade");
                    character.Skills.Increase(dice.Choose(skillList));
                }

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
                            character.AddHistory("Join a successful conspiracy of nobles.", dice);
                            var skillList = new SkillTemplateCollection();
                            skillList.Add("Deception");
                            skillList.Add("Persuade");
                            skillList.AddRange(SpecialtiesFor("Tactics"));
                            skillList.Add("Carouse");
                            character.Skills.Increase(dice.Choose(skillList));
                        }
                        else
                        {
                            var age = character.AddHistory("Join a conspiracy of nobles that were caught.", dice);
                            Mishap(character, dice, age);
                        }
                    }
                    else
                    {
                        character.AddHistory("Refuse to join a conspiracy of nobles. Gain an Enemy.", dice);
                        character.AddEnemy();
                    }
                }
                return;

            case 9:
                character.AddHistory("Your reign is acclaimed by all as being fair and wise – or in the case of a dilettante, you sponge off your family’s wealth a while longer. Gain either a jealous relative or an unhappy subject as an Enemy.", dice);
                character.AddEnemy();
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory("You manipulate and charm your way through high society. Gain a Rival and an Ally.", dice);
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
                character.AddHistory("You make an alliance with a powerful and charismatic noble, who becomes an Ally.", dice);
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
                character.AddHistory("Your efforts do not go unnoticed by the Imperium.", dice);
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
                character.AddHistory("A family scandal forces you out of your position.", age);
                character.SocialStanding += -1;
                return;

            case 3:
                character.AddHistory("A disaster or war strikes.", age);
                if (!dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 8))
                    Injury(character, dice, age);
                return;

            case 4:
                character.AddHistory("Political manoeuvrings usurp your position. Gain a Rival.", age);
                character.AddRival();
                {
                    var skillList = new SkillTemplateCollection();
                    skillList.AddRange(SpecialtiesFor("Diplomat"));
                    skillList.AddRange(SpecialtiesFor("Advocate"));
                    character.Skills.Increase(dice.Choose(skillList));
                }
                return;

            case 5:
                character.AddHistory("An assassin attempts to end your life.", age);
                if (!dice.RollHigh(character.EnduranceDM, 8))
                    Injury(character, dice, age);
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice)
    {
        if (character.SocialStanding >= 10 && !character.LongTermBenefits.Retired)
            return true;

        var dm = character.SocialStandingDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);

        return dice.RollHigh(dm, 10);
    }

    internal override void ServiceSkill(Character character, Dice dice)
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Electronics")));
                return;

            case 4:
                character.Skills.Increase("Diplomat");
                return;

            case 5:
                character.Skills.Increase("Investigate");
                return;

            case 6:
                character.Skills.Increase("Persuade");
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Language")));
                return;

            case 4:
                character.Skills.Increase("Leadership");
                return;

            case 5:
                character.Skills.Increase("Diplomat");
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Art")));
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
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Gun Combat")));
                return;

            case 6:
                character.Skills.Increase(dice.Choose(SpecialtiesFor("Melee")));
                return;
        }
    }
}
