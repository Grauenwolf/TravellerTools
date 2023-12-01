using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Web.Data;

public record WorldModel(Milieu Milieu, World World)
{
    public List<World>? Destinations { get; set; }

    public StarportDetails? DownportDetails { get; set; }

    public StarportDetails? HighportDetails { get; set; }

    public TradeGoodsList? TradeList { get; set; }
}
