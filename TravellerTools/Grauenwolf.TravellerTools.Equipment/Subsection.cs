namespace Grauenwolf.TravellerTools.Equipment;

public class Subsection : IHasItems
{
    public List<Item> Items { get; } = new();
    public string? Name { get; set; }
}
