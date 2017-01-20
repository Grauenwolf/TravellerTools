using System.Linq;
using Tortuga.Anchor.Collections;

namespace Grauenwolf.TravellerTools.Characters
{
    public class HistoryCollection : ObservableCollectionExtended<History>
    {
        public void Add(string text)
        {
            if (this.Any(x => x.Text == text))
                return;
            Add(new History(text));
        }
    }
}
