using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    public class FreightList
    {
        readonly List<FreightLot> m_Lots = new List<FreightLot>();

        public List<FreightLot> Lots
        {
            get { return m_Lots; }
        }

        public int Incidental { get; set; }
        public int Minor { get; set; }
        public int Major { get; set; }
    }
}
