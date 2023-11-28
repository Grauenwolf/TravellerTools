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

namespace Grauenwolf.TravellerTools.Maps;

/// <summary>
/// Initializes a new instance of the <see cref="TravellerMapService"/> class.
/// </summary>
/// <param name="filterUnpopulatedSectors">if set to <c>true</c> [filter unpopulated sectors].</param>
/// <param name="milieu">The milieu. The "default" is "M1105".</param>
public class TravellerMapService(bool filterUnpopulatedSectors, string milieu) //: MapService
{
    readonly static HttpClient s_Client = new();

    readonly bool m_FilterUnpopulatedSectors = filterUnpopulatedSectors;

    readonly ConcurrentDictionary<Tuple<int, int>, SectorMetadata> m_SectorMetadata = new();
    readonly ConcurrentDictionary<Tuple<int, int>, ImmutableArray<Subsector>> m_SubsectorsInSector = new();
    readonly ConcurrentDictionary<Tuple<int, int>, ImmutableArray<World>> m_WorldsInSector = new();
    readonly ConcurrentDictionary<Tuple<int, int, string>, ImmutableArray<World>> m_WorldsInSubsector = new();
    ImmutableArray<Sector> m_SectorList;
    ImmutableArray<SophontCode> m_SophontCodes;

    public event EventHandler? UniverseUpdated;

    public string Milieu { get; } = milieu;

    public async Task<Sector?> FetchSectorAsync(string sectorHex)
    {
        var sectors = await FetchUniverseAsync();
        return sectors.FirstOrDefault(s => s.Hex == sectorHex);
    }

    public async Task<SectorMetadata> FetchSectorMetadataAsync(int sectorX, int sectorY)
    {
        await FetchSophontCodesAsync();

        var cacheKey = Tuple.Create(sectorX, sectorY);
        if (m_SectorMetadata.TryGetValue(cacheKey, out var result))
            return result;

        var rawList = await s_Client.GetStringAsync(new Uri($"https://travellermap.com/api/metadata?sx={sectorX}&sy={sectorY}&milieu={Milieu}")).ConfigureAwait(false);
        result = JsonConvert.DeserializeObject<SectorMetadata>(rawList)!;

        if (result.Subsectors?.Length == 0)
        {
            //Bad data. Create fake subsectors to account for it.
            result.Subsectors = [
                new("A", "A", 0),
                new("B", "B", 1),
                new("C", "C", 2),
                new("D", "D", 3),
                new("E", "E", 4),
                new("F", "F", 5),
                new("G", "G", 6),
                new("H", "H", 7),
                new("I", "I", 8),
                new("J", "J", 9),
                new("K", "K", 10),
                new("L", "L", 11),
                new("M", "M", 12),
                new("N", "N", 13),
                new("O", "O", 14),
                new("P", "P", 15),
            ];
        }

        m_SectorMetadata.TryAdd(cacheKey, result);
        return result;
    }

    public async Task<ImmutableArray<SophontCode>> FetchSophontCodesAsync()
    {
        if (m_SophontCodes != null)
            return m_SophontCodes;

        //https://travellermap.com/t5ss/sophonts

        var rawList = await s_Client.GetStringAsync(new Uri("https://travellermap.com/t5ss/sophonts")).ConfigureAwait(false);

        var set = JsonConvert.DeserializeObject<List<SophontCode>>(rawList)!;
        RemarkDictionary.AddSophontCodes(set);
        m_SophontCodes = set.ToImmutableArray();
        return m_SophontCodes;
    }

    public Task<ImmutableArray<Subsector>> FetchSubsectorsInSectorAsync(Sector sector)
    {
        if (sector == null)
            throw new ArgumentNullException(nameof(sector), $"{nameof(sector)} is null.");

        return FetchSubsectorsInSectorAsync(sector.X, sector.Y);
    }

    public async Task<ImmutableArray<Subsector>> FetchSubsectorsInSectorAsync(int sectorX, int sectorY)
    {
        var cacheKey = Tuple.Create(sectorX, sectorY);

        if (m_SubsectorsInSector.TryGetValue(cacheKey, out var result))
            return result;

        var meta = await FetchSectorMetadataAsync(sectorX, sectorY);
        if (meta == null)
            throw new ArgumentException($"No sector at sector coordinates {sectorX},{sectorY}");
        if (meta.Subsectors == null)
            throw new ArgumentException($"No subsectors at sector coordinates {sectorX},{sectorY}");

        var worldList = await FetchWorldsInSectorAsync(sectorX, sectorY, meta.Name ?? $"{meta.X},{meta.Y}");

        //Add missing sectors. This can happen if the sector isn't named, which will prevent it from being returned by FetchSectorMetadataAsync
        var missingList = worldList.Select(w => (w.SubSectorIndex, w.SubsectorName)).Distinct()
            .Where(ss => !meta.Subsectors.Any(s => s.Index == ss.SubSectorIndex))
            .Select(ss => new Subsector()
            {
                Name = ss.SubsectorName ?? ss.SubSectorIndex,
                Index = ss.SubSectorIndex,
                IndexNumber = ((int)ss.SubSectorIndex![0]) - 65
            })
            .ToList();

        var temp = new List<Subsector>();
        foreach (var subsector in meta.Subsectors)
            if (worldList.Any(w => w.SubSectorIndex == subsector.Index))
                temp.Add(subsector);

        //These are probably unnamed subsectors
        foreach (var subsector in missingList)
            temp.Add(subsector);

        result = temp.OrderBy(s => s.Name).ToImmutableArray();

        m_SubsectorsInSector.TryAdd(cacheKey, result);

        return result;
    }

    public async Task<ImmutableArray<Sector>> FetchUniverseAsync()
    {
        if (m_SectorList != null)
            return m_SectorList;

        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

        var rawSectorList = await s_Client.GetStringAsync(new Uri($"https://travellermap.com/api/universe?milieu={Milieu}")).ConfigureAwait(false);

        var sectors = JsonConvert.DeserializeObject<Universe>(rawSectorList)!.Sectors.Where(s => s.Names?.First().Text != "Legend").ToList();

        m_SectorList = ImmutableArray.CreateRange(sectors.OrderBy(s => s.Name));

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
                        if (metadata.Subsectors?.Any(ss => !string.IsNullOrEmpty(ss.Name)) ?? false)
                        {
                            var worlds = await FetchWorldsInSectorAsync(sector.X, sector.Y, sector.Name ?? ($"{sector.X},{sector.Y}")).ConfigureAwait(false);
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

                m_SectorList = ImmutableArray.CreateRange(populatedSectors.OrderBy(s => s.Name));

                UniverseUpdated?.Invoke(this, EventArgs.Empty);
            }).RunConcurrently();
        }

        return m_SectorList;
    }

    public async Task<World?> FetchWorldAsync(string sectorHex, string planetHex)
    {
        await FetchSophontCodesAsync();

        var cord = sectorHex.Split(',').Select(s => int.Parse(s)).ToArray();
        var hexX = int.Parse(planetHex[..2]);
        var hexY = int.Parse(planetHex[2..4]);

        var worlds = await WorldsNearAsync(cord[0], cord[1], hexX, hexY, 0);
        return worlds.FirstOrDefault();

        //var worlds = await FetchWorldsInSectorAsync(cord[0], cord[1]);
        //return worlds.FirstOrDefault(w => w.Hex == planetHex);
    }

    public async Task<ImmutableArray<World>> FetchWorldsInSectorAsync(int sectorX, int sectorY, string sectorName)
    {
        var cacheKey = Tuple.Create(sectorX, sectorY);

        if (m_WorldsInSector.TryGetValue(cacheKey, out var result))
            return result;

        var baseUri = new Uri($"https://travellermap.com/api/sec?sx={sectorX}&sy={sectorY}&type=TabDelimited&milieu={Milieu}");
        var rawFile = await s_Client.GetStringAsync(baseUri).ConfigureAwait(false);
        if (rawFile.Length == 0)
            return ImmutableArray.Create<World>();

        var memStream = new StringReader(rawFile);

        var temp = new List<World>();
        using (var parser = new TextFieldParser(memStream))
        {
            parser.SetDelimiters("\t");

            var headers = new Dictionary<string, int>();
            var index = 0;
            foreach (var header in parser.ReadFields()!)
                headers[header] = index++;

            while (!parser.EndOfData)
            {
                //Sector	SS	Hex	Name	UWP	Bases	Remarks	Zone	PBG	Allegiance	Stars	{Ix}	(Ex)	[Cx]	Nobility	W	RU
                var world = new World();

                var fields = parser.ReadFields()!;

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

                if (int.TryParse(fields[headers["W"]], out int tmp))
                    world.Worlds = tmp;

                if (int.TryParse(fields[headers["RU"]], out int tmp2))
                    world.ResourceUnits = tmp2;

                world.AddMissingRemarks();

                if (string.IsNullOrWhiteSpace(world.Name))
                    world.Name = sectorName + " " + world.Hex;

                temp.Add(world);
            }
        }

        result = ImmutableArray.CreateRange(temp);
        m_WorldsInSector.TryAdd(cacheKey, result);
        return result;
    }

    public async Task<ImmutableArray<World>> FetchWorldsInSubsectorAsync(int sectorX, int sectorY, string subSectorIndex, string sectorName)
    {
        var cacheKey = Tuple.Create(sectorX, sectorY, subSectorIndex);

        if (m_WorldsInSubsector.TryGetValue(cacheKey, out var result))
            return result;

        result = (await FetchWorldsInSectorAsync(sectorX, sectorY, sectorName))
            .Where(w => w.SubSectorIndex == subSectorIndex)
            .OrderBy(w => w.Name)
            .ToImmutableArray();

        m_WorldsInSubsector.TryAdd(cacheKey, result);
        return result;
    }

    public async Task<Sector?> FindSectorByNameAsync(string sectorName)
    {
        foreach (var sector in await FetchUniverseAsync().ConfigureAwait(false))
        {
            if (sector.Names != null)
                foreach (var name in sector.Names)
                {
                    if (string.Compare(sectorName, name.Text, true) == 0)
                        return sector;
                }
        }
        return null;
    }

    public async Task<List<World>> WorldsNearAsync(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance)
    {
        //https://travellermap.com/api/jumpworlds?sector=Spinward%20Marches&hex=1910&jump=4

        var result = new List<World>();
        for (var jumpDistance = 0; jumpDistance <= maxJumpDistance; jumpDistance++)
        {
            var rawList = await s_Client.GetStringAsync(new Uri($"https://travellermap.com/api/jumpworlds?sx={sectorX}&sy={sectorY}&hx={hexX:00}&hy={hexY:00}&jump={jumpDistance}&milieu={Milieu}")).ConfigureAwait(false);

            var set = JsonConvert.DeserializeObject<WorldList>(rawList)!.Worlds;
            if (set != null)
                foreach (var world in set.Where(w => !result.Any(ww => ww.Hex == w.Hex && ww.Sector == w.Sector)))
                {
                    world.JumpDistance = jumpDistance;
                    result.Add(world);
                }
        }

        foreach (var world in result)
        {
            if (world.Sector != null)
            {
                var sector = await FindSectorByNameAsync(world.Sector).ConfigureAwait(false);
                world.SectorX = sector?.X;
                world.SectorY = sector?.Y;

                world.AddMissingRemarks();
            }
        }

        return result;
    }
}
