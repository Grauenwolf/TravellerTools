namespace Grauenwolf.TravellerTools.Maps;

public class SectorMetadata
{
    public string? Abbreviation { get; set; }
    public Allegiance[]? Allegiances { get; set; }
    public Border[]? Borders { get; set; }
    public string[]? Credits { get; set; }
    public Datafile? DataFile { get; set; }
    public object[]? Labels { get; set; }
    public string? Name => Names?[0].Text;
    public Name[]? Names { get; set; }
    public Product[]? Products { get; set; }
    public Route[]? Routes { get; set; }
    public bool Selected { get; set; }
    public string? Stylesheet { get; set; }
    public Subsector[]? Subsectors { get; set; }
    public string? Tags { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}