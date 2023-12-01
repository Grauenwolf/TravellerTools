using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
#pragma warning disable RCS1139 // Add summary element to documentation comment.

namespace Grauenwolf.TravellerTools.TradeCalculator;

/// <remarks/>
[SerializableAttribute()]
[DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true)]
public class TradeGoodSale
{
    /// <remarks/>
    [XmlAttributeAttribute()]
    public string Tag { get; set; }

    /// <remarks/>
    [XmlAttributeAttribute()]
    public int Bonus { get; set; }
}
