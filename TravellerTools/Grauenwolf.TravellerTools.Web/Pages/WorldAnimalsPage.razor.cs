using AE = Grauenwolf.TravellerTools.Animals.AE;
using Mgt = Grauenwolf.TravellerTools.Animals.Mgt;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldAnimalsPage
{
    const string AnimalEncountersRoute = "/animal-encounters";
    const string AnimalsMgtRoute = "/animals/mgt";
    const string AllTerrainsTab = "__all__";

    [Parameter]
    public string? MilieuCode { get; set; }

    [Parameter]
    public string? PlanetHex { get; set; }

    [Parameter]
    public string? SectorHex { get; set; }

    public string? AeAnimalClass { get => Get<string?>(); set => Set(value); }
    public Dictionary<string, List<AE.Animal>>? AeAnimals { get => Get<Dictionary<string, List<AE.Animal>>?>(); set => Set(value); }
    public string? AeActiveTerrainTab { get => Get<string?>(); set => Set(value); }
    public string? AnimalType { get => Get<string?>(); set => Set(value); }
    public Dictionary<string, List<Mgt.Animal>>? Animals { get => Get<Dictionary<string, List<Mgt.Animal>>?>(); set => Set(value); }
    public string? ActiveTerrainTab { get => Get<string?>(); set => Set(value); }
    public string? AeTerrainType { get => Get<string?>(); set => Set(value); }
    public int? Seed { get => Get<int?>(); set => Set(value); }
    public string? TerrainType { get => Get<string?>(); set => Set(value); }
    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get; set; }

    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected bool IsStandaloneRoute { get; private set; }
    protected bool ShowAeSection => !IsStandaloneRoute || CurrentRoutePath == AnimalEncountersRoute;
    protected bool ShowMgtSection => !IsStandaloneRoute || CurrentRoutePath == AnimalsMgtRoute;

    string CurrentRoutePath => new Uri(Navigation.Uri).AbsolutePath.ToLowerInvariant();

    protected IReadOnlyList<string> AeAnimalClassList => AE.AnimalBuilderAE.AnimalClassList.Select(x => x.Name).OrderBy(x => x).ToList();
    protected IReadOnlyList<string> AeTerrainTypeList => AE.AnimalBuilderAE.TerrainTypeList.Select(x => x.Name).OrderBy(x => x).ToList();
    protected IReadOnlyList<string> AnimalTypeList => Mgt.AnimalBuilderMgt.AnimalTypeList.Select(x => x.Name).OrderBy(x => x).ToList();
    protected string Permalink
    {
        get
        {
            string uri;
            if (Uwp != null)
                uri = $"/uwp/{Uwp}/animals?tasZone={TasZone}";
            else if (IsStandaloneRoute)
                uri = CurrentRoutePath;
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/animals";

            if (!string.IsNullOrWhiteSpace(TerrainType))
                uri = QueryHelpers.AddQueryString(uri, "terrainType", TerrainType);
            if (!string.IsNullOrWhiteSpace(AnimalType))
                uri = QueryHelpers.AddQueryString(uri, "animalType", AnimalType);
            if (!string.IsNullOrWhiteSpace(AeTerrainType))
                uri = QueryHelpers.AddQueryString(uri, "aeTerrainType", AeTerrainType);
            if (!string.IsNullOrWhiteSpace(AeAnimalClass))
                uri = QueryHelpers.AddQueryString(uri, "aeAnimalClass", AeAnimalClass);

            return QueryHelpers.AddQueryString(uri, "seed", (Seed ?? 0).ToString());
        }
    }
    protected IReadOnlyList<string> TerrainTypeList => Mgt.AnimalBuilderMgt.TerrainTypeList.Select(x => x.Name).OrderBy(x => x).ToList();

    protected IEnumerable<KeyValuePair<string, List<AE.Animal>>> DisplayedAeTerrains
    {
        get
        {
            if (AeAnimals == null)
                return Enumerable.Empty<KeyValuePair<string, List<AE.Animal>>>();

            if (string.IsNullOrWhiteSpace(AeActiveTerrainTab) || AeActiveTerrainTab == AllTerrainsTab)
                return AeAnimals.OrderBy(x => x.Key);

            return AeAnimals.Where(x => x.Key == AeActiveTerrainTab).OrderBy(x => x.Key);
        }
    }

    protected IEnumerable<KeyValuePair<string, List<Mgt.Animal>>> DisplayedMgtTerrains
    {
        get
        {
            if (Animals == null)
                return Enumerable.Empty<KeyValuePair<string, List<Mgt.Animal>>>();

            if (string.IsNullOrWhiteSpace(ActiveTerrainTab) || ActiveTerrainTab == AllTerrainsTab)
                return Animals.OrderBy(x => x.Key);

            return Animals.Where(x => x.Key == ActiveTerrainTab).OrderBy(x => x.Key);
        }
    }

    protected bool CanRerollAnimalEncounters => AeAnimals?.Count > 0;
    protected bool CanRerollAnimals => Animals?.Count > 0;

    protected void ClearAnimalEncounters()
    {
        if (AeAnimals == null)
            return;

        if (string.IsNullOrWhiteSpace(AeActiveTerrainTab) || AeActiveTerrainTab == AllTerrainsTab)
        {
            AeAnimals = null;
            return;
        }

        AeAnimals.Remove(AeActiveTerrainTab);
        if (AeAnimals.Count == 0)
            AeAnimals = null;

        AeActiveTerrainTab = AllTerrainsTab;
    }

    protected void ClearAnimals()
    {
        if (Animals == null)
            return;

        if (string.IsNullOrWhiteSpace(ActiveTerrainTab) || ActiveTerrainTab == AllTerrainsTab)
        {
            Animals = null;
            return;
        }

        Animals.Remove(ActiveTerrainTab);
        if (Animals.Count == 0)
            Animals = null;

        ActiveTerrainTab = AllTerrainsTab;
    }

    protected void OnAeTerrainChanged()
    {
        AeActiveTerrainTab = string.IsNullOrWhiteSpace(AeTerrainType) ? AllTerrainsTab : AeTerrainType;
    }

    protected void OnMgtTerrainChanged()
    {
        ActiveTerrainTab = string.IsNullOrWhiteSpace(TerrainType) ? AllTerrainsTab : TerrainType;
    }

    protected void SetAeTerrainTab(string terrain)
    {
        AeActiveTerrainTab = terrain;
    }

    protected void SetMgtTerrainTab(string terrain)
    {
        ActiveTerrainTab = terrain;
    }

    protected void GenerateAnimalEncounters()
    {
        if (Seed == null)
            return;

        var dice = new Dice(Seed.Value);

        if (!string.IsNullOrWhiteSpace(AeTerrainType) && !string.IsNullOrWhiteSpace(AeAnimalClass))
        {
            AeAnimals = new Dictionary<string, List<AE.Animal>>()
            {
                [AeTerrainType] =
                [
                    AE.AnimalBuilderAE.BuildAnimal(dice, AeTerrainType, AeAnimalClass)
                ]
            };
        }
        else if (!string.IsNullOrWhiteSpace(AeTerrainType))
        {
            AeAnimals = new Dictionary<string, List<AE.Animal>>()
            {
                [AeTerrainType] = AE.AnimalBuilderAE.BuildTerrainSet(dice, AeTerrainType)
            };
        }
        else
        {
            AeAnimals = AE.AnimalBuilderAE.BuildPlanetSet(dice);
        }
    }

    protected void GenerateAnimals()
    {
        if (Seed == null)
            return;

        var dice = new Dice(Seed.Value);

        if (!string.IsNullOrWhiteSpace(TerrainType) && !string.IsNullOrWhiteSpace(AnimalType))
        {
            Animals = new Dictionary<string, List<Mgt.Animal>>()
            {
                [TerrainType] =
                [
                    Mgt.AnimalBuilderMgt.BuildAnimal(dice, TerrainType, AnimalType)
                ]
            };
        }
        else if (!string.IsNullOrWhiteSpace(TerrainType))
        {
            Animals = new Dictionary<string, List<Mgt.Animal>>()
            {
                [TerrainType] = Mgt.AnimalBuilderMgt.BuildTerrainSet(dice, TerrainType)
            };
        }
        else
        {
            Animals = Mgt.AnimalBuilderMgt.BuildPlanetSet(dice);
        }
    }

    protected override void Initialized()
    {
        ActiveTerrainTab = AllTerrainsTab;
        AeActiveTerrainTab = AllTerrainsTab;

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

        if (Navigation.TryGetQueryString("aeTerrainType", out string aeTerrainType))
            AeTerrainType = aeTerrainType;

        if (Navigation.TryGetQueryString("aeAnimalClass", out string aeAnimalClass))
            AeAnimalClass = aeAnimalClass;

        if (!string.IsNullOrWhiteSpace(TerrainType))
            ActiveTerrainTab = TerrainType;

        if (!string.IsNullOrWhiteSpace(AeTerrainType))
            AeActiveTerrainTab = AeTerrainType;
    }

    protected void OnReroll()
    {
        Seed = (new Random()).Next();

        // Reroll is equivalent to Clear, then Generate, for sections that already have results.
        if (Animals != null)
        {
            ClearAnimals();
            GenerateAnimals();
        }

        if (AeAnimals != null)
        {
            ClearAnimalEncounters();
            GenerateAnimalEncounters();
        }
    }

    protected override async Task ParametersSetAsync()
    {
        IsStandaloneRoute = CurrentRoutePath == AnimalsMgtRoute || CurrentRoutePath == AnimalEncountersRoute;

        if (IsStandaloneRoute)
        {
            var world = new World("X000000-0", "Animals", 0, null);
            MilieuCode = Milieu.Custom.Code;
            Model = new WorldModel(Milieu.Custom, world);
        }
        else
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

        PageTitle = IsStandaloneRoute ? "Animals" : Model.World.Name ?? Uwp + " Animals";

        Animals = null;
        AeAnimals = null;

        return;

    ReturnToIndex:
        base.Navigation.NavigateTo("/");
    }
}

