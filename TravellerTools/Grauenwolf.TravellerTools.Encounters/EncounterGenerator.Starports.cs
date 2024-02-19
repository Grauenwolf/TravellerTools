using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Encounters;

partial class EncounterGenerator
{
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
                result.Add("Religious zealot.", "Preacher", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Religious));
                break;

            case 7:
                result.Add("Severe weather makes travel difficult.");
                break;

            case 8:
                result.Add("Starport chief is drunk and giving misleading flight instructions.", "Starport chief", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 9:
                {
                    result.Add("Seller", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.ShadyGoodsTrader));

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
                result.Add("Pickpocket targets a character", "Pickpocket", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal));
                break;

            case 11 or 12:
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
                    case 1 or 2:
                        result.Add("they just mugged someone."); break;
                    case 3 or 4:
                        result.Add("they just threatened a rich passenger."); break;
                    case 5:
                        result.Add("someone thinks he owes them money."); break;
                    case 6:
                        result.Add("they are leading the officers to a crime scene."); break;
                }

                result.Add("Runner", CharacterBuilder.CreateCharacter(dice, settings));
                for (var i = 0; i < dice.D(3); i++)
                    result.Add("Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                break;

            case 2:
                result.Add("Priate attack!");
                break;

            case 3:
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("Toxic cloud."); break;
                    case 3 or 4:
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
                    case 1 or 2:
                        result.Add("Broken limb."); break;
                    case 3 or 4:
                        result.Add("Unconscious."); break;
                    case 5:
                        result.Add("Heart attack."); break;
                    case 6:
                        result.Add("Bleeding out."); break;
                }
                result.Add("Starport Employee", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                result.Add("Victim", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 5:

                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("A free trader or similar ship has crashed. There is only one survivor, who is wounded.", "Survivor", CharacterBuilder.CreateCharacter(dice, settings)); break;
                    case 3 or 4:
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
                    case 1 or 2:
                        result.Add("an ancestral burial ground."); break;
                    case 3 or 4:
                        result.Add("environmental damage."); break;
                    case 5:
                        result.Add("extortion."); break;
                    case 6:
                        result.Add("personal fued."); break;
                }
                result.Add("Starport Representative", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                result.Add("Local Representative", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 7:
                result.Add("Landing pad is damaged.");
                break;

            case 8:
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("A child of a crew member of a recently berthed ship has gone missing."); break;
                    case 3 or 4:
                        result.Add("A person with amnesia is asking for help.");
                        result.Add("Amnesia Sufferer", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 5:
                        result.Add("Scientist didn't return from field work.");
                        result.Add("Missing Scientist", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.FieldScience));
                        result.Add("Scientist Reporting Loss", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Science));
                        result.Add("Starport Representative", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                        break;

                    case 6:
                        result.Add("Starport security office is missing. All space traffic is halted until the officer is found.");
                        result.Add("Missing Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        result.Add("Starport Representative", CharacterBuilder.CreateCharacter(dice, settings));
                        break;
                }
                break;

            case 9:
                result.Add("Someone is trying to sneak aboard ship.", "Stowaway", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 10:
                result.Add("Pickpocket targets a character", "Pickpocket", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal));
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
                    case 1 or 2:
                        result.Add("Attacked by pirates."); break;
                    case 3 or 4:
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
            case 11 or 12 or 13:
                result.Add("Local holiday. Non-vital services are closed down.");
                break;

            case 14 or 15 or 16:
                var type = dice.D(6) switch
                {
                    1 or 2 => "Living fabric",
                    3 or 4 => "Miracle maggots",
                    5 => "Aphrodisiac",
                    _ => "Meta-gel",
                };
                result.Add($"{type} are offered for sale.");
                result.Add("Merchant", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.ShadyGoodsTrader));
                break;

            case 21 or 22:
                var speciesA = CharacterBuilder.GetRandomSpecies(dice, settings);
                var speciesB = CharacterBuilder.GetRandomSpecies(dice, speciesA, 5); //5% chance of mixed marriage
                var characterA = CharacterBuilder.CreateCharacter(dice, speciesA, noChildren: true);
                var characterB = CharacterBuilder.CreateCharacter(dice, speciesB, noChildren: true);
                result.Add($"{characterA.Name} and {characterB.Name} are on their honeymoon, but their ship never arrived. They ask for transport with the remainder of their spending money.");
                result.Add("Newlywed", characterA);
                result.Add("Newlywed", characterB);
                break;

            case 23 or 24:
                result.Add("Of the the travellers is approached by a 'lady of the night'");
                result.Add("Professional", CharacterBuilder.CreateCharacter(dice, settings));
                break;

            case 25 or 26:
                result.Add("A reporter wants to interview the travellers.");
                result.Add("Journalist", CharacterBuilder.CreateCharacterWithCareer(dice, settings, "Journalist"));
                break;

            case 31 or 32 or 33:
                result.Add("Due to a bomb scare, security is ramped up and incoming ships are thoroughly searched.");
                break;

            case 34 or 35 or 36:
                result.Add("Due to industrial action, one or more services are unavailable. The stike typically lasts one day.");
                break;

            case 41 or 42 or 43:
                var dropoff = CharacterBuilder.CreateCharacterWithCareer(dice, settings, "IllegalCareers");
                var pickip = CharacterBuilder.CreateCharacterWithCareer(dice, settings, "IllegalCareers");

                result.Add($"Traveller is tricked into carrying drugs through security. They are either given the to character by {dropoff.Name} as a memento or hidded in their luggage. {pickip.Name} will retrieve it on the other side.");
                result.Add("Drop-off", dropoff);
                result.Add("Pick-up", pickip);
                break;

            case 44 or 45 or 46:
                result.Add("An SPA official wants to interview one of the travellers for an hour or so to see if they are happy with SPA operations.");
                result.Add("SPA Official", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                break;

            case 51 or 52 or 53:
                var service = dice.D(6) switch
                {
                    1 or 2 => "docking facilities",
                    3 or 4 => "warehousing",
                    5 => "refueling",
                    _ => "repairs",
                };

                result.Add($"Loading bay doors jammed. Increase wait times for {service}.");
                break;

            case 54 or 55 or 56:
                result.Add("A colony of scavengers are stealing food and shiny objects. They may be native or accidentally released.");
                break;

            case 61 or 62 or 63:
                result.Add($"Trader is offering {Goods(dice)}, but its a trick to lure the traveller to where they can be mugged.");
                result.Add("Trader", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal)); break;

            case 64 or 65 or 66:
                result.Add($"Trader is offering {Goods(dice)}.");
                result.Add("Trader", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.LegalGoodsTrader));
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
            case 11 or 12:
                result.Add("The starport has become the base of operations for dealing with a nearby environmental emergency.");
                break;

            case 13 or 14:
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("Travellers are asked by a desperate dealer to smuggle drugs off planet. The dealer is afraid for his life.");
                        result.Add("Dealer", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 3 or 4:
                        result.Add("Travellers are asked to help smuggle a relic off planet. The locals will not be happy to learn it was stolen.");
                        result.Add("Archaeologist", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 5:
                        result.Add("Travellers are asked to help smuggle a live animinal off planet."); //Roll for animal details
                        result.Add("Trapper", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 6:
                        result.Add("Travellers are asked to help smuggle a coma patient off planet. They are actualy drugged.");
                        result.Add("Kidnapper", CharacterBuilder.CreateCharacter(dice, settings));
                        result.Add("Victim", CharacterBuilder.CreateCharacter(dice, settings));
                        break;
                }
                break;

            case 15 or 16:
                switch (dice.D(6))
                {
                    case 1:
                        result.Add("The starport is hosting a major race around the nearest moon and back.");
                        break;

                    case 2:
                        result.Add("The starport is hosting a major race complete with mock weapons installed on the ships.");
                        break;

                    case 3:
                        result.Add("The starport is hosting a major race with atmospheric ships.");
                        break;

                    case 4:
                        result.Add("The starport is hosting a major race across multiple planets with a task on each.");
                        break;
                }
                break;

            case 21 or 22:
                var landType = dice.D(6) switch
                {
                    1 or 2 => "swamp filled with valuable aligator-like monster skins.", //roll for animal
                    3 or 4 => "volanic region filled with poison gas and valuable minerals.",
                    5 => "fertile grassland that allows animals to grow faster. Also lots of carnavores.",
                    6 => "well stocked section of ocean.",
                };
                var complication = dice.D(6) switch
                {
                    1 or 2 => "",
                    3 or 4 => "",
                    5 => "",
                    6 => "",
                };
                result.Add($"Travellers are asked to transport a person to a planet so they can obtain some land they inherited. The land is {landType}, but {complication}.");
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("Travellers are asked by a desperate dealer to smuggle drugs off planet. The dealer is afraid for his life.");
                        break;

                    case 3 or 4:
                        result.Add("Travellers are asked to help smuggle a relic off planet. The locals will not be happy to learn it was stolen.");
                        break;

                    case 5:
                        result.Add("Travellers are asked to help smuggle a live animinal off planet.");
                        break;

                    case 6:
                        result.Add("Travellers are asked to help smuggle a coma patient off planet. They are actualy drugged.");
                        break;
                }
                result.Add("Beneficiary", CharacterBuilder.CreateCharacter(dice, settings));
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("Travellers are asked by a desperate dealer to smuggle drugs off planet. The dealer is afraid for his life.");
                        result.Add("Dealer", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 3 or 4:
                        result.Add("Travellers are asked to help smuggle a relic off planet. The locals will not be happy to learn it was stolen.");
                        result.Add("Archaeologist", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 5:
                        result.Add("Travellers are asked to help smuggle a live animinal off planet."); //Roll for animal details
                        result.Add("Trapper", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 6:
                        result.Add("Travellers are asked to help smuggle a coma patient off planet. They are actualy drugged.");
                        result.Add("Kidnapper", CharacterBuilder.CreateCharacter(dice, settings));
                        result.Add("Victim", CharacterBuilder.CreateCharacter(dice, settings));
                        break;
                }
                break;

            case 23 or 24:
                result.Add("");
                break;

            case 25 or 26:
                result.Add("");
                break;

            case 31 or 32:
                result.Add("");
                break;

            case 33 or 34:
                result.Add("");
                break;

            case 35 or 36:
                result.Add("");
                break;

            case 41 or 42 or 43:
                result.Add("");
                break;

            case 44 or 45 or 46:
                result.Add("");
                break;

            case 51 or 52 or 53:
                result.Add("");
                break;

            case 54 or 55 or 56:
                result.Add("");
                break;

            case 61 or 62 or 63:
                result.Add("");
                break;

            case 64 or 65 or 66:
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
            case 11 or 12:
                result.Add("");
                break;

            case 13 or 14:
                result.Add("");
                break;

            case 15 or 16:
                result.Add("");
                break;

            case 21 or 22:
                result.Add("");
                break;

            case 23 or 24:
                result.Add("");
                break;

            case 25 or 26:
                result.Add("");
                break;

            case 31 or 32:
                result.Add("");
                break;

            case 33 or 34:
                result.Add("");
                break;

            case 35 or 36:
                result.Add("");
                break;

            case 41 or 42 or 43:
                result.Add("");
                break;

            case 44 or 45 or 46:
                result.Add("");
                break;

            case 51 or 52 or 53:
                result.Add("");
                break;

            case 54 or 55 or 56:
                result.Add("");
                break;

            case 61 or 62 or 63:
                result.Add("");
                break;

            case 64 or 65 or 66:
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
            case 11 or 12:
                result.Add("");
                break;

            case 13 or 14:
                result.Add("");
                break;

            case 15 or 16:
                result.Add("");
                break;

            case 21 or 22:
                result.Add("");
                break;

            case 23 or 24:
                result.Add("");
                break;

            case 25 or 26:
                result.Add("");
                break;

            case 31 or 32 or 33:
                result.Add("");
                break;

            case 34 or 35 or 36:
                result.Add("");
                break;

            case 41 or 42:
                result.Add("");
                break;

            case 43 or 44:
                result.Add("");
                break;

            case 45 or 46:
                result.Add("");
                break;

            case 51 or 52 or 53:
                result.Add("");
                break;

            case 54 or 55 or 56:
                result.Add("");
                break;

            case 61 or 62:
                result.Add("");
                break;

            case 63 or 64:
                result.Add("");
                break;

            case 65 or 66:
                result.Add("");
                break;
        }
        return result;
    }
}
