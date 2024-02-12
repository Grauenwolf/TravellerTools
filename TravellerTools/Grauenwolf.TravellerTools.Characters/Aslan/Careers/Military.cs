namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Military(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Military", assignment, speciesCharacterBuilder)
{
    public override CareerType CareerTypes => CareerType.Military;
    public override string? Source => "Aliens of Charted Space 1, page 28";
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Gun Combat", "Drive", "Survival", "Melee|Natural", "Athletics", "Recon");
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
                    if (dice.RollHigh(8, character.Skills.BestSkillLevel("Recon", "Gun Combat")))
                    {
                        character.AddHistory($"Sent into the maw of hell.", dice);
                        AddOneSkill(character, dice, "Stealth", "Medic", "Heavy Weapons", "Leadership");
                    }
                    else
                    {
                        var age = character.AddHistory($"Sent into the maw of hell and injured.", dice);
                        Injury(character, dice, age);
                    }
                }
                return;

            case 4:

                character.AddHistory($"Assigned to garrison duty on a clan outpost. Gain a Contact.", dice);
                character.AddContact();
                IncreaseOneSkill(character, dice, "Streetwise", "Electronics|Comms", "Mechanic");

                return;

            case 5:
                var skill = IncreaseOneSkill(character, dice, "Melee|Natural", "Gun Combat", "Drive", "Survival");
                if (skill != null && dice.RollHigh(skill.Level, 8))
                {
                    character.AddHistory($"Victorious in a border skirmish with another clan.", dice);
                    character.CurrentTermBenefits.AdvancementDM += 2;
                }
                else
                    character.AddHistory($"Involved in a border skirmish with another clan.", dice);
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
                character.AddHistory("Fight against an alien race.", dice);
                IncreaseOneSkill(character, dice, "Gun Combat", "Language", "Melee", "Recon", "Suvival");
                return;

            case 9:
                if (dice.NextBoolean())
                {
                    if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Melee", "Natural"), 8))
                    {
                        character.AddHistory($"An officer insults {character.Name}'s courage, leading to a successful duel.", dice);
                        character.SocialStanding += 1;
                    }
                    else
                    {
                        character.AddHistory($"An officer insults {character.Name}'s courage, leading to a defeat in a duel.", dice);
                        character.SocialStanding += -1;
                    }
                }
                else
                {
                    if (dice.NextBoolean())
                    {
                        var age = character.AddHistory($"An officer insults {character.Name}'s courage, leading to reckless behavior and a wound.", dice);
                        Injury(character, dice, age);
                    }
                    else
                    {
                        character.AddHistory($"An officer insults {character.Name}'s courage. Under fire, prove the office wrong and gain a Rival.", dice);
                        character.AddRival();
                        character.SocialStanding += 1;
                        character.CurrentTermBenefits.AdvancementDM += 4;
                    }
                }
                return;

            case 10:

                if (dice.RollHigh(character.RiteOfPassageDM + character.LastCareer!.Terms, 10))
                {
                    character.AddHistory($"Promoted to the officer caste.", dice);
                    character.NextTermBenefits.MustEnroll = "Military Officer";
                }
                else
                {
                    character.AddHistory($"Considered for promotion in the officer caste but didn't make the cut.", dice);
                }

                return;

            case 11:
                character.AddHistory($"Serve under a hero of the clan.", dice);
                if (dice.NextBoolean())
                    character.Skills.Increase("Tactics|Military");
                else
                    character.CurrentTermBenefits.AdvancementDM += 4;
                return;

            case 12:
                character.AddHistory($"Efforts strike a great blow for the clan.", dice);
                character.CurrentTermBenefits.AdvancementDM += 99;
                return;
        }
    }

    internal override void Mishap(Character character, Dice dice, int age)
    {
        switch (dice.D(6))
        {
            case 1:
                SevereInjury(character, dice, age);
                return;

            case 2:
                character.AddHistory($"Drumed out of the service by a superior officer. Gain a Rival.", age);
                character.AddRival();
                return;

            case 3:
                character.AddHistory($"Lost behind enemy lines.", age);
                IncreaseOneSkill(character, dice, "Stealth", "Survival", "Streetwise", "Gun Combat");
                return;

            case 4:
                character.AddHistory($"Captured and ransomed back to the clan.", age);
                character.SocialStanding += -1;
                return;

            case 5:
                if (dice.NextBoolean())
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Gun Combat", "Athletics"), 8))
                    {
                        character.AddHistory("Fight bravely in a dangerous skirmish.", dice);
                        character.NextTermBenefits.MusterOut = false;
                    }
                    else
                    {
                        character.AddHistory("Injured in a dangerous skirmish.", dice);
                    }
                }
                else
                {
                    character.AddHistory("Refused to fight in a dangerous skirmish.", dice);
                }
                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        IncreaseOneSkill(character, dice, "Gun Combat", "Drive", "Survival", "Melee|Natural", "Athletics", "Recon");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                character.Title = "Recruit";
                return;

            case 1:
                character.Title = "Soldier";
                if (allowBonus)
                    character.Skills.Add("Melee", "Natural", 1);
                return;

            case 2:
                character.Title = "Veteran Soldier";
                return;

            case 3:
                character.Title = "Warrior";
                if (allowBonus)
                    character.Endurance += 1;
                return;

            case 4:
                character.Title = "Veteran Warrior";
                return;

            case 5:
                character.Title = "Leader of Warriors";
                return;

            case 6:
                character.Title = "Honoured Warrior Leader";
                if (allowBonus)
                    character.AddHistory("Gain 3 clan shares.", character.Age);
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Navigation", "Electronics", "Melee", "Engineer", "Tactics|Military", "Admin");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 7, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Independence", "Strength", "Dexterity", "Endurance", "Melee|Natural", "Athletics");
    }
}
