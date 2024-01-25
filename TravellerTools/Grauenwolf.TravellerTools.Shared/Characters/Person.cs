using System.Text;

namespace Grauenwolf.TravellerTools.Characters;

public class Person
{
    public int ApparentAge { get; set; }

    public string Characteristics
    {
        get
        {
            var result = new StringBuilder();
            result.Append($"STR {Strength.Value} ({StrengthDM}) ");
            result.Append($"DEX {Dexterity.Value} ({DexterityDM}) ");
            result.Append($"END {Endurance.Value} ({EnduranceDM}) ");
            result.Append($"INT {Intellect.Value} ({IntellectDM}) ");
            result.Append($"EDU {Education.Value} ({EducationDM}) ");
            result.Append($"SOC {SocialStanding.Value} ({SocialStandingDM}) ");

            //TODO: Add secondary stats
            //result.Append($"TER {Ter.Value} ({EducationDM}) ");

            return result.ToString().Trim();
        }
    }

    public EHex Dexterity { get; set; }
    public int DexterityDM => Tables.DMCalc(Dexterity);
    public EHex Education { get; set; }
    public int EducationDM => Tables.DMCalc(Education);
    public EHex Endurance { get; set; }
    public int EnduranceDM => Tables.DMCalc(Endurance);
    public string? Gender { get; set; }
    public EHex Intellect { get; set; }
    public int IntellectDM => Tables.DMCalc(Intellect);
    public bool IsPatron { get; set; }
    public string? Name { get; set; }
    public string? PatronMission { get; set; }
    public EHex SocialStanding { get; set; }
    public int SocialStandingDM => Tables.DMCalc(SocialStanding);
    public EHex Strength { get; set; }
    public int StrengthDM => Tables.DMCalc(Strength);
    public string? Title { get; set; }
    public string? Trait { get; set; }

    public string Upp
    {
        get { return Strength.ToString() + Dexterity.ToString() + Endurance.ToString() + Intellect.ToString() + Education.ToString() + SocialStanding.ToString(); }
    }
}
