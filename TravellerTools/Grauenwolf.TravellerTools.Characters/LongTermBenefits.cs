using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class LongTermBenefits
    {
        /// <summary>
        /// Gets DM for qualifing for a career or assignemnt.
        /// </summary>
        public Dictionary<string, int> EnlistmentDM { get; } = new Dictionary<string, int>();

        public bool MayEnrollInSchool { get; set; } = true;
        public bool MayTestPsi { get; set; }
        public int PrisonSurvivalDM { get; set; }

        /// <summary>
        /// A flat bonus for any qualification rool.
        /// </summary>
        public int QualificationDM { get; set; }
        public int CommissionDM { get; set; }
        public int AdvancementDM { get; set; }
        public bool Retired { get; set; }
    }
}
