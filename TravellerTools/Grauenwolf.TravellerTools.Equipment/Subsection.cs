using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment
{
    public class Subsection : IHasItems
    {
        //public string? SectionKey { get; set; }

        public string? Name { get; set; }

        //public string Key => SectionKey + "_" + Name?.Replace(" ", "").Replace(":", "").Replace(",", "");

        public List<Item> Items { get; } = new List<Item>();
    }
}
