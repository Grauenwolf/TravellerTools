using System.Xml.Serialization;

namespace Grauenwolf.TravellerTools.Animals.AE
{



    [XmlType(AnonymousType = true)]
    public partial class AnimalClassTemplate
    {

        [XmlElement("Chart")]
        public Chart[] Chart { get; set; }


        [XmlArrayItem("Option", IsNullable = false)]
        public DietOption[] Diets { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public string Movement { get; set; }


        [XmlArrayItem("Skill", IsNullable = false)]
        public SkillTemplate[] Skills { get; set; }

        [XmlAttribute()]
        public int InitiativeDM { get; set; }
    }


    [XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class AnimalTemplates
    {

        [XmlArrayItem("AnimalClass", IsNullable = false)]
        public AnimalClassTemplate[] AnimalClasses { get; set; }


        [XmlArrayItem("ArmorOption", IsNullable = false)]
        public ArmorOption[] ArmorTable { get; set; }


        [XmlArrayItem("Behavior", IsNullable = false)]
        public Behavior[] Behaviors { get; set; }


        [XmlArrayItem("Strength", IsNullable = false)]
        public Strength[] DamageTable { get; set; }


        [XmlArrayItem("Option", IsNullable = false)]
        public EncounterOption[] EncounterTable { get; set; }


        [XmlArrayItem("Pack", IsNullable = false)]
        public PackSize[] NumberTable { get; set; }


        [XmlArrayItem("Size", IsNullable = false)]
        public Size[] SizeTable { get; set; }


        [XmlArrayItem("Terrain", IsNullable = false)]
        public TerrainTemplate[] Terrains { get; set; }


        [XmlArrayItem("Weapon", IsNullable = false)]
        public WeaponTemplate[] WeaponTable { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOption
    {

        public AnimalTemplatesAnimalClassChartOptionChartOptionAttribute Attribute { get; set; }


        public AnimalTemplatesAnimalClassChartOptionChartOptionFeature Feature { get; set; }

        [XmlAttribute()]
        public byte Roll { get; set; }


        public AnimalTemplatesAnimalClassChartOptionChartOptionSkill Skill { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOptionAttribute
    {

        [XmlAttribute()]
        public byte Bonus { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOptionFeature
    {


        [XmlAttribute()]
        public string Text { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionChartOptionSkill
    {

        [XmlAttribute()]
        public byte Bonus { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }
    }








    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionAttribute
    {

        [XmlAttribute()]
        public byte Bonus { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionFeature
    {

        private string textField;


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


    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionSkill
    {

        [XmlAttribute()]
        public byte Bonus { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }
    }






    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption
    {


        [XmlAttribute()]
        public string AnimalClass { get; set; }


        [XmlAttribute()]
        public byte Odds { get; set; }

        [XmlAttribute()]
        public sbyte Penalty { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption1
    {


        [XmlAttribute()]
        public string Movement { get; set; }


        [XmlAttribute()]
        public byte Roll { get; set; }

        [XmlAttribute()]
        public string SizeDM { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class WeaponTemplate
    {

        [XmlAttribute()]
        public int Bonus { get; set; }


        [XmlAttribute()]
        public byte Roll { get; set; }


        [XmlAttribute()]
        public string Weapon { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class ArmorOption
    {

        [XmlAttribute()]
        public byte Armor { get; set; }


        [XmlAttribute()]
        public byte Roll { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class AttributeTemplate
    {

        [XmlAttribute()]
        public string Bonus { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class Behavior
    {

        [XmlAttribute()]
        public string Attack { get; set; }


        [XmlElement("Attribute", typeof(AttributeTemplate))]
        public AttributeTemplate[] Attributes { get; set; }


        [XmlElement("Skill", typeof(SkillTemplate))]
        public SkillTemplate[] Skills { get; set; }


        [XmlElement("Chart", typeof(Chart))]
        public Chart[] Charts { get; set; }


        [XmlElement("Feature", typeof(FeatureTemplate))]
        public FeatureTemplate[] Features { get; set; }


        [XmlAttribute()]
        public string Flee { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public int InitiativeDM { get; set; }

    }


    [XmlType(AnonymousType = true)]
    public partial class BehaviorOption
    {

        [XmlAttribute()]
        public string Behavior { get; set; }


        [XmlAttribute()]
        public sbyte Reaction { get; set; }


        [XmlAttribute()]
        public byte Roll { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class Chart
    {

        [XmlAttribute()]
        public string Name { get; set; }


        [XmlElement("Option")]
        public ChartOption[] Option { get; set; }


        [XmlAttribute()]
        public string Roll { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class ChartOption
    {


        [XmlElement("Attribute", typeof(AttributeTemplate))]
        public AttributeTemplate[] Attributes { get; set; }

        [XmlElement("Chart", typeof(Chart))]
        public Chart[] Charts { get; set; }

        [XmlElement("Feature", typeof(FeatureTemplate))]
        public FeatureTemplate[] Features { get; set; }

        [XmlElement("PostScript", typeof(PostScriptTemplate))]
        public PostScriptTemplate[] PostScripts { get; set; }

        [XmlAttribute()]
        public byte Roll { get; set; }

        [XmlElement("Skill", typeof(SkillTemplate))]
        public SkillTemplate[] Skills { get; set; }

        [XmlElement("AddBehavior", typeof(AddBehavior))]
        public AddBehavior[] Behaviors { get; set; }

    }

    [XmlType(AnonymousType = true)]
    public partial class DietOption
    {

        [XmlElement("Attribute")]
        public AttributeTemplate[] Attribute { get; set; }


        [XmlArrayItem("Option", IsNullable = false)]
        public BehaviorOption[] Behaviors { get; set; }


        [XmlAttribute()]
        public string Diet { get; set; }


        [XmlAttribute()]
        public int Odds { get; set; }


        [XmlElement("Skill")]
        public SkillTemplate[] Skills { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class EncounterOption
    {

        [XmlAttribute()]
        public string Diet { get; set; }


        [XmlAttribute()]
        public byte Evolution { get; set; }


        [XmlAttribute()]
        public byte Roll { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class PostScriptTemplate
    {


        [XmlAttribute()]
        public string Text { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class FeatureTemplate
    {


        [XmlAttribute()]
        public string Text { get; set; }
    }
    [XmlType(AnonymousType = true)]
    public partial class AddBehavior
    {


        [XmlAttribute()]
        public string Name { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class PackSize
    {


        [XmlAttribute()]
        public byte MinValue { get; set; }


        [XmlAttribute()]
        public string Number { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class Size
    {

        [XmlAttribute()]
        public string Dexterity { get; set; }


        [XmlAttribute()]
        public string Endurance { get; set; }


        [XmlAttribute()]
        public byte Roll { get; set; }


        [XmlAttribute()]
        public string Strength { get; set; }


        [XmlAttribute()]
        public ushort WeightKG { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class SkillTemplate
    {



        [XmlAttribute()]
        public sbyte Bonus { get; set; }


        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BonusSpecified { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public byte Score { get; set; }


        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ScoreSpecified { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class Strength
    {

        [XmlAttribute()]
        public int Damage { get; set; }


        [XmlAttribute()]
        public byte MinValue { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class TerrainTemplate
    {


        [XmlArrayItem("Option", IsNullable = false)]
        public AnimalTemplatesTerrainOption[] AnimalClasses { get; set; }


        [XmlArrayItem("Option", IsNullable = false)]
        public AnimalTemplatesTerrainOption1[] MovementChart { get; set; }


        [XmlAttribute()]
        public string Name { get; set; }


        [XmlAttribute()]
        public string SizeDM { get; set; }
    }
}
