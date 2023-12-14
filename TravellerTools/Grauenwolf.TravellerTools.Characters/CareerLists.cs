using Grauenwolf.TravellerTools.Characters.Careers;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters;

public record struct CareerLists(ImmutableArray<CareerBase> defaultCareers, ImmutableArray<CareerBase> draftCareers, ImmutableArray<CareerBase> allCareers)
{
}
