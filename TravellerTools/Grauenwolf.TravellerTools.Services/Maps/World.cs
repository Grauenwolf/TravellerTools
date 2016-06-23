using System;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Maps
{
	public class World
	{
		private string m_Remarks;
		readonly HashSet<string> m_RemarksList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		static readonly Dictionary<string, string> s_RemarkMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		static World()
		{
			s_RemarkMap.Add("As", "Asteroid Belt.");
			s_RemarkMap.Add("De", "Desert.");
			s_RemarkMap.Add("Fl", "Fluid Hydrographics (in place of water).");
			s_RemarkMap.Add("Ga", "Garden World.");
			s_RemarkMap.Add("He", "Hellworld.");
			s_RemarkMap.Add("Ic", "Ice Capped.");
			s_RemarkMap.Add("Oc", "Ocean World.");
			s_RemarkMap.Add("Va", "Vacuum World.");
			s_RemarkMap.Add("Wa", "Water World.");
			s_RemarkMap.Add("Di", "Dieback.");
			s_RemarkMap.Add("Ba", "Barren.");
			s_RemarkMap.Add("Lo", "Low Population.");
			s_RemarkMap.Add("Ni", "Non-Industrial.");
			s_RemarkMap.Add("Ph", "Pre-High Population.");
			s_RemarkMap.Add("Hi", "High Population.");
			s_RemarkMap.Add("Pa", "Pre-Agricultural.");
			s_RemarkMap.Add("Ag", "Agricultural.");
			s_RemarkMap.Add("Na", "Non-Agricultural.");
			s_RemarkMap.Add("Pi", "Pre-Industrial.");
			s_RemarkMap.Add("In", "Industrialized.");
			s_RemarkMap.Add("Po", "Poor.");
			s_RemarkMap.Add("Pr", "Pre-Rich.");
			s_RemarkMap.Add("Ri", "Rich.");
			s_RemarkMap.Add("Fr", "Frozen.");
			s_RemarkMap.Add("Ho", "Hot.");
			s_RemarkMap.Add("Co", "Cold.");
			s_RemarkMap.Add("Lk", "Locked.");
			s_RemarkMap.Add("Tr", "Tropic.");
			s_RemarkMap.Add("Tu", "Tundra.");
			s_RemarkMap.Add("Tz", "Twilight Zone.");
			s_RemarkMap.Add("Fa", "Farming.");
			s_RemarkMap.Add("Mi", "Mining.");
			s_RemarkMap.Add("Mr", "Military Rule.");
			s_RemarkMap.Add("Px", "Prison, Exile Camp.");
			s_RemarkMap.Add("Pe", "Penal Colony.");
			s_RemarkMap.Add("Re", "Reserve.");
			s_RemarkMap.Add("Sa", "Satellite.");
			s_RemarkMap.Add("Fo", "Forbidden.");
			s_RemarkMap.Add("Pz", "Puzzle.");
			s_RemarkMap.Add("Da", "Danger");
			s_RemarkMap.Add("Ab", "Data Repository.");
			s_RemarkMap.Add("An", "Ancient Site.");
			s_RemarkMap.Add("Rs", "Research Station.");
			s_RemarkMap.Add("RsA", "Research Station Alpha.");
			s_RemarkMap.Add("RsB", "Research Station Beta.");
			s_RemarkMap.Add("RsG", "Research Station Gamma.");
			s_RemarkMap.Add("Lt", "Low Technology.");
			s_RemarkMap.Add("Ht", "High Technology.");
			//s_RemarkMap.Add("Fa", "Fascinating.");
			s_RemarkMap.Add("St", "Steppeworld.");
			s_RemarkMap.Add("Ex", "Exile Camp.");
			//s_RemarkMap.Add("Pr", "Prison World.");
			s_RemarkMap.Add("Xb", "Xboat Station.");
		}


		public string Name { get; set; }
		public string Hex { get; set; }
		public string UWP { get; set; }
		public string PBG { get; set; }
		public string Zone { get; set; }
		public string Bases { get; set; }
		public string Allegiance { get; set; }
		public string Stellar { get; set; }
		public string SS { get; set; }
		public string Ix { get; set; }
		public string Ex { get; set; }
		public string Cx { get; set; }
		public string Nobility { get; set; }
		public int Worlds { get; set; }
		public int ResourceUnits { get; set; }
		public int Subsector { get; set; }
		public int Quadrant { get; set; }
		public string Remarks
		{
			get
			{
				return m_Remarks;
			}
			set
			{
				m_Remarks = value;
				m_RemarksList.Clear();
				foreach (var item in m_Remarks.Split(' '))
					m_RemarksList.Add(item);
			}
		}

		public string LegacyBaseCode { get; set; }
		public string Sector { get; set; }
		public string SubsectorName { get; set; }
		public string AllegianceName { get; set; }

		public int JumpDistance { get; set; }

		public Grauenwolf.TravellerTools.EHex StarportCode { get { return UWP[0]; } }
		public Grauenwolf.TravellerTools.EHex SizeCode { get { return UWP[1]; } }
		public Grauenwolf.TravellerTools.EHex AtmosphereCode { get { return UWP[2]; } }
		public Grauenwolf.TravellerTools.EHex HydrographicsCode { get { return UWP[3]; } }
		public Grauenwolf.TravellerTools.EHex PopulationCode { get { return UWP[4]; } }
		public Grauenwolf.TravellerTools.EHex GovernmentCode { get { return UWP[5]; } }
		public Grauenwolf.TravellerTools.EHex LawCode { get { return UWP[6]; } }
		public Grauenwolf.TravellerTools.EHex TechCode { get { return UWP[8]; } }

		public string StarportDescription
		{
			get
			{
				switch (StarportCode.ToString())
				{
					//star ports
					case "A": return "Excellent Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing starships and non-starships present. Nava base and/or scout base may be present.";
					case "B": return "Good Quality. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing non-starships present. Naval base and/or scout base may be present.";
					case "C": return "Routine Quality. Only unrefined fuel available. Reasonable repair facilities present. Scout base may be present.";
					case "D": return "Poor Quality. Only unrefined fuel available. No repair facilities present. Scout base may be present.";
					case "E": return "Frontier Installation. Essentially a marked spot of bedrock with no fuel, facilities, or bases present.";
					case "X": return "No Starport. No provision is made for any ship landings.";
					//space ports
					case "F": return "Good Quality. Minor damage repairable. Unrefined fuel available.";
					case "G": return "Poor Quality. Superficial repairs possible. Unrefined fuel available.";
					case "H": return "Primitive Quality. No repairs or fuel available.";
					case "Y": return "None.";
					default: return "";
				}
			}
		}

		public string Starport
		{
			get
			{
				switch (StarportCode.ToString())
				{
					//star ports
					case "A": return "Excellent Quality.";
					case "B": return "Good Quality.";
					case "C": return "Routine Quality.";
					case "D": return "Poor Quality.";
					case "E": return "Frontier Installation.";
					case "X": return "No Starport.";
					//space ports
					case "F": return "Good Quality.";
					case "G": return "Poor Quality.";
					case "H": return "Primitive Quality.";
					case "Y": return "None.";
					default: return "";
				}
			}
		}

		public int SizeKM
		{
			get
			{
				switch (SizeCode.ToString())
				{

					case "0": return 800;
					case "1": return 1600;
					case "2": return 3200;
					case "3": return 4800;
					case "4": return 6400;
					case "5": return 8000;
					case "6": return 9600;
					case "7": return 11200;
					case "8": return 12800;
					case "9": return 14400;
					case "A": return 16000;
					default: return 0;
				}
			}
		}

		public TimeSpan TransitTimeJumpPoint(int thrustRating)
		{
			const double G = 9.80665; //meters per second per second
			var distanceM = SizeKM * 1000 * 100;
			var timeSeconds = 2 * Math.Sqrt(distanceM / (G * thrustRating));
			return TimeSpan.FromSeconds(timeSeconds);
		}

		public string Atmosphere
		{
			get
			{
				switch (AtmosphereCode.ToString())
				{
					//star ports
					case "0": return "No atmosphere.";
					case "1": return "Trace.";
					case "2": return "Very thin. Tainted.";
					case "3": return "Very thin.";
					case "4": return "Thin. Tainted.";
					case "5": return "Thin. Breathable.";
					case "6": return "Standard. Breathable.";
					case "7": return "Standard. Tainted.";
					case "8": return "Dense. Breathable";
					case "9": return "Dense. Tainted.";
					case "A": return "Exotic.";
					case "B": return "Corrosive.";
					case "C": return "Insidious.";
					case "D": return "Dense, high.";
					case "E": return "Ellipsoid.";
					case "F": return "Thin, low.";
					default: return "";
				}
			}
		}

		public string AtmosphereDescription
		{
			get
			{
				switch (AtmosphereCode.ToString())
				{
					//star ports
					case "0": return "No atmosphere. Requires vacc suit.";
					case "1": return "Trace. Requires vacc suit.";
					case "2": return "Very thin. Tainted. Requires combination respirator/filter.";
					case "3": return "Very thin. Requires respirator.";
					case "4": return "Thin. Tainted. Requires filter mask.";
					case "5": return "Thin. Breathable. ";
					case "6": return "Standard. Breathable.";
					case "7": return "Standard. Tainted. Requires filter mask.";
					case "8": return "Dense. Breathable";
					case "9": return "Dense. Tainted. Requires filter mask.";
					case "A": return "Exotic. Requires special protective equipment.";
					case "B": return "Corrosive. Requires protective suit.";
					case "C": return "Insidious. Requires protective suit.";
					case "D": return "Dense, high. Breathable above a minimum altitude.";
					case "E": return "Ellipsoid. Breathable at certain latitudes.";
					case "F": return "Thin, low. Breathable below certain altitudes.";
					default: return "";
				}
			}
		}
		public string Hydrographics
		{
			get
			{
				switch (HydrographicsCode.ToString())
				{
					//star ports
					case "0": return "No water. Desert World. ";
					case "1": return "10% water.";
					case "2": return "20% water.";
					case "3": return "30% water.";
					case "4": return "40% water.";
					case "5": return "50% water.";
					case "6": return "60% water.";
					case "7": return "70% water. Equivalent to Terra or Vland.";
					case "8": return "80% water.";
					case "9": return "90% water.";
					case "A": return "100% water. Water World.";
					default: return "";
				}
			}
		}

		public double PopulationExponent
		{
			get { return Math.Pow(10, PopulationCode.Value); }
		}

		public int PopulationMultiplier
		{
			get
			{
				if (!string.IsNullOrEmpty(PBG))
				{
					var temp = PBG.Substring(0, 1);
					int result;
					if (int.TryParse(temp, out result))
						return result;
				}
				return 1;
			}
		}

		public double Population
		{
			get { return PopulationMultiplier * PopulationExponent; }
		}

		public string GovernmentType
		{
			get
			{
				switch (GovernmentCode.ToString())
				{
					case "0": return "No Government Structure.";
					case "1": return "Company/Corporation.";
					case "2": return "Participating Democracy.";
					case "3": return "Self-Perpetuating Oligarchy.";
					case "4": return "Representative Democracy.";
					case "5": return "Feudal Technocracy.";
					case "6": return "Captive Government / Colony.";
					case "7": return "Balkanization.";
					case "8": return "Civil Service Bureaucracy.";
					case "9": return "Impersonal Bureaucracy.";
					case "A": return "Charismatic Dictator.";
					case "B": return "Non-Charismatic Dictator.";
					case "C": return "Charismatic Oligarchy.";
					case "D": return "Religious Dictatorship.";
					case "E": return "Religious Autocracy.";
					case "F": return "Totalitarian Oligarchy.";
					case "G": return "Small Station or Facility. Aslan.";
					case "H": return "Split Clan Control. Aslan.";
					case "J": return "Single On-world Clan Control. Aslan.";
					case "K": return "Single Multi-world Clan Control. Aslan.";
					case "L": return "Major Clan Control. Aslan.";
					case "M": return "Vassal Clan Control. Aslan.";
					case "N": return "Major Vassal Clan Control. Aslan.";
					case "P": return "Small Station or Facility. K’kree.";
					case "Q": return "Krurruna or Krumanak Rule for Off-world Steppelord. K’kree.";
					case "R": return "Steppelord On-world Rule. K’kree.";
					case "S": return "Sept. Hiver.";
					case "T": return "Unsupervised Anarchy. Hiver.";
					case "U": return "Supervised Anarchy. Hiver.";
					case "W": return "Committee. Hiver.";
					case "X": return "Droyne Hierarchy. Droyne.";
					default: return "";
				}
			}
		}


		public string LawLevel
		{
			get
			{
				switch (LawCode.ToString())
				{
					case "0": return "No prohibitions.";
					case "1": return "Body pistols, explosives, and poison gas prohibited.";
					case "2": return "Portable energy weapons prohibited.";
					case "3": return "Machine guns, automatic rifles prohibited.";
					case "4": return "Light assault weapons prohibited.";
					case "5": return "Personal concealable weapons prohibited.";
					case "6": return "All firearms except shotguns prohibited.";
					case "7": return "Shotguns prohibited.";
					case "8": return "Long bladed weapons controlled; open possession prohibited.";
					case "9": return "Possession of weapons outside the home prohibited.";
					case "A": return "Weapon possession prohibited.";
					case "B": return "Rigid control of civilian movement.";
					case "C": return "Unrestricted invasion of privacy.";
					case "D": return "Paramilitary law enforcement.";
					case "E": return "Full-fledged police state.";
					case "F": return "All facets of daily life regularly legislated and controlled.";
					case "G": return "Severe punishment for petty infractions.";
					case "H": return "Legalized oppressive practices.";
					case "J": return "Routinely oppressive and restrictive.";
					case "K": return "Excessively oppressive and restrictive.";
					case "L": return "Totally oppressive and restrictive.";
					case "S": return "Special/Variable situation.";

					default: return "";
				}
			}
		}

		public string TechLevel
		{
			get
			{
				switch (TechCode.ToString())
				{
					case "0": return "Stone Age. Primitive.";
					case "1": return "Bronze, Iron. Bronze Age to Middle Ages";
					case "2": return "Printing Press. circa 1400 to 1700.";
					case "3": return "Basic Science. circa 1700 to 1860.";
					case "4": return "External Combustion. circa 1860 to 1900.";
					case "5": return "Mass Production. circa 1900 to 1939.";
					case "6": return "Nuclear Power. circa 1940 to 1969.";
					case "7": return "Miniaturized Electronics. circa 1970 to 1979.";
					case "8": return "Quality Computers. circa 1980 to 1989.";
					case "9": return "Anti-Gravity. circa 1990 to 2000.";
					case "A": return "Interstellar community.";
					case "B": return "Lower Average Imperial.";
					case "C": return "Average Imperial.";
					case "D": return "Above Average Imperial.";
					case "E": return "Above Average Imperial.";
					case "F": return "Technical Imperial Maximum.";
					case "G": return "Robots.";
					case "H": return "Artificial Intelligence.";
					case "J": return "Personal Disintegrators.";
					case "K": return "Plastic Metals.";
					case "L": return "Comprehensible only as technological magic.";
					default: return "";
				}
			}
		}


		public string HexX { get { return Hex.Substring(0, 2); } }
		public string HexY { get { return Hex.Substring(2, 2); } }


		//These are added later
		public int SectorX { get; set; }
		public int SectorY { get; set; }

		public string RemarksDescription
		{
			get
			{
				var source = Remarks.Split(' ');
				var result = new List<string>();
				foreach (var remark in source)
				{
					string des;
					if (s_RemarkMap.TryGetValue(remark, out des))
						result.Add(string.Format("{0}: {1}", remark, des));
					else
						result.Add(remark);
				}
				return string.Join(" ", result);
			}
		}

		/// <summary>
		/// Determines whether world contains specified remark. This includes zones A and R.
		/// </summary>
		/// <param name="remark">The case insensitive remark.</param>
		/// <returns><c>true</c> if the specified remark contains remark; otherwise, <c>false</c>.</returns>
		public bool ContainsRemark(string remark)
		{
			return m_RemarksList.Contains(remark) || string.Compare(Zone, remark, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public string SubSectorIndex { get; set; }



	}
}
