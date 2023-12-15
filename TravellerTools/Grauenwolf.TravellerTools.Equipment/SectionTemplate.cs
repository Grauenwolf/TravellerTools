namespace Grauenwolf.TravellerTools.Equipment;

public class SectionTemplate(string name)
{
    public List<ItemTemplate> Items { get; } = [];
    public string Name { get; } = name;
    public List<SubsectionTemplate> Subsections { get; } = [];

    public bool ContainsSpecies(string species)
    {
        return Items.Any(x => x.Species == species) || Subsections.Any(x => x.ContainsSpecies(species));
    }
}
