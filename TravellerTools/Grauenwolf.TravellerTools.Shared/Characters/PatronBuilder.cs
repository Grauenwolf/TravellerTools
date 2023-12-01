namespace Grauenwolf.TravellerTools.Characters;

//TODO: Replace with one that will support a name generator
//TODO: Replace with one that will support a character generator

public static class PatronBuilder
{
    public static string PickAlliesAndEnemies(Dice dice)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        switch (dice.D66())
        {
            case 11: return "Naval Officer";
            case 12: return "Imperial Diplomat";
            case 13: return "Crooked Trader";
            case 14: return "Medical Doctor";
            case 15: return "Eccentric Scientist";
            case 16: return "Mercenary";
            case 21: return "Famous Performer";
            case 22: return "Alien Thief";
            case 23: return "Free Trader";
            case 24: return "Explorer";
            case 25: return "Marine Captain";
            case 26: return "Corporate Executive";
            case 31: return "Researcher";
            case 32: return "Cultural Attaché";
            case 33: return "Religious Leader";
            case 34: return "Conspirator";
            case 35: return "Rich Noble";
            case 36: return "Artificial Intelligence";
            case 41: return "Bored Noble";
            case 42: return "Planetary Governor";
            case 43: return "Inveterate Gambler";
            case 44: return "Crusading Journalist";
            case 45: return "Doomsday Cultist";
            case 46: return "Corporate Agent";
            case 51: return "Criminal Syndicate";
            case 52: return "Military Governor";
            case 53: return "Army Quartermaster";
            case 54: return "Private Investigator";
            case 55: return "Starport Administrator";
            case 56: return "Retired Admiral";
            case 61: return "Alien Ambassador";
            case 62: return "Smuggler";
            case 63: return "Weapons Inspector";
            case 64: return "Elder Statesman";
            case 65: return "Planetary Warlord";
            case 66: return "Imperial Agent";
        }
        return "";
    }

    public static string PickMission(Dice dice)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        switch (dice.D66())
        {
            case 11: return "Assassinate a " + PickTarget(dice);
            case 12: return "Frame a " + PickTarget(dice);
            case 13: return "Destroy a " + PickTarget(dice);
            case 14: return "Steal from a " + PickTarget(dice);
            case 15: return "Aid in a burglary";
            case 16: return "Stop a burglary";
            case 21: return "Retrieve data or an object from a secure facility";
            case 22: return "Discredit a " + PickTarget(dice);
            case 23: return "Find a lost cargo";
            case 24: return "Find a lost person";
            case 25: return "Deceive a " + PickTarget(dice);
            case 26: return "Sabotage a " + PickTarget(dice);
            case 31: return "Transport goods";
            case 32: return "Transport a person";
            case 33: return "Transport data";
            case 34: return "Transport goods secretly";
            case 35: return "Transport goods quickly";
            case 36: return "Transport dangerous goods";
            case 41: return "Investigate a crime";
            case 42: return "Investigate a theft";
            case 43: return "Investigate a murder";
            case 44: return "Investigate a mystery";
            case 45: return "Investigate a " + PickTarget(dice);
            case 46: return "Investigate an event";
            case 51: return "Join an expedition";
            case 52: return "Survey a planet";
            case 53: return "Explore a new system";
            case 54: return "Explore a ruin";
            case 55: return "Salvage a ship";
            case 56: return "Capture a creature";
            case 61: return "Hijack a ship";
            case 62: return "Entertain a noble";
            case 63: return "Protect a " + PickTarget(dice);
            case 64: return "Save a " + PickTarget(dice);
            case 65: return "Aid a " + PickTarget(dice);
            case 66: return "It is a trap – the Patron intends to betray the Traveller. Fake mission: " + PickMission(dice);
        }
        return "";
    }

    public static string PickPatron(Dice dice)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        switch (dice.D66())
        {
            case 11: return "Assassin";
            case 12: return "Smuggler";
            case 13: return "Terrorist";
            case 14: return "Embezzler";
            case 15: return "Thief";
            case 16: return "Revolutionary";
            case 21: return "Clerk";
            case 22: return "Administrator";
            case 23: return "Mayor";
            case 24: return "Minor Noble";
            case 25: return "Physician";
            case 26: return "Tribal Leader";
            case 31: return "Diplomat";
            case 32: return "Courier";
            case 33: return "Spy";
            case 34: return "Ambassador";
            case 35: return "Noble";
            case 36: return "Police Officer";
            case 41: return "Merchant";
            case 42: return "Free Trader";
            case 43: return "Broker";
            case 44: return "Corporate Executive";
            case 45: return "Corporate Agent";
            case 46: return "Financier";
            case 51: return "Belter";
            case 52: return "Researcher";
            case 53: return "Naval Officer";
            case 54: return "Pilot";
            case 55: return "Starport Administrator";
            case 56: return "Scout";
            case 61: return "Alien";
            case 62: return "Playboy";
            case 63: return "Stowaway";
            case 64: return "Family Relative";
            case 65: return "Agent of a Foreign Power";
            case 66: return "Imperial Agent";
        }
        return "";
    }

    public static string PickTarget(Dice dice)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        switch (dice.D66())
        {
            case 11: return "Common Trade Goods";
            case 12: return "Common Trade Goods";
            case 13: return "Random Trade Goods";
            case 14: return "Random Trade Goods";
            case 15: return "Illegal Trade Goods";
            case 16: return "Illegal Trade Goods";
            case 21: return "Computer Data";
            case 22: return "Alien Artifact";
            case 23: return "Personal Effects";
            case 24: return "Work of Art";
            case 25: return "Historical Artifact";
            case 26: return "Weapon";
            case 31: return "Starport";
            case 32: return "Asteroid Base";
            case 33: return "City";
            case 34: return "Research station";
            case 35: return "Bar or Nightclub";
            case 36: return "Medical Facility";
            case 41: case 42: case 43: return PickPatron(dice);
            case 44: case 45: case 46: return PickAlliesAndEnemies(dice);
            case 51: return "Local Government";
            case 52: return "Planetary Government";
            case 53: return "Corporation";
            case 54: return "Imperial Intelligence";
            case 55: return "Criminal Syndicate";
            case 56: return "Criminal Gang";
            case 61: return "Free Trader";
            case 62: return "Yacht";
            case 63: return "Cargo Hauler";
            case 64: return "Police Cutter";
            case 65: return "Space Station";
            case 66: return "Warship";
        }
        return "";
    }
}
