namespace Grauenwolf.TravellerTools.TradeCalculator;

public class StarportDetails
{
    public EHex StarportCode;
    public int BerthingCost { get; set; }
    public int BerthingCostPerDay { get; set; }

    public string? BerthingWaitTimeCapital { get; set; }
    public string? BerthingWaitTimeSmall { get; set; }
    public string? BerthingWaitTimeStar { get; set; }
    public int CargoStorageCost { get; set; }
    public string CargoStorageSecurity => Tables.StarportCargoStorageSecurity(StarportCode);
    public string? FuelNotes { get; set; }
    public string? FuelWaitTimeCapital { get; set; }
    public string? FuelWaitTimeSmall { get; set; }
    public string? FuelWaitTimeStar { get; set; }
    public EHex LawCode { get; set; }
    public string LawLevel => Tables.LawLevelDescription(LawCode);

    public string PortEnforcement => Tables.PortEnforcement(PortEnforcementCode);
    public int PortEnforcementCode { get; set; }
    public string PortEnforcementDetails => Tables.PortEnforcementDetails(PortEnforcementCode);

    public int? RefinedFuelCost { get; set; }
    public int? UnrefinedFuelCost { get; set; }
}
