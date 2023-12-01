using Grauenwolf.TravellerTools.Maps;

namespace Grauenwolf.TravellerTools.TradeCalculator;

/// <summary>
/// A manifest is the cargo, people, etc. that want to travel from one location to another.
/// </summary>
public class Manifest
{
    public List<TradeBid> Bids { get; } = new();
    public World? Destination { get; set; }
    public FreightList? FreightList { get; set; }
    public List<TradeOffer> Offers { get; } = new();
    public World? Origin { get; set; }
    public PassengerList? PassengerList { get; set; }
}
