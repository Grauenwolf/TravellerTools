using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Shared;
using Grauenwolf.TravellerTools.TradeCalculator;

namespace Grauenwolf.TravellerTools.Web.Data
{
    public class WorldModel
    {
        public WorldModel(Milieu milieu, World world)
        {
            Milieu = milieu;
            World = world;
        }

        public Milieu Milieu { get; }
        public World World { get; }

        //public bool AdvancedCharacters { get; set; }
        //public bool? AdvancedMode { get; set; }
        //public int BrokerScore { get; set; }
        public StarportDetails? DownportDetails { get; set; }

        //public Edition Edition { get; set; }
        //public int HexX { get; set; }
        //public int HexY { get; set; }
        public StarportDetails? HighportDetails { get; set; }

        //public string Test { get; set; }

        //public bool? IllegalGoods { get; set; }
        //public int MaxJumpDistance { get; set; }

        //public World Origin { get; set; }
        //public bool? Raffle { get; set; }
        //public int? SectorX { get; set; }
        //public int? SectorY { get; set; }
        //public int? Seed { get; set; }
        //public int StreetwiseScore { get; set; }
        public TradeGoodsList TradeList { get; set; }
    }
}
