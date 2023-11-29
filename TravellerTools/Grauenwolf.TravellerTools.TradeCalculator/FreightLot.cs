using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.TradeCalculator;

public class FreightLot(int size, int shippingFee)
{
    public string? Contents { get; set; }
    public decimal DeclaredValue { get; set; }
    public int DueInDays { get; set; }

    public string DueInWeeks
    {
        get
        {
            var weeks = DueInDays / 7;
            var days = DueInDays % 7;

            if (days > 0)
                return $"{weeks} weeks, {days} days";
            else
                return $"{weeks} weeks";
        }
    }

    public int LateFee { get; set; }
    public int? MailRoll { get; set; }
    public string? Owner { get; set; }
    public Character? OwnerCharacter { get; set; }
    public bool OwnerIsMegacorp { get; set; }
    public int ShippingFee { get; set; } = shippingFee;
    public int Size { get; set; } = size;
}
