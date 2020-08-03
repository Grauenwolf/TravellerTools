using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Controls
{
    partial class WorldNavigation
    {
        [Parameter]
        public string? MilieuCode { get; set; }

        [Parameter] public string? SectorHex { get; set; }
        [Parameter] public string? PlanetHex { get; set; }
        [Parameter] public string? CurrentPage { get; set; }

        public string BaseUrl => $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/";

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
}
