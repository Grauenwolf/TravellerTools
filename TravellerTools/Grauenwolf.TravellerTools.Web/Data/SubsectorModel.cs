using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public record SubsectorModel(Milieu Milieu, SectorMetadata Sector, Subsector Subsector, ImmutableArray<World> Worlds)
    {
    }
}
