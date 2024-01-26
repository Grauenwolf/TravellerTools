using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters.Careers.Humaniti;

abstract class Agent : NormalCareer
{
    readonly ImmutableArray<NormalCareer> m_Careers;

    public Agent(string assignment, SpeciesCharacterBuilder speciesCharacterBuilder) : base("Agent", assignment, speciesCharacterBuilder)
    {
        var careers = new List<NormalCareer>
        {
            new Citizen_Corporate(speciesCharacterBuilder),
            new Citizen_Worker(speciesCharacterBuilder),
            new Citizen_Colonist(speciesCharacterBuilder),
            new Rogue_Thief(speciesCharacterBuilder),
            new Rogue_Enforcer(speciesCharacterBuilder),
            new Rogue_Pirate(speciesCharacterBuilder)
        };
        m_Careers = careers.ToImmutableArray();
    }

    protected override int AdvancedEductionMin => 8;

    internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
    {
        AddBasicSkills(character, dice, all, "Streetwise", "Drive", "Investigate", "Flyer", "Recon", "Gun Combat");
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
                    var age = character.AddHistory($"An investigation takes on a dangerous turn.", dice);

                    if (dice.RollHigh(character.Skills.BestSkillLevel("Investigate", "Streetwise"), 8))
                        IncreaseOneSkill(character, dice, "Deception", "Jack-of-All-Trades", "Persuade", "Tactics");
                    else
                        Mishap(character, dice, age);
                    return;
                }
            case 4:
                character.AddHistory($"Rewarded for a successful mission.", dice);
                character.BenefitRollDMs.Add(1);
                return;

            case 5:
                var count = dice.D(3);
                character.AddHistory($"Established a network of contacts. Gain {count} contacts.", dice);
                character.AddContact(count);
                return;

            case 6:
                character.AddHistory($"Advanced training in a specialist field.", dice);
                if (dice.RollHigh(character.EducationDM, 8))
                {
                    dice.Choose(character.Skills).Level += 1;
                }
                return;

            case 7:
                LifeEvent(character, dice);
                return;

            case 8:
                {
                    var age = character.AddHistory($"Go undercover to investigate an enemy.", dice);
                    character.AddEnemy();

                    var career = dice.Choose(m_Careers);

                    if (dice.RollHigh(character.Skills.GetLevel("Deception"), 8))
                    {
                        career.Event(character, dice);
                        career.AssignmentSkills(character, dice);
                    }
                    else
                    {
                        career.Mishap(character, dice, age);
                    }
                }
                return;

            case 9:
                character.AddHistory($"{character.Name} went above and beyond the call of duty.", dice);
                character.CurrentTermBenefits.AdvancementDM += 2;
                return;

            case 10:
                character.AddHistory($"Given specialist training in vehicles.", dice);
                AddOneSkill(character, dice, "Drive", "Flyer", "Pilot", "Gunner");
                return;

            case 11:
                character.AddHistory($"Befriended by a senior agent.", dice);
                switch (dice.D(2))
                {
                    case 1:
                        character.Skills.Increase("Investigate");
                        return;

                    case 2:
                        character.CurrentTermBenefits.AdvancementDM += 4;
                        return;
                }
                return;

            case 12:
                character.AddHistory($"Uncover a major conspiracy against {character.Name}'s employers.", dice);
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
                character.AddHistory($"Life ruined by a criminal gang. Gain the gang as an Enemy", age);
                character.AddEnemy();
                return;

            case 3:
                character.AddHistory($"Hard times caused by a lack of interstellar trade costs {character.Name}'s job.", age);
                character.SocialStanding += -1;
                return;

            case 4:
                if (dice.NextBoolean())
                {
                    character.AddHistory($"Accepted a deal with criminal or other figure under investigation.", age);
                }
                else
                {
                    character.AddHistory($"Refused a deal with criminal or other figure under investigation. Gain an Enemy", age);
                    IncreaseOneRandomSkill(character, dice);
                    character.AddEnemy();
                }
                return;

            case 5:
                character.AddHistory($"{character.Name}'s work ends up coming home with {character.Name}, and someone gets hurt. Contact or ally takes the worst of 2 injury rolls.", age);
                return;

            case 6:
                Injury(character, dice, false, age);
                return;
        }
    }

    internal override bool Qualify(Character character, Dice dice, bool isPrecheck)
    {
        var dm = character.IntellectDM;
        dm += -1 * character.CareerHistory.Count;

        dm += character.GetEnlistmentBonus(Career, Assignment);
        dm += QualifyDM;

        return dice.RollHigh(dm, 6, isPrecheck);
    }

    internal override void ServiceSkill(Character character, Dice dice)
    {
        Increase(character, dice, "Streetwise", "Drive", "Investigate", "Flyer", "Recon", "Gun Combat");
    }

    protected override void AdvancedEducation(Character character, Dice dice)
    {
        Increase(character, dice, "Advocate", "Language", "Explosives", "Medic", "Vacc Suit", "Electronics");
    }

    protected override void PersonalDevelopment(Character character, Dice dice)
    {
        Increase(character, dice, "Gun Combat", "Dexterity", "Endurance", "Melee", "Intellect", "Athletics");
    }
}
