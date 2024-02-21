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
                    result.Add("Crewmember", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                break;

            case 3:
                result.Add("Meet a starport employee who is having a bad day.");
                result.Add("Employee", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));

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
                result.Add("Starport chief is drunk and giving misleading flight instructions.", "Starport chief", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
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
                result.Add($"One of the starport services is unavailable. It will take {dice.D(4, 6)} hours to fix unless help is found.");
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
                        result.Add("Pirate ship was shot down. Crew will attempt to seize the ship of any rescuers."); break;
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
                        result.Add("personal feud."); break;
                }
                result.Add("Starport Representative", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                result.Add("Local Representative", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
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
                        result.Add("Starport Representative", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
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
                        result.Add("Mating season is causing fauna to act aggressively."); break;
                    case 3:
                        result.Add("Burrowing creatures are causing the building to subside."); break;
                    case 4:
                        result.Add("Off world hunters are looking to recruit help.");
                        for (var i = 0; i < dice.D(3); i++)
                            result.Add("Hunter", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
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
        switch (dice.D(13))
        {
            case 1:
                result.Add("Local holiday. Non-vital services are closed down.");
                break;

            case 2:
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

            case 3:
                var speciesA = CharacterBuilder.GetRandomSpecies(dice, settings);
                var speciesB = CharacterBuilder.GetRandomSpecies(dice, speciesA, 5); //5% chance of mixed marriage
                var characterA = CharacterBuilder.CreateCharacter(dice, speciesA, ageClass: AgeClass.Adult);
                var characterB = CharacterBuilder.CreateCharacter(dice, speciesB, ageClass: AgeClass.Adult);
                result.Add($"{characterA.Name} and {characterB.Name} are on their honeymoon, but their ship never arrived. They ask for transport with the remainder of their spending money.");
                result.Add("Newlywed", characterA);
                result.Add("Newlywed", characterB);
                break;

            case 4:
                result.Add("Of the the travellers is approached by a 'lady of the night'");
                result.Add("Professional", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                break;

            case 5:
                result.Add("A reporter wants to interview the travellers.");
                result.Add("Journalist", CharacterBuilder.CreateCharacter(dice, settings, "Journalist"));
                break;

            case 6:
                result.Add("Due to a bomb scare, security is ramped up and incoming ships are thoroughly searched.");
                break;

            case 7:
                result.Add("Due to industrial action, one or more services are unavailable. The strike typically lasts one day.");
                break;

            case 8:
                var dropoff = CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal);
                var pickip = CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal);

                result.Add($"Traveller is tricked into carrying drugs through security. They are either given the to character by {dropoff.Name} as a memento or hidden in their luggage. {pickip.Name} will retrieve it on the other side.");
                result.Add("Drop-off", dropoff);
                result.Add("Pick-up", pickip);
                break;

            case 9:
                result.Add("An SPA official wants to interview one of the travellers for an hour or so to see if they are happy with SPA operations.");
                result.Add("SPA Official", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                break;

            case 10:
                var service = dice.D(6) switch
                {
                    1 or 2 => "docking facilities",
                    3 or 4 => "warehousing",
                    5 => "refueling",
                    _ => "repairs",
                };

                result.Add($"Loading bay doors jammed. Increase wait times for {service}.");
                break;

            case 11:
                result.Add("A colony of scavengers are stealing food and shiny objects. They may be native or accidentally released.");
                break;

            case 12:
                result.Add($"Trader is offering {Goods(dice)}, but it’s a trick to lure the traveller to where they can be mugged.");
                result.Add("Trader", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal)); break;

            case 13:
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
        switch (dice.D(15))
        {
            case 1:
                result.Add("The starport has become the base of operations for dealing with a nearby environmental emergency.");
                break;

            case 2:
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("Travellers are asked by a desperate dealer to smuggle drugs off planet. The dealer is afraid for his life.");
                        result.Add("Dealer", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 3 or 4:
                        result.Add("Travellers are asked to help smuggle a relic off planet. The locals will not be happy to learn it was stolen.");
                        result.Add("Archaeologist", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 5:
                        result.Add("Travellers are asked to help smuggle a live animal off planet."); //Roll for animal details
                        result.Add("Trapper", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 6:
                        result.Add("Travellers are asked to help smuggle a coma patient off planet. They are actually drugged.");
                        result.Add("Kidnapper", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        result.Add("Victim", CharacterBuilder.CreateCharacter(dice, settings));
                        break;
                }
                break;

            case 3:
                switch (dice.D(4))
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

            case 4:
                var landType = dice.D(6) switch
                {
                    1 or 2 => "swamp filled with valuable alligator-like monster skins.", //roll for animal
                    3 or 4 => "volcanic region filled with poison gas and valuable minerals.",
                    5 => "fertile grassland that allows animals to grow faster. Also lots of carnivores.",
                    _ => "well stocked section of ocean.",
                };
                var complication = dice.D(6) switch
                {
                    1 or 2 => "a pirate fleet attacks intruders to protect the location of their base",
                    3 or 4 => "an exploding moon has made the area treacherous",
                    5 => "an xenophobic alien race has been attacking trespassers",
                    _ => "gravitational anomalies disrupt navigation",
                };
                result.Add($"Travellers are asked to transport a person to a planet so they can obtain some land they inherited. The land is {landType}, but {complication}.");
                result.Add("Beneficiary", CharacterBuilder.CreateCharacter(dice, settings));

                break;

            case 5:
                switch (dice.D(6))
                {
                    case 1:
                        result.Add($"Targeted for a sing operation. They are offered stolen goods. {LegalTradeGood(dice)}");
                        result.Add("Fake Dealer", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        result.Add("Lead Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        break;

                    case 2:
                        result.Add("Travellers' ship looks similar to a pirate ship. Travellers are brought in for questioning by over-zealous investigator.");
                        result.Add("Investigator", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        break;

                    case 3:
                        result.Add("Witness a kidnapping of an undercover officer.");
                        result.Add("Kidnapped Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        var count = dice.D(3);
                        for (int i = 0; i < count; i++)
                            result.Add("Gangster", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal));
                        break;

                    case 4:
                        result.Add("Accidentally pick up a receiver for a 'bug' that allows them overhear a corrupt customs officer.");
                        result.Add("Corrupt Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        result.Add("Collaborator", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 5:
                        result.Add("Learn about the location of some smuggled gemstones.");
                        result.Add("Lead Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        break;

                    case 6:
                        result.Add("Bounty hunter askes crew to sneak prisoner off-planet.");
                        result.Add("Bounty Hunter", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        result.Add("Prisoner", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal));
                        break;
                }
                break;

            case 6:
                result.Add("Outbreak of Violence");
                switch (dice.D(6))
                {
                    case 1:
                        result.Add("Crew from ship on adjectent pad are involved in a brawl.");
                        result.Add("Crewmember", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        result.Add("Crewmember", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        var count = dice.D(5) - 2;
                        for (int i = 0; i < count; i++)
                            result.Add("Other", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 2:
                        result.Add("Victim of a pick-pocket that will need to be tracked down.");
                        result.Add("Thief", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal));
                        result.Add("Security", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        break;

                    case 3:
                        result.Add("Funds are counterfit. Traveller will be arrested unless explaination about their source is provided.");
                        result.Add("Seller", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        result.Add("Security", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        break;

                    case 4:
                        var cause = dice.D(2) switch
                        {
                            1 => "terrorists",
                            _ => "a mechanical failure",
                        };
                        result.Add($"Trapped in the hangar by an explosion. This was caused by {cause}.");
                        break;

                    case 5:
                        result.Add("Illegal narcotics are stashed in the traveller's luggage. If caught, the smuggler will react with violence.");
                        result.Add("Smuggler", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.Illegal));
                        result.Add("Officer", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportOfficer));
                        break;

                    case 6:
                        result.Add("A person is suffering from 'space madness' and tries to take a traveller hostage unless they \"turn on the lights\" outside the station.");
                        result.Add("Person", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;
                }
                break;

            case 7:
                result.Add("A massive meteorite or hail storm forces everyone indoors. A large predator uses this opportunity to hunt stranded people.");
                break;

            case 8:
                result.Add("Alien tech");
                switch (dice.D(6))
                {
                    case 1 or 2:
                        result.Add("Request by seller for transport to a cache of artifacts and assistance fighting claim jumpers.");
                        result.Add("Archeologist", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 3 or 4:
                        result.Add("Offered location of a derelict alien ship.");
                        result.Add("Seller", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;

                    case 5:
                        result.Add("Offered alien tech... that is embedded is partner's body. And it is rewriting his DNA.");
                        result.Add("Seller", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        result.Add("Partner", CharacterBuilder.CreateCharacter(dice, settings));
                        break;

                    case 6:
                        result.Add("Offered a homing beacon leading to an ancient alien city, or alien ship, or secret vault or...");
                        result.Add("Hard-up Seller", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                        break;
                }
                break;

            case 9:
                result.Add("Approached by bounty-hunter looking for someone the travellers recently met.");
                result.Add("Bounty Hunter", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                result.Add("Target", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                break;

            case 10:
                result.Add("Infestation");
                switch (dice.D(6))
                {
                    case 1: result.Add("A hardy type of cockroach is nesting in the station and threatens to infect the ship."); break;
                    case 2: result.Add("Rats have found their way into the ship's cargo bay."); break;
                    case 3: result.Add("A character is infected with a stomach parasite. Must be removed within a week or it will find its own way out."); break;
                    case 4: result.Add("Outbreak of lice-like portbugs."); break;
                    case 5: result.Add("Spores of an exotic type of a plant is germating on ship and will damage wiring."); break;
                    case 6: result.Add("Star spiders infect the station. Adult bites are lethal and even young can induce psionic fear."); break;
                }
                break;

            case 11:
                var reason = dice.D(2) switch
                {
                    1 => "a love-sick fool who wants to die",
                    _ => "a experienced dullist who likes the challenge",
                };
                result.Add($"Challenged to a duel by an offended visitor. The real is reason is that they are {reason}.");
                result.Add("Visitor", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Adult));
                break;

            case 12:
                result.Add("Asked to bring ship close to the sun to observe a solar flare.");
                break;

            case 13:
                result.Add("A robot has been programmed by a child to collect items for their go-cart, including parts of their ship or its cargo.");
                result.Add("Child", CharacterBuilder.CreateCharacter(dice, settings, AgeClass.Child));
                break;

            case 14:
                result.Add("A group of space nomands in patched-up ships arrive, raising tensions among the locals.");
                break;

            case 15:
                result.Add("A starport employee is found lurking outside a traveller's room or warehouse. They want to retrieve a stolen item they hid there before the room was rented.");
                result.Add("Employee", CharacterBuilder.CreateCharacter(dice, settings, CareerTypes.StarportEmployee));
                break;
        }
        return result;
    }

    public Encounter MetropolisStarportGeneralEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Metropolis Starport General Encounter";
        switch (dice.D(15))
        {
            case 1:
                result.Add("Trade fair");
                switch (dice.D(6))
                {
                    case 1: result.Add(""); break;
                    case 2: result.Add(""); break;
                    case 3: result.Add(""); break;
                    case 4: result.Add(""); break;
                    case 5: result.Add(""); break;
                    case 6: result.Add(""); break;
                }
                break;

            case 2:
                result.Add("Wildlife Photo");
                break;

            case 3:
                result.Add("Explorer");
                break;

            case 4:
                result.Add("Lost child");
                break;

            case 5:
                result.Add("More than you bargain");
                switch (dice.D(6))
                {
                    case 1: result.Add(""); break;
                    case 2: result.Add(""); break;
                    case 3: result.Add(""); break;
                    case 4: result.Add(""); break;
                    case 5: result.Add(""); break;
                    case 6: result.Add(""); break;
                }
                break;

            case 6:
                result.Add("Startown market");
                break;

            case 7:
                result.Add("CHeap fuel");
                break;

            case 8:
                result.Add("Cultural faux");
                break;

            case 9:
                result.Add("Droid for sale");
                switch (dice.D(6))
                {
                    case 1: result.Add(""); break;
                    case 2: result.Add(""); break;
                    case 3: result.Add(""); break;
                    case 4: result.Add(""); break;
                    case 5: result.Add(""); break;
                    case 6: result.Add(""); break;
                }
                break;

            case 10:
                result.Add("Traffic jam");
                break;

            case 11:
                result.Add("Drunken cadet");
                break;

            case 12:
                result.Add("Alien zoo");
                break;

            case 13:
                result.Add("Guided tours");
                break;

            case 14:
                result.Add("Recruitment drive");
                break;

            case 15:
                result.Add("ship's mascot");
                break;
        }
        return result;
    }

    public Encounter MetropolisStarportSignificantEncounter(Dice dice, IEncounterGeneratorSettings settings)
    {
        var result = new Encounter();
        result.Title = "Metropolis Starport Significant Encounter";
        switch (dice.D(16))
        {
            case 1:
                result.Add("MIstekn delivery");
                switch (dice.D(6))
                {
                    case 1 or 2: result.Add(""); break;
                    case 3 or 4 or 5 or 6: result.Add(""); break;
                }
                break;

            case 2:
                result.Add("ROgue robot");
                switch (dice.D(6))
                {
                    case 1 or 2: result.Add(""); break;
                    case 3 or 4: result.Add(""); break;
                    case 5: result.Add(""); break;
                    case 6: result.Add(""); break;
                }
                break;

            case 3:
                result.Add("banquet fit for a king");
                break;

            case 4:
                result.Add("Storm hunters");
                break;

            case 5:
                result.Add("Mercs");
                break;

            case 6:
                result.Add("Bail");
                break;

            case 7:
                result.Add("Take me to your breeder");
                break;

            case 8:
                result.Add("Starown riots");
                break;

            case 9:
                result.Add("Prototype");
                break;

            case 10:
                result.Add("More than you bargan");
                break;

            case 11:
                result.Add("Asssassins");
                break;

            case 12:
                result.Add("Bulter bot");
                break;

            case 13:
                result.Add("Refugee crisis");
                break;

            case 14:
                result.Add("port kour");
                break;

            case 15:
                result.Add("tailed");
                break;

            case 16:
                result.Add("counter-espinage");
                break;
        }
        return result;
    }
}
