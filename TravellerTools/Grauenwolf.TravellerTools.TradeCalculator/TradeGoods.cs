using System;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
#pragma warning disable RCS1139 // Add summary element to documentation comment.

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
