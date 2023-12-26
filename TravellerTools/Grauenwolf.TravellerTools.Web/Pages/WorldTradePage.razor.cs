using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class WorldTradePage
{
    public WorldTradePage()
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
    public string? TasZone { get => Get<string?>(); set => Set(value, true); }

    [Parameter]
    public string? Uwp { get => Get<string?>(); set => Set(value, true); }

    protected TradeOptions Options { get; } = new TradeOptions();
    [Inject] CharacterBuilderLocator CharacterBuilderLocator { get; set; } = null!;
    Character? CounterpartyCharacter { get; set; }
    bool CounterpartyIsInformer { get; set; }
    bool IsInformer { get; set; }
    Character? LocalBrokerCharacter { get; set; }
    [Inject] NameGenerator NameGenerator { get; set; } = null!;
    [Inject] TradeEngineLocator TradeEngineLocator { get; set; } = null!;
    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected bool AllowOnlineSupplier()
    {
        return Model?.World.TechCode.Value >= 8;
    }

    protected string CounterpartyPermalink()
    {
        if (CounterpartyCharacter == null)
            return $"/character";

        var uri = $"/character/view";
        uri = QueryHelpers.AddQueryString(uri, CounterpartyCharacter.GetCharacterBuilderOptions().ToQueryString());
        return uri;
    }

    protected void GenerateCounterpartyCharacter()
    {
        var dice = new Dice();
        CounterpartyIsInformer = dice.D(2, 6) == 2;
        Options.CounterpartyScore = (int)Math.Floor(dice.D(2, 6) / 3.0);

        var options = new CharacterBuilderOptions();
        var temp = NameGenerator.CreateRandomPerson(dice);
        options.Name = temp.FullName;
        options.Gender = temp.Gender;
        options.Species = CharacterBuilderLocator.GetRandomSpecies(dice);
        options.MaxAge = 22 + dice.D(1, 60);

        CounterpartyCharacter = null;
        for (var i = 0; i < 500; i++)
        {
            var result = CharacterBuilderLocator.Build(options);
            if (result.Skills.EffectiveSkillLevel("Broker") == Options.CounterpartyScore && !result.IsDead)
            {
                CounterpartyCharacter = result;
                break;
            }
        }
        StateHasChanged();
    }

    protected void GenerateFenceCharacter()
    {
        var dice = new Dice();
        IsInformer = dice.D(2, 6) == 2;
        Options.StreetwiseScore = (int)Math.Floor(dice.D(2, 6) / 3.0);

        var options = new CharacterBuilderOptions();
        var temp = NameGenerator.CreateRandomPerson(dice);
        options.Name = temp.FullName;
        options.Gender = temp.Gender;
        options.Species = CharacterBuilderLocator.GetRandomSpecies(dice);
        options.MaxAge = 22 + dice.D(1, 60);

        LocalBrokerCharacter = null;
        Options.BrokerScore = 0; //default

        for (var i = 0; i < 500; i++)
        {
            var result = CharacterBuilderLocator.Build(options);
            if (result.Skills.EffectiveSkillLevel("Streetwise") == Options.StreetwiseScore && !result.IsDead)
            {
                LocalBrokerCharacter = result;
                Options.BrokerScore = result.Skills.EffectiveSkillLevel("Broker");

                break;
            }
        }
        //Penalty included for the fence's profit
        Options.StreetwiseScore -= 2;

        StateHasChanged();
    }

    protected void GenerateLocalBrokerCharacter()
    {
        IsInformer = false; //No risk when using a broker instead of a fence.
        var dice = new Dice();
        Options.BrokerScore = (int)Math.Floor(dice.D(2, 6) / 3.0);

        var options = new CharacterBuilderOptions();
        var temp = NameGenerator.CreateRandomPerson(dice);
        options.Name = temp.FullName;
        options.Gender = temp.Gender;
        options.Species = CharacterBuilderLocator.GetRandomSpecies(dice);
        options.MaxAge = 22 + dice.D(1, 60);

        LocalBrokerCharacter = null;
        Options.StreetwiseScore = 0; //default

        for (var i = 0; i < 500; i++)
        {
            var result = CharacterBuilderLocator.Build(options);
            if (result.Skills.EffectiveSkillLevel("Broker") == Options.BrokerScore && !result.IsDead)
            {
                LocalBrokerCharacter = result;
                Options.StreetwiseScore = result.Skills.EffectiveSkillLevel("StreetwiseScore") - 2;
                break;
            }
        }
        StateHasChanged();
    }

    protected string GetSupplierDM()
    {
        return Model?.World.StarportCode.ToChar() switch
        {
            'A' => "+6",
            'B' => "+4",
            'C' => "+2",
            _ => "0"
        };
    }

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

    protected string LocalBrokerPermalink()
    {
        if (LocalBrokerCharacter == null)
            return $"/character";

        var uri = $"/character/view";
        uri = QueryHelpers.AddQueryString(uri, LocalBrokerCharacter.GetCharacterBuilderOptions().ToQueryString());
        return uri;
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

                Model!.TradeList = tradeEngine.BuildTradeGoodsList(Model.World, Options.AdvancedMode, Options.IllegalGoods, Options.BrokerScore, dice, Options.Raffle, Options.StreetwiseScore, Options.CounterpartyScore, destination, Options.AgeWeeks);

                InvokeAsync(StateHasChanged);
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
            Model.Destinations = await service.WorldsNearAsync(world.SectorX!.Value, world.SectorY!.Value, int.Parse(world.HexX!), int.Parse(world.HexY!), 6).ConfigureAwait(false);
        }

        PageTitle = Model.World.Name ?? Uwp + " Trade";

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
            uri = $"/uwp/{Uwp}/trade?tasZone={TasZone}";
        else
            uri = $"/world/{MilieuCode}/{SectorHex}/{PlanetHex}/trade";

        uri = QueryHelpers.AddQueryString(uri, Options.ToQueryString());
        uri = QueryHelpers.AddQueryString(uri, "seed", (Seed ?? 0).ToString());
        return uri;
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
}
