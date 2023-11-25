using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools
{
    public static class Tables
    {
        public static ImmutableArray<EHex> AtmosphereCodes => BuildCodes('0', 'F');
        public static ImmutableArray<EHex> GovernmentCodes => BuildCodes('0', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X');
        public static ImmutableArray<EHex> HydrographicsCodes => BuildCodes('0', 'A');
        public static ImmutableArray<EHex> LawLevelCodes => BuildCodes('0', 'L', 'S');
        public static ImmutableArray<EHex> PopulationCodes => BuildCodes('0', 'L');
        public static ImmutableArray<EHex> SizeCodes => BuildCodes('0', 'C');
        public static ImmutableArray<EHex> StarportCodes => ImmutableArray.Create<EHex>('A', 'B', 'C', 'D', 'E', 'X', 'F', 'G', 'H', 'Y');

        public static ImmutableArray<EHex> TechLevelCodes => BuildCodes('0', 'W');

        public static string Atmosphere(EHex atmosphereCode)
        {
            return (atmosphereCode.ToChar()) switch
            {
                '0' => "No atmosphere",
                '1' => "Trace",
                '2' => "Very thin. Tainted",
                '3' => "Very thin",
                '4' => "Thin. Tainted",
                '5' => "Thin. Breathable",
                '6' => "Standard. Breathable",
                '7' => "Standard. Tainted",
                '8' => "Dense. Breathable",
                '9' => "Dense. Tainted",
                'A' => "Exotic",
                'B' => "Corrosive",
                'C' => "Insidious",
                'D' => "Dense, high",
                'E' => "Ellipsoid",
                'F' => "Thin, low",
                _ => "",
            };
        }

        public static string AtmosphereDescription(EHex atmosphereCode)
        {
            return (atmosphereCode.ToChar()) switch
            {
                '0' => "No atmosphere. Requires vacc suit",
                '1' => "Trace. Requires vacc suit",
                '2' => "Very thin. Tainted. Requires combination respirator/filter",
                '3' => "Very thin. Requires respirator",
                '4' => "Thin. Tainted. Requires filter mask",
                '5' => "Thin. Breathable",
                '6' => "Standard. Breathable",
                '7' => "Standard. Tainted. Requires filter mask",
                '8' => "Dense. Breathable",
                '9' => "Dense. Tainted. Requires filter mask",
                'A' => "Exotic. Requires special protective equipment",
                'B' => "Corrosive. Requires protective suit",
                'C' => "Insidious. Requires protective suit",
                'D' => "Dense, high. Breathable above a minimum altitude",
                'E' => "Ellipsoid. Breathable at certain latitudes",
                'F' => "Thin, low. Breathable below certain altitudes",
                _ => "",
            };
        }

        public static EHex? RestrictedTechLevel(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => null,
                '1' => null,
                '2' => null,
                '3' => 15,
                '4' => 13,
                '5' => 11,
                '6' => 9,
                '7' => 7,
                '8' => 5,
                _ => 3,
            };
        }

        public static List<string> GovernmentContraband(EHex governmentCode)
        {
            return (governmentCode.ToChar()) switch
            {
                '0' => new List<string>(),
                '1' => new List<string>() { "Weapons", "Drugs", "Technology", "Travellers", "Psionics" },
                '2' => new List<string>() { "Drugs" },
                '3' => new List<string>() { "Weapons", "Technology", "Travellers" },
                '4' => new List<string>() { "Weapons", "Drugs", "Psionics" },
                '5' => new List<string>() { "Weapons", "Information", "Technology" },
                '6' => new List<string>() { "Weapons", "Technology", "Travellers" },
                '7' => new List<string>() { "Varies" },
                '8' => new List<string>() { "Weapons", "Drugs" },
                '9' => new List<string>() { "Weapons", "Drugs", "Information", "Technology", "Travellers", "Psionics" },
                'A' => new List<string>(),
                'B' => new List<string>() { "Weapons", "Information", "Technology" },
                'C' => new List<string>() { "Weapons" },
                'D' => new List<string>() { "Varies" },
                'E' => new List<string>() { "Varies" },
                'F' => new List<string>() { "Varies" },
                _ => new List<string>() { },
            };
        }

        public static List<(string, string)> RestrictionsByGovernmentAndLaw(EHex governmentCode, EHex lawCode)
        {
            var result = new List<(string, string)>();
            if (lawCode == 0)
                return result; //no restrictions apply

            var contraband = GovernmentContraband(governmentCode);
            foreach (var item in contraband)
                switch (item)
                {
                    case "Weapons":
                        foreach (var rule in LawLevelWeaponsRestrictedList(lawCode))
                            result.Add((item, rule));
                        break;
                    case "Drugs":
                        foreach (var rule in LawLevelDrugsRestrictedList(lawCode))
                            result.Add((item, rule));
                        break;
                    case "Travellers": result.Add((item, LawLevelTravellersRestricted(lawCode))); break;
                    case "Technology":
                        foreach (var rule in LawLevelTechnolgyRestrictedList(lawCode))
                            result.Add((item, rule));
                        break;
                    case "Psionics":
                        foreach (var rule in LawLevelPsionicsRestrictedList(lawCode))
                            result.Add((item, rule));
                        break;
                    case "Information":
                        foreach (var rule in LawLevelInformationRestrictedList(lawCode))
                            result.Add((item, rule));
                        break;
                    case "None": result.Add((item, "No restrictions.")); break;
                    case "Varies":
                        result.Add(("Varies", "One of more of the following may apply."));
                        result.Add(("Weapons", LawLevelWeaponsRestricted(lawCode)));
                        result.Add(("Drugs", LawLevelDrugsRestricted(lawCode)));
                        result.Add(("Information", LawLevelInformationRestricted(lawCode)));
                        result.Add(("Technology", LawLevelTechnolgyRestricted(lawCode)));
                        result.Add(("Travellers", LawLevelTravellersRestricted(lawCode)));
                        result.Add(("Psionics", LawLevelPsionicsRestricted(lawCode)));
                        break;
                }
            return result;
        }

        public static string GovernmentDescriptionWithContraband(EHex governmentCode)
        {
            var contraband = GovernmentContraband(governmentCode);
            if (contraband.Count > 0)
                return GovernmentDescription(governmentCode) + " Restricts: " + string.Join(", ", contraband);
            else
                return GovernmentDescription(governmentCode);
        }

        public static string GovernmentDescription(EHex governmentCode)
        {
            return (governmentCode.ToChar()) switch
            {
                '0' => "No Government Structure. In many cases, tribal, clan or family bonds predominate.",
                '1' => "Company/Corporation. Government by a company managerial elite, citizens are company employees.",
                '2' => "Participating Democracy. Government by advice and consent of the citizen.",
                '3' => "Self-Perpetuating Oligarchy. Government by a restricted minority, with little or no input from the masses.",
                '4' => "Representative Democracy. Government by elected representatives.",
                '5' => "Feudal Technocracy. Government by specific individuals for those who agree to be ruled. Relationships are based on the performance of technical activities which are mutually-beneficial.",
                '6' => "Captive Government/Colony. Government by a leadership answerable to an outside group, a colony or conquered area.",
                '7' => "Balkanization. No central ruling authority exists. Rival governments compete for control.",
                '8' => "Civil Service Bureaucracy. Government by agencies employing individuals selected for their expertise.",
                '9' => "Impersonal Bureaucracy. Government by agencies which are insulated from the governed.",
                'A' => "Charismatic Dictator. Government by a single leader enjoying the confidence of the citizens.",
                'B' => "Non-Charismatic Dictator. A previous charismatic dictator has been replaced by a leader through normal channels.",
                'C' => "Charismatic Oligarchy. Government by a select group, organization, or class enjoying overwhelming confidence of the citizenry.",
                'D' => "Religious Dictatorship. Government by a religious minority which has little regard for the needs of the citizenry.",
                'E' => "Religious Autocracy. Government by a single religious leader having absolute power over the citizenry.",
                'F' => "Totalitarian Oligarchy. Government by an all-powerful minority which maintains absolute control through widespread coercion and oppression.",
                'G' => "Small Station or Facility (Aslan).",
                'H' => "Split Clan Control (Aslan).",
                'J' => "Single On-world Clan Control (Aslan).",
                'K' => "Single Multi-world Clan Control (Aslan).",
                'L' => "Major Clan Control (Aslan).",
                'M' => "Vassal Clan Control (Aslan).",
                'N' => "Major Vassal Clan Control (Aslan).",
                'P' => "Small Station or Facility (K'kree).",
                'Q' => "Krurruna or Krumanak Rule for Off-world Steppelord (K'kree).",
                'R' => "Steppelord On-world Rule (K'kree).",
                'S' => "Sept (Hiver).",
                'T' => "Unsupervised Anarchy (Hiver).",
                'U' => "Supervised Anarchy (Hiver).",
                'W' => "Committee (Hiver).",
                'X' => "Droyne Hierarchy (Droyne).",
                _ => "",
            };
        }

        public static string GovernmentType(EHex governmentCode)
        {
            return (governmentCode.ToChar()) switch
            {
                '0' => "No Government Structure",
                '1' => "Company/Corporation",
                '2' => "Participating Democracy",
                '3' => "Self-Perpetuating Oligarchy",
                '4' => "Representative Democracy",
                '5' => "Feudal Technocracy",
                '6' => "Captive Government/Colony",
                '7' => "Balkanization",
                '8' => "Civil Service Bureaucracy",
                '9' => "Impersonal Bureaucracy",
                'A' => "Charismatic Dictator",
                'B' => "Non-Charismatic Dictator",
                'C' => "Charismatic Oligarchy",
                'D' => "Religious Dictatorship",
                'E' => "Religious Autocracy",
                'F' => "Totalitarian Oligarchy",
                'G' => "Small Station or Facility (Aslan)",
                'H' => "Split Clan Control (Aslan)",
                'J' => "Single On-world Clan Control (Aslan)",
                'K' => "Single Multi-world Clan Control (Aslan)",
                'L' => "Major Clan Control (Aslan)",
                'M' => "Vassal Clan Control (Aslan)",
                'N' => "Major Vassal Clan Control (Aslan)",
                'P' => "Small Station or Facility (K'kree)",
                'Q' => "Krurruna or Krumanak Rule for Off-world Steppelord (K'kree)",
                'R' => "Steppelord On-world Rule (K'kree)",
                'S' => "Sept (Hiver)",
                'T' => "Unsupervised Anarchy (Hiver)",
                'U' => "Supervised Anarchy (Hiver)",
                'W' => "Committee (Hiver)",
                'X' => "Droyne Hierarchy (Droyne)",
                _ => "",
            };
        }

        public static string Gravity(EHex sizeCode)
        {
            return (sizeCode.ToChar()) switch
            {
                '0' => "Microgravity (0.01 G or less)",
                '1' => "Very Low Gravity (0.05g - 0.09g)",
                '2' => "Low Gravity (0.10g - 0.17g)",
                '3' => "Low Gravity (0.24g - 0.34g)",
                '4' => "Low Gravity (0.32g - 0.46g)",
                '5' => "Standard Gravity (0.40g - 0.57g)",
                '6' => "Standard Gravity (0.60g - 0.81g)",
                '7' => "Standard Gravity (0.70g - 0.94g)",
                '8' => "Standard Gravity (0.80g - 1.08g)",
                '9' => "Standard Gravity (1.03g - 1.33g)",
                'A' => "Standard Gravity (1.14g - 1.48g)",
                'B' => "High Gravity (1.49g - 1.89g)",
                'C' => "High Gravity (1.9g - 2.0g)",
                _ => "",
            };
        }

        public static string Hydrographics(EHex hydrographicsCode)
        {
            return (hydrographicsCode.ToChar()) switch
            {
                '0' => "No water",
                '1' => "10% water",
                '2' => "20% water",
                '3' => "30% water",
                '4' => "40% water",
                '5' => "50% water",
                '6' => "60% water",
                '7' => "70% water",
                '8' => "80% water",
                '9' => "90% water",
                'A' => "100% water",
                _ => "",
            };
        }

        public static string HydrographicsDescription(EHex hydrographicsCode)
        {
            return (hydrographicsCode.ToChar()) switch
            {
                '0' => "No water. Desert World",
                '1' => "10% water",
                '2' => "20% water",
                '3' => "30% water",
                '4' => "40% water",
                '5' => "50% water",
                '6' => "60% water",
                '7' => "70% water. Equivalent to Terra or Vland",
                '8' => "80% water",
                '9' => "90% water",
                'A' => "100% water. Water World",
                _ => "",
            };
        }

        public static string LawLevel(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "No Law",
                '1' or '2' or '3' => "Low Law",
                '4' or '5' or '6' or '7' => "Moderate Law",
                '8' or '9' => "High Law",
                'A' or 'B' or 'C' or 'D' or 'E' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' => "Extreme Law",
                'S' => "Special/Variable situation",
                _ => "",
            };
        }

        public static string LawLevelDescription(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "No prohibitions.",
                '1' => "Body pistols, explosives, and poison gas prohibited.",
                '2' => "Portable energy weapons prohibited.",
                '3' => "Machine guns, automatic rifles prohibited.",
                '4' => "Light assault weapons prohibited.",
                '5' => "Personal concealable weapons prohibited.",
                '6' => "All firearms except shotguns prohibited.",
                '7' => "Shotguns prohibited.",
                '8' => "Long bladed weapons controlled; open possession prohibited.",
                '9' => "Possession of weapons outside the home prohibited.",
                'A' => "Weapon possession prohibited.",
                'B' => "Rigid control of civilian movement.",
                'C' => "Unrestricted invasion of privacy.",
                'D' => "Paramilitary law enforcement.",
                'E' => "Full-fledged police state.",
                'F' => "All facets of daily life regularly legislated and controlled.",
                'G' => "Severe punishment for petty infractions.",
                'H' => "Legalized oppressive practices.",
                'J' => "Routinely oppressive and restrictive.",
                'K' => "Excessively oppressive and restrictive.",
                'L' => "Totally oppressive and restrictive.",
                'S' => "Special/Variable situation.",
                _ => "",
            };
        }

        public static IEnumerable<string> LawLevelWeaponsRestrictedList(EHex lawCode)
        {
            switch (lawCode.Value)
            {
                case >= 6:
                    yield return LawLevelWeaponsRestricted(lawCode);
                    break;
                case >= 0:
                    for (var i = 1; i <= lawCode.Value; i++)
                        yield return LawLevelWeaponsRestricted(i);
                    break;
            }
        }

        public static string LawLevelWeaponsRestricted(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "",
                '1' => "Poison gas, explosives, undetectable weapons, WMD.",
                '2' => "Portable energy weapons (except ship-mounted weapons).",
                '3' => "Heavy weapons.",
                '4' => "Light assault weapons and submachine guns.",
                '5' => "Personal concealable weapons.",
                '6' => "All firearms except shotguns and stunners; carrying weapons discouraged.",
                '7' => "All firearms except stunners; carrying weapons discouraged.",
                '8' => "All firearms and bladed weapons.",
                _ => "Any weapons",
            };
        }

        public static IEnumerable<string> LawLevelDrugsRestrictedList(EHex lawCode)
        {
            switch (lawCode.Value)
            {
                case 1:
                case 2:
                    yield return LawLevelDrugsRestricted(lawCode);
                    break;

                case 3:
                    yield return LawLevelDrugsRestricted(3);
                    yield return LawLevelDrugsRestricted(2);
                    break;

                case 4:
                    yield return LawLevelDrugsRestricted(3);
                    yield return LawLevelDrugsRestricted(4);
                    break;

                case 5:
                    yield return LawLevelDrugsRestricted(3);
                    yield return LawLevelDrugsRestricted(4);
                    yield return LawLevelDrugsRestricted(5);
                    break;

                case 6:
                    yield return LawLevelDrugsRestricted(3);
                    yield return LawLevelDrugsRestricted(4);
                    yield return LawLevelDrugsRestricted(5);
                    yield return LawLevelDrugsRestricted(6);
                    break;

                case 7:
                    yield return LawLevelDrugsRestricted(3);
                    yield return LawLevelDrugsRestricted(5);
                    yield return LawLevelDrugsRestricted(6);
                    yield return LawLevelDrugsRestricted(7);
                    break;

                case 8:
                    yield return LawLevelDrugsRestricted(3);
                    yield return LawLevelDrugsRestricted(5);
                    yield return LawLevelDrugsRestricted(6);
                    yield return LawLevelDrugsRestricted(7);
                    yield return LawLevelDrugsRestricted(8);
                    break;


                case > 8:
                    yield return LawLevelDrugsRestricted(lawCode);
                    break;
            }
        }

        public static string LawLevelDrugsRestricted(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "",
                '1' => "Highly addictive and dangerous narcotics.",
                '2' => "Highly addictive narcotics.",
                '3' => "Combat drugs.",
                '4' => "Addictive narcotics.",
                '5' => "Anagathics.",
                '6' => "Fast and Slow drugs.",
                '7' => "All narcotics.",
                '8' => "Medicinal drugs.",
                _ => "All drugs.",
            };
        }

        public static IEnumerable<string> LawLevelInformationRestrictedList(EHex lawCode)
        {
            if (lawCode.Value >= 1)
            {
                for (var i = 1; i <= Math.Min(9, lawCode.Value); i++)
                    yield return LawLevelInformationRestricted(i);
            }
        }

        public static string LawLevelInformationRestricted(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "",
                '1' => "Intellect programs.",
                '2' => "Agent programs.",
                '3' => "Intrusion programs.",
                '4' => "Security programs.",
                '5' => "Expert programs.",
                '6' => "Recent news from offworld.",
                '7' => "Library programs, unfiltered data about other worlds. Free speech curtailed.",
                '8' => "Information technology, any non-critical data from offworld, personal media.",
                _ => "Any data from offworld. No free press.",
            };
        }

        public static IEnumerable<string> LawLevelTechnolgyRestrictedList(EHex lawCode)
        {
            if (lawCode.Value >= 1)
                yield return LawLevelTechnolgyRestricted(1);

            if (lawCode.Value >= 2)
                yield return LawLevelTechnolgyRestricted(2);

            if (lawCode.Value >= 3)
                yield return LawLevelTechnolgyRestricted(lawCode);
        }

        public static string LawLevelTechnolgyRestricted(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "",
                '1' => "Dangerous technologies such as nanotechnology",
                '2' => "Alien Technology",
                '3' => "TL 15+",
                '4' => "TL 13+",
                '5' => "TL 11+",
                '6' => "TL 9+",
                '7' => "TL 7+",
                '8' => "TL 5+",
                _ => "TL 3+",
            };
        }

        public static string LawLevelTravellersRestricted(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "",
                '1' => "Visitors must contact planetary authorities by radio, landing is permitted anywhere.",
                '2' => "Visitors must report passenger manifest, landing is permitted anywhere.",
                '3' => "Landing only at starport or other authorised sites.",
                '4' => "Landing only at starport.",
                '5' => "Citizens must register offworld travel; visitors mustt register all business.",
                '6' => "Visits discouraged; excessive contact with citizens forbidden.",
                '7' => "Citizens may not leave planet; ,visitors may not leave starport. ",
                '8' => "Landing permitted only to imperial agents.",
                _ => "No offworlders permitted.",
            };
        }

        public static IEnumerable<string> LawLevelPsionicsRestrictedList(EHex lawCode)
        {
            switch (lawCode.Value)
            {
                case 1:
                case 2:
                case 5:
                    yield return LawLevelPsionicsRestricted(lawCode);
                    break;

                case 3:
                    yield return LawLevelPsionicsRestricted(2);
                    yield return LawLevelPsionicsRestricted(3);
                    break;

                case 4:
                    yield return LawLevelPsionicsRestricted(2);
                    yield return LawLevelPsionicsRestricted(3);
                    yield return LawLevelPsionicsRestricted(4);
                    break;

                case 6:
                    yield return LawLevelPsionicsRestricted(5);
                    yield return LawLevelPsionicsRestricted(6);
                    break;

                case 7:
                    yield return LawLevelPsionicsRestricted(6);
                    yield return LawLevelPsionicsRestricted(7);
                    break;

                case 8:
                    yield return LawLevelPsionicsRestricted(6);
                    yield return LawLevelPsionicsRestricted(7);
                    yield return LawLevelPsionicsRestricted(8);
                    break;

                case > 8:
                    yield return LawLevelPsionicsRestricted(lawCode);
                    break;
            }
        }

        public static string LawLevelPsionicsRestricted(EHex lawCode)
        {
            return (lawCode.ToChar()) switch
            {
                '0' => "",
                '1' => "Dangerous talents must be registered.",
                '2' => "All psionic powers must be registered; use of dangerous powers forbidden.",
                '3' => "Use of telepathy restricted to government approved telepaths.",
                '4' => "Use of teleportation and clairvoyance restricted.",
                '5' => "Use of all psionic, powers restricted to government psionicists.",
                '6' => "Possession of psionic drugs banned.",
                '7' => "Use of psionics forbiden.",
                '8' => "Psionic-related technology banned.",
                _ => "All psionics.",
            };
        }

        public static double PopulationExponent(EHex populationCode) => Math.Pow(10, populationCode.Value);

        public static int SizeKM(EHex sizeCode)
        {
            return (sizeCode.ToChar()) switch
            {
                '0' => 800,
                '1' => 1600,
                '2' => 3200,
                '3' => 4800,
                '4' => 6400,
                '5' => 8000,
                '6' => 9600,
                '7' => 11200,
                '8' => 12800,
                '9' => 14400,
                'A' => 16000,
                'B' => 17600,
                'C' => 19400,
                _ => 0,
            };
        }

        public static string Starport(EHex starportCode)
        {
            return (starportCode.ToChar()) switch
            {
                'A' => "Excellent Quality",
                'B' => "Good Quality",
                'C' => "Routine Quality",
                'D' => "Poor Quality",
                'E' => "Frontier Installation",
                'X' => "No Starport",
                'F' => "Good Quality",
                'G' => "Poor Quality",
                'H' => "Primitive Quality",
                'Y' => "None",
                _ => "",
            };
        }

        public static string StarportDescription(EHex starportCode)
        {
            return (starportCode.ToChar()) switch
            {
                'A' => "Excellent Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing starships and non-starships present. Nava base and/or scout base may be present",
                'B' => "Good Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing non-starships present. Naval base and/or scout base may be present",
                'C' => "Routine Quality. Only unrefined fuel available. Reasonable repair facilities present. Scout base may be present",
                'D' => "Poor Quality. Only unrefined fuel available. No repair facilities present. Scout base may be present",
                'E' => "Frontier Installation. Essentially a marked spot of bedrock with no fuel, facilities, or bases present",
                'X' => "No Starport. No provision is made for any ship landings",
                'F' => "Good Quality. Minor damage repairable. Unrefined fuel available",
                'G' => "Poor Quality. Superficial repairs possible. Unrefined fuel available",
                'H' => "Primitive Quality. No repairs or fuel available",
                'Y' => "None",
                _ => "",
            };
        }

        public static string TechLevel(EHex techCode)
        {
            return (techCode.ToChar()) switch
            {
                '0' => "Stone Age. Primitive",
                '1' => "Bronze, Iron. Bronze Age to Middle Ages",
                '2' => "Printing Press. circa 1400 to 1700",
                '3' => "Basic Science. circa 1700 to 1860",
                '4' => "External Combustion. circa 1860 to 1900",
                '5' => "Mass Production. circa 1900 to 1939",
                '6' => "Nuclear Power. circa 1940 to 1969",
                '7' => "Miniaturized Electronics. circa 1970 to 1979",
                '8' => "Quality Computers. circa 1980 to 1989",
                '9' => "Anti-Gravity. circa 1990 to 2000",
                'A' => "Interstellar community",
                'B' => "Lower Average Imperial",
                'C' => "Average Imperial",
                'D' => "Above Average Imperial",
                'E' => "Above Average Imperial",
                'F' => "Technical Imperial Maximum",
                'G' => "Robots",
                'H' => "Artificial Intelligence",
                'J' => "Personal Disintegrators",
                'K' => "Plastic Metals",
                'L' => "Comprehensible only as technological magic",
                _ => "",
            };
        }

        public static string TechLevelDescription(EHex techCode)
        {
            return (techCode.ToChar()) switch
            {
                '0' => "Primitive/Stone Age",
                '1' => "Bronze Age/Iron Age",
                '2' => "Age of Sail",
                '3' => "Industrial Revolution",
                '4' => "Mechanization",
                '5' => "Polymers",
                '6' => "Nuclear Age",
                '7' => "Semiconductors; Early space age",
                '8' => "Superconductors; Early communications",
                '9' => "Gravitics; First Jump Drives",
                'A' => "Practical Fusion power",
                'B' => "Fusion+; Imperial maximum year 0",
                'C' => "Sophisticated Robots",
                'D' => "Cloning; Imperial maximum year 550",
                'E' => "Geneering; Imperial maximum year 900",
                'F' => "Anagathics; Imperial maximum year 1105",
                'G' => "Artificial Persons; Black Globe Generators",
                'H' => "Hop Drive; Permanent Personality Transfer",
                'J' => "Disruptor and Stasis weapons",
                'K' => "Limited Matter Transport; Antimatter power",
                'L' => "Skip Drives; White Globe Generators",
                'M' => "System-wide matter transport; Relativity Rifle",
                'N' => "(Beyond current technology extrapolation.)",
                'P' => "Planetary core energy tap; Rapid terraforming",
                'Q' => "Portals; Rosettes",
                'R' => "Psionic engineering",
                'S' => "Stasis globe",
                'T' => "Ringworlds",
                'U' => "Reality drive",
                'V' => "Dyson spheres",
                'W' => "Pocket universes",
                _ => "",
            };
        }

        static ImmutableArray<EHex> BuildCodes(int min, int max, params EHex[] extras)
        {
            var temp = new List<EHex>();
            for (var i = min; i <= max; i++)
                temp.Add(i);
            temp.AddRange(extras);

            return ImmutableArray.CreateRange(temp);
        }

        static ImmutableArray<EHex> BuildCodes(EHex min, EHex max, params EHex[] extras)
        {
            return BuildCodes(min.Value, max.Value, extras);
        }

        static ImmutableArray<EHex> BuildCodes(char min, char max, params EHex[] extras)
        {
            return BuildCodes(new EHex(min).Value, new EHex(max).Value, extras);
        }
    }
}
