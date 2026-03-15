using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;

namespace Grauenwolf.TravellerTools.Web.Pages;

static class SpeciesOrFactionSelection
{
    public static async Task<string?> ResolveForWorldAsync(
        string? currentSelection,
        string? milieuCode,
        string? sectorHex,
        string? planetHex,
        TravellerMapServiceLocator travellerMapServiceLocator,
        IReadOnlyList<FactionOrSpecies> speciesAndFactions)
    {
        if (!string.IsNullOrWhiteSpace(currentSelection))
            return currentSelection;

        if (string.IsNullOrWhiteSpace(milieuCode)
            || string.IsNullOrWhiteSpace(sectorHex)
            || string.IsNullOrWhiteSpace(planetHex)
            || Milieu.FromCode(milieuCode) == null)
            return null;

        var service = travellerMapServiceLocator.GetMapService(milieuCode);
        var world = await service.FetchWorldAsync(sectorHex, planetHex).ConfigureAwait(false);
        if (world == null)
            return null;

        return FindSpeciesOrFactionFromAllegiance(world.Allegiance, world.AllegianceName, speciesAndFactions);
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

    static string? FindSpeciesOrFactionFromAllegiance(string? allegianceCode, string? allegianceName, IReadOnlyList<FactionOrSpecies> speciesAndFactions)
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

        foreach (var item in speciesAndFactions)
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
}

