namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class FreightLot
    {
        public FreightLot(int size, int shippingFee)
        {
            Size = size;
            ShippingFee = shippingFee;
        }

        public decimal ActualValue { get; set; }
        public string? Contents { get; set; }
        public int ShippingFee { get; set; }
        public int Size { get; set; }
    }
}
