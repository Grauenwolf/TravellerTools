using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class SectorIndexPage
    {
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

        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

        protected override async Task ParametersSetAsync()
        {
            if (SectorHex == null || MilieuCode == null)
                goto ReturnToIndex;

            var milieu = Milieu.FromCode(MilieuCode);
            if (milieu == null)
                goto ReturnToIndex;

            var service = TravellerMapServiceLocator.GetMapService(MilieuCode);
            var sector = await service.FetchSectorAsync(SectorHex);
            if (sector == null)
                goto ReturnToIndex;

            var sectorMetadata = await service.FetchSectorMetadataAsync(sector.X, sector.Y);
            if (sectorMetadata == null)
                goto ReturnToIndex;

            Model = new SectorModel(milieu, sectorMetadata);

            PageTitle = Model.Sector.Name + " Sector";

            return;

        ReturnToIndex:
            base.Navigation.NavigateTo("/"); //bounce back to home.
        }
    }
}
