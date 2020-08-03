using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Grauenwolf.TravellerTools.Web.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    public class WorldBasePage : PageBase<WorldModel>
    {
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

        [Parameter]
        public string? MilieuCode
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        [Parameter]
        public string? SectorHex
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        [Parameter]
        public string? PlanetHex
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        protected override async Task ParametersSetAsync()
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
            PageTitle = Model.World.Name;
            return;

        ReturnToIndex:
            Navigation.NavigateTo("/"); //bounce back to home.
        }
    }

    partial class WorldInfoPage
    {
    }
}
