﻿using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Characters.Careers;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class CareersPage
{
    [Parameter] public string? SpeciesFilter { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    List<SpeciesDetails>? Species { get; set; }

    protected override void Initialized()
    {
    }

    protected override void ParametersSet()
    {
        Species = CharacterBuilder.SpeciesList
            .Where(s => SpeciesFilter == null || string.Equals(SpeciesFilter, s, StringComparison.OrdinalIgnoreCase))
            //.Where(s => SpeciesFilter == null || s.Contains(SpeciesFilter, StringComparison.OrdinalIgnoreCase))
            .Select(s => CharacterBuilder.GetCharacterBuilder(s))
            .Select(s => new SpeciesDetails(s))
            .ToList();
    }

    class CareerDetail(string career, List<string> assignments, List<string> sources, List<CareerTypes> careerTypes)
    {
        public List<string> Assignments { get; } = assignments;
        public string Career { get; } = career;
        public List<CareerTypes> CareerTypes { get; } = careerTypes;
        public List<string> Sources { get; } = sources;
    }

    class SpeciesDetails
    {
        public SpeciesDetails(SpeciesCharacterBuilder characterBuilder)
        {
            Species = characterBuilder.Species;
            SpeciesGroup = characterBuilder.SpeciesGroup;
            Faction = characterBuilder.Faction;
            SpeciesUrl = characterBuilder.SpeciesUrl;
            Source = characterBuilder.Source;
            Remarks = characterBuilder.Remarks?.Replace("\r\n", ", ");

            var careers = characterBuilder.Careers();
            foreach (var careerName in careers.OrderBy(c => c.Career).Select(c => c.Career).Distinct())
            {
                var assignments = careers.Where(c => c.Career == careerName && c.Assignment != null).Select(c => AssignmentString(c)).OrderBy(a => a).ToList();

                var sources = careers.Where(c => c.Career == careerName && c.Source != null).Select(c => c.Source).Distinct().OrderBy(s => s).ToList();

                var careerTypes = careers.Where(c => c.Career == careerName)
                    .OrderBy(c => c.Assignment).Select(a => a.CareerTypes).ToList();

                Careers.Add(new(careerName, assignments!, sources!, careerTypes));
            }
        }

        public List<CareerDetail> Careers { get; } = new();
        public string Faction { get; }
        public string? Remarks { get; }

        public string? Source { get; }

        public string Species { get; }
        public string SpeciesGroup { get; }

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
            if (careerBase.RequiredSkill != null)
                parts.Add("Requires " + careerBase.RequiredSkill);

            if (parts.Count > 0)
                result += $" ({string.Join(", ", parts)})";

            return result;
        }
    }
}
