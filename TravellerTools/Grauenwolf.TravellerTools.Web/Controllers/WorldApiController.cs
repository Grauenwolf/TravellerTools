using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Grauenwolf.TravellerTools.Web.Controllers
{
    [RoutePrefix("WorldApi")]
    public class WorldApiController : ApiController
    {

        [HttpGet]
        [Route("Sectors")]
        public async Task<IReadOnlyList<Sector>> Sectors( string milieu = "M1105")
        {
            return await Global.GetMapService(milieu).FetchUniverseAsync();
        }

        [HttpGet]
        [Route("WorldsInSector")]
        public async Task<IReadOnlyList<WorldLocation>> WorldsInSector(string sectorCoordinates, string milieu = "M1105")
        {
            var coordinates = sectorCoordinates.Split(',').Select(s => int.Parse(s)).ToList();
            var list = await Global.GetMapService(milieu).FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);
            return list.Select(w => new WorldLocation() { Name = w.Name, Hex = w.Hex }).OrderBy(w => w.Name).ToList();
        }

        [HttpGet]
        [Route("Subsectors")]
        public async Task<IReadOnlyList<Subsector>> Subsectors(string sectorCoordinates, string milieu = "M1105")
        {
            var coordinates = sectorCoordinates.Split(',').Select(s => int.Parse(s)).ToList();
            var meta = await Global.GetMapService(milieu).FetchSectorMetadataAsync(coordinates[0], coordinates[1]);

            var worldList = await Global.GetMapService(milieu).FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);

            var result = new List<Subsector>();
            foreach (var subsector in meta.Subsectors)
                if (worldList.Any(w => w.SubSectorIndex == subsector.Index && !string.IsNullOrWhiteSpace(w.Name)))
                    result.Add(subsector);

            return result;
        }

        [HttpGet]
        [Route("WorldsInSubsector")]
        public async Task<IReadOnlyList<WorldLocation>> WorldsInSubsector(string sectorCoordinates, string subsectorIndex, string milieu = "M1105")
        {
            var coordinates = sectorCoordinates.Split(',').Select(s => int.Parse(s)).ToList();
            var list = await Global.GetMapService(milieu).FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);
            return list.Where(w => w.SubSectorIndex == subsectorIndex && !string.IsNullOrWhiteSpace(w.Name)).Select(w => new WorldLocation() { Name = w.Name, Hex = w.Hex }).OrderBy(w => w.Name).ToList();

        }

    }
}
