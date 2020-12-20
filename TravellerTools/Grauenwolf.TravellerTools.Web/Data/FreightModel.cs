using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public record FreightModel(Milieu Milieu, World World, World Destination)
    {
        public FreightList? FreightList { get; set; }
    }
}
