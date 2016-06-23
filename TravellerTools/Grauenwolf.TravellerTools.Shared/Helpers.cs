using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools
{
    public static class Helpers
    {
        public static T Single<T>(this IEnumerable<T> list, int roll) where T : ITablePick
        {
            return list.Single(x => x.IsMatch(roll));
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> list, int roll) where T : ITablePick
        {
            return list.Where(x => x.IsMatch(roll));
        }

        public static int Limit(this int value, int MinValue, int MaxValue)
        {
            if (value < MinValue)
                return MinValue;
            if (value > MaxValue)
                return MaxValue;
            return value;
        }
    }
}
