namespace Grauenwolf.TravellerTools.Characters.Careers.Aslan;

abstract class Outlaw(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : NormalCareer("Outlaw", assignment, speciesCharacterBuilder)
{
    public override string? Source => "Aliens of Charted Space 1, page 40";
    internal override bool RankCarryover => true;
    protected override int AdvancedEductionMin => int.MaxValue;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Streetwise", "Gun Combat ", "Melee", "Tactics", "Persuade", "Stealth");
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
                character.AddHistory($"Barely survive on the fringes of Aslan space.", dice);
                character.Endurance += -1;
                AddOneRandomSkill(character, dice);
                return;

            case 4:

                character.AddHistory($"Crimes pays off.", dice);
                character.BenefitRolls += 1;
                return;

            case 5:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Clan puts a price on {character.Name}'s head. Gain an Enemy.", dice);
                    character.AddEnemy();
                }
                else
                {
                    if (dice.RollHigh(character.Skills.EffectiveSkillLevel("Deception"), 8))
                    {
                        character.AddHistory($"Clan puts a price on {character.Name}'s head. {character.Name} claims the price through trickery.", dice);
                        character.BenefitRolls += 3;
                    }
                    else
                    {
                        character.AddHistory($"Clan puts a price on {character.Name}'s head. {character.Name} tries the price through trickery and is caught.", dice);
                        character.Endurance += -2;
                        character.NextTermBenefits.MusterOut = true;
                    }
                }
                return;

            case 6:
                character.AddHistory("Acquire a contact in the criminal sphere. Gain a Contact.", dice);
                character.AddContact();
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                character.AddHistory("Pick up some useful skills.", dice);
                IncreaseOneSkill(character, dice, "Electronics", "Independence", "Stealth", "Gun Combat");
                return;

            case 9:
                if (dice.RollHigh(character.Skills.BestSkillLevel("Pilot", "Stealth", "Gun Combat"), 8))
                {
                    character.AddHistory($"Succeed at an audacious raid on a rival.", dice);
                    if (dice.NextBoolean())
                        character.BenefitRolls += 1;
                    else
                        character.SocialStanding += 1;
                }
                else
                {
                    character.AddHistory($"Attempt an audacious raid on a rival and are injured.", dice);
                    character.SocialStanding += -1;
                }
                return;

            case 10:

                if (dice.NextBoolean())
                {
                    if (dice.RollHigh(character.Skills.BestSkillLevel("Stealth"), 8))
                    {
                        character.AddHistory($"Employed by a clan to perform some deed that they want accomplished covertly, but fail.", dice);
                        character.BenefitRolls += 1;
                    }
                    else
                        character.AddHistory($"Employed employment by a clan to perform some deed that they want accomplished covertly, but fail.", dice);
                }
                else
                {
                    character.AddHistory($"Offered employment by a clan to perform some deed that they want accomplished covertly. Inform the clan’s enemies. Gain an enemy.", dice);
                    character.BenefitRolls += 1;
                    character.AddEnemy();
                }

                return;

            case 11:
                if (character.Gender == "M")
                {
                    character.AddHistory("Reclaim your standing in society.", dice);
                    character.Territory += 1;
                    character.SocialStanding = Math.Max(character.SocialStanding, Math.Max(dice.D(2, 6), character.Territory ?? 0));
                    character.IsOutcast = false;
                    character.NextTermBenefits.MusterOut = true;
                }
                else
                {
                    if (!character.IsMarried)
                        if (dice.NextBoolean())
                        {
                            character.AddHistory("Marry a male of good family. Gain Husband.", dice);
                            character.AddHusband();
                            character.SocialStanding = Math.Max(character.SocialStanding, Math.Max(dice.D(2, 6), character.Territory ?? 0));
                            character.IsOutcast = false;
                            character.NextTermBenefits.MusterOut = true;
                        }
                        else
                        {
                            character.AddHistory("Refused to marry a male of good family.", dice);
                            character.AddHusband();
                        }
                    else
                    {
                        character.AddHistory("Turned down an inappropriate offer to marry.", dice);
                    }
                }

                return;

            case 12:
                character.AddHistory($"Deeds are the stuff of legends and nightmares.", dice);
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
                character.AddHistory($"Captured and punished by the clan you stole from. Gain an enemy.", age);
                character.Endurance += -2;
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"Rival outlaw band attacks.", age);
                Injury(character, dice, age);
                character.BenefitRolls += -1;
                return;

            case 4:
                character.AddHistory($"Forced to flee off-planet.", age);
                AddOneSkill(character, dice, "Deception", "Pilot", "Independence", "Streetwise");
                return;

            case 5:

                if (character.RemoveContact(ContactType.Contact))
                {
                    character.AddHistory($"Betrayed by a friend. Contact becomes a Rival.", age);
                }
                else if (character.RemoveContact(ContactType.Ally))
                {
                    character.AddHistory($"Betrayed by a friend. Ally becomes a Rival.", age);
                }
                else
                {
                    character.AddHistory($"Betrayed by a friend. Gain a Rival.", age);
                }
                character.AddRival();

                return;

            case 6:
                Injury(character, dice, age);
                return;
        }
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        IncreaseOneSkill(character, dice, "Streetwise", "Gun Combat ", "Melee|Natural", "Tactics|Military", "Persuade", "Stealth");
    }

    internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice, bool allowBonus)
    {
        switch (careerHistory.Rank)
        {
            case 0:
                character.Title = "Outlaw";
                return;

            case 1:
                if (allowBonus)
                    AddOneSkill(character, dice, "Melee|Natural");

                return;

            case 2:

                return;

            case 3:
                character.Title = "Feared Outlaw";
                if (allowBonus)
                    AddOneSkill(character, dice, "Independence", "Streetwise");
                return;

            case 4:
                return;

            case 5:
                return;

            case 6:
                character.Title = "Outlaw Chief";
                if (allowBonus)
                    AddOneSkill(character, dice, "Leadership");

                return;
        }
    }

    protected override void AdvancedEducation(Character character, Dice dice) => throw new NotImplementedException();

    protected override bool OnQualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.StrengthDM;
        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6, isPrecheck);
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        if (character.Gender == "M")
            Increase(character, dice, "Independence", "Intellect", "Education", "Gambler", "Endurance", "Independence");
        else
            Increase(character, dice, "Melee", "Intellect", "Education", "Gambler", "Endurance", "Melee");
    }
}
