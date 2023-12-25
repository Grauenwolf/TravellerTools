using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using Tortuga.Anchor;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldStorePage
{
    public WorldStorePage()
    {
        Options.PropertyChanged += (sender, e) => OnOptionsChanged();
    }

    [Parameter]
    public string? MilieuCode { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? PlanetHex { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? SectorHex { get => Get<string?>(); set => Set(value, true); }

    public int? Seed { get => Get<int?>(); set => Set(value, true); }
    public string? SpeciesFilter { get => Get<string?>(); set => Set(value); }
    public List<string> SpeciesList => EquipmentBuilder.GetSpeciesNames();
    public Store? Store { get => Get<Store?>(); set => Set(value); }
    public string? StoreTypeFilter { get => Get<string?>(); set => Set(value); }
    public List<string> StoreTypeList => EquipmentBuilder.GetSectionNames();
    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get => Get<string?>(); set => Set(value, true); }

    protected Data.StoreOptions Options { get; } = new Data.StoreOptions();
    [Inject] EquipmentBuilder EquipmentBuilder { get; set; } = null!;
    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    public string ClassificationString(Item item)
    {
        if (item.Contraband.IsNullOrEmpty())
            return "";

        if (item.Category > 1)
            return $"{item.Contraband}/{item.Category} ({Tables.ItemCategory(item.Category)})";
        else
            return $"{item.Contraband}";
    }

    public string LawString(Item item)
    {
        if (item.Law == 0)
            return "";

        return item.Law.ToString();
    }

    public string LawTitle(Item item)
    {
        var result = "";
        if (item.Law > 0)
        {
            result = item.Contraband switch
            {
                "Weapons" => Tables.LawLevelWeaponsRestricted(item.Law),
                "Drugs" => Tables.LawLevelDrugsRestricted(item.Law),
                "Information" => Tables.LawLevelInformationRestricted(item.Law),
                "Psionics" => Tables.LawLevelPsionicsRestricted(item.Law),
                _ => Tables.LawLevelDescription(item.Law)
            };
            //if (item.Category > 0 && !item.Contraband.IsNullOrEmpty())
            //    result += $" {item.Contraband}/{Tables.ItemCategory(item.Category)}";
        }

        return result;
    }

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("seed", out int seed))
            Seed = seed;
        else
            Seed = (new Random()).Next();

        if (Navigation.TryGetQueryString("storeTypeFilter", out string storeTypeFilter))
            StoreTypeFilter = storeTypeFilter;
        if (Navigation.TryGetQueryString("speciesFilter", out string speciesFilter))
            SpeciesFilter = speciesFilter;

        Options.FromQueryString(Navigation.ParsedQueryString());

        if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            TasZone = tasZone;
    }

    protected void OnOptionsChanged()
    {
        if (Model == null)
            return; //We're setting up parameters

        try
        {
            if (Seed != null)
            {
                var restrictions = Tables.GovernmentContraband(Model.World.GovernmentCode);

                var options = new Grauenwolf.TravellerTools.Equipment.StoreOptions()
                {
                    BrokerScore = Options.BrokerScore,
                    LawLevel = Model.World.LawCode,
                    Population = Model.World.PopulationCode,
                    AutoRoll = Options.AutoRoll,
                    DiscountPrices = Options.DiscountPrices,
                    Starport = Model.World.StarportCode,
                    StreetwiseScore = Options.StreetwiseScore,
                    TechLevel = Model.World.TechCode,
                    Seed = Seed.Value,
                    DrugsRestricted = Options.DrugsRestricted,
                    WeaponsRestricted = Options.WeaponsRestricted,
                    TechnologyRestricted = Options.TechnologyRestricted,
                    PsionicsRestricted = Options.PsionicsRestricted,
                    InformationRestricted = Options.InformationRestricted
                };
                options.TradeCodes.AddRange(Model.World.RemarksList.Keys);

                Store = EquipmentBuilder.AvailabilityTable(options);

                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in updating options using {nameof(OnOptionsChanged)}.");
        }
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

            var restrictions = Tables.GovernmentContraband(Model.World.GovernmentCode);
            Options.DrugsRestricted = restrictions.Contains("Drugs");
            Options.WeaponsRestricted = restrictions.Contains("Weapons");
            Options.TechnologyRestricted = restrictions.Contains("Technology");
            Options.PsionicsRestricted = restrictions.Contains("Psionics");
            Options.InformationRestricted = restrictions.Contains("Information");

            //Reread from query string so we don't override user settings
            Options.FromQueryString(Navigation.ParsedQueryString());
        }

        PageTitle = Model.World.Name ?? Uwp + " Store";

        OnOptionsChanged();

        return;

    ReturnToIndex:
        base.Navigation.NavigateTo("/"); //bounce back to home.
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
        if (!string.IsNullOrEmpty(SpeciesFilter))
            uri = QueryHelpers.AddQueryString(uri, "speciesFilter", SpeciesFilter);
        return uri;
    }

    protected void Reroll(MouseEventArgs _)
    {
        try
        {
            string uri;
            if (Uwp != null)
                uri = $"/uwp/{Uwp}/store?tasZone={TasZone}";
            else
                uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/store";

            uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
            if (!string.IsNullOrEmpty(StoreTypeFilter))
                uri = QueryHelpers.AddQueryString(uri, "storeTypeFilter", StoreTypeFilter);
            if (!string.IsNullOrEmpty(SpeciesFilter))
                uri = QueryHelpers.AddQueryString(uri, "speciesFilter", SpeciesFilter);

            Navigation.NavigateTo(uri, false);
        }
        catch (Exception ex)
        {
            LogError(ex, $"Error in rerolling using {nameof(Reroll)}.");
        }
    }
}
