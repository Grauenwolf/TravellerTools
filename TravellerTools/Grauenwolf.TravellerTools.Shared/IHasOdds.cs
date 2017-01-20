using System.Collections.Generic;

namespace Grauenwolf.TravellerTools
{
    public interface IHasOdds
    {
        int Odds { get; }
    }

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

    public class OddsTable<T> : List<OddsRow<T>>
    {
        public void Add(T value, int odds)
        {
            Add(new OddsRow<T>(value, odds));
        }
    }
}
