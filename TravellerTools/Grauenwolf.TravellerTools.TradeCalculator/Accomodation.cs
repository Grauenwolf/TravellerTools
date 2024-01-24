namespace Grauenwolf.TravellerTools.TradeCalculator;

public class Accomodation
{
    public Accomodation(string? type, int costPerDay)
    {
        Type = type;
        CostPerDay = costPerDay;
    }

    public int CostPerDay { get; set; }
    public string? Type { get; set; }
}
