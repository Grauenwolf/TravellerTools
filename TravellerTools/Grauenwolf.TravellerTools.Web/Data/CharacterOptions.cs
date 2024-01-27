using Grauenwolf.TravellerTools.Characters;
using Tortuga.Anchor;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class CharacterOptions : ModelBase
{
    public CharacterOptions(CharacterBuilder characterBuilder)
    {
        CharacterBuilder = characterBuilder;
        SkillList = CharacterBuilder.AllSkills.AddRange(CharacterBuilder.AllPsionicTalents).ToList();

        AgeList = new List<int>();
        for (var terms = -1; terms <= 15; terms++)
            AgeList.Add(terms);
    }

    public List<int> AgeList { get; }

    [CalculatedField("SpeciesOrFaction")]
    public IReadOnlyList<string> CareerList
    {
        get
        {
            if (SpeciesOrFaction.IsNullOrEmpty() || !CharacterBuilder.SpeciesList.Contains(SpeciesOrFaction))
                return CharacterBuilder.CareerNameList;
            else
                return CharacterBuilder.GetCharacterBuilder(SpeciesOrFaction).Careers(null).Select(c => c.Career).Distinct().OrderBy(s => s).ToList();
        }
    }

    public CharacterBuilder CharacterBuilder { get; }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Dex { get => Get<string?>(); set => Set(value); }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Edu { get => Get<string?>(); set => Set(value); }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? End { get => Get<string?>(); set => Set(value); }

    public string? FinalAssignment { get => Get<string?>(); set => Set(value); }

    [CalculatedField("FinalCareer")]
    public List<string> FinalAssignmentList
    {
        get
        {
            if (FinalCareer.IsNullOrEmpty())
                return new List<string>();
            else
                return CharacterBuilder.GetAssignmentList(SpeciesOrFaction, FinalCareer);
        }
    }

    public string? FinalCareer
    {
        get => Get<string?>(); set
        {
            if (Set(value))
                FinalAssignment = "";
        }
    }

    public string? FirstAssignment { get => Get<string?>(); set => Set(value); }

    [CalculatedField("FirstCareer")]
    public List<string> FirstAssignmentList
    {
        get
        {
            if (FirstCareer.IsNullOrEmpty())
                return new List<string>();
            else
                return CharacterBuilder.GetAssignmentList(SpeciesOrFaction, FirstCareer);
        }
    }

    public string? FirstCareer
    {
        get => Get<string?>(); set
        {
            if (Set(value))
                FirstAssignment = "";
        }
    }

    public string? Gender { get => Get<string?>(); set => Set(value); }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Int { get => Get<string?>(); set => Set(value); }

    public bool PreferYounger { get => Get<bool>(); set => Set(value); }
    public string? SkillA { get => Get<string?>(); set => Set(value); }
    public string? SkillB { get => Get<string?>(); set => Set(value); }
    public string? SkillC { get => Get<string?>(); set => Set(value); }
    public string? SkillD { get => Get<string?>(); set => Set(value); }
    public IReadOnlyList<SkillTemplate> SkillList { get; }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Soc { get => Get<string?>(); set => Set(value); }

    public IReadOnlyList<FactionOrSpecies> SpeciesAndFactionsList => CharacterBuilder.FactionsAndSpecies;

    public string? SpeciesOrFaction
    {
        get => Get<string?>(); set
        {
            if (Set(value))
            {
                FirstCareer = "";
                FinalCareer = "";
            }
        }
    }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Str { get => Get<string?>(); set => Set(value); }

    public int? Terms { get => Get<int?>(); set => Set(value); }

    public string? TermsCode
    {
        get => Terms.ToString();
        set
        {
            if (int.TryParse(value, out var score))
                Terms = score;
            else
                Terms = null;
        }
    }

    public int? Year { get => Get<int?>(); set => Set(value); }
}
