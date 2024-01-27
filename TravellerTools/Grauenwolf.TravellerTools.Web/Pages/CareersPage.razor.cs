using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Characters.Careers;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class CareersPage
{
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;

    List<SpeciesDetails>? Species { get; set; }

    protected override void Initialized()
    {
        Species = CharacterBuilder.SpeciesList
            .Select(s => CharacterBuilder.GetCharacterBuilder(s))
            .Select(s => new SpeciesDetails(s))
            .ToList();
    }

    class CareerDetail(string career, List<string> assignments, List<string> sources)
    {
        public List<string> Assignments { get; } = assignments;
        public string Career { get; } = career;
        public List<string> Sources { get; } = sources;
    }

    class SpeciesDetails
    {
        public SpeciesDetails(SpeciesCharacterBuilder characterBuilder)
        {
            Species = characterBuilder.Species;
            SpeciesUrl = characterBuilder.SpeciesUrl;
            Source = characterBuilder.Source;
            Remarks = characterBuilder.Remarks?.Replace("\r\n", ", ");

            if (characterBuilder.CareersFrom != null)
                CareersFrom = characterBuilder.CareersFrom;
            else
            {
                var careers = characterBuilder.Careers();
                foreach (var careerName in careers.OrderBy(c => c.Career).Select(c => c.Career).Distinct())
                {
                    var assignments = careers.Where(c => c.Career == careerName && c.Assignment != null).Select(c => AssignmentString(c)).OrderBy(a => a).ToList();

                    var sources = careers.Where(c => c.Career == careerName && c.Source != null).Select(c => c.Source).Distinct().OrderBy(s => s).ToList();

                    Careers.Add(new(careerName, assignments!, sources!));
                }
            }
        }

        public List<CareerDetail> Careers { get; } = new();

        public string? CareersFrom { get; }

        public string? Remarks { get; }

        public string? Source { get; }

        public string Species { get; }

        public string SpeciesUrl { get; }

        static string AssignmentString(CareerBase careerBase)
        {
            const string Format = "+0;-#";

            var result = careerBase.Assignment ?? careerBase.Career;
            var parts = new List<string>();

            if (careerBase.QualifyDM != 0)
                parts.Add($"Quality DM{careerBase.QualifyDM.ToString(Format)}");

            if (careerBase is MilitaryCareer mc)
            {
                if (mc.AdvancementDM != 0)
                    parts.Add($"Advancement DM{mc.AdvancementDM.ToString(Format)}");
                if (mc.CommissionDM != 0)
                    parts.Add($"Commission DM{mc.CommissionDM.ToString(Format)}");
            }
            else if (careerBase is NormalCareer nc)
            {
                if (nc.AdvancementDM != 0)
                    parts.Add($"Advancement DM{nc.AdvancementDM.ToString(Format)}");
            }
            if (parts.Count > 0)
                result += $" ({string.Join(", ", parts)})";

            return result;
        }
    }
}
