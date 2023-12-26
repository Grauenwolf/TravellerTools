using Grauenwolf.TravellerTools.Characters;

namespace Grauenwolf.TravellerTools.Web.Data;

public class CrewModel
{
    public List<CrewMember> Crew { get; set; } = new();
    public int Seed { get; set; }
}
