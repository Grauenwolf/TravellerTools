namespace Grauenwolf.TravellerTools.Equipment;

public class Subsection : IHasItems
{
    public List<Item> Items { get; } = new();
    public string? Name { get; set; }
    public string? Species { get; set; }

    public bool ContainsSpecies(string species)
    {
        return Items.Any(x => x.Species == species);
    }
}
