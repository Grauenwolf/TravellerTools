namespace Grauenwolf.TravellerTools.Characters.Careers;

abstract class Prisoner(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : FullCareer("Prisoner", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Traveller Core, page 56";
    internal override bool RankCarryover { get; } = true;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        //Rank 0 skill
        character.Skills.Add("Melee", "Unarmed", 1);

        AddBasicSkills(character, dice, all, "Athletics", "Deception", "Profession", "Streetwise", "Melee", "Persuade");
    }

    internal override void Event(Character character, Dice dice)
    {
        switch (dice.D(2, 6))
        {
            case 2:
                Mishap(character, dice, character.Age + dice.D(4));
                return;

            case 3:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 10))
                {
                    character.AddHistory($"Escaped from prison.", dice);
                    character.NextTermBenefits.MusterOut = true;
                }
                else
                {
                    character.AddHistory($"Failed to escaped from prison.", dice);
                    character.Parole += 2;
                }
                return;

            case 4:
                character.AddHistory($"Assigned to difficult or backbreaking labour.", dice);
                if (dice.RollHigh(character.EnduranceDM, 8))
                {
                    character.Parole += -1;
                    var skills = new SkillTemplateCollection();
                    skills.AddRange(SpecialtiesFor(character, "Athletics"));
                    skills.Add("Mechanic");
                    skills.Add("Melee", "Unarmed");
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                else
                {
                    character.Parole += 1;
                }
                return;

            case 5:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Persuade", "Melee"), 8))
                {
                    character.AddHistory($"Joined a prison gang", dice);
                    character.LongTermBenefits.PrisonSurvivalDM += 1;
                    character.Parole += 1;

                    var skills = new SkillTemplateCollection();
                    skills.Add("Deception");
                    skills.Add("Persuade");
                    skills.Add("Stealth");
                    skills.Add("Melee", "Unarmed");
                    skills.RemoveOverlap(character.Skills, 1);
                    if (skills.Count > 0)
                        character.Skills.Add(dice.Choose(skills), 1);
                }
                else
                {
                    character.AddHistory($"Offended a prison gang {character.Name} tried to join. Gain an Enemy.", dice);
                    character.AddEnemy();
                }
                return;

            case 6:
                character.AddHistory($"Vocational Training.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                {
                    IncreaseOneRandomSkill(character, dice);
                }
                return;

            case 7:
                switch (dice.D(6))
                {
                    case 1:
                        {
                            var age = character.AddHistory($"A riot engulfs the prison.", dice);
                            if (dice.D(6) <= 2)
                            {
                                Injury(character, dice, age);
                            }
                            else
                            {
                                character.AddHistory($"Loot the guards/other prisoners.", age);
                                character.BenefitRolls += 1;
                            }
                            return;
                        }
                    case 2:
                        character.AddHistory($"Make friends with another inmate; gain them as a Contact.", dice);
                        character.AddContact();
                        return;

                    case 3:
                        character.AddHistory($"{character.Name} gain a new Rival among the inmates or guards.", dice);
                        return;

                    case 4:
                        var oldParole = character.Parole;
                        character.Parole = dice.D(6) + 4;

                        if (oldParole > character.Parole)
                            character.AddHistory($"{character.Name} is moved to a lower security prison.", dice);
                        else if (oldParole == character.Parole)
                            character.AddHistory($"{character.Name} is moved to a different prison.", dice);
                        else
                            character.AddHistory($"{character.Name} is moved to a higher security prison.", dice);
                        return;

                    case 5:
                        character.AddHistory($"Good Behaviour.", dice);
                        character.Parole += -2;
                        return;

                    case 6:
                        {
                            var age = character.AddHistory($"Attacked by another prisoner.", dice);
                            if (!dice.RollHigh(character.Skills.GetLevel("Melee", "Unarmed"), 8))
                            {
                                Injury(character, dice, age);
                            }
                            return;
                        }
                }
                return;

            case 8:
                character.AddHistory($"Parole hearing.", dice);
                character.Parole += -1;
                return;

            case 9:
                character.AddHistory($"Hire a new lawyer.", dice);
                var advocate = dice.D(6) - 2;
                if (dice.RollHigh(advocate, 8))
                {
                    character.Parole -= dice.D(6);
                }
                return;

            case 10:
                character.AddHistory($"Special Duty.", dice);
                IncreaseOneSkill(character, dice, "Admin", "Advocate", "Electronics|Computers", "Steward");
                return;

            case 11:
                character.AddHistory($"The warden takes an interest in {character.Name}'s case.", dice);
                character.Parole += -2;
                return;

            case 12:
                if (dice.RollHigh(8))
                {
                    character.AddHistory($"Saved a guard or prison officer. Gain an Ally.", dice);
                    character.AddAlly();
                    character.Parole += -2;
                }
                else
                {
                    character.AddHistory($"Attmped but failed to save a guard or prison officer.", dice);
                    InjuryRollAge(character, dice, false);
                }
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
                character.AddHistory($"Accused of assaulting a prison guard.", age);
                character.Parole += 2;
                return;

            case 3:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Persecuted by a prison gang.", age);
                    character.BenefitRolls = 0;
                }
                else
                {
                    if (dice.RollHigh(character.Skills.GetLevel("Melee", "Unarmed"), 8))
                    {
                        character.AddHistory($"Beaten by a prison gang.", age);
                        Injury(character, dice, true, age);
                    }
                    else
                    {
                        character.AddHistory($"Defeated a prison gang. Gain an Enemy.", age);
                        character.AddEnemy();
                        character.Parole += 1;
                    }
                }
                return;

            case 4:
                character.AddHistory($"A guard takes a dislike to {character.Name}.", age);
                character.Parole += 1;
                return;

            case 5:
                character.AddHistory($"Disgraced. Word of {character.Name}'s criminal past reaches {character.Name}'s homeworld.", age);
                character.SocialStanding += -1;
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    internal override void Run(Character character, Dice dice)
    {
        var careerHistory = SetupAndSkills(character, dice);

        careerHistory.Terms += 1;
        character.LastCareer = careerHistory;

        character.Parole ??= dice.D(6) + 4;

        var survived = dice.RollHigh(character.GetDM(SurvivalAttribute) + character.NextTermBenefits.SurvivalDM, SurvivalTarget);
        if (survived)
        {
            character.BenefitRolls += 1;

            Event(character, dice);
            character.Age += 4;

            var advancementRoll = dice.D(2, 6);

            advancementRoll += character.GetDM(AdvancementAttribute) + character.GetAdvancementBonus(Career, Assignment);

            if (advancementRoll >= AdvancementTarget)
            {
                Promote(character, dice, careerHistory);
                FixupSkills(character, dice);

                //advancement skill
                CareerSkill(character, dice);
                FixupSkills(character, dice);
            }

            if (character.NextTermBenefits.MusterOut)
            {
                //must have escaped due to event roll.
            }
            else if (advancementRoll > character.Parole)
            {
                character.NextTermBenefits.MusterOut = true;
                character.AddHistory($"Paroled from prison.", character.Age);
            }
            else
                character.NextTermBenefits.MustEnroll = "Prisoner";
        }
        else
        {
            var mishapAge = character.Age + dice.D(4);

            character.NextTermBenefits.MustEnroll = "Prisoner";
            Mishap(character, dice, mishapAge);

            if (character.NextTermBenefits.MusterOut)
                character.Age = mishapAge;
            else
                character.Age += +4; //Complete the term dispite the mishap.
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Athletics", "Deception", "Profession", "Streetwise", "Melee|Unarmed", "Persuade");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 1:
                return;

            case 2:
                if (allowBonus)
                    AddOneSkill(character, dice, "Athletics");
                return;

            case 3:
                return;

            case 4:
                if (allowBonus)
                    character.Skills.Add("Advocate");
                return;

            case 5:
                return;

            case 6:
                if (allowBonus)
                    character.Endurance += 1;
                return;
        }
    }

    protected override void CareerSkill(Character character, Dice dice)
    {
        var skillTables = new List<SkillTable>();
        skillTables.Add(PersonalDevelopment);
        skillTables.Add(ServiceSkill);
        skillTables.Add(AssignmentSkills);
        dice.Choose(skillTables)(character, dice);
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck) => false;

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Strength", "Melee|Unarmed", "Endurance", "Jack-of-All-Trades", "Education", "Gambler");
    }
}
