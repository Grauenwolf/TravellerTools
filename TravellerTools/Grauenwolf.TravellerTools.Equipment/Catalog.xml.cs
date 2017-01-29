using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Equipment
{

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Catalog
    {

        /// <remarks/>
        [XmlElement("Section")]
        public CatalogSection[] Section { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class CatalogSection
    {

        /// <remarks/>
        [XmlElement("Item")]
        public CatalogSectionItem[] Item { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Book { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class CatalogSectionItem
    {

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public int TL { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public int Price { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public int Law { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public int Category { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Mod { get; set; }
    }


}
