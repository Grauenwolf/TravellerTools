namespace Grauenwolf.TravellerTools.TradeCalculator;

public class FreightLot(int size, int shippingFee)
{
    public decimal ActualValue { get; set; }
    public string? Contents { get; set; }
    public int ShippingFee { get; set; } = shippingFee;
    public int Size { get; set; } = size;
    public int LateFee { get; set; }
}
