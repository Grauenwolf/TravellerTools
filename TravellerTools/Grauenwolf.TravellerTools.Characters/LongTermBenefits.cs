namespace Grauenwolf.TravellerTools.Characters.Careers;

class LongTermBenefits
{
    public int AdvancementDM { get; set; }

    public int CommissionDM { get; set; }

    /// <summary>
    /// Gets DM for qualifing for a career or assignemnt.
    /// </summary>
    public Dictionary<string, int> EnlistmentDM { get; } = new();

    public bool MayEnrollInSchool { get; set; } = true;
    public bool MayTestPsi { get; set; }
    public int PrisonSurvivalDM { get; set; }

    /// <summary>
    /// A flat bonus for any qualification rool.
    /// </summary>
    public int QualificationDM { get; set; }

    public bool Retired { get; set; }
}
