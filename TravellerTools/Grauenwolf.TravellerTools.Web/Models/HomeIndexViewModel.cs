using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

namespace Grauenwolf.TravellerTools.Web.Models
{
	public class HomeIndexViewModel
	{
		readonly IReadOnlyList<Sector> m_Sectors;

		public HomeIndexViewModel(IReadOnlyList<Sector> sectors)
		{
			m_Sectors = sectors;
		}

		public IEnumerable<SelectListItem> SectorList()
		{
			yield return new SelectListItem() { Text = "", Value = "" };

			foreach (var sector in m_Sectors.OrderBy(s => s.Name).Distinct(new SectorComparer()))
			{
				yield return new SelectListItem() { Text = sector.Name, Value = string.Format("{0},{1}", sector.X, sector.Y) };
			}
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
