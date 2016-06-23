using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class TradeGood
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeGood"/> class.
        /// </summary>
        public TradeGood()
        {
            m_AvailabilityList = ImmutableHashSet<string>.Empty;
            DetailRoll = "2D6";
            Legal = true;
            m_PurchaseDMs = new List<TradeGoodPurchase>();
            m_SaleDMs = new List<TradeGoodSale>();
        }

        /// <remarks/>
        [XmlElement("Purchase")]
        public List<TradeGoodPurchase> PurchaseDMs
        {
            get { return m_PurchaseDMs; }
        }

        /// <remarks/>
        [XmlElement("Sale")]
        public List<TradeGoodSale> SaleDMs
        {
            get { return m_SaleDMs; }
        }

        /// <remarks/>
        [XmlElement("Detail")]
        public TradeGoodDetail[] Details { get; set; }


        public TradeGoodDetail ChooseRandomDetail(Dice random)
        {
            int roll = random.D(DetailRoll);

            foreach (var detail in Details)
                if (detail.MinRoll <= roll && roll <= detail.MaxRoll)
                    return detail;
            throw new Exception("Could not find a detail for the roll " + roll + " in the trade good " + Name);
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Roll { get; set; }

        [XmlAttribute()]
        public decimal BasePrice { get; set; }

        /// <remarks/>
        [XmlAttribute]
        public string Availability
        {
            get { return string.Join(",", m_AvailabilityList); }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    m_AvailabilityList = (value.Split(',').Select(s => s.Trim())).ToImmutableHashSet();
                else
                    m_AvailabilityList = ImmutableHashSet.Create<string>();
            }
        }

        private List<TradeGoodSale> m_SaleDMs;
        private List<TradeGoodPurchase> m_PurchaseDMs;
        ImmutableHashSet<string> m_AvailabilityList;

        [XmlIgnore]
        public ImmutableHashSet<string> AvailabilityList
        {
            get { return m_AvailabilityList; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Tons { get; set; }

        [XmlAttribute]
        public string DetailRoll { get; set; }

        [XmlAttribute]
        public bool Legal { get; set; }


    }
}
