using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Controls
{
    public class WorldPickerBase : Shared.ControlBase<PlanetPickerOptions>
    {
        public WorldPickerBase()
        {
            Model = new PlanetPickerOptions();
            Model.PropertyChanged += Options_PropertyChanged;
        }

        private async void Options_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PlanetPickerOptions.SelectedMilieu): await OnMilieuChangedAsync(); break;
                case nameof(PlanetPickerOptions.SelectedSector): await OnSectorChangedAsync(); break;
                case nameof(PlanetPickerOptions.SelectedSubsector): await OnSubsectorChangedAsync(); break;
            }
        }

        //PlanetPickerOptions Model { get; } = new PlanetPickerOptions();
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

        protected bool WorldNotSelected => Model!.SelectedWorld == null;

        protected override async Task InitializedAsync()
        {
            await OnMilieuChangedAsync();
        }

        protected void GotoAnimals() => GotoPlanet("animals");

        protected void GotoPlanet() => GotoPlanet("info");

        protected void GotoTravel() => GotoPlanet("travel");

        protected void GotoPlanet(string suffix)
        {
            if (Model!.SelectedMilieuCode == null || Model!.SelectedSectorHex == null || Model!.SelectedWorldHex == null)
                return;
            Navigation.NavigateTo($"/world/{Model!.SelectedMilieuCode}/{Model!.SelectedSectorHex}/{Model!.SelectedWorldHex}/{suffix}");
        }

        protected void GotoShopping() => GotoPlanet("store");

        protected void GotoTrade() => GotoPlanet("trade");

        async Task OnMilieuChangedAsync()
        {
            if (Model!.SelectedMilieu == null)
                Model.SelectedMilieu = Milieu.DefaultMilieu;

            var service = TravellerMapServiceLocator.GetMapService(Model.SelectedMilieu!);
            Model.SectorList = await service.FetchUniverseAsync();
        }

        async Task OnSectorChangedAsync()
        {
            if (Model!.SelectedSector == null)
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
            if (Model!.SelectedSector == null || Model.SelectedSubsector == null)
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
}
