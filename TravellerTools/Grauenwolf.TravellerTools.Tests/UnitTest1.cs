using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public async Task FetchUniverse()
        //{
        //    var universe = await TravellerMapService.FetchUniverseAsync();
        //}



        //[TestMethod]

        //public async Task WorldsInSector()
        //{
        //    var universe = await TravellerMapService.FetchUniverseAsync();
        //    foreach (var item in universe.Where(s => s.Name.Contains("Spin")))
        //    {
        //        var coordinates = new[] { item.X, item.Y };
        //        var list = await TravellerMapService.FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);
        //        var result = list.Select(w => new WorldLocation() { Name = w.Name, Hex = w.Hex }).OrderBy(w => w.Name).ToList();

        //    }


        //}

        //[TestMethod]

        //public async Task Subsectors()
        //{
        //    var universe = await TravellerMapService.FetchUniverseAsync();
        //    foreach (var item in universe.Where(s => s.Name.Contains("Spin")))
        //    {
        //        var coordinates = new[] { item.X, item.Y };
        //        var meta = await TravellerMapService.FetchSectorMetadataAsync(coordinates[0], coordinates[1]);

        //        var worldList = await TravellerMapService.FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);

        //        var result = new List<Subsector>();
        //        foreach (var subsector in meta.Subsectors)
        //            if (worldList.Any(w => w.SubSectorIndex == subsector.Index && !string.IsNullOrWhiteSpace(w.Name)))
        //                result.Add(subsector);

        //    }
        //}

        //[TestMethod]

        //public async Task WorldsInSubsector(string sectorCoordinates, string subsectorIndex)
        //{
        //    var universe = await TravellerMapService.FetchUniverseAsync();
        //    foreach (var item in universe.Where(s => s.Name.Contains("Spin")))
        //    {
        //        var coordinates = new[] { item.X, item.Y }; 
        //        var list = await TravellerMapService.FetchWorldsInSectorAsync(coordinates[0], coordinates[1]);
        //        var result = list.Where(w => w.SubSectorIndex == subsectorIndex && !string.IsNullOrWhiteSpace(w.Name)).Select(w => new WorldLocation() { Name = w.Name, Hex = w.Hex }).OrderBy(w => w.Name).ToList();
        //    }
        //}


    }
}


