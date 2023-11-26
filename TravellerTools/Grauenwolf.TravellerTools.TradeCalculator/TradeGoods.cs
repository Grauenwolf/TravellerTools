using System;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
#pragma warning disable RCS1139 // Add summary element to documentation comment.

namespace Grauenwolf.TravellerTools.TradeCalculator;

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class TradeGoods
{
    /// <remarks/>
    [XmlElement("TradeGood")]
    public TradeGood[] TradeGood { get; set; }
}