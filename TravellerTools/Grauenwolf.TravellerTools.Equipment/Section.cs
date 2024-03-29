﻿namespace Grauenwolf.TravellerTools.Equipment;

public class Section
{
    public List<Item> Items { get; } = new();
    public string? Name { get; set; }
    public List<Subsection> Subsections { get; } = new();

    public bool ContainsSpecies(string species)
    {
        return Items.Any(x => x.Species == species) || Subsections.Any(x => x.ContainsSpecies(species));
    }
}
