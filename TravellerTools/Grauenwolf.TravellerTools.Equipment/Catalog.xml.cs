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

        /// <remarks/>
        [XmlAttribute()]
        public int Law { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public int Category { get; set; }


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
        public string Price { get; set; }

        [XmlAttribute()]
        public string Mass { get; set; }

        public int PriceCredits
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Price))
                    return 0;
                if (Price.StartsWith("Cr"))
                    return int.Parse(Price.Substring(2));
                if (Price.StartsWith("KCr"))
                    return int.Parse(Price.Substring(3)) * 1000 * 1000;
                if (Price.StartsWith("MCr"))
                    return int.Parse(Price.Substring(3)) * 1000 * 1000;

                return int.Parse(Price);

                //throw new BookException($"Cannot parse price of '{Price}'");
            }
        }

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
