namespace Grauenwolf.TravellerTools.Animals.Mgt;

partial class AnimalTemplatesAnimalType
{
    public override string ToString() => Name;
}

partial class AnimalTemplatesAnimalTypeOption : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class AnimalTemplatesArmorOption : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class AnimalTemplatesSize : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class AnimalTemplatesTerrain
{
    public override string ToString() => Name;
}

partial class AnimalTemplatesTerrainOption : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}

partial class AnimalTemplatesWeapon : ITablePick
{
    public bool IsMatch(int value) => Roll == value;
}
