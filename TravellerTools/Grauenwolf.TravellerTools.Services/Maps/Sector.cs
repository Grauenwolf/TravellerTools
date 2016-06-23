namespace Grauenwolf.TravellerTools.Maps
{
	public class Sector
	{
		public int X { get; set; }
		public int Y { get; set; }
		public string Tags { get; set; }
		public Name[] Names { get; set; }
		public string Abbreviation { get; set; }


		public string Name { get { return Names[0].Text; } }
	}
}
