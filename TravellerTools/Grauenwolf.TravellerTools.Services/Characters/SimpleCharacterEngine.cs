namespace Grauenwolf.TravellerTools.Characters;

public static class SimpleCharacterEngine
{
    public static void AddCharacteristics(Person person, Dice random)
    {
        Func<int> Bump = () => ((random.NextDouble() <= 0.054) ? 1 : 0);

        person.Strength = random.D(2, 6);
        person.Dexterity = random.D(2, 6);
        person.Endurance = random.D(2, 6);
        person.Intellect = random.D(2, 6);
        person.Education = random.D(2, 6);
        person.Social = random.D(2, 6);

        var termsServed = (int)Math.Floor((person.ApparentAge - 18) / 4.0);
        for (var i = 0; i < termsServed; i++)
        {
            person.Strength += Bump();
            person.Dexterity += Bump();
            person.Endurance += Bump();
            person.Intellect += Bump();
            person.Education += Bump();
            person.Social += Bump();

            if (i >= 4)
                ApplyAgePenalty(person, random, i);
        }
    }

    static public void AddTrait(Person person, Dice dice)
    {
        int roll1 = dice.D66();

        switch (roll1)
        {
            case 11: person.Trait = "Loyal"; return;
            case 12: person.Trait = "Distracted by other worries"; return;
            case 13: person.Trait = "In debt to criminals"; return;
            case 14: person.Trait = "Makes very bad jokes"; return;
            case 15: person.Trait = "Will betray characters"; return;
            case 16: person.Trait = "Aggressive"; return;
            case 21: person.Trait = "Has secret allies"; return;
            case 22: person.Trait = "Secret anagathic user"; return;
            case 23: person.Trait = "Looking for something"; return;
            case 24: person.Trait = "Helpful"; return;
            case 25: person.Trait = "Forgetful"; return;
            case 26:
                person.IsPatron = true;
                person.Trait = "Wants to hire the characters";
                person.PatronMission = PatronBuilder.PickMission(dice);
                return;

            case 31: person.Trait = "Has useful contacts"; return;
            case 32: person.Trait = "Artistic"; return;
            case 33: person.Trait = "Easily confused"; return;
            case 34: person.Trait = "Unusually ugly"; return;
            case 35: person.Trait = "Worried about current situation"; return;
            case 36: person.Trait = "Shows pictures of children"; return;
            case 41: person.Trait = "Rumor-monger"; return;
            case 42: person.Trait = "Unusually provincial"; return;
            case 43: person.Trait = "Drunkard or drug addict"; return;
            case 44: person.Trait = "Government informant"; return;
            case 45: person.Trait = "Mistakes a PC for someone else"; return;
            case 46: person.Trait = "Possess unusually advanced technology"; return;
            case 51: person.Trait = "Unusually handsome or beautiful"; return;
            case 52: person.Trait = "Spying on the characters"; return;
            case 53: person.Trait = "Possesses a TAS membership"; return;
            case 54: person.Trait = "Is secretly hostile to characters"; return;
            case 55: person.Trait = "Wants to borrow money"; return;
            case 56: person.Trait = "Is convinced the PCs are dangerous"; return;
            case 61: person.Trait = "Involved in political intrigue"; return;
            case 62: person.Trait = "Has a dangerous secret"; return;
            case 63: person.Trait = "Wants to get off-planet as soon as possible"; return;
            case 64: person.Trait = "Attracted to a player character"; return;
            case 65: person.Trait = "From offworld"; return;
            case 66: person.Trait = "Possesses telepathy or other usual ability"; return;
        }
    }

    public static void ApplyAgePenalty(Person person, Dice random, int termsComplete)
    {
        var roll = random.D(2, 6) - termsComplete;
        if (roll == -6)
        {
            person.Strength -= 2;
            person.Dexterity -= 2;
            person.Endurance -= 2;
            AlterRandomMental(person, random, -1);
        }
        else if (roll == -5)
        {
            person.Strength -= 2;
            person.Dexterity -= 2;
            person.Endurance -= 2;
        }
        else if (roll == -4)
        {
            person.Strength -= 2;
            person.Dexterity -= 2;
            person.Endurance -= 2;
            AlterRandomPhysical(person, random, 1);//undo one reduction
        }
        else if (roll == -3)
        {
            person.Strength -= 1;
            person.Dexterity -= 1;
            person.Endurance -= 1;
            AlterRandomPhysical(person, random, -1);
        }
        else if (roll == -2)
        {
            person.Strength -= 1;
            person.Dexterity -= 1;
            person.Endurance -= 1;
        }
        else if (roll == -1)
        {
            person.Strength -= 1;
            person.Dexterity -= 1;
            person.Endurance -= 1;
            AlterRandomPhysical(person, random, 1); //undo one reduction
        }
        else if (roll == 0)
        {
            AlterRandomPhysical(person, random, -1);
        }

        //Upper limits
        if (person.Strength.Value > 15) { person.Strength = 15; }
        if (person.Dexterity.Value > 15) { person.Dexterity = 15; }
        if (person.Endurance.Value > 15) { person.Endurance = 15; }
        if (person.Intellect.Value > 15) { person.Intellect = 15; }
        if (person.Education.Value > 15) { person.Education = 15; }
        if (person.Social.Value > 15) { person.Social = 15; }
    }

    static void AlterRandomMental(Person person, Dice random, int amount)
    {
        switch (random.D(3))
        {
            case 1: person.Intellect += amount; return;
            case 2: person.Education += amount; return;
            case 3: person.Social += amount; return;
        }
    }

    static void AlterRandomPhysical(Person person, Dice random, int amount)
    {
        switch (random.D(3))
        {
            case 1: person.Strength += amount; return;
            case 2: person.Dexterity += amount; return;
            case 3: person.Endurance += amount; return;
        }
    }
}
