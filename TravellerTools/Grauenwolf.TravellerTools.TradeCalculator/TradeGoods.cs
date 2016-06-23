using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class TradeGoods
    {

        /// <remarks/>
        [XmlElementAttribute("TradeGood")]
        public TradeGood[] TradeGood { get; set; }
    }
}
