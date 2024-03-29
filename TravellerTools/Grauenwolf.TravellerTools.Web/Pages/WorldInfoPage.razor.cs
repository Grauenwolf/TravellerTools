﻿using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldInfoPage
{
    [Parameter]
    public string? MilieuCode { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? PlanetHex { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? SectorHex { get => Get<string?>(); set => Set(value, true); }

    public int? Seed { get => Get<int?>(); set => Set(value, true); }

    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get => Get<string?>(); set => Set(value, true); }

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

    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected override void Initialized()
    {
        if (Navigation.TryGetQueryString("seed", out int seed))
            Seed = seed;
        else
            Seed = (new Random()).Next();

        if (Navigation.TryGetQueryString("tasZone", out string tasZone))
            TasZone = tasZone;
    }

    protected void OnPermalink(MouseEventArgs _)
    {
        Navigation.NavigateTo(Permalink, true);
    }

    protected void OnReroll(MouseEventArgs _)
    {
        if (Uwp != null)
            Navigation.NavigateTo($"/uwp/{Uwp}/info?tasZone={TasZone}", false);
        else
            Navigation.NavigateTo($"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/info", false);
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

        PageTitle = Model.World.Name ?? Uwp + " Info";

        if (Seed != null)
        {
            var dice = new Dice(Seed.Value);
            Model.StarportFacilities = TradeEngine.CalculateStarportFacilities(Model.World, dice);
        }
        return;

    ReturnToIndex:
        base.Navigation.NavigateTo("/"); //bounce back to home.
    }
}
