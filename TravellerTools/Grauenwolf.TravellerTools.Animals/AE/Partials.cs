namespace Grauenwolf.TravellerTools.Animals.AE
{


    partial class TerrainTemplate
    {
        public override string ToString()
        {
            return Name;
        }
    }

    //partial class AnimalTemplatesAnimalType
    //{
    //    public override string ToString()
    //    {
    //        return Name;
    //    }
    //}

    partial class AnimalTemplatesTerrainOption : IHasOdds
    {
        int IHasOdds.Odds
        {
            get { return Odds; }
        }
    }

    partial class DietOption : IHasOdds
    {
        int IHasOdds.Odds
        {
            get { return Odds; }
        }
    }

    //partial class AnimalTemplatesTerrainOption : ITablePick
    //{
    //    public bool IsMatch(int value)
    //    {
    //        return Roll == value;
    //    }
    //}
    partial class AnimalTemplatesTerrainOption1 : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }

    partial class BehaviorOption : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }
    partial class ChartOption : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }

    partial class AnimalTemplatesBehaviorChartOption : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }

    partial class Size : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }


    partial class ArmorOption : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }

    partial class AnimalTemplatesWeapon : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }

}

