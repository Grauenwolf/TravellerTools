using System;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment;

static class Helpers
{
    public static bool Contains(this IEnumerable<string> list, string value, StringComparison comparisonType)
    {
        foreach (var item in list)
            if (string.Equals(item, value, comparisonType))
                return true;
        return false;
    }
}