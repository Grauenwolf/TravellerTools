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
        [Route("WorldsInSector")]
        public async Task<IReadOnlyList<WorldLocation>> WorldsInSector(string sectorCoordinates)
        {
            var coordinates = sectorCoordinates.Split(',').Select(s => int.Parse(s)).ToList();
            var list = await Global.MapService.FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);
            return list.Select(w => new WorldLocation() { Name = w.Name, Hex = w.Hex }).OrderBy(w => w.Name).ToList();
        }

        [HttpGet]
        [Route("Subsectors")]
        public async Task<IReadOnlyList<Subsector>> Subsectors(string sectorCoordinates)
        {
            var coordinates = sectorCoordinates.Split(',').Select(s => int.Parse(s)).ToList();
            var meta = await Global.MapService.FetchSectorMetadataAsync(coordinates[0], coordinates[1]);

            var worldList = await Global.MapService.FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);

            var result = new List<Subsector>();
            foreach (var subsector in meta.Subsectors)
                if (worldList.Any(w => w.SubSectorIndex == subsector.Index && !string.IsNullOrWhiteSpace(w.Name)))
                    result.Add(subsector);

            return result;
        }

        [HttpGet]
        [Route("WorldsInSubsector")]
        public async Task<IReadOnlyList<WorldLocation>> WorldsInSubsector(string sectorCoordinates, string subsectorIndex)
        {
            var coordinates = sectorCoordinates.Split(',').Select(s => int.Parse(s)).ToList();
            var list = await Global.MapService.FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);
            return list.Where(w => w.SubSectorIndex == subsectorIndex && !string.IsNullOrWhiteSpace(w.Name)).Select(w => new WorldLocation() { Name = w.Name, Hex = w.Hex }).OrderBy(w => w.Name).ToList();

        }

    }
}
