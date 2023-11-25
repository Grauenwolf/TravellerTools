using System;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Maps
{
    public class World
    {
        static readonly Dictionary<string, string> s_RemarkMap = new(StringComparer.OrdinalIgnoreCase);
        readonly HashSet<string> m_RemarksList = new(StringComparer.OrdinalIgnoreCase);
        private string? m_Remarks;

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

        public World(string uwp, string name, int jumpDistance, string? tasZone)
        {
            uwp = uwp.Trim();
            if (!uwp.Contains('-') && uwp.Length == 8)
                uwp = string.Concat(uwp.AsSpan(0, 7), "-", uwp.AsSpan(7)); //add the missing dash

            UWP = uwp;
            Name = name;
            JumpDistance = jumpDistance;

            Zone = tasZone;

            AddMissingRemarks();
        }

        public World(string uwp, string name, int jumpDistance, TasZone tasZone)
        {
            uwp = uwp.Trim();
            if (!uwp.Contains('-') && uwp.Length == 8)
                uwp = string.Concat(uwp.AsSpan(0, 7), "-", uwp.AsSpan(7)); //add the missing dash

            UWP = uwp;
            Name = name;
            JumpDistance = jumpDistance;

            if (tasZone == TasZone.Amber)
                Zone = "A";
            else if (tasZone == TasZone.Red)
                Zone = "R";

            AddMissingRemarks();
        }

        public string? Allegiance { get; set; }

        public string? AllegianceName { get; set; }

        public string Atmosphere => Tables.Atmosphere(AtmosphereCode);

        public EHex AtmosphereCode { get { return UWP?[2]; } }

        public string? AtmosphereDescription => Tables.AtmosphereDescription(AtmosphereCode);

        public string? Bases { get; set; }

        public string? Cx { get; set; }

        public string? Ex { get; set; }

        public EHex GovernmentCode { get { return UWP?[5]; } }

        public string? GovernmentType => Tables.GovernmentDescriptionWithContraband(GovernmentCode);

        public string? Hex { get; set; }

        public string? HexX { get { return Hex?.Substring(0, 2); } }

        public string? HexY { get { return Hex?.Substring(2, 2); } }

        public string Hydrographics => Tables.Hydrographics(HydrographicsCode);

        public EHex HydrographicsCode { get { return UWP?[3]; } }

        public string? Ix { get; set; }

        public int JumpDistance { get; set; }

        public EHex LawCode { get { return UWP?[6]; } }

        public string LawLevel => Tables.LawLevelDescription(LawCode);

        public string? LegacyBaseCode { get; set; }

        public string? Name { get; set; }

        public string? Nobility { get; set; }

        public string? PBG { get; set; }

        public double Population
        {
            get { return PopulationMultiplier * PopulationExponent; }
        }

        public EHex PopulationCode { get { return UWP?[4]; } }

        public double PopulationExponent => Tables.PopulationExponent(PopulationCode);

        public int PopulationMultiplier
        {
            get
            {
                if (!string.IsNullOrEmpty(PBG))
                {
                    var temp = PBG[..1];
                    if (int.TryParse(temp, out var result))
                        return result;
                }
                return 1;
            }
        }

        public int Quadrant { get; set; }

        public string? Remarks
        {
            get => m_Remarks;
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

        public string? Sector { get; set; }

        //These are added later
        public int? SectorX { get; set; }

        public int? SectorY { get; set; }

        public EHex SizeCode { get { return UWP?[1]; } }

        public int SizeKM => Tables.SizeKM(SizeCode);

        public string? SS { get => SubSectorIndex; set => SubSectorIndex = value; }

        public string? Starport => Tables.Starport(StarportCode);

        public EHex StarportCode { get { return UWP?[0]; } }

        public string? StarportDescription => Tables.StarportDescription(StarportCode);

        public string? Stellar { get; set; }

        public int Subsector { get; set; }

        public string? SubSectorIndex { get; set; }
        public string? SubsectorName { get; set; }

        public EHex TechCode { get { return UWP?[8]; } }

        public string? TechLevel => Tables.TechLevel(TechCode);

        public string? UWP { get; set; }

        public int Worlds { get; set; }

        public string? Zone { get; set; }

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
        /// Transit time to jump point.
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
            static void TryAdd(string name, string description)
            {
                if (!s_RemarkMap.ContainsKey(name))
                    s_RemarkMap.Add(name, description);
            }

            foreach (var code in sophontCodes)
            {
                if (code.Code != null)
                {
                    TryAdd(code.Code, code.Name ?? "<unknown>");
                    for (int i = 1; i <= 9; i++)
                    {
                        TryAdd(code.Code + i, $"{code.Name}, Population {i * 10}%.");
                    }
                }
            }
        }
    }
}
