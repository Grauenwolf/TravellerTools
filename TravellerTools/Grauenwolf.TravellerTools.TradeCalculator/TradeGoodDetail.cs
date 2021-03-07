using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
#pragma warning disable RCS1139 // Add summary element to documentation comment.

namespace Grauenwolf.TravellerTools.TradeCalculator
{
    /// <remarks/>
    //[Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class TradeGoodDetail
    {
        [XmlIgnore]
        public ImmutableList<string> NameList
        {
            get { return m_NameList; }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Roll { get; set; }

        string m_Name;
        ImmutableList<string> m_NameList = ImmutableList.Create<string>();

        /// <remarks/>
        [XmlAttribute]
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                if (string.IsNullOrWhiteSpace(m_Name))
                    m_NameList = ImmutableList.Create<string>();
                else
                    m_NameList = value.Split('/').Select(s => s.Trim()).ToImmutableList();
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Tons { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public decimal Price { get; set; }

        public int MinRoll
        {
            get
            {
                if (Roll.Contains("-"))
                    return int.Parse(Roll.Substring(0, Roll.IndexOf("-")).Trim());
                return int.Parse(Roll);
            }
        }

        public int MaxRoll
        {
            get
            {
                if (Roll.Contains("-"))
                    return int.Parse(Roll.Substring(1 + Roll.IndexOf("-")).Trim());
                return int.Parse(Roll);
            }
        }
    }
}
