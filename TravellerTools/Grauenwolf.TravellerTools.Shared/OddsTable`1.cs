using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Grauenwolf.TravellerTools;

[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
public class OddsTable<T> : List<OddsRow<T>>
{
    public void Add(T value, int odds) => Add(new OddsRow<T>(value, odds));
}