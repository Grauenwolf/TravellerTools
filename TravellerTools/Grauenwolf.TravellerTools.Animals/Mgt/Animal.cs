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
    public int DexterityDM => DMCalc(Dexterity);

    public int Endurance { get => Get<int>(); set => Set(value); }

    [CalculatedField("Endurance")]
    public int EnduranceDM => DMCalc(Endurance);

    public string Flee { get => Get<string>(); set => Set(value); }
    public int Instinct { get => Get<int>(); set => Set(value); }

    [CalculatedField("Instinct")]
    public int InstinctDM => DMCalc(Instinct);

    public int Intelligence { get => Get<int>(); set => Set(value); }

    [CalculatedField("Intelligence")]
    public int IntelligenceDM => DMCalc(Intelligence);

    public string Movement { get => Get<string>(); set => Set(value); }
    public string NumberEncountered { get => Get<string>(); set => Set(value); }
    public int Pack { get => Get<int>(); set => Set(value); }

    [CalculatedField("Pack")]
    public int PackDM => DMCalc(Pack);

    /// <summary>
    /// Roll used for encoutner tables.
    /// </summary>
    public int Roll { get => Get<int>(); set => Set(value); }

    public int Size { get => Get<int>(); set => Set(value); }
    public SkillCollection Skills => GetNew<SkillCollection>();
    public int Strength { get => Get<int>(); set => Set(value); }

    [CalculatedField("Strength")]
    public int StrengthDM => DMCalc(Strength);

    public string TerrainType { get => Get<string>(); set => Set(value); }
    public WeaponCollection Weapons => GetNew<WeaponCollection>();
    public int WeightKG { get { return Get<int>(); } set { Set(value); } }

    int DMCalc(int value)
    {
        return value switch
        {
            0 => -3,
            <= 2 => -2,
            <= 5 => -1,
            <= 8 => 0,
            <= 11 => 1,
            <= 14 => 2,
            _ => 3
        };
    }
}