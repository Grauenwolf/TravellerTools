using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Characters
{


    /// <remarks/>
    [System.SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CharacterTemplates
    {

        /// <remarks/>
        [XmlArrayItemAttribute("Skill", IsNullable = false)]
        public CharacterTemplatesSkill[] Skills { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class CharacterTemplatesSkill
    {

        /// <remarks/>
        [XmlElementAttribute("Specialty")]
        public CharacterTemplatesSkillSpecialty[] Specialty { get; set; }

        /// <remarks/>
        [XmlTextAttribute()]
        public string[] Text { get; set; }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string Name { get; set; }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class CharacterTemplatesSkillSpecialty
    {

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string Name { get; set; }
    }


}
