using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Grauenwolf.TravellerTools.Web.Shared;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class SubsectorIndexPage
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

        [Parameter]
        public string? Subsector
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

        protected override async Task ParametersSetAsync()
        {
            if (SectorHex == null || MilieuCode == null || Subsector == null)
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

            var subsector = sectorMetadata.Subsectors?.SingleOrDefault(ss => ss.Index == Subsector);
            if (subsector == null)
                goto ReturnToIndex;

            var worlds = await service.FetchWorldsInSubsectorAsync(sector.X, sector.Y, Subsector, subsector.Name!);

            Model = new SubsectorModel(milieu, sectorMetadata, subsector, worlds);

            PageTitle = Model.Sector.Name;

            return;

        ReturnToIndex:
            base.Navigation.NavigateTo("/"); //bounce back to home.
        }
    }
}
