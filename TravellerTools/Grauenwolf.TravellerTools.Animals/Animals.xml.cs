namespace Grauenwolf.TravellerTools.Animals
{


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class AnimalTemplates
    {

        private AnimalTemplatesTerrain[] terrainListField;

        private AnimalTemplatesOption[] encounterTableField;

        private AnimalTemplatesAnimalType[] animalTypeListField;

        private AnimalTemplatesSize[] sizeTableField;

        private AnimalTemplatesWeapon[] weaponTableField;

        private AnimalTemplatesArmorOption[] armorTableField;

        private AnimalTemplatesStrength[] damageTableField;

        private AnimalTemplatesPack[] numberTableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Terrain", IsNullable = false)]
        public AnimalTemplatesTerrain[] TerrainList
        {
            get
            {
                return this.terrainListField;
            }
            set
            {
                this.terrainListField = value;
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
        [System.Xml.Serialization.XmlArrayItemAttribute("AnimalType", IsNullable = false)]
        public AnimalTemplatesAnimalType[] AnimalTypeList
        {
            get
            {
                return this.animalTypeListField;
            }
            set
            {
                this.animalTypeListField = value;
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesTerrain
    {

        private AnimalTemplatesTerrainOption[] optionField;

        private string nameField;

        private string typeDMField;

        private string sizeDMField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesTerrainOption[] Option
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TypeDM
        {
            get
            {
                return this.typeDMField;
            }
            set
            {
                this.typeDMField = value;
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesOption
    {

        private byte rollField;

        private string animalTypeField;

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
        public string AnimalType
        {
            get
            {
                return this.animalTypeField;
            }
            set
            {
                this.animalTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalType
    {

        private AnimalTemplatesAnimalTypeBehavior[] behaviorField;

        private AnimalTemplatesAnimalTypeOption[] optionField;

        private string nameField;

        private sbyte weaponDMField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Behavior")]
        public AnimalTemplatesAnimalTypeBehavior[] Behavior
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
        [System.Xml.Serialization.XmlElementAttribute("Option")]
        public AnimalTemplatesAnimalTypeOption[] Option
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte WeaponDM
        {
            get
            {
                return this.weaponDMField;
            }
            set
            {
                this.weaponDMField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalTypeBehavior
    {

        private AnimalTemplatesAnimalTypeBehaviorTable tableField;

        private AnimalTemplatesAnimalTypeBehaviorSkill[] skillField;

        private AnimalTemplatesAnimalTypeBehaviorAttribute[] attributeField;

        private string nameField;

        private string attackField;

        private string fleeField;

        /// <remarks/>
        public AnimalTemplatesAnimalTypeBehaviorTable Table
        {
            get
            {
                return this.tableField;
            }
            set
            {
                this.tableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Skill")]
        public AnimalTemplatesAnimalTypeBehaviorSkill[] Skill
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
        public AnimalTemplatesAnimalTypeBehaviorAttribute[] Attribute
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalTypeBehaviorTable
    {

        private AnimalTemplatesAnimalTypeBehaviorTableAttribute[] attributeField;

        private string rollField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attribute")]
        public AnimalTemplatesAnimalTypeBehaviorTableAttribute[] Attribute
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalTypeBehaviorTableAttribute
    {

        private byte rollField;

        private string nameField;

        private byte addField;

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
        public byte Add
        {
            get
            {
                return this.addField;
            }
            set
            {
                this.addField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalTypeBehaviorSkill
    {

        private string nameField;

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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalTypeBehaviorAttribute
    {

        private string nameField;

        private sbyte addField;

        private string atttackField;

        private string fleeField;

        private string attackField;

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
        public sbyte Add
        {
            get
            {
                return this.addField;
            }
            set
            {
                this.addField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Atttack
        {
            get
            {
                return this.atttackField;
            }
            set
            {
                this.atttackField = value;
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesAnimalTypeOption
    {

        private string behaviorField;

        private byte rollField;

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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AnimalTemplatesStrength
    {

        private byte minValueField;

        private byte damageField;

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
        public byte Damage
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
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
