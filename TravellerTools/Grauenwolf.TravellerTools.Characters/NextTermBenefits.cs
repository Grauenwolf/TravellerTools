using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters.Careers;

class NextTermBenefits
{
    public int AdvancementDM { get; set; }
    public int CommissionDM { get; set; }
    public Dictionary<string, int> EnlistmentDM { get; } = new();
    public bool FreeCommissionRoll { get; set; }
    public int GraduationDM { get; set; }
    public string? MustEnroll { get; set; }
    public bool MusterOut { get; set; }
    public int QualificationDM { get; set; }
    public int SurvivalDM { get; set; }
}