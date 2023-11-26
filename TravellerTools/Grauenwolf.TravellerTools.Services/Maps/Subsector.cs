namespace Grauenwolf.TravellerTools.Maps;

public class Subsector
{
    public Subsector()
    {
    }

    public Subsector(string? name, string? index, int indexNumber)
    {
        Name = name;
        Index = index;
        IndexNumber = indexNumber;
    }

    public string? Index { get; set; }
    public int IndexNumber { get; set; }
    public string? Name { get; set; }
}