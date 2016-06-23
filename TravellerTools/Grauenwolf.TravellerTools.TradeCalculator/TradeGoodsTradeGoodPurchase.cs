using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class TradeGoodPurchase
    {

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string Tag { get; set; }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public int Bonus { get; set; }
    }
}
