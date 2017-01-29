using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment
{
    public class Store
    {
        public List<Section> Sections { get; } = new List<Section>();

        public StoreOptions Options { get; set; }

    }
}
