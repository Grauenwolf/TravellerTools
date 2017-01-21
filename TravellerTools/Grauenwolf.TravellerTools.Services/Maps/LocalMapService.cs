using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Maps
{
    public class LocalMapService : MapService
    {
        ImmutableList<Sector> m_SectorList;
        ConcurrentDictionary<Tuple<int, int>, SectorMetadata> m_SectorMetadata = new ConcurrentDictionary<Tuple<int, int>, SectorMetadata>();
        ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>> m_WorldsInSector = new ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>>();


        public LocalMapService(FileInfo universeFile)
        {

        }

        public override Task<SectorMetadata> FetchSectorMetadataAsync(int sectorX, int sectorY)
        {
            throw new NotImplementedException();
        }

        public override Task<ImmutableList<Sector>> FetchUniverseAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<ImmutableList<World>> FetchWorldsInSectorAsync(int sectorX, int sectorY)
        {
            throw new NotImplementedException();
        }

        public override Task<Sector> FindSectorByNameAsync(string sectorName)
        {
            throw new NotImplementedException();
        }

        public override Task<List<World>> WorldsNearAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance)
        {
            throw new NotImplementedException();
        }
    }
}

