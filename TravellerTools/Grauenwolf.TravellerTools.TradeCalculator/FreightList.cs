using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.TradeCalculator;

public class FreightList
{
    public int Incidental { get; set; }
    public List<FreightLot> Lots { get; } = new();
    public int Major { get; set; }
    public int Minor { get; set; }
}