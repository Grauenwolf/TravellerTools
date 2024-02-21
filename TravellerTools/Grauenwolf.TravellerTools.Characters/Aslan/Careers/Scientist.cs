namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Scientist(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Scientist", assignment, speciesCharacterBuilder)
{
    public override CareerTypes CareerTypes => CareerTypes.Science | CareerTypes.Civilian;
    public override string? Source => "Aliens of Charted Space 1, page 32";
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Electronics", "Science", "Science", "Science", "Investigate", "Admin");
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

                if (dice.NextBoolean() && AddOneSkill(character, dice, "Carouse", "Survival", "Streetwise"))
                {
                    character.AddHistory($"Spend some time outside of the lab.", dice);
                }
                else
                {
                    character.AddHistory($"Spend some time outside of the lab. Gain a Contact.", dice);
                    character.AddContact();
                }
                return;

            case 4:
                character.AddHistory($"Work on weapons technology for the clan.", dice);
                AddOneSkill(character, dice, "Science", "Engineer", "Gunner", "Gun Combat");

                return;

            case 5:
                character.AddHistory($"Work closely with a scientist from another species. Gain a Contact.", dice);
                AddOneSkill(character, dice, "Tolerance");
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
                if (dice.NextBoolean() && AddOneSkill(character, dice, "Admin", "Art", "Science"))
                {
                    character.AddHistory($"Teach the young cubs of the clan.", dice);
                }
                else
                {
                    character.AddHistory($"Teach the young cubs of the clan. Gain a Contact.", dice);
                    character.AddContact();
                }
                return;

            case 9:
                switch (dice.D(3))
                {
                    case 1:
                        if (dice.RollHigh(character.Skills.BestSkillLevel("Science"), 10))
                        {
                            character.AddHistory($"Scientist from another clan tried to make a breakthrough in an area that {character.Name} was researching, but {character.Name} beat them to it. Gain a Rival.", dice); character.CurrentTermBenefits.AdvancementDM += 2;
                        }
                        else
                        {
                            character.AddHistory($"Scientist from another clan made to make a breakthrough in an area that {character.Name} was researching despite {character.Name} pouring funds into it. Gain a Rival.", dice);
                            character.BenefitRolls += -1;
                        }
                        character.AddRival();
                        break;

                    case 2:
                        if (dice.RollHigh(character.Skills.BestSkillLevel("Stealth", "Deception"), 8))
                        {
                            character.AddHistory($"Scientist from another clan tried to make a breakthrough in an area that {character.Name} was researching, so {character.Name} sabotaged their work. Gain an Enemy.", dice);
                            character.CurrentTermBenefits.AdvancementDM += 2;
                        }
                        else
                        {
                            character.AddHistory($"Scientist from another clan made a breakthrough in an area that {character.Name} was researching despite {character.Name} trying to sabotaged their work. Gain an Enemy.", dice);
                            character.SocialStanding -= 2;
                        }
                        character.AddEnemy();
                        break;

                    case 3:
                        character.AddHistory($"Scientist from another clan makes a breakthrough in an area that {character.Name} was researching.", dice);
                        break;
                }
                return;

            case 10:

                var itemType = dice.NextBoolean() ? "a rare alien artefact" : "an alien life form";

                if (dice.RollHigh(character.Skills.BestSkillLevel("Science"), 8))
                {
                    character.AddHistory($"Uncover secrets about {itemType} that you found.", dice);
                    character.CurrentTermBenefits.AdvancementDM += 2;
                }
                else
                {
                    character.AddHistory($"Failed to understand {itemType} that you found.", dice);
                    character.CurrentTermBenefits.AdvancementDM += -2;
                }
                return;

            case 11:
                character.AddHistory($"Study at one of the great universities or research facilities.", dice);
                if (dice.NextBoolean())
                    character.Skills.Increase("Investigate");
                else
                    character.CurrentTermBenefits.AdvancementDM += 4;
                return;

            case 12:
                character.AddHistory($"Make a scientific breakthrough.", dice);
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
                character.AddHistory($"Exposed to something dangerous in a lab accident.", age);
                character.Endurance += -1;
                return;

            case 3:
                character.AddHistory($"Work sabotaged by another researcher. Gain a Rival.", age);
                character.AddRival();
                return;

            case 4:
                character.AddHistory($"A lab ship misjumps, stranding you on an alien world.", age);
                AddOneSkill(character, dice, "Survival", "Astrogation", "Mechanic", "Science");
                return;

            case 5:
                if (dice.NextBoolean())
                {
                    if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Melee", "Natural"), 8))
                    {
                        character.AddHistory($"Clan elder challenged {character.Name}'s work as being flawed and was defeated in a duel.", dice);
                        character.NextTermBenefits.MusterOut = false;
                        character.SocialStanding += 1;
                    }
                    else
                    {
                        character.AddHistory($"Clan elder challenged {character.Name}'s work as being flawed, then defeated {character.Name} in a duel.", dice);
                        character.SocialStanding += -2;
                    }
                }
                else
                {
                    character.AddHistory($"Backed down when a clan elder challenged {character.Name}'s work as being flawed.", dice);
                }
                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        IncreaseOneSkill(character, dice, "Electronics", "Science", "Science", "Science", "Investigate", "Admin");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                return;

            case 1:
                character.Title = "Scholar";
                if (allowBonus)
                    AddOneSkill(character, dice, "Electronics|Computers");
                return;

            case 2:
                return;

            case 3:
                character.Title = "Respected Scholar";
                if (allowBonus)
                    AddOneSkill(character, dice, "Admin");
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                character.Title = "Revered Scholar";
                if (allowBonus)
                    character.SocialStanding += 1;
                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Admin", "Astrogation", "Engineer", "Electronics", "Advocate", "Medic");
    }

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.RiteOfPassageDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        if (character.Gender == "M")
            return dice.RollHigh(dm, 10, isPrecheck);
        else
            return dice.RollHigh(dm, 7, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        if (character.Gender == "M")
            Increase(character, dice, "Intellect", "Education", "SocialStanding", "Diplomat", "Tolerance", "Independence");
        else
            Increase(character, dice, "Intellect", "Education", "SocialStanding", "Diplomat", "Tolerance", "Tolerance");
    }
}
