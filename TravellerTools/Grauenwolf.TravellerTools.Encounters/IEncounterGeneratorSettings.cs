using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;

namespace Grauenwolf.TravellerTools.Encounters;

public interface IEncounterGeneratorSettings : ISpeciesSettings
{
    World? World { get; } //not currently used.
}
