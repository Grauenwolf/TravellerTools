using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class WorldFreightPage
    {
        public WorldFreightPage()
        {
            Options.PropertyChanged += (sender, e) => OnOptionsChanged();
        }

        [Parameter]
        public string? DestinationPlanetHex { get => Get<string?>(); set => Set(value, true); }

        [Parameter]
        public string? DestinationSectorHex { get => Get<string?>(); set => Set(value, true); }

        protected FreightOptions Options { get; } = new FreightOptions();

        [Parameter]
        public string? MilieuCode { get => Get<string?>(); set => Set(value, true); }

        [Parameter]
        public string? PlanetHex { get => Get<string?>(); set => Set(value, true); }

        [Parameter]
        public string? SectorHex { get => Get<string?>(); set => Set(value, true); }

        public int? Seed { get => Get<int?>(); set => Set(value, true); }
        [Inject] TradeEngineLocator TradeEngineLocator { get; set; } = null!;
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;
        //public string? TasZone { get => Get<string?>(); set => Set(value, true); }

        //[Parameter]
        //public string? Uwp { get => Get<string?>(); set => Set(value, true); }

        protected override void Initialized()
        {
            if (Navigation.TryGetQueryString("seed", out int seed))
                Seed = seed;
            else
                Seed = (new Random()).Next();

            Options.FromQueryString(Navigation.ParsedQueryString());

            //if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            //    TasZone = tasZone;
        }

        void OnReroll(MouseEventArgs _)
        {
            string uri;
            //if (Uwp != null)
            //    uri = $"/uwp/{Uwp}/trade?tasZone={TasZone}";
            //else

            uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/destination/{DestinationSectorHex}/{DestinationPlanetHex}/freight";
            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            Navigation.NavigateTo(uri, false);
        }

        void OnPermalink(MouseEventArgs _)
        {
            string uri = Permalink();
            Navigation.NavigateTo(uri, true);
        }

        string Permalink()
        {
            string uri;
            //if (Uwp != null)
            //    uri = $"/uwp/{Uwp}/trade?tasZone={TasZone}";
            //else
            uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/destination/{DestinationSectorHex}/{DestinationPlanetHex}/freight";

            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            uri = QueryHelpers.AddQueryString(uri, "seed", (Seed ?? 0).ToString());
            return uri;
        }

        protected override async Task ParametersSetAsync()
        {
            //if (Uwp != null)
            //{
            //    var world = new World(Uwp, Uwp, 0, TasZone);

            //    MilieuCode = Milieu.Custom.Code;
            //    Model = new WorldModel(Milieu.Custom, world);
            //}
            //else
            //{
            if (PlanetHex == null || SectorHex == null || MilieuCode == null || DestinationSectorHex == null || DestinationPlanetHex == null)
                goto ReturnToIndex;

            var milieu = Milieu.FromCode(MilieuCode);
            if (milieu == null)
                goto ReturnToIndex;

            var service = TravellerMapServiceLocator.GetMapService(MilieuCode);
            var world = await service.FetchWorldAsync(SectorHex, PlanetHex);
            if (world == null)
                goto ReturnToIndex;

            //We use WorldsNearAsync because it includes jump distances
            var destinations = await service.WorldsNearAsync(world.SectorX!.Value, world.SectorY!.Value, int.Parse(world.HexX!), int.Parse(world.HexY!), 6);
            var destination = destinations.SingleOrDefault(d => (d.SectorX + "," + d.SectorY) == DestinationSectorHex && d.Hex == DestinationPlanetHex);
            if (destination == null)
                goto ReturnToIndex;

            //var tradeEngine = TradeEngineLocator.GetTradeEngine(MilieuCode!, Options.SelectedEdition);

            Model = new FreightModel(milieu, world, destination);
            //}

            PageTitle = Model.World.Name + " to " + Model.Destination.Name + " Freight";

            OnOptionsChanged();

            return;

        ReturnToIndex:
            Navigation.NavigateTo("/"); //bounce back to home.
        }

        void OnOptionsChanged()
        {
            if (Model == null)
                return; //We're setting up parameters

            try
            {
                if (Seed != null)
                {
                    var dice = new Dice(Seed.Value);
                    var tradeEngine = TradeEngineLocator.GetTradeEngine(MilieuCode!, Options.SelectedEdition);

                    Model.FreightList = tradeEngine.Freight(Model.World, Model.Destination, dice);

                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, $"Error in updating options using {nameof(OnOptionsChanged)}.");
            }
        }
    }
}
