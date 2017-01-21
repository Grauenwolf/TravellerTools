using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Maps
{
    public abstract class MapService
    {
        public abstract Task<SectorMetadata> FetchSectorMetadataAsync(int sectorX, int sectorY);
        public abstract Task<ImmutableList<Sector>> FetchUniverseAsync();
        public abstract Task<ImmutableList<World>> FetchWorldsInSectorAsync(int sectorX, int sectorY);
        public abstract Task<Sector> FindSectorByNameAsync(string sectorName);
        public abstract Task<List<World>> WorldsNearAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance);
    }
}

