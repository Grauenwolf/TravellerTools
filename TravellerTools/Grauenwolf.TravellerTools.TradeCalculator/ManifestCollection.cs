using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class ManifestCollection(World origin) : List<Manifest>
    {
        public bool AdvancedCharacters { get; set; }
        public bool? AdvancedMode { get; set; }
        public int BrokerScore { get; set; }
        public int CounterpartyScore { get; set; }
        public StarportDetails? DownportDetails { get; internal set; }
        public Edition Edition { get; set; }
        public int HexX { get; set; }
        public int HexY { get; set; }
        public StarportDetails? HighportDetails { get; internal set; }
        public bool? IllegalGoods { get; set; }
        public int MaxJumpDistance { get; set; }
        public string? Milieu { get; set; }
        public World Origin { get; } = origin;
        public bool? Raffle { get; set; }
        public int? SectorX { get; set; }
        public int? SectorY { get; set; }
        public int? Seed { get; set; }
        public int StreetwiseScore { get; set; }
        public TradeGoodsList? TradeList { get; set; }
    }
}