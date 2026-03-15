using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Encounters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class EncountersPage
{
    [Parameter]
    public string? MilieuCode { get; set; }

    [Parameter]
    public string? PlanetHex { get; set; }

    [Parameter]
    public string? SectorHex { get; set; }

    [Parameter]
    public string? Uwp { get; set; }

    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    [Inject] EncounterGenerator EncounterGenerator { get; set; } = null!;
    [Inject] TravellerMapServiceLocator TravellerMapServiceLocator { get; set; } = null!;

    protected void BackwaterStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BackwaterStarportGeneralEncounter(new Dice(), Model));
    }

    protected void BackwaterStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BackwaterStarportSignificantEncounter(new Dice(), Model));
    }

    protected void BustlingStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BustlingStarportGeneralEncounter(new Dice(), Model));
    }

    protected void BustlingStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.BustlingStarportSignificantEncounter(new Dice(), Model));
    }

    protected void GenerateAlliesAndEnemies()
    {
        Model.Encounters.Insert(0, EncounterGenerator.GenerateAlliesAndEnemies(new Dice(), Model));
    }

    protected void GenerateMission()
    {
        Model.Encounters.Insert(0, EncounterGenerator.GenerateMission(new Dice(), Model));
    }

    protected void GenerateNpc()
    {
        Model.Encounters.Insert(0, EncounterGenerator.GenerateNpc(new Dice(), Model, Model.CareerType, Model.NpcCount));
    }

    protected void GeneratePatron()
    {
        Model.Encounters.Insert(0, EncounterGenerator.GeneratePatron(new Dice(), Model));
    }

    protected override void Initialized()
    {
        Model.SpeciesAndFactionsList = CharacterBuilder.FactionsAndSpecies;
    }

    protected override async Task ParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(Model.SpeciesOrFaction)
            && !string.IsNullOrWhiteSpace(MilieuCode)
            && !string.IsNullOrWhiteSpace(SectorHex)
            && !string.IsNullOrWhiteSpace(PlanetHex))
        {
            var milieu = Milieu.FromCode(MilieuCode);
            if (milieu == null)
                return;

            var service = TravellerMapServiceLocator.GetMapService(MilieuCode);
            var world = await service.FetchWorldAsync(SectorHex, PlanetHex).ConfigureAwait(false);
            if (world == null)
                return;

            Model.SpeciesOrFaction = FindSpeciesOrFactionFromAllegiance(world.Allegiance, world.AllegianceName);
        }
    }

    static string NormalizeForMatch(string value)
    {
        Span<char> buffer = stackalloc char[value.Length];
        var index = 0;
        var previousWasSpace = true;

        foreach (var character in value)
        {
            if (char.IsLetterOrDigit(character))
            {
                buffer[index++] = char.ToLowerInvariant(character);
                previousWasSpace = false;
            }
            else if (!previousWasSpace)
            {
                buffer[index++] = ' ';
                previousWasSpace = true;
            }
        }

        if (index > 0 && buffer[index - 1] == ' ')
            index--;

        return new string(buffer[..index]);
    }

    static int MatchScore(string source, string option)
    {
        if (source == option)
            return 1000 + option.Length;

        var score = 0;

        if (source.Contains(option, StringComparison.Ordinal))
            score = Math.Max(score, 700 + option.Length);

        if (source.Length >= 4 && option.Contains(source, StringComparison.Ordinal))
            score = Math.Max(score, 600 + source.Length);

        foreach (var token in option.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (token.Length < 3)
                continue;

            if (source.Contains(token, StringComparison.Ordinal))
                score += token.Length * 3;
        }

        return score;
    }

    string? FindSpeciesOrFactionFromAllegiance(string? allegianceCode, string? allegianceName)
    {
        var sourceValues = new[] { allegianceName, allegianceCode }
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => NormalizeForMatch(x!))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (sourceValues.Length == 0)
            return null;

        FactionOrSpecies? bestFaction = null;
        var bestFactionScore = 0;
        FactionOrSpecies? bestSpecies = null;
        var bestSpeciesScore = 0;

        foreach (var item in Model.SpeciesAndFactionsList)
        {
            var itemKey = NormalizeForMatch(item.Key);
            if (string.IsNullOrWhiteSpace(itemKey))
                continue;

            var itemDisplay = NormalizeForMatch(item.DisplayText);
            var score = sourceValues.Select(source => Math.Max(MatchScore(source, itemKey), MatchScore(source, itemDisplay))).Max();

            if (item.IsFaction)
            {
                if (score > bestFactionScore)
                {
                    bestFactionScore = score;
                    bestFaction = item;
                }
            }
            else if (score > bestSpeciesScore)
            {
                bestSpeciesScore = score;
                bestSpecies = item;
            }
        }

        if (bestFactionScore > 0)
            return bestFaction!.Key;

        if (bestSpeciesScore > 0)
            return bestSpecies!.Key;

        return null;
    }

    protected void MetropolisStarportGeneralEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.MetropolisStarportGeneralEncounter(new Dice(), Model));
    }

    protected void MetropolisStarportSignificantEncounter()
    {
        Model.Encounters.Insert(0, EncounterGenerator.MetropolisStarportSignificantEncounter(new Dice(), Model));
    }
}
