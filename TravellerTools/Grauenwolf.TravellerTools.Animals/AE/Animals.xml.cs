using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Animals.AE
{


    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalClassTemplate
    {
        /// <remarks/>
        [XmlElement("Chart")]
        public Chart[] Chart { get; set; }

        /// <remarks/>
        [XmlArrayItem("Option", IsNullable = false)]
        public DietOption[] Diets { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlArrayItem("Skill", IsNullable = false)]
        public SkillTemplate[] Skills { get; set; }

        [XmlAttribute()]
        public int InitiativeDM { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class AnimalTemplates
    {
        /// <remarks/>
        [XmlArrayItem("AnimalClass", IsNullable = false)]
        public AnimalClassTemplate[] AnimalClasses { get; set; }

        /// <remarks/>
        [XmlArrayItem("ArmorOption", IsNullable = false)]
        public ArmorOption[] ArmorTable { get; set; }

        /// <remarks/>
        [XmlArrayItem("Behavior", IsNullable = false)]
        public Behavior[] Behaviors { get; set; }

        /// <remarks/>
        [XmlArrayItem("Strength", IsNullable = false)]
        public Strength[] DamageTable { get; set; }

        /// <remarks/>
        [XmlArrayItem("Option", IsNullable = false)]
        public EncounterOption[] EncounterTable { get; set; }

        /// <remarks/>
        [XmlArrayItem("Pack", IsNullable = false)]
        public PackSize[] NumberTable { get; set; }

        /// <remarks/>
        [XmlArrayItem("Size", IsNullable = false)]
        public Size[] SizeTable { get; set; }

        /// <remarks/>
        [XmlArrayItem("Terrain", IsNullable = false)]
        public TerrainTemplate[] Terrains { get; set; }

        /// <remarks/>
        [XmlArrayItem("Weapon", IsNullable = false)]
        public WeaponTemplate[] WeaponTable { get; set; }
    }
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOption
    {
        /// <remarks/>
        public AnimalTemplatesAnimalClassChartOptionChartOptionAttribute Attribute { get; set; }

        /// <remarks/>
        public AnimalTemplatesAnimalClassChartOptionChartOptionFeature Feature { get; set; }
        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }

        /// <remarks/>
        public AnimalTemplatesAnimalClassChartOptionChartOptionSkill Skill { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOptionAttribute
    {
        /// <remarks/>
        [XmlAttribute()]
        public byte Bonus { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOptionFeature
    {

        /// <remarks/>
        [XmlAttribute()]
        public string Text { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOptionSkill
    {
        /// <remarks/>
        [XmlAttribute()]
        public byte Bonus { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
    }







    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionAttribute
    {
        /// <remarks/>
        [XmlAttribute()]
        public byte Bonus { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionFeature
    {

        private string textField;

        /// <remarks/>
        [XmlAttribute()]
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionSkill
    {
        /// <remarks/>
        [XmlAttribute()]
        public byte Bonus { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
    }





    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption
    {

        /// <remarks/>
        [XmlAttribute()]
        public string AnimalClass { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Odds { get; set; }
        /// <remarks/>
        [XmlAttribute()]
        public sbyte Penalty { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption1
    {

        /// <remarks/>
        [XmlAttribute()]
        public string Movement { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }
        /// <remarks/>
        [XmlAttribute()]
        public string SizeDM { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesWeapon
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Bonus { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Weapon { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class ArmorOption
    {
        /// <remarks/>
        [XmlAttribute()]
        public byte Armor { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class AttributeTemplate
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Bonus { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class Behavior
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Attack { get; set; }

        /// <remarks/>
        [XmlElement("Attribute", typeof(AttributeTemplate))]
        public AttributeTemplate[] Attributes { get; set; }


        [XmlElement("Skill", typeof(SkillTemplate))]
        public SkillTemplate[] Skills { get; set; }

        /// <remarks/>
        [XmlElement("Chart", typeof(Chart))]
        public Chart[] Charts { get; set; }

        /// <remarks/>
        [XmlElement("Feature", typeof(FeatureTemplate))]
        public FeatureTemplate[] Features { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Flee { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public int InitiativeDM { get; set; }

    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class BehaviorOption
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Behavior { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public sbyte Reaction { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class Chart
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlElement("Option")]
        public ChartOption[] Option { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Roll { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class ChartOption
    {

        /// <remarks/>
        [XmlElement("Attribute", typeof(AttributeTemplate))]
        public AttributeTemplate[] Attributes { get; set; }

        [XmlElement("Chart", typeof(Chart))]
        public Chart[] Charts { get; set; }

        [XmlElement("Feature", typeof(FeatureTemplate))]
        public FeatureTemplate[] Features { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }

        [XmlElement("Skill", typeof(SkillTemplate))]
        public SkillTemplate[] Skills { get; set; }
    }
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class DietOption
    {
        /// <remarks/>
        [XmlElement("Attribute")]
        public AttributeTemplate[] Attribute { get; set; }

        /// <remarks/>
        [XmlArrayItem("Option", IsNullable = false)]
        public BehaviorOption[] Behaviors { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Diet { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public int Odds { get; set; }

        /// <remarks/>
        [XmlElement("Skill")]
        public SkillTemplate[] Skills { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class EncounterOption
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Diet { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Evolution { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class FeatureTemplate
    {

        /// <remarks/>
        [XmlAttribute()]
        public string Text { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class PackSize
    {

        /// <remarks/>
        [XmlAttribute()]
        public byte MinValue { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Number { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class Size
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Dexterity { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Endurance { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte Roll { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Strength { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public ushort WeightKG { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class SkillTemplate
    {

        /// <remarks/>
        /// <remarks/>
        [XmlAttribute()]
        public sbyte Bonus { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BonusSpecified { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
        /// <remarks/>
        [XmlAttribute()]
        public byte Score { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ScoreSpecified { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class Strength
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Damage { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public byte MinValue { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class TerrainTemplate
    {

        /// <remarks/>
        [XmlArrayItem("Option", IsNullable = false)]
        public AnimalTemplatesTerrainOption[] AnimalClasses { get; set; }

        /// <remarks/>
        [XmlArrayItem("Option", IsNullable = false)]
        public AnimalTemplatesTerrainOption1[] MovementChart { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string SizeDM { get; set; }
    }
}
