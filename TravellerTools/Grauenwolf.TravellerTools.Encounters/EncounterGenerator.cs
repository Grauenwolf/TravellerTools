using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.TradeCalculator;

namespace Grauenwolf.TravellerTools.Encounters;

public interface IEncounterGeneratorSettings : ISpeciesSettings
{
    World? World { get; } //not currently used.
}

public class EncounterGenerator(CharacterBuilder characterBuilder, TradeEngine tradeEngine)
{
    readonly string[] AllScienceCareers = ["Explorer", "Surveyor", "Scholar", "Scientist"];
    readonly string[] FieldScienceCareers = ["Explorer", "Surveyor", "Field Researcher"];
    readonly string[] IllegalCareers = ["Rogue", "Outlaw"];
    readonly string[] LegalGoodsCareers = ["Trader", "Merchant"];
    readonly string[] OfficerCareers = ["Law Enforcement", "Marine", "Guardian", "Warrior", "Commando"];
    readonly string[] ReligiousCareers = ["Priest", "Believer", "Truther", "Shaper Priest"];
    readonly string[] ShadyGoodsCareers = ["Rogue", "Trader", "Merchant"];
    readonly string[] StarportCareers = ["Corporate", "Worker", "Law Enforcement", "Administrator", "Clan Agent", "Management"];

    public CharacterBuilder CharacterBuilder { get; } = characterBuilder;

    public TradeEngine TradeEngine { get; } = tradeEngine;

    public static Encounter PickAlliesAndEnemies(Dice dice, IEncounterGeneratorSettings settings, Encounter? encounter = null)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        encounter ??= new Encounter() { Title = "Allies and Enemies" };

        //TODO: Add name generator

        switch (dice.D66())
        {
            case 11: encounter.Add($"Naval Officer"); break;
            case 12: encounter.Add($"Imperial Diplomat"); break;
            case 13: encounter.Add($"Crooked Trader"); break;
            case 14: encounter.Add($"Medical Doctor"); break;
            case 15: encounter.Add($"Eccentric Scientist"); break;
            case 16: encounter.Add($"Mercenary"); break;
            case 21: encounter.Add($"Famous Performer"); break;
            case 22: encounter.Add($"Alien Thief"); break;
            case 23: encounter.Add($"Free Trader"); break;
            case 24: encounter.Add($"Explorer"); break;
            case 25: encounter.Add($"Marine Captain"); break;
            case 26: encounter.Add($"Corporate Executive"); break;
            case 31: encounter.Add($"Researcher"); break;
            case 32: encounter.Add($"Cultural Attaché"); break;
            case 33: encounter.Add($"Religious Leader"); break;
            case 34: encounter.Add($"Conspirator"); break;
            case 35: encounter.Add($"Rich Noble"); break;
            case 36: encounter.Add($"Artificial Intelligence"); break;
            case 41: encounter.Add($"Bored Noble"); break;
            case 42: encounter.Add($"Planetary Governor"); break;
            case 43: encounter.Add($"Inveterate Gambler"); break;
            case 44: encounter.Add($"Crusading Journalist"); break;
            case 45: encounter.Add($"Doomsday Cultist"); break;
            case 46: encounter.Add($"Corporate Agent"); break;
            case 51: encounter.Add($"Criminal Syndicate"); break;
            case 52: encounter.Add($"Military Governor"); break;
            case 53: encounter.Add($"Army Quartermaster"); break;
            case 54: encounter.Add($"Private Investigator"); break;
            case 55: encounter.Add($"Starport Administrator"); break;
            case 56: encounter.Add($"Retired Admiral"); break;
            case 61: encounter.Add($"Alien Ambassador"); break;
            case 62: encounter.Add($"Smuggler"); break;
            case 63: encounter.Add($"Weapons Inspector"); break;
            case 64: encounter.Add($"Elder Statesman"); break;
            case 65: encounter.Add($"Planetary Warlord"); break;
            case 66: encounter.Add($"Imperial Agent"); break;
        }
        return encounter;
    }

    public static Encounter PickPatron(Dice dice, IEncounterGeneratorSettings settings, Encounter? encounter = null)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        encounter ??= new Encounter() { Title = "Patron" };

        //TODO: Add name generator
        //TODO: Add actual characters
        switch (dice.D66())
        {
            case 11: encounter.Add("Assassin"); break;
            case 12: encounter.Add("Smuggler"); break;
            case 13: encounter.Add("Terrorist"); break;
            case 14: encounter.Add("Embezzler"); break;
            case 15: encounter.Add("Thief"); break;
            case 16: encounter.Add("Revolutionary"); break;
            case 21: encounter.Add("Clerk"); break;
            case 22: encounter.Add("Administrator"); break;
            case 23: encounter.Add("Mayor"); break;
            case 24: encounter.Add("Minor Noble"); break;
            case 25: encounter.Add("Physician"); break;
            case 26: encounter.Add("Tribal Leader"); break;
            case 31: encounter.Add("Diplomat"); break;
            case 32: encounter.Add("Courier"); break;
            case 33: encounter.Add("Spy"); break;
            case 34: encounter.Add("Ambassador"); break;
            case 35: encounter.Add("Noble"); break;
            case 36: encounter.Add("Police Officer"); break;
            case 41: encounter.Add("Merchant"); break;
            case 42: encounter.Add("Free Trader"); break;
            case 43: encounter.Add("Broker"); break;
            case 44: encounter.Add("Corporate Executive"); break;
            case 45: encounter.Add("Corporate Agent"); break;
            case 46: encounter.Add("Financier"); break;
            case 51: encounter.Add("Belter"); break;
            case 52: encounter.Add("Researcher"); break;
            case 53: encounter.Add("Naval Officer"); break;
            case 54: encounter.Add("Pilot"); break;
            case 55: encounter.Add("Starport Administrator"); break;
            case 56: encounter.Add("Scout"); break;
            case 61: encounter.Add("Alien"); break;
            case 62: encounter.Add("Playboy"); break;
            case 63: encounter.Add("Stowaway"); break;
            case 64: encounter.Add("Family Relative"); break;
            case 65: encounter.Add("Agent of a Foreign Power"); break;
            case 66: encounter.Add("Imperial Agent"); break;
        }

        return encounter;
    }

    public Encounter BackwaterStarportGeneralEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Backwater Starport General Encounter";

        switch (dice.D(12))
        {
            case 1:
                result.Add("Hitchhiker wants to leave the planet and is willing to work for passage.");
                result.Add("Hitchhiker", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 2:
                result.Add("Another ship is desperate for spare parts.");
                for (var i = 0; i < dice.D(3); i++)
                    result.Add("Crewmember", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 3:
                result.Add("Meet a starport employee who is having a bad day.");
                result.Add("Employee", CharacterBuilder.CreateCharacter(dice, settings));

                switch (dice.D(6))
                {
                    case 1: result.Add("Lost lucky charm somewhere in spaceport"); break;
                    case 2: result.Add("Dumped by boy/girl friend.", "Employee", CharacterBuilder.CreateCharacter(dice, settings)); break;
                    case 3: result.Add("Passed over for promotion."); break;
                    case 4: result.Add("Forced to work overtime."); break;
                    case 5: result.Add("Lost heavily on a bet."); break;
                    case 6: result.Add("Pet died this morning."); break;
                }

                break;

            case 4:
                result.Add("A pulp fuction writer is willing to buy drinks in exchange for a story.");
                result.Add("Author", CharacterBuilder.CreateCharacterWithSkill(dice, settings, "Art", "Write"));
                break;

            case 5:
                result.Add("Alien street performers put on a show.");
                {
                    var species = CharacterBuilder.GetRandomSpecies(dice);
                    for (var i = 0; i < dice.D(3); i++)
                        result.Add("Performer", CharacterBuilder.CreateCharacterWithSkill(dice, "Art", "Performing", null, species));
                }
                break;

            case 6:
                result.Add("Religious zealot.", "Preacher", CharacterBuilder.CreateCharacterWithCareer(dice, settings, ReligiousCareers));
                break;

            case 7:
                result.Add("Severe weather makes travel difficult.");
                break;

            case 8:
                result.Add("Starport chief is drunk and giving misleading flight instructions.", "Starport chief", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 9:
                {
                    result.Add("Seller", CharacterBuilder.CreateCharacterWithCareer(dice, settings, ShadyGoodsCareers));

                    var price = (dice.D(5) + 3) * 10;
                    var source = dice.D(5) switch
                    {
                        <= 2 => "Stolen.",
                        <= 4 => "Spoiled or defective.",
                        _ => "Legitmate surplus.",
                    };

                    switch (dice.D(6))
                    {
                        case 1: result.Add($"Offer to buy weapons at {price}% normal price. {source}"); break;
                        case 2: result.Add($"Offer to buy armor at {price}% normal price. {source}"); break;
                        case 3: result.Add($"Offer to buy ships fuel at {price}% normal price. {source}"); break;
                        case 4: result.Add($"Offer to buy medical supplies at {price}% normal price. {source}"); break;
                        case 5: result.Add($"Offer to buy ships stores at {price}% normal price. {source}"); break;
                        case 6: result.Add($"Offer to buy experimental tech. {source}"); break;
                    }
                }
                break;

            case 10:
                result.Add("Pickpocket targets a character", "Pickpocket", CharacterBuilder.CreateCharacterWithCareer(dice, settings, IllegalCareers));
                break;

            case 11:
            case 12:
                result.Add($"One of the starport services is unavilable. It will take {dice.D(4, 6)} hours to fix unless help is found.");
                break;
        }

        return result;
    }

    public Encounter BackwaterStarportSignificantEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Backwater Starport Significant Encounter";

        switch (dice.D(12))
        {
            case 1:
                result.Add("Someone is seen running, followed by security officers. He was running because ");
                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("they just mugged someone."); break;
                    case 3:
                    case 4:
                        result.Add("they just threatened a rich passenger."); break;
                    case 5:
                        result.Add("someone thinks he owes them money."); break;
                    case 6:
                        result.Add("they are leading the officers to a crime scene."); break;
                }

                result.Add("Runner", CharacterBuilder.CreateCharacter(dice, settings));
                for (var i = 0; i < dice.D(3); i++)
                    result.Add("Officer", CharacterBuilder.CreateCharacterWithCareer(dice, settings, OfficerCareers));
                break;

            case 2:
                result.Add("Priate attack!");
                break;

            case 3:
                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("Toxic cloud."); break;
                    case 3:
                    case 4:
                        result.Add("Chemical fire."); break;
                    case 5:
                        result.Add("Biological hazard."); break;
                    case 6:
                        result.Add("Explosion."); break;
                }

                break;

            case 4:
                result.Add("A starport employee is looking for help for a victim suffering ");
                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("Broken limb."); break;
                    case 3:
                    case 4:
                        result.Add("Unconscious."); break;
                    case 5:
                        result.Add("Heart attack."); break;
                    case 6:
                        result.Add("Bleeding out."); break;
                }
                result.Add("Starport Employee", CharacterBuilder.CreateCharacterWithCareer(dice, settings, StarportCareers));
                result.Add("Victim", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 5:

                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("A free trader or similar ship has crashed. There is only one survivor, who is wounded.", "Survivor", CharacterBuilder.CreateCharacter(dice, settings)); break;
                    case 3:
                    case 4:
                        result.Add("Abandoned ship. No sign of crew and logs were erased."); break;
                    case 5:
                        result.Add("Plague ship. Communications are down so crew cannot warn the outside."); break;
                    case 6:
                        result.Add("Priate ship was shot down. Crew will attempt to sieze the ship of any rescuers."); break;
                }
                break;

            case 6:

                result.Add("Locals have become hostile against starport due to a dispute over ");
                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("an ancestral burial ground."); break;
                    case 3:
                    case 4:
                        result.Add("environmental damage."); break;
                    case 5:
                        result.Add("extortion."); break;
                    case 6:
                        result.Add("personal fued."); break;
                }
                result.Add("Starport Representative", CharacterBuilder.CreateCharacterWithCareer(dice, settings, StarportCareers));
                result.Add("Local Representative", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 7:
                result.Add("Landing pad is damaged.");
                break;

            case 8:
                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("A child of a crew member of a recently berthed ship has gone missing."); break;
                    case 3:
                    case 4:
                        result.Add("A person with amnesia is asking for help.");
                        result.Add("Amnesia Sufferer", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 5:
                        result.Add("Scientist didn't return from field work.");
                        result.Add("Missing Scientist", CharacterBuilder.CreateCharacterWithCareer(dice, settings, FieldScienceCareers));
                        result.Add("Scientist Reporting Loss", CharacterBuilder.CreateCharacterWithCareer(dice, settings, AllScienceCareers));
                        result.Add("Starport Representative", CharacterBuilder.CreateCharacterWithCareer(dice, settings, StarportCareers));
                        break;

                    case 6:
                        result.Add("Starport security office is missing. All space traffic is halted until the officer is found.");
                        result.Add("Missing Officer", CharacterBuilder.CreateCharacterWithCareer(dice, settings, OfficerCareers));
                        result.Add("Starport Representative", CharacterBuilder.CreateCharacter(dice, settings));
                        break;
                }
                break;

            case 9:
                result.Add("Someone is trying to sneak aboard ship.", "Stowaway", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 10:
                result.Add("Pickpocket targets a character", "Pickpocket", CharacterBuilder.CreateCharacterWithCareer(dice, settings, IllegalCareers));
                break;

            case 11:

                switch (dice.D(6))
                {
                    case 1:
                        //TODO : Add the random animal creator here
                        result.Add("Flying fauna is swarming around the starport, blocking space traffic."); break;
                    case 2:
                        //TODO : Add the random animal creator here
                        result.Add("Mating season is causing fauna to act aggresively."); break;
                    case 3:
                        result.Add("Burrowing creatures are causing the building to subside."); break;
                    case 4:
                        result.Add("Offworld hunters are looking to recuit help.");
                        for (var i = 0; i < dice.D(3); i++)
                            result.Add("Hunter", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 5:
                        result.Add("Rapid growing plant is causing problems such as entangling power generators."); break;
                    case 6:
                        result.Add("Lost alien child was found. Local food must be found for it."); break;
                }
                break;

            case 12:
                result.Add("X-Boat (or other ship) is in distress.");
                switch (dice.D(6))
                {
                    case 1:
                    case 2:
                        result.Add("Attacked by pirates."); break;
                    case 3:
                    case 4:
                        result.Add("Collision with micrometeorites."); break;
                    case 5:
                        result.Add("Systems failure."); break;
                    case 6:
                        result.Add("Bluff. It is actually a trap."); break;
                }

                break;
        }

        return result;
    }

    public Encounter BustlingStarportGeneralEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Bustling Starport General Encounter";
        switch (dice.D66())
        {
            case 11:
            case 12:
            case 13:
                result.Add("Local holiday. Non-vital services are closed down.");
                break;

            case 14:
            case 15:
            case 16:
                var type = dice.D(6) switch
                {
                    1 or 2 => "Living fabric",
                    3 or 4 => "Miracle maggots",
                    5 => "Aphrodisiac",
                    _ => "Meta-gel",
                };
                result.Add($"{type} are offered for sale.");
                result.Add("Merchant", CharacterBuilder.CreateCharacterWithCareer(dice, settings, ShadyGoodsCareers));
                break;

            case 21:
            case 22:
                var speciesA = CharacterBuilder.GetRandomSpecies(dice, settings);
                var speciesB = CharacterBuilder.GetRandomSpecies(dice, speciesA, 5); //5% chance of mixed marriage
                var characterA = CharacterBuilder.CreateCharacter(dice, speciesA, noChildren: true);
                var characterB = CharacterBuilder.CreateCharacter(dice, speciesB, noChildren: true);
                result.Add($"{characterA} and {characterB} are on their honeymoon, but their ship never arrived. They ask for transport with the remainder of their spending money.");
                result.Add("Newlywed", characterA);
                result.Add("Newlywed", characterB);
                break;

            case 23:
            case 24:
                result.Add("Of the the travellers is approached by a 'lady of the night'");
                result.Add("Professional", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 25:
            case 26:
                result.Add("A reporter wants to interview the travellers.");
                result.Add("Journalist", CharacterBuilder.CreateCharacterWithCareer(dice, settings, "Journalist"));
                break;

            case 31:
            case 32:
            case 33:
                result.Add("Due to a bomb scare, security is ramped up and incoming ships are thoroughly searched.");
                break;

            case 34:
            case 35:
            case 36:
                result.Add("Due to industrial action, one or more services are unavailable. The stike typically lasts one day.");
                break;

            case 41:
            case 42:
            case 43:
                var dropoff = CharacterBuilder.CreateCharacterWithCareer(dice, settings, "IllegalCareers");
                var pickip = CharacterBuilder.CreateCharacterWithCareer(dice, settings, "IllegalCareers");

                result.Add($"Traveller is tricked into carrying drugs through security. They are either given the to character by {dropoff.Name} as a memento or hidded in their luggage. {pickip.Name} will retrieve it on the other side.");
                result.Add("Drop-off", dropoff);
                result.Add("Pick-up", pickip);
                break;

            case 44:
            case 45:
            case 46:
                result.Add("An SPA official wants to interview one of the travellers for an hour or so to see if they are happy with SPA operations.");
                result.Add("SPA Official", CharacterBuilder.CreateCharacterWithCareer(dice, settings, StarportCareers));
                break;

            case 51:
            case 52:
            case 53:
                var service = dice.D(6) switch
                {
                    1 or 2 => "docking facilities",
                    3 or 4 => "warehousing",
                    5 => "refueling",
                    _ => "repairs",
                };

                result.Add($"Loading bay doors jammed. Increase wait times for {service}.");
                break;

            case 54:
            case 55:
            case 56:
                result.Add("A colony of scavengers are stealing food and shiny objects. They may be native or accidentally released.");
                break;

            case 61:
            case 62:
            case 63:
                result.Add($"Trader is offering {Goods(dice)}, but its a trick to lure the traveller to where they can be mugged.");
                result.Add("Trader", CharacterBuilder.CreateCharacterWithCareer(dice, settings, IllegalCareers)); break;

            case 64:
            case 65:
            case 66:
                result.Add($"Trader is offering {Goods(dice)}.");
                result.Add("Trader", CharacterBuilder.CreateCharacterWithCareer(dice, settings, LegalGoodsCareers));
                break;
        }
        return result;

        string Goods(Dice dice)
        {
            return dice.D(6) switch
            {
                1 => CommonTradeGood(dice).Name,
                2 => UncommonTradeGood(dice).Name,
                3 => IllegalTradeGood(dice).Name,
                4 => "land at Cr1000 per acre",
                5 => dice.D(6) switch
                {
                    1 or 2 => "air/raft",
                    3 or 4 => "ground car",
                    5 => "ATV",
                    _ => "AFV",
                } + " " + dice.D(6) switch
                {
                    1 => "with a damaged engine (-10% speed)",
                    2 => "with battle damage (-1 armour)",
                    3 => "that is sealed",
                    4 => "with an autopilot",
                    5 => "with a modified engine",
                    _ => "that is heavily armored (+5 armour)",
                } + " " + dice.D(6) switch
                {
                    1 => "for a 25% discount",
                    2 => "for a 20% discount",
                    3 => "for a 15% premium",
                    4 => "",
                    5 => "for a 40% premium",
                    _ => "for a 20% premium",
                }
                ,
                _ => "advanced weapon modifications"
            };
        }
    }

    public Encounter BustlingStarportSignificantEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Bustling Starport Significant Encounter";
        switch (dice.D66())
        {
            case 11:
            case 12:
                result.Add("");
                break;

            case 13:
            case 14:
                result.Add("");
                break;

            case 15:
            case 16:
                result.Add("");
                break;

            case 21:
            case 22:
                result.Add("");
                break;

            case 23:
            case 24:
                result.Add("");
                break;

            case 25:
            case 26:
                result.Add("");
                break;

            case 31:
            case 32:
                result.Add("");
                break;

            case 33:
            case 34:
                result.Add("");
                break;

            case 35:
            case 36:
                result.Add("");
                break;

            case 41:
            case 42:
            case 43:
                result.Add("");
                break;

            case 44:
            case 45:
            case 46:
                result.Add("");
                break;

            case 51:
            case 52:
            case 53:
                result.Add("");
                break;

            case 54:
            case 55:
            case 56:
                result.Add("");
                break;

            case 61:
            case 62:
            case 63:
                result.Add("");
                break;

            case 64:
            case 65:
            case 66:
                result.Add("");
                break;
        }
        return result;
    }

    public Encounter MetropolisStarportGeneralEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Metropolis Starport General Encounter";
        switch (dice.D66())
        {
            case 11:
            case 12:
                result.Add("");
                break;

            case 13:
            case 14:
                result.Add("");
                break;

            case 15:
            case 16:
                result.Add("");
                break;

            case 21:
            case 22:
                result.Add("");
                break;

            case 23:
            case 24:
                result.Add("");
                break;

            case 25:
            case 26:
                result.Add("");
                break;

            case 31:
            case 32:
                result.Add("");
                break;

            case 33:

            case 34:
                result.Add("");
                break;

            case 35:
            case 36:
                result.Add("");
                break;

            case 41:
            case 42:
            case 43:
                result.Add("");
                break;

            case 44:
            case 45:
            case 46:
                result.Add("");
                break;

            case 51:
            case 52:
            case 53:
                result.Add("");
                break;

            case 54:
            case 55:
            case 56:
                result.Add("");
                break;

            case 61:
            case 62:
            case 63:
                result.Add("");
                break;

            case 64:
            case 65:
            case 66:
                result.Add("");
                break;
        }
        return result;
    }

    public Encounter MetropolisStarportSignificantEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Metropolis Starport Significant Encounter";
        switch (dice.D66())
        {
            case 11:
            case 12:
                result.Add("");
                break;

            case 13:
            case 14:
                result.Add("");
                break;

            case 15:
            case 16:
                result.Add("");
                break;

            case 21:
            case 22:
                result.Add("");
                break;

            case 23:
            case 24:
                result.Add("");
                break;

            case 25:
            case 26:
                result.Add("");
                break;

            case 31:
            case 32:
            case 33:
                result.Add("");
                break;

            case 34:
            case 35:
            case 36:
                result.Add("");
                break;

            case 41:
            case 42:
                result.Add("");
                break;

            case 43:
            case 44:
                result.Add("");
                break;

            case 45:
            case 46:
                result.Add("");
                break;

            case 51:
            case 52:
            case 53:
                result.Add("");
                break;

            case 54:
            case 55:
            case 56:
                result.Add("");
                break;

            case 61:
            case 62:
                result.Add("");
                break;

            case 63:
            case 64:
                result.Add("");
                break;

            case 65:
            case 66:
                result.Add("");
                break;
        }
        return result;
    }

    public Encounter PickMission(Dice dice, IEncounterGeneratorSettings settings, Encounter? encounter = null)
    {
        if (dice == null)
            throw new ArgumentNullException(nameof(dice), $"{nameof(dice)} is null.");

        encounter ??= new Encounter() { Title = "Mission" };

        switch (dice.D66())
        {
            case 11: encounter.Add($"Assassinate a ", PickTarget(dice, settings)); break;
            case 12: encounter.Add($"Frame a ", PickTarget(dice, settings)); break;
            case 13: encounter.Add($"Destroy a ", PickTarget(dice, settings)); break;
            case 14: encounter.Add($"Steal from a ", PickTarget(dice, settings)); break;
            case 15: encounter.Add($"Aid in a burglary"); break;
            case 16: encounter.Add($"Stop a burglary"); break;
            case 21: encounter.Add($"Retrieve data or an object from a secure facility"); break;
            case 22: encounter.Add($"Discredit a ", PickTarget(dice, settings)); break;
            case 23: encounter.Add($"Find a lost cargo"); break;
            case 24: encounter.Add($"Find a lost person"); break;
            case 25: encounter.Add($"Deceive a ", PickTarget(dice, settings)); break;
            case 26: encounter.Add($"Sabotage a ", PickTarget(dice, settings)); break;
            case 31: encounter.Add($"Transport {LegalTradeGood(dice).Name}."); break;
            case 32: encounter.Add($"Transport a person"); break;
            case 33: encounter.Add($"Transport data"); break;
            case 34: encounter.Add($"Transport {TradeGood(dice).Name} secretly"); break;
            case 35: encounter.Add($"Transport {LegalTradeGood(dice).Name} quickly"); break;
            case 36: encounter.Add($"Transport dangerous goods"); break;
            case 41: encounter.Add($"Investigate a crime"); break;
            case 42: encounter.Add($"Investigate a theft"); break;
            case 43: encounter.Add($"Investigate a murder"); break;
            case 44: encounter.Add($"Investigate a mystery"); break;
            case 45: encounter.Add($"Investigate a ", PickTarget(dice, settings)); break;
            case 46: encounter.Add($"Investigate an event"); break;
            case 51: encounter.Add($"Join an expedition"); break;
            case 52: encounter.Add($"Survey a planet"); break;
            case 53: encounter.Add($"Explore a new system"); break;
            case 54: encounter.Add($"Explore a ruin"); break;
            case 55: encounter.Add($"Salvage a ship"); break;
            case 56: encounter.Add($"Capture a creature"); break;
            case 61: encounter.Add($"Hijack a ship"); break;
            case 62: encounter.Add($"Entertain a noble"); break;
            case 63: encounter.Add($"Protect a ", PickTarget(dice, settings)); break;
            case 64: encounter.Add($"Save a ", PickTarget(dice, settings)); break;
            case 65: encounter.Add($"Aid a ", PickTarget(dice, settings)); break;
            case 66: encounter.Add($"It is a trap – the Patron intends to betray the Traveller. Fake mission: ", PickMission(dice, settings)); break;
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

    TradeGoodDetail CommonTradeGood(Dice dice) => dice.Choose(TradeEngine.CommonTradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail IllegalTradeGood(Dice dice) => dice.Choose(TradeEngine.IllegalTradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail LegalTradeGood(Dice dice) => dice.Choose(TradeEngine.LegalTradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail TradeGood(Dice dice) => dice.Choose(TradeEngine.TradeGoods).ChooseRandomDetail(dice);

    TradeGoodDetail UncommonTradeGood(Dice dice) => dice.Choose(TradeEngine.UncommonTradeGoods).ChooseRandomDetail(dice);
}
