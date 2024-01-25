using Grauenwolf.TravellerTools.Characters;
using Tortuga.Anchor.Modeling;

namespace Grauenwolf.TravellerTools.Animals.AE;

public class Animal : ModelBase
{
    public string AnimalClass { get => Get<string>(); set => Set(value); }
    public int Armor { get => Get<int>(); set => Set(value); }
    public int? Attack { get => Get<int?>(); set => Set(value); }
    public string Behavior { get => Get<string>(); set => Set(value); }
    public int BehaviorCount { get => GetDefault(1); set { Set(value); } }
    public int Dexterity { get => Get<int>(); set => Set(value); }

    [CalculatedField("Dexterity")]
    public int DexterityDM => Tables.DMCalc(Dexterity);

    public string Diet { get => Get<string>(); set => Set(value); }
    public int Endurance { get => Get<int>(); set => Set(value); }

    [CalculatedField("Endurance")]
    public int EnduranceDM => Tables.DMCalc(Endurance);

    public int EvolutionDM { get => Get<int>(); set => Set(value); }
    public int EvolutionRolls { get => Get<int>(); set => Set(value); }
    public int EvolutionSkills { get => Get<int>(); set => Set(value); }
    public FeatureCollection Features => GetNew<FeatureCollection>();
    public int? Flee { get => Get<int?>(); set => Set(value); }
    public int InitiativeDM { get => Get<int>(); set => Set(value); }
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

    public int PhysicalSkills { get => Get<int>(); set => Set(value); }

    /// <summary>
    /// Gets the scripts to run at the very end of character creation.
    /// </summary>
    /// <value>The scripts.</value>
    public List<string> PostScripts => GetNew<List<string>>();

    public int QuirkRolls { get => Get<int>(); set => Set(value); }

    /// <summary>
    /// Roll used for encounter tables.
    /// </summary>
    public int Roll { get => Get<int>(); set => Set(value); }

    public string SecondaryBehavior { get => Get<string>(); set => Set(value); }
    public int Size { get => Get<int>(); set => Set(value); }
    public SkillCollection Skills => GetNew<SkillCollection>();
    public int SocialSkills { get => Get<int>(); set => Set(value); }
    public int Strength { get => Get<int>(); set => Set(value); }

    [CalculatedField("Strength")]
    public int StrengthDM => Tables.DMCalc(Strength);

    public string TerrainType { get => Get<string>(); set => Set(value); }
    public WeaponCollection Weapons => GetNew<WeaponCollection>();
    public int WeightKG { get => Get<int>(); set => Set(value); }

    public void Increase(string attributeName, int bonus)
    {
        switch (attributeName)
        {
            case "Size": Size += bonus; return;

            case "Strength":
            case "Str":
                Strength += bonus; return;

            case "Dexterity":
            case "Dex":
                Dexterity += bonus; return;

            case "Endurance":
            case "End":
                Endurance += bonus; return;

            case "Intelligence":
            case "Int":
                Intelligence += bonus; return;

            case "Instinct":
            case "Ins":
                Instinct += bonus; return;

            case "Pack": Pack += bonus; return;

            case "Armor": Armor += bonus; return;

            case "QuirkRolls":
            case "Quirks":
                QuirkRolls += bonus; return;

            case "PhysicalSkills": PhysicalSkills += bonus; return;
            case "SocialSkills": SocialSkills += bonus; return;

            case "EvolutionSkills": EvolutionSkills += bonus; return;
            case "EvolutionDM": EvolutionDM += bonus; return;
            case "EvolutionRolls": EvolutionRolls += bonus; return;
            case "InitiativeDM": InitiativeDM += bonus; return;
            case "BehaviorCount": BehaviorCount += bonus; return;

            default:
                throw new System.ArgumentOutOfRangeException("attributeName", attributeName, "Unknown attribute " + attributeName);
        }
    }
}
