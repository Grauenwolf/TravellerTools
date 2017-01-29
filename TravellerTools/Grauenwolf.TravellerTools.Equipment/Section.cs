using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment
{
    public class Section
    {
        public string Name { get; set; }

        public List<Item> Items { get; } = new List<Item>();

    }
}
