using Grauenwolf.TravellerTools.Characters;
using Tortuga.Anchor;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class CharacterOptions : ModelBase
{
    public CharacterOptions(CharacterBuilderLocator characterBuilderLocator)
    {
        CharacterBuilderLocator = characterBuilderLocator;
        SkillList = CharacterBuilderLocator.AllSkills.AddRange(CharacterBuilderLocator.AllPsionicTalents).ToList();

        AgeList = new List<int>();
        for (var terms = 0; terms <= 15; terms++)
            AgeList.Add(terms);
    }

    public List<int> AgeList { get; }

    [CalculatedField("Species")]
    public IReadOnlyList<string> CareerList
    {
        get
        {
            if (Species.IsNullOrEmpty())
                return CharacterBuilderLocator.CareerNameList;
            else
                return CharacterBuilderLocator.GetCharacterBuilder(Species).Careers.Select(c => c.Career).Distinct().OrderBy(s => s).ToList();
        }
    }

    public CharacterBuilderLocator CharacterBuilderLocator { get; }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Dex { get => Get<string?>(); set => Set(value); }

    //TODO: Make this user configurable
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
                return CharacterBuilderLocator.GetAssignmentList(Species, FinalCareer);
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
                return CharacterBuilderLocator.GetAssignmentList(Species, FirstCareer);
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

    public string? SkillA { get => Get<string?>(); set => Set(value); }
    public string? SkillB { get => Get<string?>(); set => Set(value); }
    public string? SkillC { get => Get<string?>(); set => Set(value); }
    public string? SkillD { get => Get<string?>(); set => Set(value); }
    public IReadOnlyList<SkillTemplate> SkillList { get; }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Soc { get => Get<string?>(); set => Set(value); }

    public string? Species
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

    public IReadOnlyList<string> SpeciesList => CharacterBuilderLocator.SpeciesList;

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
