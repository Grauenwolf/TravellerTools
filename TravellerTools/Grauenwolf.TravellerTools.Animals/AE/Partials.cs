namespace Grauenwolf.TravellerTools.Animals.AE;

partial class AnimalTemplatesTerrainOption : IHasOdds
{
    int IHasOdds.Odds => Odds;
}

partial class AnimalTemplatesTerrainOption1 : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class ArmorOption : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class BehaviorOption : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class ChartOption : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class DietOption : IHasOdds
{
    int IHasOdds.Odds => Odds;
}

partial class Size : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class TerrainTemplate
{
    public override string ToString() => Name;
}

partial class WeaponTemplate : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}