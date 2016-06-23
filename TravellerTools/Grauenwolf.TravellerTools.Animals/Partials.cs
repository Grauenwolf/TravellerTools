using System;

namespace Grauenwolf.TravellerTools.Animals
{


    partial class AnimalTemplatesTerrain
    {
        public override string ToString()
        {
            return Name;
        }
    }

    partial class AnimalTemplatesAnimalType
    {
        public override string ToString()
        {
            return Name;
        }
    }

    partial class AnimalTemplatesTerrainOption : ITablePick
    {
        public bool IsMatch(int value)
        {
            return Roll == value;
        }
    }
    partial class AnimalTemplatesAnimalTypeOption : ITablePick
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
    public interface ITablePick
    {
        bool IsMatch(int value);
    }
}

