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
            switch (atmosphereCode.ToString())
            {
                case "0": return "No atmosphere";
                case "1": return "Trace";
                case "2": return "Very thin. Tainted";
                case "3": return "Very thin";
                case "4": return "Thin. Tainted";
                case "5": return "Thin. Breathable";
                case "6": return "Standard. Breathable";
                case "7": return "Standard. Tainted";
                case "8": return "Dense. Breathable";
                case "9": return "Dense. Tainted";
                case "A": return "Exotic";
                case "B": return "Corrosive";
                case "C": return "Insidious";
                case "D": return "Dense, high";
                case "E": return "Ellipsoid";
                case "F": return "Thin, low";
                default: return "";
            }
        }

        public static string AtmosphereDescription(EHex atmosphereCode)
        {
            switch (atmosphereCode.ToString())
            {
                case "0": return "No atmosphere. Requires vacc suit";
                case "1": return "Trace. Requires vacc suit";
                case "2": return "Very thin. Tainted. Requires combination respirator/filter";
                case "3": return "Very thin. Requires respirator";
                case "4": return "Thin. Tainted. Requires filter mask";
                case "5": return "Thin. Breathable";
                case "6": return "Standard. Breathable";
                case "7": return "Standard. Tainted. Requires filter mask";
                case "8": return "Dense. Breathable";
                case "9": return "Dense. Tainted. Requires filter mask";
                case "A": return "Exotic. Requires special protective equipment";
                case "B": return "Corrosive. Requires protective suit";
                case "C": return "Insidious. Requires protective suit";
                case "D": return "Dense, high. Breathable above a minimum altitude";
                case "E": return "Ellipsoid. Breathable at certain latitudes";
                case "F": return "Thin, low. Breathable below certain altitudes";
                default: return "";
            }
        }

        public static EHex? RestrictedTechLevel(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return null;
                case "1": return null;
                case "2": return null;
                case "3": return 15;
                case "4": return 13;
                case "5": return 11;
                case "6": return 9;
                case "7": return 7;
                case "8": return 5;
                default: return 3;
            }
        }

        public static List<string> GovernmentContraband(EHex governmentCode)
        {
            switch (governmentCode.ToString())
            {
                case "0": return new List<string>();
                case "1": return new List<string>() { "Weapons", "Drugs", "Technology", "Travellers", "Psionics" };
                case "2": return new List<string>() { "Drugs" };
                case "3": return new List<string>() { "Weapons", "Technology", "Travellers" };
                case "4": return new List<string>() { "Weapons", "Drugs", "Psionics" };
                case "5": return new List<string>() { "Weapons", "Information", "Technology" };
                case "6": return new List<string>() { "Weapons", "Technology", "Travellers" };
                case "7": return new List<string>() { "Varies" };
                case "8": return new List<string>() { "Weapons", "Drugs" };
                case "9": return new List<string>() { "Weapons", "Drugs", "Information", "Technology", "Travellers", "Psionics" };
                case "A": return new List<string>();
                case "B": return new List<string>() { "Weapons", "Information", "Technology" };
                case "C": return new List<string>() { "Weapons" };
                case "D": return new List<string>() { "Varies" };
                case "E": return new List<string>() { "Varies" };
                case "F": return new List<string>() { "Varies" };
                case "G":
                case "H":
                case "J":
                case "K":
                case "L":
                case "M":
                case "N":
                case "P":
                case "Q":
                case "R":
                case "S":
                case "T":
                case "U":
                case "W":
                case "X":
                default: return new List<string>() { };
            }
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
                    case "Weapons": result.Add((item, LawLevelWeaponsRestricted(lawCode))); break;
                    case "Drugs": result.Add((item, LawLevelDrugsRestricted(lawCode))); break;
                    case "Travellers": result.Add((item, LawLevelTravellersRestricted(lawCode))); break;
                    case "Technology": result.Add((item, LawLevelTechnolgyRestricted(lawCode))); break;
                    case "Psionics": result.Add((item, LawLevelPsionicsRestricted(lawCode))); break;
                    case "Information": result.Add((item, LawLevelInformationRestricted(lawCode))); break;
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
                return GovernmentDescription(governmentCode) + " Bans: " + string.Join(", ", contraband);
            else
                return GovernmentDescription(governmentCode);
        }

        public static string GovernmentDescription(EHex governmentCode)
        {
            switch (governmentCode.ToString())
            {
                case "0": return "No Government Structure. In many cases, tribal, clan or family bonds predominate.";
                case "1": return "Company/Corporation. Government by a company managerial elite, citizens are company employees.";
                case "2": return "Participating Democracy. Government by advice and consent of the citizen.";
                case "3": return "Self-Perpetuating Oligarchy. Government by a restricted minority, with little or no input from the masses.";
                case "4": return "Representative Democracy. Government by elected representatives.";
                case "5": return "Feudal Technocracy. Government by specific individuals for those who agree to be ruled. Relationships are based on the performance of technical activities which are mutually-beneficial.";
                case "6": return "Captive Government/Colony. Government by a leadership answerable to an outside group, a colony or conquered area.";
                case "7": return "Balkanization. No central ruling authority exists. Rival governments compete for control.";
                case "8": return "Civil Service Bureaucracy. Government by agencies employing individuals selected for their expertise.";
                case "9": return "Impersonal Bureaucracy. Government by agencies which are insulated from the governed.";
                case "A": return "Charismatic Dictator. Government by a single leader enjoying the confidence of the citizens.";
                case "B": return "Non-Charismatic Dictator. A previous charismatic dictator has been replaced by a leader through normal channels.";
                case "C": return "Charismatic Oligarchy. Government by a select group, organization, or class enjoying overwhelming confidence of the citizenry.";
                case "D": return "Religious Dictatorship. Government by a religious minority which has little regard for the needs of the citizenry.";
                case "E": return "Religious Autocracy. Government by a single religious leader having absolute power over the citizenry.";
                case "F": return "Totalitarian Oligarchy. Government by an all-powerful minority which maintains absolute control through widespread coercion and oppression.";
                case "G": return "Small Station or Facility (Aslan).";
                case "H": return "Split Clan Control (Aslan).";
                case "J": return "Single On-world Clan Control (Aslan).";
                case "K": return "Single Multi-world Clan Control (Aslan).";
                case "L": return "Major Clan Control (Aslan).";
                case "M": return "Vassal Clan Control (Aslan).";
                case "N": return "Major Vassal Clan Control (Aslan).";
                case "P": return "Small Station or Facility (K'kree).";
                case "Q": return "Krurruna or Krumanak Rule for Off-world Steppelord (K'kree).";
                case "R": return "Steppelord On-world Rule (K'kree).";
                case "S": return "Sept (Hiver).";
                case "T": return "Unsupervised Anarchy (Hiver).";
                case "U": return "Supervised Anarchy (Hiver).";
                case "W": return "Committee (Hiver).";
                case "X": return "Droyne Hierarchy (Droyne).";
                default: return "";
            }
        }

        public static string GovernmentType(EHex governmentCode)
        {
            switch (governmentCode.ToString())
            {
                case "0": return "No Government Structure";
                case "1": return "Company/Corporation";
                case "2": return "Participating Democracy";
                case "3": return "Self-Perpetuating Oligarchy";
                case "4": return "Representative Democracy";
                case "5": return "Feudal Technocracy";
                case "6": return "Captive Government/Colony";
                case "7": return "Balkanization";
                case "8": return "Civil Service Bureaucracy";
                case "9": return "Impersonal Bureaucracy";
                case "A": return "Charismatic Dictator";
                case "B": return "Non-Charismatic Dictator";
                case "C": return "Charismatic Oligarchy";
                case "D": return "Religious Dictatorship";
                case "E": return "Religious Autocracy";
                case "F": return "Totalitarian Oligarchy";
                case "G": return "Small Station or Facility (Aslan)";
                case "H": return "Split Clan Control (Aslan)";
                case "J": return "Single On-world Clan Control (Aslan)";
                case "K": return "Single Multi-world Clan Control (Aslan)";
                case "L": return "Major Clan Control (Aslan)";
                case "M": return "Vassal Clan Control (Aslan)";
                case "N": return "Major Vassal Clan Control (Aslan)";
                case "P": return "Small Station or Facility (K'kree)";
                case "Q": return "Krurruna or Krumanak Rule for Off-world Steppelord (K'kree)";
                case "R": return "Steppelord On-world Rule (K'kree)";
                case "S": return "Sept (Hiver)";
                case "T": return "Unsupervised Anarchy (Hiver)";
                case "U": return "Supervised Anarchy (Hiver)";
                case "W": return "Committee (Hiver)";
                case "X": return "Droyne Hierarchy (Droyne)";
                default: return "";
            }
        }

        public static string Gravity(EHex sizeCode)
        {
            switch (sizeCode.ToString())
            {
                case "0": return "Microgravity (0.01 G or less)";
                case "1": return "Very Low Gravity (0.05g - 0.09g)";
                case "2": return "Low Gravity (0.10g - 0.17g)";
                case "3": return "Low Gravity (0.24g - 0.34g)";
                case "4": return "Low Gravity (0.32g - 0.46g)";
                case "5": return "Standard Gravity (0.40g - 0.57g)";
                case "6": return "Standard Gravity (0.60g - 0.81g)";
                case "7": return "Standard Gravity (0.70g - 0.94g)";
                case "8": return "Standard Gravity (0.80g - 1.08g)";
                case "9": return "Standard Gravity (1.03g - 1.33g)";
                case "A": return "Standard Gravity (1.14g - 1.48g)";
                case "B": return "High Gravity (1.49g - 1.89g)";
                case "C": return "High Gravity (1.9g - 2.0g)";
                default: return "";
            }
        }

        public static string Hydrographics(EHex hydrographicsCode)
        {
            switch (hydrographicsCode.ToString())
            {
                //star ports
                case "0": return "No water";
                case "1": return "10% water";
                case "2": return "20% water";
                case "3": return "30% water";
                case "4": return "40% water";
                case "5": return "50% water";
                case "6": return "60% water";
                case "7": return "70% water";
                case "8": return "80% water";
                case "9": return "90% water";
                case "A": return "100% water";
                default: return "";
            }
        }

        public static string HydrographicsDescription(EHex hydrographicsCode)
        {
            switch (hydrographicsCode.ToString())
            {
                //star ports
                case "0": return "No water. Desert World";
                case "1": return "10% water";
                case "2": return "20% water";
                case "3": return "30% water";
                case "4": return "40% water";
                case "5": return "50% water";
                case "6": return "60% water";
                case "7": return "70% water. Equivalent to Terra or Vland";
                case "8": return "80% water";
                case "9": return "90% water";
                case "A": return "100% water. Water World";
                default: return "";
            }
        }

        public static string LawLevel(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "No Law";
                case "1":
                case "2":
                case "3": return "Low Law";
                case "4":
                case "5":
                case "6":
                case "7": return "Moderate Law";
                case "8":
                case "9": return "High Law";
                case "A":
                case "B":
                case "C":
                case "D":
                case "E":
                case "F":
                case "G":
                case "H":
                case "J":
                case "K":
                case "L": return "Extreme Law";
                case "S": return "Special/Variable situation";

                default: return "";
            }
        }

        public static string LawLevelDescription(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "No prohibitions";
                case "1": return "Body pistols, explosives, and poison gas prohibited";
                case "2": return "Portable energy weapons prohibited";
                case "3": return "Machine guns, automatic rifles prohibited";
                case "4": return "Light assault weapons prohibited";
                case "5": return "Personal concealable weapons prohibited";
                case "6": return "All firearms except shotguns prohibited";
                case "7": return "Shotguns prohibited";
                case "8": return "Long bladed weapons controlled; open possession prohibited";
                case "9": return "Possession of weapons outside the home prohibited";
                case "A": return "Weapon possession prohibited";
                case "B": return "Rigid control of civilian movement";
                case "C": return "Unrestricted invasion of privacy";
                case "D": return "Paramilitary law enforcement";
                case "E": return "Full-fledged police state";
                case "F": return "All facets of daily life regularly legislated and controlled";
                case "G": return "Severe punishment for petty infractions";
                case "H": return "Legalized oppressive practices";
                case "J": return "Routinely oppressive and restrictive";
                case "K": return "Excessively oppressive and restrictive";
                case "L": return "Totally oppressive and restrictive";
                case "S": return "Special/Variable situation";

                default: return "";
            }
        }

        public static string LawLevelWeaponsRestricted(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "";
                case "1": return "Poison gas, explosives, undetectable weapons, WMD";
                case "2": return "Portable energy weapons (except ship-mounted weapons)";
                case "3": return "Heavy weapons";
                case "4": return "Light assault weapons and submachine guns";
                case "5": return "Personal concealable weapons";
                case "6": return "All firearms except shotguns and stunners; carrying weapons discouraged";
                case "7": return "Shotguns";
                case "8": return "All bladed weapons, stunners";
                default: return "Any weapons";
            }
        }

        public static string LawLevelDrugsRestricted(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "";
                case "1": return "Highly  addictive  and dangerous   narcotics";
                case "2": return "Highly  addictive narcotics";
                case "3": return "Combat drugs";
                case "4": return "Addictive  narcotics";
                case "5": return "Anagathics";
                case "6": return "Fast and Slow   drugs";
                case "7": return "All narcotics";
                case "8": return "Medicinal   drugs";
                default: return "All drugs";
            }
        }

        public static string LawLevelInformationRestricted(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "";
                case "1": return "Intellect programs.";
                case "2": return "Agent programs.";
                case "3": return "Intrusion programs.";
                case "4": return "Security programs.";
                case "5": return "Expert programs.";
                case "6": return "Recent news from offworld.";
                case "7": return "Library programs, unfiltered data about other worlds. Free speech curtailed.";
                case "8": return "Information technology, any non-critical data from offworld, personal media.";
                default: return "Any data from offworld. No free press.";
            }
        }

        public static string LawLevelTechnolgyRestricted(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "";
                case "1": return "Dangerous technologies such as nanotechnology";
                case "2": return "Alien Technology";
                case "3": return "TL 15";
                case "4": return "TL 13";
                case "5": return "TL 11";
                case "6": return "TL 9";
                case "7": return "TL 7";
                case "8": return "TL 5";
                default: return "TL 3";
            }
        }

        public static string LawLevelTravellersRestricted(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "";
                case "1": return "Visitors must contact planetary authorities by radio, landing is permitted anywhere.";
                case "2": return "Visitors must report passenger manifest, landing is permitted anywhere.";
                case "3": return "Landing only at starport or other authorised sites.";
                case "4": return "Landing only at starport.";
                case "5": return "Citizens must register offworld travel; visitors mustt register all business.";
                case "6": return "Visits discouraged; excessive contact with citizens forbidden.";
                case "7": return "Citizens may not leave planet; ,visitors may not leave starport. ";
                case "8": return "Landing permitted only to imperial agents.";
                default: return "No offworlders permitted.";
            }
        }

        public static string LawLevelPsionicsRestricted(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "";
                case "1": return "Dangerous talents must be registered. ";
                case "2": return "All psionic powers must be registered; use of dangerous powers forbidden. ";
                case "3": return "Use of telepathy restricted to - government- approved telepaths.";
                case "4": return "Use of teleportation and clairvoyance restricted.";
                case "5": return "Use of all psionic ,powers restricted to government psionicists.";
                case "6": return "Possession of psionic drugs banned.";
                case "7": return "Use of psionics forbiden.";
                case "8": return "Psionic-related technology banned.";
                default: return "All psionics.";
            }
        }

        public static double PopulationExponent(EHex populationCode) => Math.Pow(10, populationCode.Value);

        public static int SizeKM(EHex sizeCode)
        {
            switch (sizeCode.ToString())
            {
                case "0": return 800;
                case "1": return 1600;
                case "2": return 3200;
                case "3": return 4800;
                case "4": return 6400;
                case "5": return 8000;
                case "6": return 9600;
                case "7": return 11200;
                case "8": return 12800;
                case "9": return 14400;
                case "A": return 16000;
                case "B": return 17600;
                case "C": return 19400;
                default: return 0;
            }
        }

        public static string Starport(EHex starportCode)
        {
            switch (starportCode.ToString())
            {
                //star ports
                case "A": return "Excellent Quality";
                case "B": return "Good Quality";
                case "C": return "Routine Quality";
                case "D": return "Poor Quality";
                case "E": return "Frontier Installation";
                case "X": return "No Starport";
                //space ports
                case "F": return "Good Quality";
                case "G": return "Poor Quality";
                case "H": return "Primitive Quality";
                case "Y": return "None";
                default: return "";
            }
        }

        public static string StarportDescription(EHex starportCode)
        {
            switch (starportCode.ToString())
            {
                //star ports
                case "A": return "Excellent Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing starships and non-starships present. Nava base and/or scout base may be present";
                case "B": return "Good Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing non-starships present. Naval base and/or scout base may be present";
                case "C": return "Routine Quality. Only unrefined fuel available. Reasonable repair facilities present. Scout base may be present";
                case "D": return "Poor Quality. Only unrefined fuel available. No repair facilities present. Scout base may be present";
                case "E": return "Frontier Installation. Essentially a marked spot of bedrock with no fuel, facilities, or bases present";
                case "X": return "No Starport. No provision is made for any ship landings";
                //space ports
                case "F": return "Good Quality. Minor damage repairable. Unrefined fuel available";
                case "G": return "Poor Quality. Superficial repairs possible. Unrefined fuel available";
                case "H": return "Primitive Quality. No repairs or fuel available";
                case "Y": return "None";
                default: return "";
            }
        }

        public static string TechLevel(EHex techCode)
        {
            switch (techCode.ToString())
            {
                case "0": return "Stone Age. Primitive";
                case "1": return "Bronze, Iron. Bronze Age to Middle Ages";
                case "2": return "Printing Press. circa 1400 to 1700";
                case "3": return "Basic Science. circa 1700 to 1860";
                case "4": return "External Combustion. circa 1860 to 1900";
                case "5": return "Mass Production. circa 1900 to 1939";
                case "6": return "Nuclear Power. circa 1940 to 1969";
                case "7": return "Miniaturized Electronics. circa 1970 to 1979";
                case "8": return "Quality Computers. circa 1980 to 1989";
                case "9": return "Anti-Gravity. circa 1990 to 2000";
                case "A": return "Interstellar community";
                case "B": return "Lower Average Imperial";
                case "C": return "Average Imperial";
                case "D": return "Above Average Imperial";
                case "E": return "Above Average Imperial";
                case "F": return "Technical Imperial Maximum";
                case "G": return "Robots";
                case "H": return "Artificial Intelligence";
                case "J": return "Personal Disintegrators";
                case "K": return "Plastic Metals";
                case "L": return "Comprehensible only as technological magic";
                default: return "";
            }
        }

        public static string TechLevelDescription(EHex techCode)
        {
            switch (techCode.ToString())
            {
                case "0": return "Primitive/Stone Age";
                case "1": return "Bronze Age/Iron Age";
                case "2": return "Age of Sail";
                case "3": return "Industrial Revolution";
                case "4": return "Mechanization";
                case "5": return "Polymers";
                case "6": return "Nuclear Age";
                case "7": return "Semiconductors; Early space age";
                case "8": return "Superconductors; Early communications";
                case "9": return "Gravitics; First Jump Drives";
                case "A": return "Practical Fusion power";
                case "B": return "Fusion+; Imperial maximum year 0";
                case "C": return "Sophisticated Robots";
                case "D": return "Cloning; Imperial maximum year 550";
                case "E": return "Geneering; Imperial maximum year 900";
                case "F": return "Anagathics; Imperial maximum year 1105";
                case "G": return "Artificial Persons; Black Globe Generators";
                case "H": return "Hop Drive; Permanent Personality Transfer";
                case "J": return "Disruptor and Stasis weapons";
                case "K": return "Limited Matter Transport; Antimatter power";
                case "L": return "Skip Drives; White Globe Generators";
                case "M": return "System-wide matter transport; Relativity Rifle";
                case "N": return "(Beyond current technology extrapolation.)";
                case "P": return "Planetary core energy tap; Rapid terraforming";
                case "Q": return "Portals; Rosettes";
                case "R": return "Psionic engineering";
                case "S": return "Stasis globe";
                case "T": return "Ringworlds";
                case "U": return "Reality drive";
                case "V": return "Dyson spheres";
                case "W": return "Pocket universes";
                default: return "";
            }
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
