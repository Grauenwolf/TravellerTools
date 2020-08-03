using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class PlanetPickerOptions
    {
        public IReadOnlyList<Milieu> MilieuList => Milieu.MilieuList;
        public IReadOnlyList<Sector> SectorList { get; set; } = Array.Empty<Sector>();
        public Milieu? SelectedMilieu { get; set; } = Milieu.DefaultMilieu;

        public string? SelectedMilieuCode
        {
            get => SelectedMilieu?.Code;
            set => SelectedMilieu = Milieu.FromCode(value ?? "");
        }

        public Sector? SelectedSector { get; set; }

        public string? SelectedSectorHex
        {
            get => SelectedSector?.Hex;
            set => SelectedSector = SectorList.FirstOrDefault(s => s.Hex == value);
        }

        public Subsector? SelectedSubsector { get; set; }

        public string? SelectedSubsectorIndex
        {
            get => SelectedSubsector?.Index;
            set => SelectedSubsector = SubsectorList.FirstOrDefault(s => s.Index == value);
        }

        public World? SelectedWorld { get; set; }

        public string? SelectedWorldHex
        {
            get => SelectedWorld?.Hex;
            set => SelectedWorld = WorldList.FirstOrDefault(s => s.Hex == value);
        }

        public IReadOnlyList<Subsector> SubsectorList { get; set; } = Array.Empty<Subsector>();
        public IReadOnlyList<World> WorldList { get; set; } = Array.Empty<World>();
    }
}
