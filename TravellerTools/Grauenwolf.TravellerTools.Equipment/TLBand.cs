namespace Grauenwolf.TravellerTools.Equipment;

public class TLBand(string? techLevels, int targetNumber, bool highTech)
{
    public bool HighTech { get; } = highTech;
    public int TargetNumber { get; } = targetNumber;
    public string? TechLevels { get; } = techLevels;
}
