using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools
{
    public static class Tables
    {

        public static ImmutableArray<EHex> StarportCodes => ImmutableArray.Create<EHex>("A", "B", "C", "D", "E", "X", "F", "G", "H", "Y");

        public static string Starport(EHex starportCode)
        {
            switch (starportCode.ToString())
            {
                //star ports
                case "A": return "Excellent Quality.";
                case "B": return "Good Quality.";
                case "C": return "Routine Quality.";
                case "D": return "Poor Quality.";
                case "E": return "Frontier Installation.";
                case "X": return "No Starport.";
                //space ports
                case "F": return "Good Quality.";
                case "G": return "Poor Quality.";
                case "H": return "Primitive Quality.";
                case "Y": return "None.";
                default: return "";
            }
        }

        public static string StarportDescription(EHex starportCode)
        {
            switch (starportCode.ToString())
            {
                //star ports
                case "A": return "Excellent Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing starships and non-starships present. Nava base and/or scout base may be present.";
                case "B": return "Good Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing non-starships present. Naval base and/or scout base may be present.";
                case "C": return "Routine Quality. Only unrefined fuel available. Reasonable repair facilities present. Scout base may be present.";
                case "D": return "Poor Quality. Only unrefined fuel available. No repair facilities present. Scout base may be present.";
                case "E": return "Frontier Installation. Essentially a marked spot of bedrock with no fuel, facilities, or bases present.";
                case "X": return "No Starport. No provision is made for any ship landings.";
                //space ports
                case "F": return "Good Quality. Minor damage repairable. Unrefined fuel available.";
                case "G": return "Poor Quality. Superficial repairs possible. Unrefined fuel available.";
                case "H": return "Primitive Quality. No repairs or fuel available.";
                case "Y": return "None.";
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


        public static ImmutableArray<EHex> PopulationCodes => BuildCodes("0", "L");

        public static double PopulationExponent(EHex populationCode) => Math.Pow(10, populationCode.Value);



        public static ImmutableArray<EHex> LawLevelCodes => BuildCodes("0", "L", "S");

        public static string LawLevel(EHex lawCode)
        {
            switch (lawCode.ToString())
            {
                case "0": return "No prohibitions.";
                case "1": return "Body pistols, explosives, and poison gas prohibited.";
                case "2": return "Portable energy weapons prohibited.";
                case "3": return "Machine guns, automatic rifles prohibited.";
                case "4": return "Light assault weapons prohibited.";
                case "5": return "Personal concealable weapons prohibited.";
                case "6": return "All firearms except shotguns prohibited.";
                case "7": return "Shotguns prohibited.";
                case "8": return "Long bladed weapons controlled; open possession prohibited.";
                case "9": return "Possession of weapons outside the home prohibited.";
                case "A": return "Weapon possession prohibited.";
                case "B": return "Rigid control of civilian movement.";
                case "C": return "Unrestricted invasion of privacy.";
                case "D": return "Paramilitary law enforcement.";
                case "E": return "Full-fledged police state.";
                case "F": return "All facets of daily life regularly legislated and controlled.";
                case "G": return "Severe punishment for petty infractions.";
                case "H": return "Legalized oppressive practices.";
                case "J": return "Routinely oppressive and restrictive.";
                case "K": return "Excessively oppressive and restrictive.";
                case "L": return "Totally oppressive and restrictive.";
                case "S": return "Special/Variable situation.";

                default: return "";
            }
        }

        public static ImmutableArray<EHex> TechLevelCodes => BuildCodes("0", "L");


        public static string TechLevel(EHex techCode)
        {

            switch (techCode.ToString())
            {
                case "0": return "Stone Age. Primitive.";
                case "1": return "Bronze, Iron. Bronze Age to Middle Ages";
                case "2": return "Printing Press. circa 1400 to 1700.";
                case "3": return "Basic Science. circa 1700 to 1860.";
                case "4": return "External Combustion. circa 1860 to 1900.";
                case "5": return "Mass Production. circa 1900 to 1939.";
                case "6": return "Nuclear Power. circa 1940 to 1969.";
                case "7": return "Miniaturized Electronics. circa 1970 to 1979.";
                case "8": return "Quality Computers. circa 1980 to 1989.";
                case "9": return "Anti-Gravity. circa 1990 to 2000.";
                case "A": return "Interstellar community.";
                case "B": return "Lower Average Imperial.";
                case "C": return "Average Imperial.";
                case "D": return "Above Average Imperial.";
                case "E": return "Above Average Imperial.";
                case "F": return "Technical Imperial Maximum.";
                case "G": return "Robots.";
                case "H": return "Artificial Intelligence.";
                case "J": return "Personal Disintegrators.";
                case "K": return "Plastic Metals.";
                case "L": return "Comprehensible only as technological magic.";
                default: return "";
            }
        }
    }
}
