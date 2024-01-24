using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldStarportPage
{
    [Parameter]
    public string? MilieuCode { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? PlanetHex { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? SectorHex { get => Get<string?>(); set => Set(value, true); }

    public int? Seed { get => Get<int?>(); set => Set(value, true); }

    [Parameter]
    public string? Spaceport { get => Get<string?>(); set => Set(value, true); }

    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get => Get<string?>(); set => Set(value, true); }

    protected string Permalink
    {
        get
        {
            if (Spaceport != null)
            {
                if (Uwp != null)
                    return $"/uwp/{Uwp}/starport/{Spaceport}?tasZone={TasZone}&seed={Seed}";
                else
                    return $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/starport/{Spaceport}?seed={Seed}";
            }
            else
            {
                if (Uwp != null)
                    return $"/uwp/{Uwp}/starport/{Spaceport}?tasZone={TasZone}&seed={Seed}";
                else
                    return $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/starport?seed={Seed}";
            }
        }
    }

    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("seed", out int seed))
            Seed = seed;
        else
            Seed = (new Random()).Next();

        if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            TasZone = tasZone;
    }

    protected void OnPermalink(MouseEventArgs _)
    {
        Navigation.NavigateTo(Permalink, true);
    }

    protected void OnReroll(MouseEventArgs _)
    {
        Seed = (new Random()).Next();
        Navigation.NavigateTo(Permalink, false);
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

        PageTitle = Model.World.Name ?? Uwp + " Starport";

        if (Seed != null)
        {
            var dice = new Dice(Seed.Value);

            var spaceport = Spaceport != null ? new EHex(Spaceport) : (EHex?)null;
            Model.StarportFacilities = TradeEngine.CalculateStarportFacilities(Model.World, dice, spaceport);
        }
        return;

    ReturnToIndex:
        base.Navigation.NavigateTo("/"); //bounce back to home.
    }

    private void GenerateStarportF(MouseEventArgs e)
    {
        Spaceport = "F";
        Seed = (new Random()).Next();
        Navigation.NavigateTo(Permalink, true);
    }

    private void GenerateStarportG(MouseEventArgs e)
    {
        Spaceport = "G";
        Seed = (new Random()).Next();
        Navigation.NavigateTo(Permalink, true);
    }

    private void GenerateStarportH(MouseEventArgs e)
    {
        Spaceport = "H";
        Seed = (new Random()).Next();
        Navigation.NavigateTo(Permalink, true);
    }

    private void GenerateStarportJ(MouseEventArgs e)
    {
        Spaceport = "J";
        Seed = (new Random()).Next();
        Navigation.NavigateTo(Permalink, true);
    }
}
