namespace Grauenwolf.TravellerTools.Maps;

public class World
{
    string? m_Bases;
    string? m_Remarks;

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

    //public World(string uwp, string name, int jumpDistance, TasZone tasZone)
    //{
    //    uwp = uwp.Trim();
    //    if (!uwp.Contains('-') && uwp.Length == 8)
    //        uwp = string.Concat(uwp.AsSpan(0, 7), "-", uwp.AsSpan(7)); //add the missing dash

    //    UWP = uwp;
    //    Name = name;
    //    JumpDistance = jumpDistance;

    //    if (tasZone == TasZone.Amber)
    //        Zone = "A";
    //    else if (tasZone == TasZone.Red)
    //        Zone = "R";

    //    AddMissingRemarks();
    //}

    public string Acceptance => Tables.Acceptance(AcceptanceCode);
    public EHex AcceptanceCode => Cx?.Length >= 3 ? new EHex(Cx[2]) : EHex.Unknown;
    public string? Allegiance { get; set; }
    public string? AllegianceName => Tables.Allegiance(Allegiance);
    public string Atmosphere => Tables.Atmosphere(AtmosphereCode);
    public EHex AtmosphereCode { get { return UWP?[2]; } }
    public string? AtmosphereDescription => Tables.AtmosphereDescription(AtmosphereCode);

    public string? Bases
    {
        get => m_Bases;
        set
        {
            m_Bases = value;
            MilitaryBases.Clear();
            if (m_Bases != null)
                foreach (var code in m_Bases)
                {
                    MilitaryBases.Add(Tables.MilitaryBase(code));
                }
        }
    }

    public string? Cx { get; set; }
    public string Efficiency => Tables.Efficiency(EfficiencyCode);
    public string? EfficiencyCode => Ex?.Length >= 6 ? Ex[4..6] : null;
    public string? Ex { get; set; }

    //public IReadOnlyList<Demographics> Demographics
    //{
    //    get
    //    {
    //        if (m_Demographics == null)
    //        {
    //            var result = new List<Demographics>(); ;
    //            foreach (var remark in RemarksList)
    //            {
    //                if (s_DemographicsMap.TryGetValue(remark, out var item))
    //                    result.Add(item);
    //            }
    //            m_Demographics = result.OrderByDescending(d => d.Population).ThenBy(d => d.Name).ToImmutableArray();
    //        }
    //        return m_Demographics;
    //    }
    //}
    public EHex GovernmentCode { get { return UWP?[5]; } }

    public string? GovernmentType => Tables.GovernmentDescriptionWithContraband(GovernmentCode);
    public string Gravity => Tables.Gravity(SizeCode);
    public string Heterogeneity => Tables.Heterogeneity(HeterogeneityCode);
    public EHex HeterogeneityCode => Cx?.Length >= 2 ? new EHex(Cx[1]) : EHex.Unknown;
    public string? Hex { get; set; }
    public string? HexX { get { return Hex?.Substring(0, 2); } }
    public string? HexY { get { return Hex?.Substring(2, 2); } }
    public string Hydrographics => Tables.Hydrographics(HydrographicsCode);
    public EHex HydrographicsCode { get { return UWP?[3]; } }
    public string Importance => Tables.Importance(ImportanceCode);
    public string? ImportanceCode => Ix?.Replace("{", "").Replace("}", "").Replace(" ", "");
    public string Infrastructure => Tables.Infrastructure(InfrastructureCode);
    public EHex InfrastructureCode => Ex?.Length >= 4 ? new EHex(Ex[3]) : EHex.Unknown;
    public string? Ix { get; set; }
    public int JumpDistance { get; set; }
    public string Labor => Tables.Labor(LaborCode);
    public EHex LaborCode => Ex?.Length >= 3 ? new EHex(Ex[2]) : EHex.Unknown;
    public EHex LawCode { get { return UWP?[6]; } }
    public string LawLevel => Tables.LawLevelDescription(LawCode);
    public string? LegacyBaseCode { get; set; }
    public List<MilitaryBase> MilitaryBases { get; } = new();
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
            RemarksList = new RemarkDictionary(m_Remarks);
        }
    }

    public string RemarksDescription
    {
        get
        {
            return string.Join(" ", RemarksList.Values.Select(x => $"{x.RemarkCode}: {x.Description}"));
        }
    }

    public RemarkDictionary RemarksList { get; private set; } = new();

    public string Resources => Tables.Resources(ResourcesCode);
    public EHex ResourcesCode => Ex?.Length >= 2 ? new EHex(Ex[1]) : EHex.Unknown;
    public int ResourceUnits { get; set; }
    public string? Sector { get; set; }

    public int? SectorX { get; set; }

    public int? SectorY { get; set; }
    public EHex SizeCode { get { return UWP?[1]; } }
    public int SizeKM => Tables.SizeKM(SizeCode);
    public string? SS { get => SubSectorIndex; set => SubSectorIndex = value; }
    public string? Starport => Tables.Starport(StarportCode);
    public EHex StarportCode { get { return UWP?[0]; } }
    public string? StarportDescription => Tables.StarportDescription(StarportCode);
    public string? Stellar { get; set; }
    public string Strangeness => Tables.Strangeness(StrangenessCode);
    public EHex StrangenessCode => Cx?.Length >= 4 ? new EHex(Cx[3]) : EHex.Unknown;

    public int Subsector { get; set; }
    public string? SubSectorIndex { get; set; }
    public string? SubsectorName { get; set; }
    public string Symbols => Tables.Symbols(SymbolsCode);
    public EHex SymbolsCode => Cx?.Length >= 5 ? new EHex(Cx[4]) : EHex.Unknown;
    public EHex TechCode { get { return UWP?[8]; } }

    public string? TechLevel => Tables.TechLevel(TechCode);

    public string? UWP { get; set; }

    public int Worlds { get; set; }

    public string? Zone { get; set; }

    public void AddMissingRemarks()
    {
        //Add any missing items
        if (AtmosphereCode.Between(4, 9) && HydrographicsCode.Between(4, 8) && PopulationCode.Between(5, 7))
            RemarksList.TryAdd("Ag");
        if (SizeCode == 0 && AtmosphereCode == 0 && HydrographicsCode == 0)
            RemarksList.TryAdd("As");
        if (PopulationCode == 0 & GovernmentCode == 0 && LawCode == 0)
            RemarksList.TryAdd("Ba");
        if (AtmosphereCode >= 2 && HydrographicsCode == 0)
            RemarksList.TryAdd("De");
        if (AtmosphereCode >= 10 && HydrographicsCode >= 1)
            RemarksList.TryAdd("Fl");
        if (SizeCode.Between(6, 8) && AtmosphereCode.AnyOf(5, 6, 8) && HydrographicsCode.Between(5, 7))
            RemarksList.TryAdd("Ga");
        if (PopulationCode >= 9)
            RemarksList.TryAdd("Hi");
        if (TechCode >= 12)
            RemarksList.TryAdd("Ht");
        if (AtmosphereCode.Between(0, 1) && HydrographicsCode >= 1)
            RemarksList.TryAdd("Ic");
        if (AtmosphereCode.AnyOf(0, 1, 2, 4, 7, 9) && PopulationCode >= 9)
            RemarksList.TryAdd("In");
        if (PopulationCode <= 3)
            RemarksList.TryAdd("Lo");
        if (TechCode <= 5)
            RemarksList.TryAdd("Lt");
        if (AtmosphereCode <= 3 && HydrographicsCode <= 3 && PopulationCode >= 6)
            RemarksList.TryAdd("Na");
        if (PopulationCode <= 6)
            RemarksList.TryAdd("Ni");
        if (AtmosphereCode.Between(2, 5) && HydrographicsCode <= 3)
            RemarksList.TryAdd("Po");
        if (AtmosphereCode.AnyOf(6, 8) && PopulationCode.Between(6, 8) && GovernmentCode.Between(4, 9))
            RemarksList.TryAdd("Ri");
        if (AtmosphereCode == 0)
            RemarksList.TryAdd("Va");
        if (HydrographicsCode >= 10)
            RemarksList.TryAdd("Wa");

        m_Remarks = string.Join(" ", RemarksList.Keys);
    }

    /// <summary>
    /// Determines whether world contains specified remark. This includes zones A and R.
    /// </summary>
    /// <param name="remarkCode">The case insensitive remark.</param>
    /// <returns><c>true</c> if the specified remark contains remark; otherwise, <c>false</c>.</returns>
    public bool ContainsRemark(string remarkCode)
    {
        return RemarksList.ContainsKey(remarkCode) || string.Compare(Zone, remarkCode, StringComparison.OrdinalIgnoreCase) == 0;
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
}