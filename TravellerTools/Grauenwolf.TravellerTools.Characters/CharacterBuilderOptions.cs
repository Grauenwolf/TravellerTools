using Grauenwolf.TravellerTools.Shared;
using Microsoft.Extensions.Primitives;

namespace Grauenwolf.TravellerTools.Characters;

public class CharacterBuilderOptions
{
    public string? FirstAssignment { get; set; }

    public string? FirstCareer { get; set; }

    public string? Gender { get; set; }
    public int? MaxAge { get; set; }
    public string? Name { get; set; }

    public int? Seed { get; set; }
    public string? Species { get; set; }
    public int? Year { get; set; }

    public void FromQueryString(Dictionary<string, StringValues> keyValuePairs)
    {
        if (keyValuePairs.TryGetValue("year", out var year))
            Year = int.Parse(year!);

        Seed = keyValuePairs.ParseIntOrNull("seed");

        if (keyValuePairs.TryGetValue("name", out var name))
            Name = name;
        if (keyValuePairs.TryGetValue("maxAge", out var maxAge))
        {
            MaxAge = int.Parse(maxAge!);
            if (MaxAge == 0)
                MaxAge = null;
        }
        if (keyValuePairs.TryGetValue("gender", out var gender))
            Gender = gender;

        if (keyValuePairs.TryGetValue("firstAssignment", out var firstAssignment))
            FirstAssignment = firstAssignment;

        if (keyValuePairs.TryGetValue("firstCareer", out var firstCareer))
            FirstCareer = firstCareer;

        if (keyValuePairs.TryGetValue("species", out var species))
            Species = species;
    }

    public Dictionary<string, string?> ToQueryString()
    {
        return new Dictionary<string, string?>
        {
            { "seed", Seed.ToString()},
            { "name", Name },
            { "maxAge", (MaxAge??0).ToString() },
            { "gender", Gender },
            { "firstAssignment", FirstAssignment },
            { "firstCareer", FirstCareer },
            { "species", Species },
            { "year", Year?.ToString() }
        };
    }
}
