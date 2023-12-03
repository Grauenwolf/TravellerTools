using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Grauenwolf.TravellerTools.Characters;

public class HistoryCollection : Collection<History>
{
    protected override void InsertItem(int index, History item)
    {
        var maxAge = Count > 0 ? this.Max(s => s.Age) : 0;
        if (item.Age < maxAge)
            Debug.WriteLine($"Age inversion detected!!!! Attempting to add age {item.Age} after age {maxAge}.");
        base.InsertItem(index, item);
    }
}
