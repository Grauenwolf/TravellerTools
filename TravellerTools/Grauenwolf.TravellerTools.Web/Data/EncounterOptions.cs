using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Encounters;

namespace Grauenwolf.TravellerTools.Web.Data;

public class EncounterOptions
{
    public List<Encounter> Encounters { get; } = new();

    public IReadOnlyList<FactionOrSpecies> SpeciesAndFactionsList { get; set; } = null!;
    public string? SpeciesOrFaction { get; set; }
}
