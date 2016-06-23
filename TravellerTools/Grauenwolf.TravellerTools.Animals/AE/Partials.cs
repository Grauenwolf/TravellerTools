namespace Grauenwolf.TravellerTools.Animals.AE
{


    partial class AnimalTemplatesTerrain
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

    partial class AnimalTemplatesAnimalClassOption : IHasOdds
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

    partial class AnimalTemplatesAnimalClassOptionOption : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }

    partial class AnimalTemplatesSize : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }


    partial class AnimalTemplatesArmorOption : ITablePick
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

