namespace Grauenwolf.TravellerTools
{
    public class OddsRow<T> : IHasOdds
    {
        public OddsRow(T value, int odds)
        {
            Odds = odds;
            Value = value;
        }

        public T Value { get; }

        public int Odds { get; }
    }
}
