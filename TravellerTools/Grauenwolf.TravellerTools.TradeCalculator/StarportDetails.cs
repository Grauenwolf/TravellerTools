namespace Grauenwolf.TravellerTools.TradeCalculator;

public class StarportDetails
{
    public int BerthingCost { get; set; }
    public int BerthingCostPerDay { get; set; }

    public string? BerthingWaitTimeCapital { get; set; }
    public string? BerthingWaitTimeSmall { get; set; }
    public string? BerthingWaitTimeStar { get; set; }

    public string? FuelNotes { get; set; }
    public string? FuelWaitTimeCapital { get; set; }
    public string? FuelWaitTimeSmall { get; set; }
    public string? FuelWaitTimeStar { get; set; }

    public int? RefinedFuelCost { get; set; }
    public string? RepairWaitTimeCapital { get; set; }
    public string? RepairWaitTimeSmall { get; set; }
    public string? RepairWaitTimeStar { get; set; }
    public int? UnrefinedFuelCost { get; set; }

    public string? WarehousingWaitTimeCapital { get; set; }
    public string? WarehousingWaitTimeSmall { get; set; }
    public string? WarehousingWaitTimeStar { get; set; }
}
