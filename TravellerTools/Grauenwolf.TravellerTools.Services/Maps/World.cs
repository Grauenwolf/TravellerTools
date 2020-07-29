using System;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Maps
{
    public class World
    {
        static readonly Dictionary<string, string> s_RemarkMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        readonly HashSet<string> m_RemarksList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private string m_Remarks;

        static World()
        {
            s_RemarkMap.Add("Ab", "Data Repository.");
            s_RemarkMap.Add("Ag", "Agricultural.");
            s_RemarkMap.Add("An", "Ancient Site.");
            s_RemarkMap.Add("As", "Asteroid Belt.");
            s_RemarkMap.Add("Ba", "Barren.");
            s_RemarkMap.Add("Co", "Cold.");
            s_RemarkMap.Add("Da", "Danger.");
            s_RemarkMap.Add("De", "Desert.");
            s_RemarkMap.Add("Di", "Dieback.");
            s_RemarkMap.Add("Ex", "Exile Camp.");
            s_RemarkMap.Add("Fa", "Farming.");
            s_RemarkMap.Add("Fl", "Fluid Hydrographics (in place of water).");
            s_RemarkMap.Add("Fo", "Forbidden.");
            s_RemarkMap.Add("Fr", "Frozen.");
            s_RemarkMap.Add("Ga", "Garden World.");
            s_RemarkMap.Add("He", "Hellworld.");
            s_RemarkMap.Add("Hi", "High Population.");
            s_RemarkMap.Add("Ho", "Hot.");
            s_RemarkMap.Add("Ht", "High Technology.");
            s_RemarkMap.Add("Ic", "Ice Capped.");
            s_RemarkMap.Add("In", "Industrialized.");
            s_RemarkMap.Add("Lk", "Locked.");
            s_RemarkMap.Add("Lo", "Low Population.");
            s_RemarkMap.Add("Lt", "Low Technology.");
            s_RemarkMap.Add("Mi", "Mining.");
            s_RemarkMap.Add("Mr", "Military Rule.");
            s_RemarkMap.Add("Na", "Non-Agricultural.");
            s_RemarkMap.Add("Ni", "Non-Industrial.");
            s_RemarkMap.Add("Oc", "Ocean World.");
            s_RemarkMap.Add("Pa", "Pre-Agricultural.");
            s_RemarkMap.Add("Pe", "Penal Colony.");
            s_RemarkMap.Add("Ph", "Pre-High Population.");
            s_RemarkMap.Add("Pi", "Pre-Industrial.");
            s_RemarkMap.Add("Po", "Poor.");
            s_RemarkMap.Add("Pr", "Pre-Rich.");
            s_RemarkMap.Add("Px", "Prison, Exile Camp.");
            s_RemarkMap.Add("Pz", "Puzzle.");
            s_RemarkMap.Add("Re", "Reserve.");
            s_RemarkMap.Add("Ri", "Rich.");
            s_RemarkMap.Add("Rs", "Research Station.");
            s_RemarkMap.Add("RsA", "Research Station Alpha.");
            s_RemarkMap.Add("RsB", "Research Station Beta.");
            s_RemarkMap.Add("RsG", "Research Station Gamma.");
            s_RemarkMap.Add("Sa", "Satellite.");
            s_RemarkMap.Add("St", "Steppeworld.");
            s_RemarkMap.Add("Tr", "Tropic.");
            s_RemarkMap.Add("Tu", "Tundra.");
            s_RemarkMap.Add("Tz", "Twilight Zone.");
            s_RemarkMap.Add("Va", "Vacuum World.");
            s_RemarkMap.Add("Wa", "Water World.");
            s_RemarkMap.Add("Xb", "Xboat Station.");
            s_RemarkMap.Add("A", "Amber.");
            s_RemarkMap.Add("R", "Red.");

            //s_RemarkMap.Add("Fa", "Fascinating.");
            //s_RemarkMap.Add("Pr", "Prison World.");
        }

        public World()
        {
        }

        public World(string uwp, string name, int jumpDistance, TasZone tasZone)
        {
            uwp = uwp.Trim();
            if (!uwp.Contains("-") && uwp.Length == 8)
                uwp = uwp.Substring(0, 7) + "-" + uwp.Substring(7); //add the missing dash

            UWP = uwp;
            Name = name;
            JumpDistance = jumpDistance;

            if (tasZone == TasZone.Amber)
                Zone = "A";
            else if (tasZone == TasZone.Red)
                Zone = "R";

            AddMissingRemarks();
        }

        public string Allegiance { get; set; }

        public string AllegianceName { get; set; }

        public string Atmosphere
        {
            get
            {
                switch (AtmosphereCode.ToString())
                {
                    //star ports
                    case "0": return "No atmosphere.";
                    case "1": return "Trace.";
                    case "2": return "Very thin. Tainted.";
                    case "3": return "Very thin.";
                    case "4": return "Thin. Tainted.";
                    case "5": return "Thin. Breathable.";
                    case "6": return "Standard. Breathable.";
                    case "7": return "Standard. Tainted.";
                    case "8": return "Dense. Breathable";
                    case "9": return "Dense. Tainted.";
                    case "A": return "Exotic.";
                    case "B": return "Corrosive.";
                    case "C": return "Insidious.";
                    case "D": return "Dense, high.";
                    case "E": return "Ellipsoid.";
                    case "F": return "Thin, low.";
                    default: return "";
                }
            }
        }

        public EHex AtmosphereCode { get { return UWP[2]; } }

        public string AtmosphereDescription
        {
            get
            {
                switch (AtmosphereCode.ToString())
                {
                    //star ports
                    case "0": return "No atmosphere. Requires vacc suit.";
                    case "1": return "Trace. Requires vacc suit.";
                    case "2": return "Very thin. Tainted. Requires combination respirator/filter.";
                    case "3": return "Very thin. Requires respirator.";
                    case "4": return "Thin. Tainted. Requires filter mask.";
                    case "5": return "Thin. Breathable. ";
                    case "6": return "Standard. Breathable.";
                    case "7": return "Standard. Tainted. Requires filter mask.";
                    case "8": return "Dense. Breathable";
                    case "9": return "Dense. Tainted. Requires filter mask.";
                    case "A": return "Exotic. Requires special protective equipment.";
                    case "B": return "Corrosive. Requires protective suit.";
                    case "C": return "Insidious. Requires protective suit.";
                    case "D": return "Dense, high. Breathable above a minimum altitude.";
                    case "E": return "Ellipsoid. Breathable at certain latitudes.";
                    case "F": return "Thin, low. Breathable below certain altitudes.";
                    default: return "";
                }
            }
        }

        public string Bases { get; set; }

        public string Cx { get; set; }

        public string Ex { get; set; }

        public EHex GovernmentCode { get { return UWP[5]; } }

        public string GovernmentType
        {
            get
            {
                switch (GovernmentCode.ToString())
                {
                    case "0": return "No Government Structure.";
                    case "1": return "Company/Corporation.";
                    case "2": return "Participating Democracy.";
                    case "3": return "Self-Perpetuating Oligarchy.";
                    case "4": return "Representative Democracy.";
                    case "5": return "Feudal Technocracy.";
                    case "6": return "Captive Government / Colony.";
                    case "7": return "Balkanization.";
                    case "8": return "Civil Service Bureaucracy.";
                    case "9": return "Impersonal Bureaucracy.";
                    case "A": return "Charismatic Dictator.";
                    case "B": return "Non-Charismatic Dictator.";
                    case "C": return "Charismatic Oligarchy.";
                    case "D": return "Religious Dictatorship.";
                    case "E": return "Religious Autocracy.";
                    case "F": return "Totalitarian Oligarchy.";
                    case "G": return "Small Station or Facility. Aslan.";
                    case "H": return "Split Clan Control. Aslan.";
                    case "J": return "Single On-world Clan Control. Aslan.";
                    case "K": return "Single Multi-world Clan Control. Aslan.";
                    case "L": return "Major Clan Control. Aslan.";
                    case "M": return "Vassal Clan Control. Aslan.";
                    case "N": return "Major Vassal Clan Control. Aslan.";
                    case "P": return "Small Station or Facility. K�kree.";
                    case "Q": return "Krurruna or Krumanak Rule for Off-world Steppelord. K�kree.";
                    case "R": return "Steppelord On-world Rule. K�kree.";
                    case "S": return "Sept. Hiver.";
                    case "T": return "Unsupervised Anarchy. Hiver.";
                    case "U": return "Supervised Anarchy. Hiver.";
                    case "W": return "Committee. Hiver.";
                    case "X": return "Droyne Hierarchy. Droyne.";
                    default: return "";
                }
            }
        }

        public string Hex { get; set; }

        public string HexX { get { return Hex?.Substring(0, 2); } }

        public string HexY { get { return Hex?.Substring(2, 2); } }

        public string Hydrographics
        {
            get
            {
                switch (HydrographicsCode.ToString())
                {
                    //star ports
                    case "0": return "No water. Desert World. ";
                    case "1": return "10% water.";
                    case "2": return "20% water.";
                    case "3": return "30% water.";
                    case "4": return "40% water.";
                    case "5": return "50% water.";
                    case "6": return "60% water.";
                    case "7": return "70% water. Equivalent to Terra or Vland.";
                    case "8": return "80% water.";
                    case "9": return "90% water.";
                    case "A": return "100% water. Water World.";
                    default: return "";
                }
            }
        }

        public EHex HydrographicsCode { get { return UWP[3]; } }

        public string Ix { get; set; }

        public int JumpDistance { get; set; }

        public EHex LawCode { get { return UWP[6]; } }

        public string LawLevel => Tables.LawLevel(LawCode);

        public string LegacyBaseCode { get; set; }

        public string Name { get; set; }

        public string Nobility { get; set; }

        public string PBG { get; set; }

        public double Population
        {
            get { return PopulationMultiplier * PopulationExponent; }
        }

        public EHex PopulationCode { get { return UWP[4]; } }

        public double PopulationExponent
        {
            get { return Math.Pow(10, PopulationCode.Value); }
        }

        public int PopulationMultiplier
        {
            get
            {
                if (!string.IsNullOrEmpty(PBG))
                {
                    var temp = PBG.Substring(0, 1);
                    int result;
                    if (int.TryParse(temp, out result))
                        return result;
                }
                return 1;
            }
        }

        public int Quadrant { get; set; }

        public string Remarks
        {
            get
            {
                return m_Remarks;
            }
            set
            {
                m_Remarks = value ?? "";
                m_RemarksList.Clear();
                foreach (var item in m_Remarks.Split(' '))
                    m_RemarksList.Add(item);
            }
        }

        public string RemarksDescription
        {
            get
            {
                var source = (Remarks ?? "").Split(' ');
                var result = new List<string>();
                foreach (var remark in source)
                {
                    if (s_RemarkMap.TryGetValue(remark, out var des))
                        result.Add(string.Format("{0}: {1}", remark, des));
                    else
                        result.Add(remark);
                }
                //Add TAS zone
                if (Zone != null)
                {
                    if (s_RemarkMap.TryGetValue(Zone, out var des))
                        result.Add(string.Format("{0}: {1}", Zone, des));
                    else
                        result.Add(Zone);
                }
                return string.Join(" ", result);
            }
        }

        public HashSet<string> RemarksList => m_RemarksList;

        public int ResourceUnits { get; set; }

        public string Sector { get; set; }

        //These are added later
        public int? SectorX { get; set; }

        public int? SectorY { get; set; }

        public EHex SizeCode { get { return UWP[1]; } }

        public int SizeKM
        {
            get
            {
                switch (SizeCode.ToString())
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
                    default: return 0;
                }
            }
        }

        public string SS { get; set; }

        public string Starport => Tables.Starport(StarportCode);

        public EHex StarportCode { get { return UWP[0]; } }

        public string StarportDescription => Tables.StarportDescription(StarportCode);

        public string Stellar { get; set; }

        public int Subsector { get; set; }

        public string SubSectorIndex { get; set; }

        public string SubsectorName { get; set; }

        public EHex TechCode { get { return UWP[8]; } }

        public string TechLevel => Tables.TechLevel(TechCode);

        public string UWP { get; set; }

        public int Worlds { get; set; }

        public string Zone { get; set; }

        public void AddMissingRemarks()
        {
            var remarks = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            //Copy the existing list
            foreach (var item in RemarksList)
                remarks.Add(item);

            //Add any missing items
            if (AtmosphereCode.Between(4, 9) && HydrographicsCode.Between(4, 8) && PopulationCode.Between(5, 7))
                remarks.Add("Ag");
            if (SizeCode == 0 && AtmosphereCode == 0 && HydrographicsCode == 0)
                remarks.Add("As");
            if (PopulationCode == 0 & GovernmentCode == 0 && LawCode == 0)
                remarks.Add("Ba");
            if (AtmosphereCode >= 2 && HydrographicsCode == 0)
                remarks.Add("De");
            if (AtmosphereCode >= 10 && HydrographicsCode >= 1)
                remarks.Add("Fl");
            if (SizeCode.Between(6, 8) && AtmosphereCode.AnyOf(5, 6, 8) && HydrographicsCode.Between(5, 7))
                remarks.Add("Ga");
            if (PopulationCode >= 9)
                remarks.Add("Hi");
            if (TechCode >= 12)
                remarks.Add("Ht");
            if (AtmosphereCode.Between(0, 1) && HydrographicsCode >= 1)
                remarks.Add("Ic");
            if (AtmosphereCode.AnyOf(0, 1, 2, 4, 7, 9) && PopulationCode >= 9)
                remarks.Add("In");
            if (PopulationCode <= 3)
                remarks.Add("Lo");
            if (TechCode <= 5)
                remarks.Add("Lt");
            if (AtmosphereCode <= 3 && HydrographicsCode <= 3 && PopulationCode >= 6)
                remarks.Add("Na");
            if (PopulationCode <= 6)
                remarks.Add("Ni");
            if (AtmosphereCode.Between(2, 5) && HydrographicsCode <= 3)
                remarks.Add("Po");
            if (AtmosphereCode.AnyOf(6, 8) && PopulationCode.Between(6, 8) && GovernmentCode.Between(4, 9))
                remarks.Add("Ri");
            if (AtmosphereCode == 0)
                remarks.Add("Va");
            if (HydrographicsCode >= 10)
                remarks.Add("Wa");

            Remarks = string.Join(" ", remarks);
        }

        /// <summary>
        /// Determines whether world contains specified remark. This includes zones A and R.
        /// </summary>
        /// <param name="remark">The case insensitive remark.</param>
        /// <returns><c>true</c> if the specified remark contains remark; otherwise, <c>false</c>.</returns>
        public bool ContainsRemark(string remark)
        {
            return m_RemarksList.Contains(remark) || string.Compare(Zone, remark, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public int JumpDistanceKM(int jumpDistanceFactor = 100)
        {
            return SizeKM * jumpDistanceFactor;
        }

        /// <summary>
        /// Transits the time jump point.
        /// </summary>
        /// <param name="thrustRating">The thrust rating.</param>
        /// <param name="jumpDistanceFactor">The jump distance factor. Normally this is 100.</param>
        /// <returns>TimeSpan.</returns>
        public TimeSpan TransitTimeJumpPoint(int thrustRating, int jumpDistanceFactor = 100)
        {
            const double G = 9.80665; //meters per second per second
            var distanceM = (double)SizeKM * 1000 * jumpDistanceFactor;
            var timeSeconds = 2 * Math.Sqrt(distanceM / (G * thrustRating));
            return TimeSpan.FromSeconds(timeSeconds);
        }

        internal static void AddSophontCodes(IEnumerable<SophontCode> sophontCodes)
        {
            void TryAdd(string name, string description)
            {
                if (!s_RemarkMap.ContainsKey(name))
                    s_RemarkMap.Add(name, description);
            }

            foreach (var code in sophontCodes)
            {
                TryAdd(code.Code, code.Name);
                for (int i = 1; i <= 9; i++)
                {
                    TryAdd(code.Code + i, $"{code.Name}, Population {(i * 10)}%.");
                }
            }
        }
    }
}