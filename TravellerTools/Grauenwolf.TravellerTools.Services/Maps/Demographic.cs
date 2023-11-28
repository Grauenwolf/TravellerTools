namespace Grauenwolf.TravellerTools.Maps;

public class Demographic(string remarkCode, string sophontCode, string name, decimal? population, bool isHomeworld)
    : Remark(remarkCode, BuildDescription(name, population, isHomeworld))
{
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
                _ => (Population.Value * 100).ToString("N0") + '%' //P0 adds a space betwen the number and the '%' symbol
            };
        }
    }

    public string SophontCode { get; } = sophontCode;

    static string BuildDescription(string name, decimal? population, bool isHomeworld)
    {
        string result = name;
        if (isHomeworld)
            result += " Homeworld";

        if (population.HasValue)
        {
            if (population == -1)
                result += ", Extinct";
            else
                result += ", " + population.Value.ToString("P0");
        }
        result += ".";
        return result;
    }
}
