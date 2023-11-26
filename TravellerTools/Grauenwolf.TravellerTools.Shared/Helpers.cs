using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools;

public static class Helpers
{
    public static int Limit(this int value, int minValue, int maxValue)
    {
        if (value < minValue)
            return minValue;
        if (value > maxValue)
            return maxValue;
        return value;
    }

    public static T Single<T>(this IEnumerable<T> list, int roll) where T : ITablePick
    {
        return list.Single(x => x.IsMatch(roll));
    }

    public static int? ToIntOrNull(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        return int.Parse(value);
    }
}