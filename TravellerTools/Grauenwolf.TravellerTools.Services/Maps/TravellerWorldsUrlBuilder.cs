namespace Grauenwolf.TravellerTools.Maps;

public static class TravellerWorldsUrlBuilder
{
    const string BaseUrl = "https://www.travellerworlds.com/";

    public static string Build(World world)
    {
        ArgumentNullException.ThrowIfNull(world);

        var query = new List<string>();

        AddQueryParameter(query, "hex", world.Hex);
        AddQueryParameter(query, "sector", world.Sector);
        AddQueryParameter(query, "name", world.Name);
        AddQueryParameter(query, "uwp", world.UWP);

        foreach (var tc in world.RemarksList.Keys)
            AddQueryParameter(query, "tc", tc);

        AddQueryParameter(query, "iX", world.ImportanceCode);
        AddQueryParameter(query, "eX", world.Ex);
        AddQueryParameter(query, "cX", world.Cx);
        AddQueryParameter(query, "pbg", world.PBG);
        if (world.Worlds > 0)
            AddQueryParameter(query, "worlds", world.Worlds.ToString());

        AddQueryParameter(query, "bases", BuildBasesString(world.Bases));
        AddQueryParameter(query, "travelZone", world.Zone, allowEmpty: true);
        AddQueryParameter(query, "nobz", world.Nobility);
        AddQueryParameter(query, "allegiance", world.Allegiance);
        AddQueryParameter(query, "stellar", world.Stellar);
        AddQueryParameter(query, "seed", "1"); //hardcode as 1 for consistency

        return query.Count == 0 ? BaseUrl : $"{BaseUrl}?{string.Join("&", query)}";
    }

    static void AddQueryParameter(List<string> query, string key, string? value, bool allowEmpty = false)
    {
        if (value == null)
            return;

        if (!allowEmpty && string.IsNullOrWhiteSpace(value))
            return;

        query.Add($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
    }

    static string? BuildBasesString(string? bases)
    {
        if (string.IsNullOrWhiteSpace(bases))
            return null;

        var filtered = new string(bases.Where(c => c is 'N' or 'S' or 'W' or 'D').ToArray());
        return string.IsNullOrEmpty(filtered) ? null : filtered;
    }

    static string? GetSeedValue(string? hex, int? querySeed)
    {
        if (!string.IsNullOrEmpty(hex) && hex.Length == 4 && hex.All(char.IsDigit))
            return hex + hex;

        return querySeed?.ToString();
    }
}

