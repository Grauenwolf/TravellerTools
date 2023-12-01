using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Web.Data;

public class PlanetPickerOptions : ModelBase
{
    public IReadOnlyList<Milieu> MilieuList => Milieu.MilieuList;

    public IReadOnlyList<Sector> SectorList
    {
        get => GetDefault<IReadOnlyList<Sector>>(Array.Empty<Sector>());
        set => Set(value);
    }

    public Milieu? SelectedMilieu { get => GetDefault<Milieu?>(Milieu.DefaultMilieu); set => Set(value); }

    public string? SelectedMilieuCode
    {
        get => SelectedMilieu?.Code;
        set => SelectedMilieu = Milieu.FromCode(value ?? "");
    }

    public Sector? SelectedSector { get => Get<Sector?>(); set => Set(value); }

    public string? SelectedSectorHex
    {
        get => SelectedSector?.Hex;
        set => SelectedSector = SectorList.FirstOrDefault(s => s.Hex == value);
    }

    public Subsector? SelectedSubsector { get => Get<Subsector?>(); set => Set(value); }

    public string? SelectedSubsectorIndex
    {
        get => SelectedSubsector?.Index;
        set => SelectedSubsector = SubsectorList.FirstOrDefault(s => s.Index == value);
    }

    public World? SelectedWorld { get => Get<World?>(); set => Set(value); }

    public string? SelectedWorldHex
    {
        get => SelectedWorld?.Hex;
        set => SelectedWorld = WorldList.FirstOrDefault(s => s.Hex == value);
    }

    public IReadOnlyList<Subsector> SubsectorList
    {
        get => GetDefault<IReadOnlyList<Subsector>>(Array.Empty<Subsector>());
        set => Set(value);
    }

    public IReadOnlyList<World> WorldList
    {
        get => GetDefault<IReadOnlyList<World>>(Array.Empty<World>());
        set => Set(value);
    }
}
