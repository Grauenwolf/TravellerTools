using Microsoft.Extensions.Primitives;

namespace Grauenwolf.TravellerTools.Shared;

public static class OptionsHelper
{
    public static bool ParseBool(this Dictionary<string, StringValues> keyValuePairs, string key, bool defaultValue = false)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
        {
            if (bool.TryParse(value, out var result))
                return result;
        }
        return defaultValue;
    }

    public static int ParseInt(this Dictionary<string, StringValues> keyValuePairs, string key, int defaultValue = 0)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
        {
            if (int.TryParse(value, out var result))
                return result;
        }
        return defaultValue;
    }

    public static int? ParseIntOrNull(this Dictionary<string, StringValues> keyValuePairs, string key)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
        {
            if (int.TryParse(value, out var result))
                return result;
        }
        return null;
    }

    public static string ParseString(this Dictionary<string, StringValues> keyValuePairs, string key, string defaultValue = "")
    {
        if (keyValuePairs.TryGetValue(key, out var value))
            return (string?)value ?? defaultValue;
        else
            return defaultValue;
    }

    public static string? ParseStringOrNull(this Dictionary<string, StringValues> keyValuePairs, string key)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
            return value;
        else
            return null;
    }
}
