using Grauenwolf.TravellerTools.Characters.Careers;
using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools.Characters;

public record struct CareerLists(ImmutableArray<CareerBase> DefaultCareers, ImmutableArray<CareerBase> DraftCareers, ImmutableArray<CareerBase> AllCareers)
{
    public static CareerLists Empty { get; } = new CareerLists(ImmutableArray<CareerBase>.Empty, ImmutableArray<CareerBase>.Empty, ImmutableArray<CareerBase>.Empty);
}
