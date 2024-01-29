namespace Grauenwolf.TravellerTools;

public record OddsRow<T>(T value, int odds) : IHasOdds
{
    public int Odds { get; } = odds;
    public T Value { get; } = value;

    public static implicit operator OddsRow<T>(ValueTuple<T, int> v)
    {
        return new OddsRow<T>(v.Item1, v.Item2);
    }
}
