using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools.Maps
{
    public class TravellerMapService : MapService
    {
        static HttpClient s_Client = new HttpClient();

        readonly bool m_FilterUnpopulatedSectors;

        ImmutableList<Sector> m_SectorList;

        ConcurrentDictionary<Tuple<int, int>, SectorMetadata> m_SectorMetadata = new ConcurrentDictionary<Tuple<int, int>, SectorMetadata>();

        ImmutableList<SophontCode> m_SophontCodes;

        ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>> m_WorldsInSector = new ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TravellerMapService"/> class.
        /// </summary>
        /// <param name="filterUnpopulatedSectors">if set to <c>true</c> [filter unpopulated sectors].</param>
        /// <param name="milieu">The milieu. The "default" is "M1105".</param>
        public TravellerMapService(bool filterUnpopulatedSectors, string milieu)
        {
            m_FilterUnpopulatedSectors = filterUnpopulatedSectors;
            Milieu = milieu;
        }

        public event EventHandler UniverseUpdated;

        public string Milieu { get; }

        public override async Task<SectorMetadata> FetchSectorMetadataAsync(int sectorX, int sectorY)
        {
            await FetchSophontCodesAsync();

            SectorMetadata result;
            var cacheKey = Tuple.Create(sectorX, sectorY);
            if (m_SectorMetadata.TryGetValue(cacheKey, out result))
                return result;

            var rawList = await s_Client.GetStringAsync(new Uri($"https://travellermap.com/api/metadata?sx={sectorX}&sy={sectorY}&milieu={Milieu}")).ConfigureAwait(false);
            result = JsonConvert.DeserializeObject<SectorMetadata>(rawList);
            m_SectorMetadata.TryAdd(cacheKey, result);
            return result;
        }

        public override async Task<ImmutableList<SophontCode>> FetchSophontCodesAsync()
        {
            if (m_SophontCodes != null)
                return m_SophontCodes;

            //https://travellermap.com/t5ss/sophonts

            var rawList = await s_Client.GetStringAsync(new Uri("https://travellermap.com/t5ss/sophonts")).ConfigureAwait(false);

            var set = JsonConvert.DeserializeObject<List<SophontCode>>(rawList);
            World.AddSophontCodes(set);
            m_SophontCodes = set.ToImmutableList();
            return m_SophontCodes;
        }

        public override async Task<ImmutableList<Sector>> FetchUniverseAsync()
        {
            if (m_SectorList != null)
                return m_SectorList;

            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            var rawSectorList = await s_Client.GetStringAsync(new Uri($"https://travellermap.com/api/universe?milieu={Milieu}")).ConfigureAwait(false);

            var sectors = JsonConvert.DeserializeObject<Universe>(rawSectorList).Sectors.Where(s => s.Names.First().Text != "Legend").ToList();

            m_SectorList = ImmutableList.CreateRange(sectors);

            if (m_FilterUnpopulatedSectors)
            {
                Task.Run(async () =>
               {
                   var populatedSectors = new List<Sector>();
                   foreach (var sector in m_SectorList)
                   {
                       try
                       {
                           var metadata = await FetchSectorMetadataAsync(sector.X, sector.Y);
                           if (metadata.Subsectors.Any(ss => !string.IsNullOrEmpty(ss.Name)))
                           {
                               var worlds = await FetchWorldsInSectorAsync(sector.X, sector.Y).ConfigureAwait(false);
                               if (worlds.Any(p => !string.IsNullOrWhiteSpace(p.Name)))
                                   populatedSectors.Add(sector);
                           }
                       }
                       catch
                       {
                           //we don't want to lose anything due to temporary service failures.
                           populatedSectors.Add(sector);
                       }
                   }

                   m_SectorList = ImmutableList.CreateRange(populatedSectors);

                   UniverseUpdated?.Invoke(this, EventArgs.Empty);
               }).RunConcurrently();
            }

            return m_SectorList;
        }

        public override async Task<ImmutableList<World>> FetchWorldsInSectorAsync(int sectorX, int sectorY)
        {
            ImmutableList<World> result;
            var cacheKey = Tuple.Create(sectorX, sectorY);

            if (m_WorldsInSector.TryGetValue(cacheKey, out result))
                return result;

            var baseUri = new Uri($"https://travellermap.com/api/sec?sx={sectorX}&sy={sectorY}&type=TabDelimited&milieu={Milieu}");
            var rawFile = await s_Client.GetStringAsync(baseUri).ConfigureAwait(false);
            if (rawFile.Length == 0)
                return ImmutableList.Create<World>();

            var memStream = new StringReader(rawFile);

            var temp = new List<World>();
            using (var parser = new TextFieldParser(memStream))
            {
                parser.SetDelimiters("\t");

                var headers = new Dictionary<string, int>();
                var index = 0;
                foreach (var header in parser.ReadFields())
                    headers[header] = index++;

                while (!parser.EndOfData)
                {
                    //Sector	SS	Hex	Name	UWP	Bases	Remarks	Zone	PBG	Allegiance	Stars	{Ix}	(Ex)	[Cx]	Nobility	W	RU
                    World world = new World();

                    var fields = parser.ReadFields();

                    world.SectorX = sectorX;
                    world.SectorY = sectorY;
                    //world.SectorCode = fields[headers["Sector"]];
                    world.SubSectorIndex = fields[headers["SS"]];
                    world.Hex = fields[headers["Hex"]];
                    world.Name = fields[headers["Name"]];
                    world.UWP = fields[headers["UWP"]];
                    world.Bases = fields[headers["Bases"]];
                    world.Remarks = fields[headers["Remarks"]];
                    world.Zone = fields[headers["Zone"]];
                    world.PBG = fields[headers["PBG"]];
                    world.Allegiance = fields[headers["Allegiance"]];
                    //world.Stars = fields[headers["Stars"]];
                    world.Ix = fields[headers["{Ix}"]];
                    world.Ex = fields[headers["(Ex)"]];
                    world.Cx = fields[headers["[Cx]"]];
                    world.Nobility = fields[headers["Nobility"]];

                    int.TryParse(fields[headers["W"]], out int tmp);
                    world.Worlds = tmp;

                    world.ResourceUnits = int.Parse(fields[headers["RU"]]);

                    world.AddMissingRemarks();

                    temp.Add(world);
                }
            }

            result = ImmutableList.CreateRange(temp);
            m_WorldsInSector.TryAdd(cacheKey, result);
            return result;
        }

        public override async Task<Sector> FindSectorByNameAsync(string sectorName)
        {
            foreach (var sector in await FetchUniverseAsync().ConfigureAwait(false))
            {
                foreach (var name in sector.Names)
                {
                    if (string.Compare(sectorName, name.Text, true) == 0)
                        return sector;
                }
            }
            return null;
        }

        public override async Task<List<World>> WorldsNearAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance)
        {
            //https://travellermap.com/api/jumpworlds?sector=Spinward%20Marches&hex=1910&jump=4

            var result = new List<World>();
            for (var jumpDistance = 0; jumpDistance <= maxJumpDistance; jumpDistance++)
            {
                var rawList = await s_Client.GetStringAsync(new Uri($"https://travellermap.com/api/jumpworlds?sx={sectorX}&sy={sectorY}&hx={hexX:00}&hy={hexY:00}&jump={jumpDistance}&milieu={Milieu}")).ConfigureAwait(false);

                var set = JsonConvert.DeserializeObject<WorldList>(rawList).Worlds;
                foreach (var world in set.Where(w => !result.Any(ww => ww.Hex == w.Hex && ww.Sector == w.Sector)))
                {
                    world.JumpDistance = jumpDistance;
                    result.Add(world);
                }
            }

            foreach (var world in result)
            {
                var sector = await FindSectorByNameAsync(world.Sector).ConfigureAwait(false);
                world.SectorX = sector.X;
                world.SectorY = sector.Y;

                world.AddMissingRemarks();
            }

            return result;
        }
    }
}