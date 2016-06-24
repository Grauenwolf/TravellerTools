using System.Linq;
using Tortuga.Anchor.Collections;

namespace Grauenwolf.TravellerTools.Characters
{


    public class FeatureCollection : ObservableCollectionExtended<Feature>
    {
        public void Add(string text)
        {
            if (this.Any(x => x.Text == text))
                return;
            Add(new Feature(text));
        }
    }
}
