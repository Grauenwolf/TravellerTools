namespace Grauenwolf.TravellerTools.Maps;

public class Demographics(string code, string name, decimal? population, bool isHomeworld)
{
    public string Code { get; } = code;
    public string? HomeworldString => IsHomeworld ? "Homeworld" : null;
    public bool IsHomeworld { get; } = isHomeworld;
    public string Name { get; } = name;
    public decimal? Population { get; } = population;

    public string? PopulationString
    {
        get
        {
            return Population switch
            {
                null => null,
                -1 => "Extinct",
                _ => Population.Value.ToString("P0")
            };
        }
    }
}
