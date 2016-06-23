using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Maps
{

    public static class TravellerMapService
    {
        static ImmutableList<Sector> s_SectorList;

        public static async Task<Sector> FindSectorByNameAsync(string sectorName)
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

        public static async Task<ImmutableList<Sector>> FetchUniverseAsync()
        {
            if (s_SectorList != null)
                return s_SectorList;

            using (var client = new WebClient())
            {
                var rawSectorList = await client.DownloadStringTaskAsync(new Uri("http://travellermap.com/api/universe")).ConfigureAwait(false);

                var sectors = JsonConvert.DeserializeObject<Universe>(rawSectorList).Sectors.Where(s => s.Names.First().Text != "Legend").ToList();
                var populatedSectors = new List<Sector>();
                foreach (var sector in sectors)
                {
                    var worlds = await FetchWorldsInSectorAsync(sector.X, sector.Y).ConfigureAwait(false);
                    if (worlds.Any(p => !string.IsNullOrWhiteSpace(p.Name)))
                        populatedSectors.Add(sector);
                }


                s_SectorList = ImmutableList.CreateRange(populatedSectors);
                return s_SectorList;
            }
        }

        public static async Task<List<World>> WorldsNearAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance)
        {
            //http://travellermap.com/api/jumpworlds?sector=Spinward%20Marches&hex=1910&jump=4
            const string uriFormat = "http://travellermap.com/api/jumpworlds?sx={0}&sy={1}&hx={2:00}&hy={3:00}&jump={4}";

            var result = new List<World>();
            using (var client = new WebClient())
            {
                for (var jumpDistance = 0; jumpDistance <= maxJumpDistance; jumpDistance++)
                {
                    var rawList = await client.DownloadStringTaskAsync(new Uri(String.Format(uriFormat, sectorX, sectorY, hexX, hexY, jumpDistance))).ConfigureAwait(false);

                    var set = JsonConvert.DeserializeObject<WorldList>(rawList).Worlds;
                    foreach (var world in set.Where(w => !result.Any(ww => ww.Hex == w.Hex && ww.Sector == w.Sector)))
                    {
                        world.JumpDistance = jumpDistance;
                        result.Add(world);
                    }
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

        static ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>> s_WorldsInSector = new ConcurrentDictionary<Tuple<int, int>, ImmutableList<World>>();

        public static async Task<ImmutableList<World>> FetchWorldsInSectorAsync(int sectorX, int sectorY)
        {
            ImmutableList<World> result;
            var cacheKey = Tuple.Create(sectorX, sectorY);

            if (s_WorldsInSector.TryGetValue(cacheKey, out result))
                return result;


            using (var client = new WebClient())
            {
                var baseUri = new Uri(string.Format("http://travellermap.com/api/sec?sx={0}&sy={1}&type=TabDelimited", sectorX, sectorY));
                var rawFile = await client.DownloadStringTaskAsync(baseUri).ConfigureAwait(false);
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
            }
            s_WorldsInSector.TryAdd(cacheKey, result);
            return result;
        }

        static ConcurrentDictionary<Tuple<int, int>, SectorMetadata> s_SectorMetadata = new ConcurrentDictionary<Tuple<int, int>, SectorMetadata>();

        public static async Task<SectorMetadata> FetchSectorMetadataAsync(int sectorX, int sectorY)
        {
            SectorMetadata result;
            var cacheKey = Tuple.Create(sectorX, sectorY);
            if (s_SectorMetadata.TryGetValue(cacheKey, out result))
                return result;


            const string uriFormat = "http://travellermap.com/api/metadata?sx={0}&sy={1}";


            using (var client = new WebClient())
            {
                var rawList = await client.DownloadStringTaskAsync(new Uri(String.Format(uriFormat, sectorX, sectorY))).ConfigureAwait(false);
                result = JsonConvert.DeserializeObject<SectorMetadata>(rawList);
            }
            s_SectorMetadata.TryAdd(cacheKey, result);
            return result;
        }

    }

}

