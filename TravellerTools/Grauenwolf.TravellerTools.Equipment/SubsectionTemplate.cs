namespace Grauenwolf.TravellerTools.Equipment;

public class SubsectionTemplate(string name)
{
    public List<ItemTemplate> Items { get; } = [];
    public string Name { get; } = name;

    public bool ContainsSpecies(string species)
    {
        return Items.Any(x => x.Species == species);
    }
}
