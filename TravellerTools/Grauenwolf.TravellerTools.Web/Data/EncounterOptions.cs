﻿using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Encounters;
using Grauenwolf.TravellerTools.Maps;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class EncounterOptions : ModelBase, IEncounterGeneratorSettings
{
    public List<Encounter> Encounters { get; } = new();

    public int PercentOfOtherSpecies
    {
        get => GetDefault<int>(5);
        set
        {
            Set(value);
            if (value < 0)
                Set(0);
            if (value > 100)
                Set(100);
        }
    }

    int? ISpeciesSettings.PercentOfOtherSpecies => PercentOfOtherSpecies;
    public IReadOnlyList<FactionOrSpecies> SpeciesAndFactionsList { get; set; } = null!;
    public string? SpeciesOrFaction { get => Get<string?>(); set => Set(value); }

    World? IEncounterGeneratorSettings.World => null;
}
