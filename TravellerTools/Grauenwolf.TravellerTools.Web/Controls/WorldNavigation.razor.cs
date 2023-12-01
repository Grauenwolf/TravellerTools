using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Controls;

partial class WorldNavigation
{
    [Parameter] public string? CurrentPage { get; set; }
    [Parameter] public string? MilieuCode { get; set; }
    [Parameter] public World World { get; set; } = null!;

    protected string BaseUrl
    {
        get
        {
            if (IsCustom)
                return $"/uwp/{World.UWP}/";
            else
                return $"/world/{MilieuCode}/{World.SectorX},{World.SectorY}/{World.Hex}/";
        }
    }

    protected bool IsCustom => MilieuCode == Milieu.Custom.Code;
    protected string? QueryParameters { get; private set; }
    [Inject] NavigationManager NavigationManager { get; set; } = null!;

    protected override void Initialized()
    {
        base.Initialized();
        QueryParameters = NavigationManager.QueryString();
    }

    /*
    void GotoAnimals()
    {
        GotoPlanet("animals");
    }

    void GotoPlanet()
    {
        GotoPlanet("info");
    }

    private void GotoPlanet(string suffix)
    {
        if (Options.SelectedMilieuCode == null || Options.SelectedSectorHex == null || Options.SelectedWorldHex == null)
            return;
        Navigation.NavigateTo($"/world/{Options.SelectedMilieuCode}/{Options.SelectedSectorHex}/{Options.SelectedWorldHex}/{suffix}");
    }

    void GotoShopping()
    {
        GotoPlanet("store");
    }

    void GotoTrade()
    {
        GotoPlanet("trade");
    }

    */
}
