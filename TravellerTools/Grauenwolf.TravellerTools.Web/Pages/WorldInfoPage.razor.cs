using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class WorldInfoPage
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

        [Parameter]
        public string? Uwp
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        public string? TasZone
        {
            get => Get<string?>();
            set => Set(value, true);
        }

        public int? Seed
        {
            get => Get<int?>();
            set => Set(value, true);
        }

        protected override void Initialized()
        {
            if (Navigation.TryGetQueryString("seed", out int seed))
                Seed = seed;
            else
                Seed = (new Random()).Next();

            if (Navigation.TryGetQueryString("tasZone", out string tasZone))
                TasZone = tasZone;
        }

        protected void OnReroll(MouseEventArgs _)
        {
            if (Uwp != null)
                Navigation.NavigateTo($"/uwp/{Uwp}/info?tasZone={TasZone}", false);
            else
                Navigation.NavigateTo($"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/info", false);
        }

        protected string Permalink
        {
            get
            {
                if (Uwp != null)
                    return $"/uwp/{Uwp}/info?tasZone={TasZone}&seed={Seed}";
                else
                    return $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/info?seed={Seed}";
            }
        }

        protected void OnPermalink(MouseEventArgs _)
        {
            Navigation.NavigateTo(Permalink, true);
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

            PageTitle = Model.World.Name;

            if (Seed != null)
            {
                var dice = new Dice(Seed.Value);
                Model.HighportDetails = TradeEngine.CalculateStarportDetails(Model.World, dice, true);
                Model.DownportDetails = TradeEngine.CalculateStarportDetails(Model.World, dice, false);
            }
            return;

        ReturnToIndex:
            base.Navigation.NavigateTo("/"); //bounce back to home.
        }
    }
}
