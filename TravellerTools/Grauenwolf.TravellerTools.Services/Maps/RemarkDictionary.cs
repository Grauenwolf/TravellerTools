using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Grauenwolf.TravellerTools.Maps;

public class RemarkDictionary() : Dictionary<string, Remark>(StringComparer.OrdinalIgnoreCase)
{
    static readonly ConcurrentDictionary<string, Remark> s_RemarkMasterList = new(StringComparer.OrdinalIgnoreCase);

    static RemarkDictionary()
    {
        RegisterRemark("A", "Amber.");
        RegisterRemark("Ab", "Data Repository.");
        RegisterRemark("Ag", "Agricultural.");
        RegisterRemark("An", "Ancient Site.");
        RegisterRemark("As", "Asteroid Belt.");
        RegisterRemark("Ba", "Barren.");
        RegisterRemark("Co", "Cold.");
        RegisterRemark("Cp", "Subsector Capital.");
        RegisterRemark("Cs", "Sector Capital.");
        RegisterRemark("Cx", "Capital.");
        RegisterRemark("Cy", "Colony.");
        RegisterRemark("Da", "Danger (Amber Zone).");
        RegisterRemark("De", "Desert.");
        RegisterRemark("Di", "Dieback.");
        RegisterRemark("Ex", "Exile Camp.");
        RegisterRemark("Fa", "Farming.");
        RegisterRemark("Fl", "Fluid Hydrographics (in place of water).");
        RegisterRemark("Fo", "Forbidden (Red Zone).");
        RegisterRemark("Fr", "Frozen.");
        RegisterRemark("Ga", "Garden World.");
        RegisterRemark("He", "Hellworld.");
        RegisterRemark("Hi", "High Population.");
        RegisterRemark("Ho", "Hot.");
        RegisterRemark("Ht", "High Technology.");
        RegisterRemark("Ic", "Ice Capped.");
        RegisterRemark("In", "Industrialized.");
        RegisterRemark("Lk", "Locked.");
        RegisterRemark("Lo", "Low Population.");
        RegisterRemark("Lt", "Low Technology.");
        RegisterRemark("Mi", "Mining.");
        RegisterRemark("Mr", "Military Rule.");
        RegisterRemark("Na", "Non-Agricultural.");
        RegisterRemark("Ni", "Non-Industrial.");
        RegisterRemark("Oc", "Ocean World.");
        RegisterRemark("Pa", "Pre-Agricultural.");
        RegisterRemark("Pe", "Penal Colony.");
        RegisterRemark("Ph", "Pre-High Population.");
        RegisterRemark("Pi", "Pre-Industrial.");
        RegisterRemark("Po", "Poor.");
        RegisterRemark("Pr", "Pre-Rich.");
        RegisterRemark("Px", "Prison, Exile Camp.");
        RegisterRemark("Pz", "Puzzle (Amber Zone).");
        RegisterRemark("R", "Red.");
        RegisterRemark("Re", "Reserve.");
        RegisterRemark("Ri", "Rich.");
        RegisterRemark("Rs", "Research Station.");
        RegisterRemark("RsA", "Research Station Alpha.");
        RegisterRemark("RsB", "Research Station Beta.");
        RegisterRemark("RsG", "Research Station Gamma.");
        RegisterRemark("Sa", "Satellite (Main World is a moon of a Gas Giant).");
        RegisterRemark("St", "Steppeworld.");
        RegisterRemark("Tr", "Tropic.");
        RegisterRemark("Tu", "Tundra.");
        RegisterRemark("Tz", "Twilight Zone.");
        RegisterRemark("Va", "Vacuum World.");
        RegisterRemark("Wa", "Water World.");
        RegisterRemark("Xb", "Xboat Station.");
    }

    public RemarkDictionary(string remarks) : this()
    {
        foreach (var item in remarks.Split(' '))
        {
            Add(item, GetRemark(item));
        }
    }

    public static Remark GetRemark(string code)
    {
        if (s_RemarkMasterList.TryGetValue(code, out var remark))
            return remark;

        //Does it look like a sophont homeworld with a population?
        {
            var regex = new Regex(@"^\((.*)\)(\d)$");
            var match = regex.Match(code);
            if (match.Success)
            {
                return RegisterDemographic(code, match.Groups[1].Value, match.Groups[1].Value, 0.1M * decimal.Parse(match.Groups[2].Value), true); ;
            }
        }
        //Does it look like a sophont homeworld without a population?
        {
            var regex = new Regex(@"^\((.*)\)$");
            var match = regex.Match(code);
            if (match.Success)
            {
                return RegisterDemographic(code, match.Groups[1].Value, match.Groups[1].Value, null, true);
            }
        }
        //Does it look like am unknown sophont code with a population?
        {
            var regex = new Regex(@"^(....)(\d)$");
            var match = regex.Match(code);
            if (match.Success)
            {
                return RegisterDemographic(code, match.Groups[1].Value, match.Groups[1].Value, 0.1M * decimal.Parse(match.Groups[2].Value), false);
            }
        }

        return new Remark(code, null);
    }

    public List<Demographic> GetDemographics()
    {
        return this.Values.OfType<Demographic>().OrderByDescending(d => d.Population).ThenBy(d => d.Name).ToList();
    }

    public bool TryAdd(string remarkCode)
    {
        return TryAdd(remarkCode, GetRemark(remarkCode));
    }

    public bool TryAdd(string code, string description) => TryAdd(code, new Remark(code, description));

    internal static void AddSophontCodes(IEnumerable<SophontCode> sophontCodes)
    {
        foreach (var code in sophontCodes.Where(c => c.Code != null))
        {
            if (code.Code == null)
                continue;

            var name = code.Name ?? "<unknown>";

            RegisterDemographic(code.Code, code.Code, name, null, false);
            for (int i = 0; i <= 9; i++)
            {
                RegisterDemographic(code.Code + i, code.Code, name, i * 0.10M, false);
            }
            RegisterDemographic(code.Code + 'W', code.Code, name, 1.0M, false);

            for (int i = 0; i <= 9; i++)
            {
                RegisterDemographic("[" + code.Code + "]" + i, code.Code, name, i * 0.10M, true);
            }
            RegisterDemographic("[" + code.Code + "]", code.Code, name, null, true);
            RegisterDemographic("[" + code.Code + "]W", code.Code, name, 1.0M, true);

            for (int i = 0; i <= 9; i++)
            {
                RegisterDemographic("(" + code.Code + ")" + i, code.Code, name, i * 0.10M, true);
            }
            RegisterDemographic("(" + code.Code + ")", code.Code, name, null, true);
            RegisterDemographic("(" + code.Code + ")W", code.Code, name, 1.0M, true);
            RegisterDemographic("Di(" + code.Code + ")", code.Code, name, -1, true);
        }
    }

    internal static Demographic RegisterDemographic(string remarkCode, string sophontCode, string name, decimal? population, bool isHomeworld)
    {
        var result = new Demographic(remarkCode, sophontCode, name, population, isHomeworld);
        s_RemarkMasterList[remarkCode] = result;
        return result;
    }

    internal static void RegisterRemark(string remarkCode, string description) => s_RemarkMasterList[remarkCode] = new Remark(remarkCode, description);
}
