using System.Diagnostics.CodeAnalysis;

namespace Grauenwolf.TravellerTools.Characters;

public class DistinctTypeComparer<T> : IEqualityComparer<T>
{
    public static DistinctTypeComparer<T> Default { get; } = new();

    public bool Equals(T? x, T? y)
    {
        if (x == null || y == null) return false;
        return x.GetType() == y.GetType();
    }

    public int GetHashCode([DisallowNull] T obj)
    {
        return obj.GetType().GetHashCode();
    }
}
