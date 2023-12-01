using Grauenwolf.TravellerTools.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class CharacterOptions : ModelBase
{
    public CharacterOptions(CharacterBuilder characterBuilder)
    {
        CharacterBuilder = characterBuilder ?? throw new ArgumentNullException(nameof(characterBuilder), $"{nameof(characterBuilder)} is null.");
        CareerList = CharacterBuilder.Careers.Select(c => c.Career).Distinct().OrderBy(s => s).ToList();
        SkillList = CharacterBuilder.Book.AllSkills.AddRange(CharacterBuilder.Book.PsionicTalents).ToList();

        AgeList = new List<int>();
        for (var terms = 0; terms <= 15; terms++)
            AgeList.Add(terms);
    }

    public List<int> AgeList { get; }
    public List<string> CareerList { get; }

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
            if (CharacterBuilder == null || string.IsNullOrEmpty(FinalCareer))
                return new List<string>();
            else
                return CharacterBuilder.Careers.Where(c => c.Career == FinalCareer).Select(c => c.Assignment).OrderBy(s => s).ToList()!;
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
            if (CharacterBuilder == null || string.IsNullOrEmpty(FirstCareer))
                return new List<string>();
            else
                return CharacterBuilder.Careers.Where(c => c.Career == FirstCareer).Select(c => c.Assignment).OrderBy(s => s).ToList();
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
    public List<SkillTemplate> SkillList { get; }

    /// <summary>
    /// Valid values are High, Low, and empty.
    /// </summary>
    public string? Soc { get => Get<string?>(); set => Set(value); }

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
}
