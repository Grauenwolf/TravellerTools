using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Grauenwolf.TravellerTools.Maps;

public class RemarkDictionary() : Dictionary<string, Remark>(StringComparer.OrdinalIgnoreCase)
{
    static readonly RemarkDictionary s_RemarkMasterList = new();

    static RemarkDictionary()
    {
        s_RemarkMasterList.Add("A", "Amber.");
        s_RemarkMasterList.Add("Ab", "Data Repository.");
        s_RemarkMasterList.Add("Ag", "Agricultural.");
        s_RemarkMasterList.Add("An", "Ancient Site.");
        s_RemarkMasterList.Add("As", "Asteroid Belt.");
        s_RemarkMasterList.Add("Ba", "Barren.");
        s_RemarkMasterList.Add("Co", "Cold.");
        s_RemarkMasterList.Add("Cp", "Subsector Capital.");
        s_RemarkMasterList.Add("Cs", "Sector Capital.");
        s_RemarkMasterList.Add("Cx", "Capital.");
        s_RemarkMasterList.Add("Cy", "Colony.");
        s_RemarkMasterList.Add("Da", "Danger (Amber Zone).");
        s_RemarkMasterList.Add("De", "Desert.");
        s_RemarkMasterList.Add("Di", "Dieback.");
        s_RemarkMasterList.Add("Ex", "Exile Camp.");
        s_RemarkMasterList.Add("Fa", "Farming.");
        s_RemarkMasterList.Add("Fl", "Fluid Hydrographics (in place of water).");
        s_RemarkMasterList.Add("Fo", "Forbidden (Red Zone).");
        s_RemarkMasterList.Add("Fr", "Frozen.");
        s_RemarkMasterList.Add("Ga", "Garden World.");
        s_RemarkMasterList.Add("He", "Hellworld.");
        s_RemarkMasterList.Add("Hi", "High Population.");
        s_RemarkMasterList.Add("Ho", "Hot.");
        s_RemarkMasterList.Add("Ht", "High Technology.");
        s_RemarkMasterList.Add("Ic", "Ice Capped.");
        s_RemarkMasterList.Add("In", "Industrialized.");
        s_RemarkMasterList.Add("Lk", "Locked.");
        s_RemarkMasterList.Add("Lo", "Low Population.");
        s_RemarkMasterList.Add("Lt", "Low Technology.");
        s_RemarkMasterList.Add("Mi", "Mining.");
        s_RemarkMasterList.Add("Mr", "Military Rule.");
        s_RemarkMasterList.Add("Na", "Non-Agricultural.");
        s_RemarkMasterList.Add("Ni", "Non-Industrial.");
        s_RemarkMasterList.Add("Oc", "Ocean World.");
        s_RemarkMasterList.Add("Pa", "Pre-Agricultural.");
        s_RemarkMasterList.Add("Pe", "Penal Colony.");
        s_RemarkMasterList.Add("Ph", "Pre-High Population.");
        s_RemarkMasterList.Add("Pi", "Pre-Industrial.");
        s_RemarkMasterList.Add("Po", "Poor.");
        s_RemarkMasterList.Add("Pr", "Pre-Rich.");
        s_RemarkMasterList.Add("Px", "Prison, Exile Camp.");
        s_RemarkMasterList.Add("Pz", "Puzzle (Amber Zone).");
        s_RemarkMasterList.Add("R", "Red.");
        s_RemarkMasterList.Add("Re", "Reserve.");
        s_RemarkMasterList.Add("Ri", "Rich.");
        s_RemarkMasterList.Add("Rs", "Research Station.");
        s_RemarkMasterList.Add("RsA", "Research Station Alpha.");
        s_RemarkMasterList.Add("RsB", "Research Station Beta.");
        s_RemarkMasterList.Add("RsG", "Research Station Gamma.");
        s_RemarkMasterList.Add("Sa", "Satellite (Main World is a moon of a Gas Giant).");
        s_RemarkMasterList.Add("St", "Steppeworld.");
        s_RemarkMasterList.Add("Tr", "Tropic.");
        s_RemarkMasterList.Add("Tu", "Tundra.");
        s_RemarkMasterList.Add("Tz", "Twilight Zone.");
        s_RemarkMasterList.Add("Va", "Vacuum World.");
        s_RemarkMasterList.Add("Wa", "Water World.");
        s_RemarkMasterList.Add("Xb", "Xboat Station.");
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
                var demo = new Demographic(code, match.Groups[1].Value, match.Groups[1].Value, 0.1M * decimal.Parse(match.Groups[2].Value), true);
                s_RemarkMasterList.Add(code, demo);
                return demo;
            }
        }
        //Does it look like a sophont homeworld without a population?
        {
            var regex = new Regex(@"^\((.*)\)$");
            var match = regex.Match(code);
            if (match.Success)
            {
                var demo = new Demographic(code, match.Groups[1].Value, match.Groups[1].Value, null, true);
                s_RemarkMasterList.Add(code, demo);
                return demo;
            }
        }
        //Does it look like am unknown sophont code with a population?
        {
            var regex = new Regex(@"^(....)(\d)$");
            var match = regex.Match(code);
            if (match.Success)
            {
                var demo = new Demographic(code, match.Groups[1].Value, match.Groups[1].Value, 0.1M * decimal.Parse(match.Groups[2].Value), false);
                s_RemarkMasterList.Add(code, demo);
                return demo;
            }
        }

        return new Remark(code, null);
    }

    public void Add(string remarkCode, string description) => Add(remarkCode, new Remark(remarkCode, description));

    public void AddDemographic(string remarkCode, string sophontCode, string name, decimal? population, bool isHomeworld) =>
        this[remarkCode] = new Demographic(remarkCode, sophontCode, name, population, isHomeworld);

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

            s_RemarkMasterList.AddDemographic(code.Code, code.Code, name, null, false);
            for (int i = 0; i <= 9; i++)
            {
                s_RemarkMasterList.AddDemographic(code.Code + i, code.Code, name, i * 0.10M, false);
            }
            s_RemarkMasterList.AddDemographic(code.Code + 'W', code.Code, name, 1.0M, false);

            for (int i = 0; i <= 9; i++)
            {
                s_RemarkMasterList.AddDemographic("[" + code.Code + "]" + i, code.Code, name, i * 0.10M, true);
            }
            s_RemarkMasterList.AddDemographic("[" + code.Code + "]", code.Code, name, null, true);
            s_RemarkMasterList.AddDemographic("[" + code.Code + "]W", code.Code, name, 1.0M, true);

            for (int i = 0; i <= 9; i++)
            {
                s_RemarkMasterList.AddDemographic("(" + code.Code + ")" + i, code.Code, name, i * 0.10M, true);
            }
            s_RemarkMasterList.AddDemographic("(" + code.Code + ")", code.Code, name, null, true);
            s_RemarkMasterList.AddDemographic("(" + code.Code + ")W", code.Code, name, 1.0M, true);
            s_RemarkMasterList.AddDemographic("Di(" + code.Code + ")", code.Code, name, -1, true);
        }
    }
}
