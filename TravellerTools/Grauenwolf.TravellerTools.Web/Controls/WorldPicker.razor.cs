using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Grauenwolf.TravellerTools.Web.Controls;

partial class WorldPicker
{
    protected bool SectorNotSelected => Model.SelectedSector == null;

    protected bool SubsectorNotSelected => Model.SelectedSubsector == null;

    protected bool WorldNotSelected => Model.SelectedWorld == null;

    //PlanetPickerOptions Model { get; } = new PlanetPickerOptions();
    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected override async Task InitializedAsync()
    {
        await OnMilieuChangedAsync();
    }

    protected override async void OnModelPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(PlanetPickerOptions.SelectedMilieu): await OnMilieuChangedAsync(); break;
            case nameof(PlanetPickerOptions.SelectedSector): await OnSectorChangedAsync(); break;
            case nameof(PlanetPickerOptions.SelectedSubsector): await OnSubsectorChangedAsync(); break;
        }
    }

    protected string? PlanetUrl(string suffix)
    {
        if (Model.SelectedMilieuCode == null || Model.SelectedSectorHex == null || Model.SelectedWorldHex == null)
            return null;
        return $"/world/{Model.SelectedMilieuCode}/{Model.SelectedSectorHex}/{Model.SelectedWorldHex}/{suffix}";
    }

    protected string? SectorTasUrl()
    {
        if (Model.SelectedMilieuCode == null || Model.SelectedSector == null)
            return null;
        return $"https://wiki.travellerrpg.com/{Model.SelectedSector.Name}_Sector";
    }

    protected string? SectorUrl()
    {
        if (Model.SelectedMilieuCode == null || Model.SelectedSectorHex == null)
            return null;
        return $"/world/{Model.SelectedMilieuCode}/{Model.SelectedSectorHex}";
    }

    protected string? SubsectorTasUrl()
    {
        if (Model.SelectedMilieuCode == null || Model.SelectedSectorHex == null || Model.SelectedSubsectorIndex == null)
            return null;
        return $"/world/{Model.SelectedMilieuCode}/{Model.SelectedSectorHex}/subsector/{Model.SelectedSubsectorIndex}";
    }

    protected string? SubsectorUrl()
    {
        if (Model.SelectedMilieuCode == null || Model.SelectedSectorHex == null || Model.SelectedSubsectorIndex == null)
            return null;
        return $"/world/{Model.SelectedMilieuCode}/{Model.SelectedSectorHex}/subsector/{Model.SelectedSubsectorIndex}";
    }

    async Task OnMilieuChangedAsync()
    {
        if (Model.SelectedMilieu == null)
            Model.SelectedMilieu = Milieu.DefaultMilieu;

        var service = TravellerMapServiceLocator.GetMapService(Model.SelectedMilieu!);
        Model.SectorList = await service.FetchUniverseAsync();
    }

    async Task OnSectorChangedAsync()
    {
        if (Model.SelectedSector == null)
        {
            Model.SubsectorList = Array.Empty<Subsector>();
            Model.WorldList = Array.Empty<World>();
            Model.SelectedSubsector = null;
            Model.SelectedWorld = null;
            return;
        }

        var service = TravellerMapServiceLocator.GetMapService(Model.SelectedMilieu!);
        Model.SubsectorList = await service.FetchSubsectorsInSectorAsync(Model.SelectedSector);
        Model.SelectedSubsector = null;
        Model.SelectedWorld = null;
    }

    async Task OnSubsectorChangedAsync()
    {
        if (Model.SelectedSector == null || Model.SelectedSubsector == null)
        {
            Model.WorldList = Array.Empty<World>();
            Model.SelectedWorld = null;
            return;
        }

        var service = TravellerMapServiceLocator.GetMapService(Model.SelectedMilieu!);
        Model.WorldList = await service.FetchWorldsInSubsectorAsync(Model.SelectedSector.X, Model.SelectedSector.Y, Model.SelectedSubsector.Index!, Model.SelectedSector.Name!);
        Model.SelectedWorld = null;
    }
}
