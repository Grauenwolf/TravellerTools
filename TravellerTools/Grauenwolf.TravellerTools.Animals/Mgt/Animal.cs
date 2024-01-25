using Grauenwolf.TravellerTools.Characters;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Animals.Mgt;

public class Animal : ModelBase
{
    public string AnimalType { get => Get<string>(); set => Set(value); }
    public int Armor { get => Get<int>(); set => Set(value); }
    public string Attack { get => Get<string>(); set => Set(value); }
    public string Behavior { get => Get<string>(); set => Set(value); }
    public int Dexterity { get => Get<int>(); set => Set(value); }

    [CalculatedField("Dexterity")]
    public int DexterityDM => Tables.DMCalc(Dexterity);

    public int Endurance { get => Get<int>(); set => Set(value); }

    [CalculatedField("Endurance")]
    public int EnduranceDM => Tables.DMCalc(Endurance);

    public string Flee { get => Get<string>(); set => Set(value); }
    public int Instinct { get => Get<int>(); set => Set(value); }

    [CalculatedField("Instinct")]
    public int InstinctDM => Tables.DMCalc(Instinct);

    public int Intelligence { get => Get<int>(); set => Set(value); }

    [CalculatedField("Intelligence")]
    public int IntelligenceDM => Tables.DMCalc(Intelligence);

    public string Movement { get => Get<string>(); set => Set(value); }
    public string NumberEncountered { get => Get<string>(); set => Set(value); }
    public int Pack { get => Get<int>(); set => Set(value); }

    [CalculatedField("Pack")]
    public int PackDM => Tables.DMCalc(Pack);

    /// <summary>
    /// Roll used for encoutner tables.
    /// </summary>
    public int Roll { get => Get<int>(); set => Set(value); }

    public int Size { get => Get<int>(); set => Set(value); }
    public SkillCollection Skills => GetNew<SkillCollection>();
    public int Strength { get => Get<int>(); set => Set(value); }

    [CalculatedField("Strength")]
    public int StrengthDM => Tables.DMCalc(Strength);

    public string TerrainType { get => Get<string>(); set => Set(value); }
    public WeaponCollection Weapons => GetNew<WeaponCollection>();
    public int WeightKG { get { return Get<int>(); } set { Set(value); } }
}
