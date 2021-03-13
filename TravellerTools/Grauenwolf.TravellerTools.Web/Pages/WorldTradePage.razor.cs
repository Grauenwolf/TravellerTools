using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class WorldTradePage
    {
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;
        [Inject] TradeEngineLocator TradeEngineLocator { get; set; } = null!;

        protected TradeOptions Options { get; } = new TradeOptions();

        public WorldTradePage()
        {
            Options.PropertyChanged += (sender, e) => OnOptionsChanged();
        }

        [Parameter]
        public string? MilieuCode { get => Get<string?>(); set => Set(value, true); }

        [Parameter]
        public string? SectorHex { get => Get<string?>(); set => Set(value, true); }

        [Parameter]
        public string? PlanetHex { get => Get<string?>(); set => Set(value, true); }

        public int? Seed { get => Get<int?>(); set => Set(value, true); }

        public string? TasZone { get => Get<string?>(); set => Set(value, true); }

        [Parameter]
        public string? Uwp { get => Get<string?>(); set => Set(value, true); }

        protected override void Initialized()
        {
            if (Navigation.TryGetQueryString("seed", out int seed))
                Seed = seed;
            else
                Seed = (new Random()).Next();

            Options.FromQueryString(Navigation.ParsedQueryString());

            if (Navigation.TryGetQueryString("tasZone", out string tasZone))
                TasZone = tasZone;
        }

        protected void Reroll(MouseEventArgs _)
        {
            string uri;
            if (Uwp != null)
                uri = $"/uwp/{Uwp}/trade?tasZone={TasZone}";
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/trade";

            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            Navigation.NavigateTo(uri, false);
        }

        protected void Permalink(MouseEventArgs _)
        {
            string uri = Permalink();
            Navigation.NavigateTo(uri, true);
        }

        protected string Permalink()
        {
            string uri;
            if (Uwp != null)
                uri = $"/uwp/{Uwp}/trade?tasZone={TasZone}";
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/trade";

            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            uri = QueryHelpers.AddQueryString(uri, "seed", (Seed ?? 0).ToString());
            return uri;
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
                Model.Destinations = await service.WorldsNearAsync(world.SectorX!.Value, world.SectorY!.Value, int.Parse(world.HexX!), int.Parse(world.HexY!), 6).ConfigureAwait(false);
            }

            PageTitle = Model.World.Name ?? Uwp + " Trade";

            OnOptionsChanged();

            return;

        ReturnToIndex:
            base.Navigation.NavigateTo("/"); //bounce back to home.
        }

        protected void OnOptionsChanged()
        {
            if (Model == null)
                return; //We're setting up parameters

            try
            {
                if (Seed != null)
                {
                    var dice = new Dice(Seed.Value);
                    var tradeEngine = TradeEngineLocator.GetTradeEngine(MilieuCode!, Options.SelectedEdition);
                    World? destination = null;
                    if (Options.DestinationIndex >= 0)
                        destination = Model.Destinations![Options.DestinationIndex];

                    Model!.TradeList = tradeEngine.BuildTradeGoodsList(Model.World, Options.AdvancedMode, Options.IllegalGoods, Options.BrokerScore, dice, Options.Raffle, Options.StreetwiseScore, destination);

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
