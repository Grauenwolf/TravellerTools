using Grauenwolf.TravellerTools.Maps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grauenwolf.TravellerTools.Tests;

[TestClass]
public class TravellerWorldsUrlBuilderTests
{
    [TestMethod]
    public void Build_IncludesEncodedCoreFieldsAndRepeatedTradeCodes()
    {
        var world = new World
        {
            Hex = "1827",
            Sector = "Solomani Rim",
            Name = "Terra",
            UWP = "A867A69-F",
            Ix = "{ 4 }",
            Ex = "(H9C+4)",
            Cx = "[BE6G]",
            PBG = "114",
            Worlds = 10,
            Bases = "NWQ",
            Zone = "",
            Nobility = "BEf",
            Allegiance = "ImDs",
            Stellar = "G2 V",
            Remarks = "Hi Ga [Solomani] Dolp0 Mr Ht"
        };

        var url = TravellerWorldsUrlBuilder.Build(world);

        StringAssert.StartsWith(url, "https://www.travellerworlds.com/?");
        StringAssert.Contains(url, "hex=1827");
        StringAssert.Contains(url, "sector=Solomani%20Rim");
        StringAssert.Contains(url, "name=Terra");
        StringAssert.Contains(url, "uwp=A867A69-F");
        StringAssert.Contains(url, "tc=Hi");
        StringAssert.Contains(url, "tc=Ga");
        StringAssert.Contains(url, "tc=%5BSolomani%5D");
        StringAssert.Contains(url, "tc=Dolp0");
        StringAssert.Contains(url, "tc=Mr");
        StringAssert.Contains(url, "tc=Ht");
        StringAssert.Contains(url, "iX=4");
        StringAssert.Contains(url, "eX=%28H9C%2B4%29");
        StringAssert.Contains(url, "cX=%5BBE6G%5D");
        StringAssert.Contains(url, "pbg=114");
        StringAssert.Contains(url, "worlds=10");
        StringAssert.Contains(url, "bases=NW");
        StringAssert.Contains(url, "travelZone=");
        StringAssert.Contains(url, "nobz=BEf");
        StringAssert.Contains(url, "allegiance=ImDs");
        StringAssert.Contains(url, "stellar=G2%20V");
    }

    [TestMethod]
    public void Build_UsesHexDerivedSeedWhenHexIsValid()
    {
        var world = new World { Hex = "1827", UWP = "A000000-0", Name = "Seed Hex" };

        var url = TravellerWorldsUrlBuilder.Build(world);

        StringAssert.Contains(url, "seed=1");
    }

    [TestMethod]
    public void Build_UsesQuerySeedWhenHexMissing()
    {
        var world = new World { UWP = "A000000-0", Name = "Seed Query" };

        var url = TravellerWorldsUrlBuilder.Build(world);

        StringAssert.Contains(url, "seed=1");
    }

    [TestMethod]
    public void Build_OmitsTravelZoneWhenNull()
    {
        var world = new World { Hex = "1827", UWP = "A000000-0", Name = "No Zone", Zone = null };

        var url = TravellerWorldsUrlBuilder.Build(world);

        Assert.IsFalse(url.Contains("travelZone="));
    }
}

