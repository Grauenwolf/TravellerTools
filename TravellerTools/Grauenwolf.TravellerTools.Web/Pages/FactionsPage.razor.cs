using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class FactionsPage
{
    [Parameter] public string? FactionsFilter { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    List<FactionDetails>? Factions { get; set; }

    protected override void ParametersSet()
    {
        var characterBuilders = CharacterBuilder.SpeciesList.Select(s => CharacterBuilder.GetCharacterBuilder(s)).ToList();

        //Doing the filter this way gives us the correct casing for the faction.
        var factions = CharacterBuilder.FactionsList
            .Where(s => FactionsFilter == null || string.Equals(s, FactionsFilter, StringComparison.OrdinalIgnoreCase))
            .OrderBy(s => s).ToList();

        var result = new List<FactionDetails>();
        foreach (var faction in factions)
        {
            result.Add(new FactionDetails(faction, characterBuilders.Where(c => c.Faction == faction)));
        }
        Factions = result;
    }

    class FactionDetails
    {
        public FactionDetails(string faction, IEnumerable<SpeciesCharacterBuilder> characterBuilders)
        {
            Faction = faction;
            Species = characterBuilders.OrderBy(c => c.Species).Select(c => new SpeciesDetails(c)).ToList();
        }

        public string Faction { get; }
        public List<SpeciesDetails> Species { get; }
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
        }

        public string Faction { get; }
        public string? Remarks { get; }

        public string? Source { get; }

        public string Species { get; }
        public string SpeciesGroup { get; }

        public string SpeciesUrl { get; }
    }
}
