using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldTravelPage
{
    [Parameter]
    public string? MilieuCode { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? PlanetHex { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? SectorHex { get => Get<string?>(); set => Set(value, true); }

    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get => Get<string?>(); set => Set(value, true); }

    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            TasZone = tasZone;
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
            Model.Destinations = await service.WorldsNearAsync(world.SectorX!.Value, world.SectorY!.Value, int.Parse(world.HexX!), int.Parse(world.HexY!), 6);
        }

        PageTitle = Model.World.Name ?? Uwp + " Travel";

        return;

    ReturnToIndex:
        Navigation.NavigateTo("/"); //bounce back to home.
    }
}
