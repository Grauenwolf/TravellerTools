using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Grauenwolf.TravellerTools.Web.Models
{
    public class HomeIndexViewModel
    {
        readonly IReadOnlyList<string> m_AnimalClasses; //Animal Encounters
        readonly IReadOnlyList<string> m_AnimalTypes; //Mongoose 1
        readonly IReadOnlyList<Sector> m_Sectors;
        readonly IReadOnlyList<string> m_Terrains;

        public HomeIndexViewModel(IReadOnlyList<Sector> sectors, IReadOnlyList<string> terrains, IReadOnlyList<string> animalTypes, IReadOnlyList<string> animalClasses)
        {
            m_AnimalClasses = animalClasses;
            m_AnimalTypes = animalTypes;
            m_Terrains = terrains;
            m_Sectors = sectors;
        }

        public IEnumerable<SelectListItem> SectorList()
        {
            yield return new SelectListItem() { Text = "", Value = "" };

            foreach (var sector in m_Sectors.OrderBy(s => s.Name).Distinct(new SectorComparer()))
                yield return new SelectListItem() { Text = sector.Name, Value = string.Format("{0},{1}", sector.X, sector.Y) };
        }

        public IEnumerable<SelectListItem> TerrainList()
        {
            yield return new SelectListItem() { Text = "(All)", Value = "" };

            foreach (var terrain in m_Terrains.OrderBy(s => s))
                yield return new SelectListItem() { Text = terrain, Value = terrain };
        }

        public IEnumerable<SelectListItem> AnimalTypeList()
        {
            yield return new SelectListItem() { Text = "", Value = "" };

            foreach (var terrain in m_AnimalTypes.OrderBy(s => s))
                yield return new SelectListItem() { Text = terrain, Value = terrain };
        }

        public IEnumerable<SelectListItem> AnimalClassList()
        {
            yield return new SelectListItem() { Text = "", Value = "" };

            foreach (var terrain in m_AnimalClasses.OrderBy(s => s))
                yield return new SelectListItem() { Text = terrain, Value = terrain };
        }

        private class SectorComparer : IEqualityComparer<Sector>
        {
            public bool Equals(Sector x, Sector y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode(Sector obj)
            {
                return obj.Name.GetHashCode();
            }
        }
    }
}
