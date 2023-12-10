using System.ComponentModel;
using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Characters;

/// <remarks/>
[System.SerializableAttribute()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class CharacterTemplates
{
    /// <remarks/>
    [XmlArrayItem("Skill", IsNullable = false)]
    public CharacterTemplatesSkill[] Skills { get; set; } = null!;
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class CharacterTemplatesSkill
{
    /// <remarks/>
    [XmlAttribute()]
    public string Name { get; set; } = null!;

    /// <remarks/>
    [XmlElement("Specialty")]
    public CharacterTemplatesSkillSpecialty[] Specialty { get; set; } = null!;

    ///// <remarks/>
    //[XmlText()]
    //public string[] Text { get; set; } = null!;
}

/// <remarks/>
[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class CharacterTemplatesSkillSpecialty
{
    /// <remarks/>
    ///
    [XmlAttribute()]
    public string? Group { get; set; }

    /// <remarks/>
    [XmlAttribute()]
    public string Name { get; set; } = null!;
}
