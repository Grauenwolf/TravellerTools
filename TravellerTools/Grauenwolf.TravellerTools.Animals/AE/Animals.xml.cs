namespace Grauenwolf.TravellerTools.Animals.AE
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class AnimalTemplates
    {

        private AnimalTemplatesAnimalClass[] animalClassesField;

        private AnimalTemplatesTerrain[] terrainsField;

        private AnimalTemplatesOption[] encounterTableField;

        private AnimalTemplatesBehavior[] behaviorsField;

        private AnimalTemplatesWeapon[] weaponTableField;

        private AnimalTemplatesSize[] sizeTableField;

        private AnimalTemplatesArmorOption[] armorTableField;

        private AnimalTemplatesStrength[] damageTableField;

        private AnimalTemplatesPack[] numberTableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("AnimalClass", IsNullable = false)]
        public AnimalTemplatesAnimalClass[] AnimalClasses
        {
            get
            {
                return this.animalClassesField;
            }
            set
            {
                this.animalClassesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Terrain", IsNullable = false)]
        public AnimalTemplatesTerrain[] Terrains
        {
            get
            {
                return this.terrainsField;
            }
            set
            {
                this.terrainsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Option", IsNullable = false)]
        public AnimalTemplatesOption[] EncounterTable
        {
            get
            {
                return this.encounterTableField;
            }
            set
            {
                this.encounterTableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Behavior", IsNullable = false)]
        public AnimalTemplatesBehavior[] Behaviors
        {
            get
            {
                return this.behaviorsField;
            }
            set
            {
                this.behaviorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Weapon", IsNullable = false)]
        public AnimalTemplatesWeapon[] WeaponTable
        {
            get
            {
                return this.weaponTableField;
            }
            set
            {
                this.weaponTableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Size", IsNullable = false)]
        public AnimalTemplatesSize[] SizeTable
        {
            get
            {
                return this.sizeTableField;
            }
            set
            {
                this.sizeTableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ArmorOption", IsNullable = false)]
        public AnimalTemplatesArmorOption[] ArmorTable
        {
            get
            {
                return this.armorTableField;
            }
            set
            {
                this.armorTableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Strength", IsNullable = false)]
        public AnimalTemplatesStrength[] DamageTable
        {
            get
            {
                return this.damageTableField;
            }
            set
            {
                this.damageTableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Pack", IsNullable = false)]
        public AnimalTemplatesPack[] NumberTable
        {
            get
            {
                return this.numberTableField;
            }
            set
            {
                this.numberTableField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClass
    {

        private AnimalTemplatesAnimalClassOption[] dietsField;

        private AnimalTemplatesAnimalClassSkill[] skillsField;

        private AnimalTemplatesAnimalClassChart[] chartField;

        private AnimalTemplatesAnimalClassOption2[] quirksField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Option", IsNullable = false)]
        public AnimalTemplatesAnimalClassOption[] Diets
        {
            get
            {
                return this.dietsField;
            }
            set
            {
                this.dietsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Skill", IsNullable = false)]
        public AnimalTemplatesAnimalClassSkill[] Skills
        {
            get
            {
                return this.skillsField;
            }
            set
            {
                this.skillsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Chart")]
        public AnimalTemplatesAnimalClassChart[] Chart
        {
            get
            {
                return this.chartField;
            }
            set
            {
                this.chartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Option", IsNullable = false)]
        public AnimalTemplatesAnimalClassOption2[] Quirks
        {
            get
            {
                return this.quirksField;
            }
            set
            {
                this.quirksField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOption
    {

        private AnimalTemplatesAnimalClassOptionAttribute[] attributeField;

        private AnimalTemplatesAnimalClassOptionSkill skillField;

        private AnimalTemplatesAnimalClassOptionOption[] behaviorsField;

        private byte oddsField;

        private string dietField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attribute")]
        public AnimalTemplatesAnimalClassOptionAttribute[] Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionSkill Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Option", IsNullable = false)]
        public AnimalTemplatesAnimalClassOptionOption[] Behaviors
        {
            get
            {
                return this.behaviorsField;
            }
            set
            {
                this.behaviorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Odds
        {
            get
            {
                return this.oddsField;
            }
            set
            {
                this.oddsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Diet
        {
            get
            {
                return this.dietField;
            }
            set
            {
                this.dietField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionAttribute
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionSkill
    {

        private string nameField;

        private byte scoreField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionOption
    {

        private byte rollField;

        private string behaviorField;

        private sbyte reactionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Behavior
        {
            get
            {
                return this.behaviorField;
            }
            set
            {
                this.behaviorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Reaction
        {
            get
            {
                return this.reactionField;
            }
            set
            {
                this.reactionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassSkill
    {

        private string nameField;

        private byte scoreField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChart
    {

        private AnimalTemplatesAnimalClassChartOption[] optionField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesAnimalClassChartOption[] Option
        {
            get
            {
                return this.optionField;
            }
            set
            {
                this.optionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOption
    {

        private AnimalTemplatesAnimalClassChartOptionSkill skillField;

        private AnimalTemplatesAnimalClassChartOptionAttribute[] attributeField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassChartOptionSkill Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attribute")]
        public AnimalTemplatesAnimalClassChartOptionAttribute[] Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionSkill
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassChartOptionAttribute
    {

        private string nameField;

        private string bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOption2
    {

        private AnimalTemplatesAnimalClassOptionFeature featureField;

        private AnimalTemplatesAnimalClassOptionAttribute1 attributeField;

        private AnimalTemplatesAnimalClassOptionSkill1 skillField;

        private AnimalTemplatesAnimalClassOptionChart chartField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionFeature Feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionAttribute1 Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionSkill1 Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionChart Chart
        {
            get
            {
                return this.chartField;
            }
            set
            {
                this.chartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionFeature
    {

        private string textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionAttribute1
    {

        private string nameField;

        private sbyte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionSkill1
    {

        private string nameField;

        private sbyte bonusField;

        private bool bonusFieldSpecified;

        private byte scoreField;

        private bool scoreFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BonusSpecified
        {
            get
            {
                return this.bonusFieldSpecified;
            }
            set
            {
                this.bonusFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ScoreSpecified
        {
            get
            {
                return this.scoreFieldSpecified;
            }
            set
            {
                this.scoreFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionChart
    {

        private AnimalTemplatesAnimalClassOptionChartOption[] optionField;

        private string rollField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesAnimalClassOptionChartOption[] Option
        {
            get
            {
                return this.optionField;
            }
            set
            {
                this.optionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionChartOption
    {

        private AnimalTemplatesAnimalClassOptionChartOptionFeature featureField;

        private AnimalTemplatesAnimalClassOptionChartOptionAttribute attributeField;

        private AnimalTemplatesAnimalClassOptionChartOptionSkill skillField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionChartOptionFeature Feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionChartOptionAttribute Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesAnimalClassOptionChartOptionSkill Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionChartOptionFeature
    {

        private string textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionChartOptionAttribute
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassOptionChartOptionSkill
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesTerrain
    {

        private AnimalTemplatesTerrainOption[] animalClassesField;

        private AnimalTemplatesTerrainOption1[] movementChartField;

        private string nameField;

        private string sizeDMField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Option", IsNullable = false)]
        public AnimalTemplatesTerrainOption[] AnimalClasses
        {
            get
            {
                return this.animalClassesField;
            }
            set
            {
                this.animalClassesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Option", IsNullable = false)]
        public AnimalTemplatesTerrainOption1[] MovementChart
        {
            get
            {
                return this.movementChartField;
            }
            set
            {
                this.movementChartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SizeDM
        {
            get
            {
                return this.sizeDMField;
            }
            set
            {
                this.sizeDMField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption
    {

        private byte oddsField;

        private string animalClassField;

        private sbyte penaltyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Odds
        {
            get
            {
                return this.oddsField;
            }
            set
            {
                this.oddsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AnimalClass
        {
            get
            {
                return this.animalClassField;
            }
            set
            {
                this.animalClassField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Penalty
        {
            get
            {
                return this.penaltyField;
            }
            set
            {
                this.penaltyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption1
    {

        private byte rollField;

        private string movementField;

        private string sizeDMField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Movement
        {
            get
            {
                return this.movementField;
            }
            set
            {
                this.movementField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SizeDM
        {
            get
            {
                return this.sizeDMField;
            }
            set
            {
                this.sizeDMField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesOption
    {

        private byte rollField;

        private string dietField;

        private byte evolutionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Diet
        {
            get
            {
                return this.dietField;
            }
            set
            {
                this.dietField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Evolution
        {
            get
            {
                return this.evolutionField;
            }
            set
            {
                this.evolutionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehavior
    {

        private AnimalTemplatesBehaviorAttribute attributeField;

        private AnimalTemplatesBehaviorSkill skillField;

        private AnimalTemplatesBehaviorChart chartField;

        private AnimalTemplatesBehaviorFeature featureField;

        private string nameField;

        private string attackField;

        private string fleeField;

        /// <remarks/>
        public AnimalTemplatesBehaviorAttribute Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesBehaviorSkill Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesBehaviorChart Chart
        {
            get
            {
                return this.chartField;
            }
            set
            {
                this.chartField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesBehaviorFeature Feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Attack
        {
            get
            {
                return this.attackField;
            }
            set
            {
                this.attackField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Flee
        {
            get
            {
                return this.fleeField;
            }
            set
            {
                this.fleeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorAttribute
    {

        private string nameField;

        private sbyte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorSkill
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChart
    {

        private AnimalTemplatesBehaviorChartOption[] optionField;

        private string rollField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesBehaviorChartOption[] Option
        {
            get
            {
                return this.optionField;
            }
            set
            {
                this.optionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOption
    {

        private AnimalTemplatesBehaviorChartOptionFeature featureField;

        private AnimalTemplatesBehaviorChartOptionAttribute attributeField;

        private AnimalTemplatesBehaviorChartOptionSkill skillField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesBehaviorChartOptionFeature Feature
        {
            get
            {
                return this.featureField;
            }
            set
            {
                this.featureField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesBehaviorChartOptionAttribute Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        /// <remarks/>
        public AnimalTemplatesBehaviorChartOptionSkill Skill
        {
            get
            {
                return this.skillField;
            }
            set
            {
                this.skillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionFeature
    {

        private string textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionAttribute
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorChartOptionSkill
    {

        private string nameField;

        private byte bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesBehaviorFeature
    {

        private string textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesWeapon
    {

        private byte rollField;

        private string weaponField;

        private string bonusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Weapon
        {
            get
            {
                return this.weaponField;
            }
            set
            {
                this.weaponField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Bonus
        {
            get
            {
                return this.bonusField;
            }
            set
            {
                this.bonusField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesSize
    {

        private byte rollField;

        private ushort weightKGField;

        private string strengthField;

        private string dexterityField;

        private string enduranceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort WeightKG
        {
            get
            {
                return this.weightKGField;
            }
            set
            {
                this.weightKGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Strength
        {
            get
            {
                return this.strengthField;
            }
            set
            {
                this.strengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Dexterity
        {
            get
            {
                return this.dexterityField;
            }
            set
            {
                this.dexterityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Endurance
        {
            get
            {
                return this.enduranceField;
            }
            set
            {
                this.enduranceField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesArmorOption
    {

        private byte rollField;

        private byte armorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Roll
        {
            get
            {
                return this.rollField;
            }
            set
            {
                this.rollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Armor
        {
            get
            {
                return this.armorField;
            }
            set
            {
                this.armorField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesStrength
    {

        private byte minValueField;

        private string damageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte MinValue
        {
            get
            {
                return this.minValueField;
            }
            set
            {
                this.minValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Damage
        {
            get
            {
                return this.damageField;
            }
            set
            {
                this.damageField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesPack
    {

        private byte minValueField;

        private string numberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte MinValue
        {
            get
            {
                return this.minValueField;
            }
            set
            {
                this.minValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }
    }

}
