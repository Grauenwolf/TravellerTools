namespace Grauenwolf.TravellerTools;

public class OddsRow<T>(T value, int odds) : IHasOdds
{
    public int Odds { get; } = odds;
    public T Value { get; } = value;
}