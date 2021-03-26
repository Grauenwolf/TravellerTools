using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment
{
    public class Store
    {
        internal Store()
        {
        }

        public List<Section> Sections { get; } = new List<Section>();
        public List<CatalogBook> Books { get; } = new List<CatalogBook>();
    }
}
