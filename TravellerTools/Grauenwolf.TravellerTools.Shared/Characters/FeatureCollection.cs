using Tortuga.Anchor.Collections;

namespace Grauenwolf.TravellerTools.Characters
{


    public class FeatureCollection : ObservableCollectionExtended<Feature>
    {
        public void Add(string text)
        {
            Add(new Feature(text));
        }
    }
}
