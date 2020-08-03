using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Controls
{
    partial class WorldPicker
    {
        PlanetPickerOptions Options { get; } = new PlanetPickerOptions();
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;
        bool WorldNotSelected => Options.SelectedWorld == null;

        protected override async Task InitializedAsync()
        {
            await OnMilieuChangedAsync();
        }

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

        async Task OnMilieuChangedAsync()
        {
            if (Options.SelectedMilieu == null)
                Options.SelectedMilieu = Milieu.DefaultMilieu;

            var service = TravellerMapServiceLocator.GetMapService(Options.SelectedMilieu!);
            Options.SectorList = await service.FetchUniverseAsync();
        }

        async Task OnSectorChangedAsync()
        {
            if (Options.SelectedSector == null)
            {
                Options.SubsectorList = Array.Empty<Subsector>();
                Options.WorldList = Array.Empty<World>();
                Options.SelectedSubsector = null;
                Options.SelectedWorld = null;
                return;
            }

            var service = TravellerMapServiceLocator.GetMapService(Options.SelectedMilieu!);
            Options.SubsectorList = await service.FetchSubsectorsInSectorAsync(Options.SelectedSector);
            Options.SelectedSubsector = null;
            Options.SelectedWorld = null;
        }

        async Task OnSubsectorChangedAsync()
        {
            if (Options.SelectedSector == null || Options.SelectedSubsector == null)
            {
                Options.WorldList = Array.Empty<World>();
                Options.SelectedWorld = null;
                return;
            }

            var service = TravellerMapServiceLocator.GetMapService(Options.SelectedMilieu!);
            Options.WorldList = await service.FetchWorldsInSubsectorAsync(Options.SelectedSector.X, Options.SelectedSector.Y, Options.SelectedSubsector.Index);
            Options.SelectedWorld = null;
        }
    }
}
