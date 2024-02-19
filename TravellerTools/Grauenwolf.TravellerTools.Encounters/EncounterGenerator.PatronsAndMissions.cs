using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Encounters;

partial class EncounterGenerator
{
    public Encounter PickAlliesAndEnemies(Dice dice, IEncounterGeneratorSettings settings, Encounter? encounter = null)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        encounter ??= new Encounter() { Title = "Allies and Enemies" };

        //TODO: Add name generator

        switch (dice.D66())
        {
            case 11: encounter.Add($"Naval Officer"); break;
            case 12: encounter.Add($"Imperial Diplomat", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Diplomat)); break;
            case 13: encounter.Add($"Crooked Trader", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.ShadyGoodsTrader)); break;
            case 14: encounter.Add($"Medical Doctor", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Healer)); break;
            case 15: encounter.Add($"Eccentric Scientist", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Science)); break;
            case 16: encounter.Add($"Mercenary"); break;
            case 21: encounter.Add($"Famous Performer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.ArtistOrPerformer)); break;
            case 22: encounter.Add($"Alien Thief", CharacterBuilder.CreateCharacter(dice, CareerTypes.Illegal)); break;
            case 23: encounter.Add($"Free Trader"); break;
            case 24: encounter.Add($"Explorer"); break;
            case 25: encounter.Add($"Marine Captain"); break;
            case 26: encounter.Add($"Corporate Executive"); break;
            case 31: encounter.Add($"Researcher", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Science)); break;
            case 32: encounter.Add($"Cultural Attaché", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Diplomat)); break;
            case 33: encounter.Add($"Religious Leader", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Religious)); break;
            case 34: encounter.Add($"Conspirator"); break;
            case 35: encounter.Add($"Rich Noble"); break;
            case 36: encounter.Add($"Artificial Intelligence"); break;
            case 41: encounter.Add($"Bored Noble"); break;
            case 42: encounter.Add($"Planetary Governor"); break;
            case 43: encounter.Add($"Inveterate Gambler"); break;
            case 44: encounter.Add($"Crusading Journalist"); break;
            case 45: encounter.Add($"Doomsday Cultist", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Religious)); break;
            case 46: encounter.Add($"Corporate Agent"); break;
            case 51: encounter.Add($"Criminal Syndicate", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal)); break;
            case 52: encounter.Add($"Military Governor"); break;
            case 53: encounter.Add($"Army Quartermaster"); break;
            case 54: encounter.Add($"Private Investigator"); break;
            case 55: encounter.Add($"Starport Administrator", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee)); break;
            case 56: encounter.Add($"Retired Admiral"); break;
            case 61: encounter.Add($"Alien Ambassador"); break;
            case 62: encounter.Add($"Smuggler", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.ShadyGoodsTrader)); break;
            case 63: encounter.Add($"Weapons Inspector", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee)); break;
            case 64: encounter.Add($"Elder Statesman"); break;
            case 65: encounter.Add($"Planetary Warlord"); break;
            case 66: encounter.Add($"Imperial Agent"); break;
        }
        return encounter;
    }

    public Encounter PickMission(Dice dice, IEncounterGeneratorSettings settings, Encounter? encounter = null)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        encounter ??= new Encounter() { Title = "Mission" };

        switch (dice.D66())
        {
            case 11: encounter.Merge($"Assassinate a ", PickTarget(dice, settings)); break;
            case 12: encounter.Merge($"Frame a ", PickTarget(dice, settings)); break;
            case 13: encounter.Merge($"Destroy a ", PickTarget(dice, settings)); break;
            case 14: encounter.Merge($"Steal from a ", PickTarget(dice, settings)); break;
            case 15: encounter.Add($"Aid in a burglary"); break;
            case 16: encounter.Add($"Stop a burglary"); break;
            case 21: encounter.Add($"Retrieve data or an object from a secure facility"); break;
            case 22: encounter.Merge($"Discredit a ", PickTarget(dice, settings)); break;
            case 23: encounter.Add($"Find a lost cargo of {TradeGood(dice).Name}"); break;
            case 24: encounter.Add($"Find a lost person", "Person", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 25: encounter.Merge($"Deceive a ", PickTarget(dice, settings)); break;
            case 26: encounter.Merge($"Sabotage a ", PickTarget(dice, settings)); break;
            case 31: encounter.Add($"Transport {LegalTradeGood(dice).Name}."); break;
            case 32: encounter.Add($"Transport a person", "Person", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 33: encounter.Add($"Transport data"); break;
            case 34: encounter.Add($"Transport {TradeGood(dice).Name} secretly"); break;
            case 35: encounter.Add($"Transport {LegalTradeGood(dice).Name} quickly"); break;
            case 36: encounter.Add($"Transport dangerous goods"); break;
            case 41: encounter.Add($"Investigate a crime"); break;
            case 42: encounter.Add($"Investigate a theft"); break;
            case 43: encounter.Add($"Investigate a murder", "Victim", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 44: encounter.Add($"Investigate a mystery"); break;
            case 45: encounter.Merge($"Investigate a ", PickTarget(dice, settings)); break;
            case 46: encounter.Add($"Investigate an event"); break;
            case 51: encounter.Add($"Join an expedition"); break;
            case 52: encounter.Add($"Survey a planet"); break;
            case 53: encounter.Add($"Explore a new system"); break;
            case 54: encounter.Add($"Explore a ruin"); break;
            case 55: encounter.Add($"Salvage a ship"); break;
            case 56: encounter.Add($"Capture a creature"); break;
            case 61: encounter.Add($"Hijack a ship"); break;
            case 62: encounter.Add($"Entertain a noble", "Noble", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Noble)); break;
            case 63: encounter.Merge($"Protect a ", PickTarget(dice, settings)); break;
            case 64: encounter.Merge($"Save a ", PickTarget(dice, settings)); break;
            case 65: encounter.Merge($"Aid a ", PickTarget(dice, settings)); break;
            case 66: encounter.Add($"It is a trap – the Patron intends to betray the Traveller. Fake mission: ", PickMission(dice, settings)); break;
        }
        return encounter;
    }

    public Encounter PickPatron(Dice dice, IEncounterGeneratorSettings settings, Encounter? encounter = null)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        encounter ??= new Encounter() { Title = "Patron" };

        //TODO: Add name generator
        //TODO: Add actual characters
        switch (dice.D66())
        {
            case 11: encounter.Add("Assassin", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 12: encounter.Add("Smuggler"); break;
            case 13: encounter.Add("Terrorist", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 14: encounter.Add("Embezzler", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 15: encounter.Add("Thief", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal)); break;
            case 16: encounter.Add("Revolutionary"); break;
            case 21: encounter.Add("Clerk"); break;
            case 22: encounter.Add("Administrator"); break;
            case 23: encounter.Add("Mayor"); break;
            case 24: encounter.Add("Minor Noble", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Noble)); break;
            case 25: encounter.Add("Physician", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Healer)); break;
            case 26: encounter.Add("Tribal Leader"); break;
            case 31: encounter.Add("Diplomat", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Diplomat)); break;
            case 32: encounter.Add("Courier"); break;
            case 33: encounter.Add("Spy", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Spy)); break;
            case 34: encounter.Add("Ambassador", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Diplomat)); break;
            case 35: encounter.Add("Noble", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Noble)); break;
            case 36: encounter.Add("Police Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Police)); break;
            case 41: encounter.Add("Merchant", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.CorporateMerchant)); break;
            case 42: encounter.Add("Free Trader", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.FreeTrader)); break;
            case 43: encounter.Add("Broker", CharacterBuilder.CreateCharacterWithSkill(dice, settings, "Broker")); break;
            case 44: encounter.Add("Corporate Executive", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Corporate)); break;
            case 45: encounter.Add("Corporate Agent", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Corporate)); break;
            case 46: encounter.Add("Financier", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Corporate)); break;
            case 51: encounter.Add("Belter", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Belter)); break;
            case 52: encounter.Add("Researcher", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.FieldScience)); break;
            case 53: encounter.Add("Naval Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.MilitaryNavy)); break;
            case 54: encounter.Add("Pilot", CharacterBuilder.CreateCharacterWithSkill(dice, settings, "Pilot")); break;
            case 55: encounter.Add("Starport Administrator", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee)); break;
            case 56: encounter.Add("Scout", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Scout)); break;
            case 61: encounter.Add("Alien", CharacterBuilder.CreateCharacter(dice)); break;
            case 62: encounter.Add("Playboy", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 63: encounter.Add("Stowaway", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 64: encounter.Add("Family Relative", CharacterBuilder.CreateCharacter(dice, settings)); break;
            case 65: encounter.Add("Agent of a Foreign Power", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Diplomat)); break;
            case 66: encounter.Add("Imperial Agent", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Government)); break;
        }

        return encounter;
    }

    public Encounter PickTarget(Dice dice, IEncounterGeneratorSettings settings)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        var result = new Encounter() { Title = "Target" };

        switch (dice.D66())
        {
            case 11 or 12: result.Add(CommonTradeGood(dice).Name); break;
            case 13 or 14: result.Add(UncommonTradeGood(dice).Name); break;
            case 15 or 16: result.Add(IllegalTradeGood(dice).Name); break;
            case 21: result.Add("Computer Data"); break;
            case 22: result.Add("Alien Artifact"); break;
            case 23: result.Add("Personal Effects"); break;
            case 24: result.Add("Work of Art"); break;
            case 25: result.Add("Historical Artifact"); break;
            case 26: result.Add("Weapon"); break;
            case 31: result.Add("Starport"); break;
            case 32: result.Add("Asteroid Base"); break;
            case 33: result.Add("City"); break;
            case 34: result.Add("Research station"); break;
            case 35: result.Add("Bar or Nightclub"); break;
            case 36: result.Add("Medical Facility"); break;
            case 41 or 42 or 43: result.Add(PickPatron(dice, settings)); break;
            case 44 or 45 or 46: result.Add(PickAlliesAndEnemies(dice, settings)); break;
            case 51: result.Add("Local Government"); break;
            case 52: result.Add("Planetary Government"); break;
            case 53: result.Add("Corporation"); break;
            case 54: result.Add("Imperial Intelligence"); break;
            case 55: result.Add("Criminal Syndicate"); break;
            case 56: result.Add("Criminal Gang"); break;
            case 61: result.Add("Free Trader"); break;
            case 62: result.Add("Yacht"); break;
            case 63: result.Add("Cargo Hauler"); break;
            case 64: result.Add("Police Cutter"); break;
            case 65: result.Add("Space Station"); break;
            case 66: result.Add("Warship"); break;
        }
        return result;
    }
}
