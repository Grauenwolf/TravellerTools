using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class NextTermBenefits
    {
        public string MustEnroll { get; set; }

        public bool FreeCommissionRoll { get; set; }

        public int CommissionDM { get; set; }

        public Dictionary<string, int> EnlistmentDM { get; } = new Dictionary<string, int>();
        public int GraduationDM { get; set; }
        public int QualificationDM { get; set; }
        public bool MusterOut { get; set; }


    }
}
