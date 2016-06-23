using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

			foreach (var sector in m_Sectors.OrderBy(s => s.Name))
			{
				yield return new SelectListItem() { Text = sector.Name, Value = string.Format("{0},{1}", sector.X, sector.Y) };
			}
		}
	}
}
