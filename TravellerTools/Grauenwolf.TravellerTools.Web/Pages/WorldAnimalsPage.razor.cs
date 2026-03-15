using Grauenwolf.TravellerTools.Animals.Mgt;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldAnimalsPage
{
    [Parameter]
    public string? MilieuCode { get; set; }

    [Parameter]
    public string? PlanetHex { get; set; }

    [Parameter]
    public string? SectorHex { get; set; }

    public string? AnimalType { get => Get<string?>(); set => Set(value, true); }
    public Dictionary<string, List<Animal>>? Animals { get => Get<Dictionary<string, List<Animal>>?>(); set => Set(value); }
    public int? Seed { get => Get<int?>(); set => Set(value, true); }
    public string? TerrainType { get => Get<string?>(); set => Set(value, true); }
    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get; set; }

    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected IReadOnlyList<string> AnimalTypeList => AnimalBuilderMgt.AnimalTypeList.Select(x => x.Name).OrderBy(x => x).ToList();
    protected string Permalink
    {
        get
        {
            string uri;
            if (Uwp != null)
                uri = $"/uwp/{Uwp}/animals?tasZone={TasZone}";
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/animals";

            if (!string.IsNullOrWhiteSpace(TerrainType))
                uri = QueryHelpers.AddQueryString(uri, "terrainType", TerrainType);
            if (!string.IsNullOrWhiteSpace(AnimalType))
                uri = QueryHelpers.AddQueryString(uri, "animalType", AnimalType);

            return QueryHelpers.AddQueryString(uri, "seed", (Seed ?? 0).ToString());
        }
    }
    protected IReadOnlyList<string> TerrainTypeList => AnimalBuilderMgt.TerrainTypeList.Select(x => x.Name).OrderBy(x => x).ToList();

    protected void GenerateAnimals()
    {
        if (Seed == null)
            return;

        var dice = new Dice(Seed.Value);

        if (!string.IsNullOrWhiteSpace(TerrainType) && !string.IsNullOrWhiteSpace(AnimalType))
        {
            Animals = new Dictionary<string, List<Animal>>()
            {
                [TerrainType] =
                [
                    AnimalBuilderMgt.BuildAnimal(dice, TerrainType, AnimalType)
                ]
            };
        }
        else if (!string.IsNullOrWhiteSpace(TerrainType))
        {
            Animals = new Dictionary<string, List<Animal>>()
            {
                [TerrainType] = AnimalBuilderMgt.BuildTerrainSet(dice, TerrainType)
            };
        }
        else
        {
            Animals = AnimalBuilderMgt.BuildPlanetSet(dice);
        }
    }

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("seed", out int seed))
            Seed = seed;
        else
            Seed = (new Random()).Next();

        if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            TasZone = tasZone;

        if (Navigation.TryGetQueryString("terrainType", out string terrainType))
            TerrainType = terrainType;

        if (Navigation.TryGetQueryString("animalType", out string animalType))
            AnimalType = animalType;
    }

    protected void OnReroll()
    {
        string uri;
        if (Uwp != null)
            uri = $"/uwp/{Uwp}/animals?tasZone={TasZone}";
        else
            uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/animals";

        if (!string.IsNullOrWhiteSpace(TerrainType))
            uri = QueryHelpers.AddQueryString(uri, "terrainType", TerrainType);
        if (!string.IsNullOrWhiteSpace(AnimalType))
            uri = QueryHelpers.AddQueryString(uri, "animalType", AnimalType);

        Navigation.NavigateTo(uri, false);
    }

    protected override async Task ParametersSetAsync()
    {
        if (Uwp != null)
        {
            var world = new World(Uwp, Uwp, 0, TasZone);

            MilieuCode = Milieu.Custom.Code;
            Model = new WorldModel(Milieu.Custom, world);
        }
        else
        {
            if (PlanetHex == null || SectorHex == null || MilieuCode == null)
                goto ReturnToIndex;

            var milieu = Milieu.FromCode(MilieuCode);
            if (milieu == null)
                goto ReturnToIndex;

            var service = TravellerMapServiceLocator.GetMapService(MilieuCode);
            var world = await service.FetchWorldAsync(SectorHex, PlanetHex);
            if (world == null)
                goto ReturnToIndex;

            Model = new WorldModel(milieu, world);
        }

        PageTitle = Model.World.Name ?? Uwp + " Animals";
        GenerateAnimals();
        return;

    ReturnToIndex:
        base.Navigation.NavigateTo("/");
    }
}

