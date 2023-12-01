using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class UwpOptions : ModelBase
{
    public string AtmosphereCode { get => GetDefault("0"); set => Set(value); }

    [CalculatedField("StarportCode,SizeCode,AtmosphereCode,HydrographicsCode,PopulationCode,GovernmentCode,LawLevelCode,TechLevelCode")]
    public string CalculatedUwp => StarportCode + SizeCode + AtmosphereCode + HydrographicsCode + PopulationCode + GovernmentCode + LawLevelCode + "-" + TechLevelCode;

    public string GovernmentCode { get => GetDefault("0"); set => Set(value); }
    public string HydrographicsCode { get => GetDefault("0"); set => Set(value); }
    public string LawLevelCode { get => GetDefault("0"); set => Set(value); }
    public string PopulationCode { get => GetDefault("0"); set => Set(value); }
    public string? RawUwp { get => Get<string?>(); set => Set(value); }

    public string SizeCode { get => GetDefault("0"); set => Set(value); }

    public string StarportCode { get => GetDefault("A"); set => Set(value); }

    /// <summary>
    /// Gets or sets the TAS zone, which can be 'A' or 'R' (amber or red).
    /// </summary>
    public string TasZone { get => GetDefault(""); set => Set(value); }

    public string TechLevelCode { get => GetDefault("0"); set => Set(value); }

    [CalculatedField(nameof(RawUwp))]
    public bool UwpNotSelected => false; //TODO: This is true if RawUwp cannot be parsed.
}
