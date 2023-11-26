using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;

namespace Grauenwolf.TravellerTools.Web.Data;

public record PassengerModel(Milieu Milieu, World World, World Destination)
{
    public PassengerList? PassengerList { get; set; }
}