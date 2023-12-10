using Microsoft.Extensions.Primitives;

namespace Grauenwolf.TravellerTools.Web.Data;

static class OptionsHelper
{
    public static int ParseInt(this Dictionary<string, StringValues> keyValuePairs, string key, int defaultValue = 0)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
        {
            var stringValue = (string?)value;
            if (stringValue != null)
                return int.Parse(stringValue);
        }
        return defaultValue;
    }

    public static int? ParseIntOrNull(this Dictionary<string, StringValues> keyValuePairs, string key)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
        {
            var stringValue = (string?)value;
            if (stringValue != null)
                return int.Parse(stringValue);
        }
        return null;
    }

    public static string? ParseStringOrNull(this Dictionary<string, StringValues> keyValuePairs, string key)
    {
        if (keyValuePairs.TryGetValue(key, out var value))
            return value;
        else
            return null;
    }
}
