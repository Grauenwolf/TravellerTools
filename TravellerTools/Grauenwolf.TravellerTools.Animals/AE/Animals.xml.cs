namespace Grauenwolf.TravellerTools.Animals.AE
{



    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class AnimalTemplates
    {

        private AnimalTemplatesAnimalClasses animalClassesField;

        private AnimalTemplatesTerrain[] terrainsField;

        private AnimalTemplatesOption[] encounterTableField;

        private AnimalTemplatesBehavior[] behaviorsField;

        private AnimalTemplatesWeapon[] weaponTableField;

        private AnimalTemplatesSize[] sizeTableField;

        private AnimalTemplatesArmorOption[] armorTableField;

        private AnimalTemplatesStrength[] damageTableField;

        private AnimalTemplatesPack[] numberTableField;

        /// <remarks/>
        public AnimalTemplatesAnimalClasses AnimalClasses
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
    public partial class AnimalTemplatesAnimalClasses
    {

        private AnimalTemplatesAnimalClassesAnimalClass animalClassField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassesAnimalClass AnimalClass
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalClassesAnimalClass
    {

        private AnimalTemplatesAnimalClassesAnimalClassDiet[] dietsField;

        private AnimalTemplatesAnimalClassesAnimalClassSkill[] skillsField;

        private AnimalTemplatesAnimalClassesAnimalClassChart[] chartField;

        private AnimalTemplatesAnimalClassesAnimalClassOption[] quirksField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Diet", IsNullable = false)]
        public AnimalTemplatesAnimalClassesAnimalClassDiet[] Diets
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
        public AnimalTemplatesAnimalClassesAnimalClassSkill[] Skills
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
        public AnimalTemplatesAnimalClassesAnimalClassChart[] Chart
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
        public AnimalTemplatesAnimalClassesAnimalClassOption[] Quirks
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassDiet
    {

        private AnimalTemplatesAnimalClassesAnimalClassDietAttribute[] attributeField;

        private AnimalTemplatesAnimalClassesAnimalClassDietSkill skillField;

        private AnimalTemplatesAnimalClassesAnimalClassDietOption[] behaviorsField;

        private byte oddsField;

        private string dietField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attribute")]
        public AnimalTemplatesAnimalClassesAnimalClassDietAttribute[] Attribute
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
        public AnimalTemplatesAnimalClassesAnimalClassDietSkill Skill
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
        public AnimalTemplatesAnimalClassesAnimalClassDietOption[] Behaviors
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassDietAttribute
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassDietSkill
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassDietOption
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassSkill
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassChart
    {

        private AnimalTemplatesAnimalClassesAnimalClassChartOption[] optionField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesAnimalClassesAnimalClassChartOption[] Option
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassChartOption
    {

        private AnimalTemplatesAnimalClassesAnimalClassChartOptionSkill skillField;

        private AnimalTemplatesAnimalClassesAnimalClassChartOptionAttribute[] attributeField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassesAnimalClassChartOptionSkill Skill
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
        public AnimalTemplatesAnimalClassesAnimalClassChartOptionAttribute[] Attribute
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassChartOptionSkill
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassChartOptionAttribute
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOption
    {

        private AnimalTemplatesAnimalClassesAnimalClassOptionFeature featureField;

        private AnimalTemplatesAnimalClassesAnimalClassOptionAttribute attributeField;

        private AnimalTemplatesAnimalClassesAnimalClassOptionSkill skillField;

        private AnimalTemplatesAnimalClassesAnimalClassOptionChart chartField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassesAnimalClassOptionFeature Feature
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
        public AnimalTemplatesAnimalClassesAnimalClassOptionAttribute Attribute
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
        public AnimalTemplatesAnimalClassesAnimalClassOptionSkill Skill
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
        public AnimalTemplatesAnimalClassesAnimalClassOptionChart Chart
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionFeature
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionAttribute
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionSkill
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionChart
    {

        private AnimalTemplatesAnimalClassesAnimalClassOptionChartOption[] optionField;

        private string rollField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesAnimalClassesAnimalClassOptionChartOption[] Option
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionChartOption
    {

        private AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionFeature featureField;

        private AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionAttribute attributeField;

        private AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionSkill skillField;

        private byte rollField;

        /// <remarks/>
        public AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionFeature Feature
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
        public AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionAttribute Attribute
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
        public AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionSkill Skill
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionFeature
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionAttribute
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
    public partial class AnimalTemplatesAnimalClassesAnimalClassOptionChartOptionSkill
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

        private AnimalTemplatesTerrainAnimalClasses animalClassesField;

        private AnimalTemplatesTerrainOption[] movementChartField;

        private string nameField;

        private string sizeDMField;

        /// <remarks/>
        public AnimalTemplatesTerrainAnimalClasses AnimalClasses
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
        public AnimalTemplatesTerrainOption[] MovementChart
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
    public partial class AnimalTemplatesTerrainAnimalClasses
    {

        private AnimalTemplatesTerrainAnimalClassesOption optionField;

        /// <remarks/>
        public AnimalTemplatesTerrainAnimalClassesOption Option
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainAnimalClassesOption
    {

        private byte oddsField;

        private string animalClassField;

        private sbyte penalityField;

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
        public sbyte Penality
        {
            get
            {
                return this.penalityField;
            }
            set
            {
                this.penalityField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesTerrainOption
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

        private AnimalTemplatesBehaviorFeature featureField;

        private string nameField;

        private string attackField;

        private string fleeField;

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
