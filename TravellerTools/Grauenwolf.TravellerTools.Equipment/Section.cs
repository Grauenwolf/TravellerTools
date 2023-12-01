namespace Grauenwolf.TravellerTools.Equipment;

public class Section : IHasItems
{
    public List<Item> Items { get; } = new List<Item>();
    public string? Name { get; set; }
    public List<Subsection> Subsections { get; } = new List<Subsection>();
}
