using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class WorldStorePage
    {
        [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;
        [Inject] EquipmentBuilder EquipmentBuilder { get; set; } = null!;

        protected Data.StoreOptions Options { get; } = new Data.StoreOptions();

        public WorldStorePage()
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

        public Store? Store { get => Get<Store?>(); set => Set(value); }

        public string? StoreTypeFilter { get => Get<string?>(); set => Set(value); }

        public List<string> StoreTypeList
        {
            get
            {
                var result = new List<string>() { "" };
                result.AddRange(EquipmentBuilder.GetSectionNames());
                return result;
            }
        }

        protected override void Initialized()
        {
            if (Navigation.TryGetQueryString("seed", out int seed))
                Seed = seed;
            else
                Seed = (new Random()).Next();

            if (Navigation.TryGetQueryString("storeTypeFilter", out string storeTypeFilter))
                StoreTypeFilter = storeTypeFilter;

            Options.FromQueryString(Navigation.ParsedQueryString());

            if (Navigation.TryGetQueryString("tasZone", out string tasZone))
                TasZone = tasZone;
        }

        protected void Reroll(MouseEventArgs _)
        {
            string uri;
            if (Uwp != null)
                uri = $"/uwp/{Uwp}/store?tasZone={TasZone}";
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/store";

            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            if (!string.IsNullOrEmpty(StoreTypeFilter))
                uri = QueryHelpers.AddQueryString(uri, "storeTypeFilter", StoreTypeFilter);

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
                uri = $"/uwp/{Uwp}/store?tasZone={TasZone}";
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/store";

            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            uri = QueryHelpers.AddQueryString(uri, "seed", (Seed ?? 0).ToString());
            if (!string.IsNullOrEmpty(StoreTypeFilter))
                uri = QueryHelpers.AddQueryString(uri, "storeTypeFilter", StoreTypeFilter);
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
            }

            PageTitle = Model.World.Name ?? Uwp + " Store";

            OnOptionsChanged();

            return;

        ReturnToIndex:
            base.Navigation.NavigateTo("/"); //bounce back to home.
        }

        protected void OnOptionsChanged()
        {
            if (Model == null)
                return; //We're setting up parameters

            if (Seed != null)
            {
                var options = new Grauenwolf.TravellerTools.Equipment.StoreOptions()
                {
                    BrokerScore = Options.BrokerScore,
                    LawLevel = Model.World.LawCode,
                    Population = Model.World.PopulationCode,
                    AutoRoll = Options.AutoRoll,
                    Starport = Model.World.StarportCode,
                    StreetwiseScore = Options.StreetwiseScore,
                    TechLevel = Model.World.TechCode,
                    Seed = Seed.Value
                };
                options.TradeCodes.AddRange(Model.World.RemarksList);

                Store = EquipmentBuilder.AvailabilityTable(options);

                StateHasChanged();
            }
        }
    }
}
