using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment;

public class Store
{
    internal Store()
    {
    }

    public List<CatalogBook> Books { get; } = new();
    public List<Section> Sections { get; } = new();
}