namespace Grauenwolf.TravellerTools;

public class MilitaryBase(char code, string description, string? allegiance)
{
    public string? Allegiance { get; } = allegiance;
    public char Code { get; } = code;
    public string Description { get; } = description;
}
