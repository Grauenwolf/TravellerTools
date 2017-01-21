using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools.Maps
{
    public class TravellerMapService : MapService
    {
        public TravellerMapService(bool filterUnpopulatedSectors)
        {
            m_FilterUnpopulatedSectors = filterUnpopulatedSectors;
        }

        static HttpClient s_Client = new HttpClient();
        ImmutableList<Sector> m_SectorList;
        ConcurrentDictionary<Tuple<int, int>, SectorMetadata> m_SectorMetadata = new ConcurrentDictionary<Tuple<int, int>, SectorMetadata>();
        ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>> m_WorldsInSector = new ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>>();
        readonly bool m_FilterUnpopulatedSectors;

        public override async Task<SectorMetadata> FetchSectorMetadataAsync(int sectorX, int sectorY)
        {
            SectorMetadata result;
            var cacheKey = Tuple.Create(sectorX, sectorY);
            if (m_SectorMetadata.TryGetValue(cacheKey, out result))
                return result;


            const string uriFormat = "http://travellermap.com/api/metadata?sx={0}&sy={1}";


            var rawList = await s_Client.GetStringAsync(new Uri(String.Format(uriFormat, sectorX, sectorY))).ConfigureAwait(false);
            result = JsonConvert.DeserializeObject<SectorMetadata>(rawList);
            m_SectorMetadata.TryAdd(cacheKey, result);
            return result;
        }

        public override async Task<ImmutableList<Sector>> FetchUniverseAsync()
        {
            if (m_SectorList != null)
                return m_SectorList;


            var rawSectorList = await s_Client.GetStringAsync(new Uri("http://travellermap.com/api/universe")).ConfigureAwait(false);

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


            var baseUri = new Uri(string.Format("http://travellermap.com/api/sec?sx={0}&sy={1}&type=TabDelimited", sectorX, sectorY));
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
                    int tmp;
                    int.TryParse(fields[headers["W"]], out tmp);
                    world.Worlds = tmp;
                    world.ResourceUnits = int.Parse(fields[headers["RU"]]);



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
            //http://travellermap.com/api/jumpworlds?sector=Spinward%20Marches&hex=1910&jump=4
            const string uriFormat = "http://travellermap.com/api/jumpworlds?sx={0}&sy={1}&hx={2:00}&hy={3:00}&jump={4}";

            var result = new List<World>();
            for (var jumpDistance = 0; jumpDistance <= maxJumpDistance; jumpDistance++)
            {
                var rawList = await s_Client.GetStringAsync(new Uri(String.Format(uriFormat, sectorX, sectorY, hexX, hexY, jumpDistance))).ConfigureAwait(false);

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
            }

            return result;
        }
    }

}

