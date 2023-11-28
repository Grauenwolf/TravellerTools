namespace Grauenwolf.TravellerTools.Maps;

public class Remark(string remarkCode, string? description)
{
    public string? Description { get; } = description;
    public string RemarkCode { get; } = remarkCode;
}
