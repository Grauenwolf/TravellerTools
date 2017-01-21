using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class LongTermBenefits
    {
        public Dictionary<string, int> EnlistmentDM { get; } = new Dictionary<string, int>();

        public bool MayEnrollInSchool { get; set; } = true;
        public bool MayTestPsi { get; set; }
        public int PrisonSurvivalDM { get; set; }
    }
}
