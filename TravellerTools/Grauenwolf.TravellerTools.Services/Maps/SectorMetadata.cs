namespace Grauenwolf.TravellerTools.Maps
{
    public class SectorMetadata
    {
        public bool Selected { get; set; }
        public string? Tags { get; set; }
        public string? Abbreviation { get; set; }
        public Name[]? Names { get; set; }
        public string? Name { get { return Names?[0].Text; } }
        public string[]? Credits { get; set; }
        public Datafile? DataFile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Product[]? Products { get; set; }
        public Subsector[]? Subsectors { get; set; }
        public Allegiance[]? Allegiances { get; set; }
        public string? Stylesheet { get; set; }
        public object[]? Labels { get; set; }
        public Border[]? Borders { get; set; }
        public Route[]? Routes { get; set; }
    }
}
