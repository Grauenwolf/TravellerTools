using Grauenwolf.TravellerTools.Maps;
using System.Collections.ObjectModel;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class ManifestCollection : Collection<Manifest>
    {
        public int SectorX { get; set; }
        public int SectorY { get; set; }
        public int HexX { get; set; }
        public int HexY { get; set; }
        public int MaxJumpDistance { get; set; }

        public World Origin { get; set; }
        public TradeGoodsList TradeList { get; set; }

        public int BerthingCost { get; set; }



        public int BrokerScore { get; set; }
        public bool? AdvancedMode { get; set; }
        public bool? IllegalGoods { get; set; }
        public Edition Edition { get; set; }
        public int? Seed { get; set; }
        public bool AdvancedCharacters { get; set; }

    }
}
