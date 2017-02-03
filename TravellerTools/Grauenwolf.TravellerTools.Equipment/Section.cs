using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment
{
    public interface IHasItems
    {
        List<Item> Items { get; }
    }
    public class Section : IHasItems
    {
        public string Name { get; set; }

        public string Key => Name.Replace(" ", "").Replace(":", "").Replace(",", "");

        public List<Item> Items { get; } = new List<Item>();

        public List<Subsection> Subsections { get; } = new List<Subsection>();

    }
}
