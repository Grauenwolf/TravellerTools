using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;

namespace Grauenwolf.TravellerTools.Web.Data;

public record WorldModel(Milieu Milieu, World World)
{
    public List<World>? Destinations { get; set; }

    public StarportFacilities? StarportFacilities { get; set; }

    public TradeGoodsList? TradeList { get; set; }
}
