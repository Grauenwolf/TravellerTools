using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Characters.Careers;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grauenwolf.TravellerTools.Web.Models
{
    public class HomeIndexViewModel
    {
        IReadOnlyList<string> m_AnimalClasses; //Animal Encounters
        IReadOnlyList<string> m_AnimalTypes; //Mongoose 1
        IReadOnlyList<Sector> m_Sectors;
        IReadOnlyList<string> m_Terrains;

        private HomeIndexViewModel()
        {
        }

        public IReadOnlyList<CareerBase> Careers { get; private set; }
        public IReadOnlyList<SkillTemplate> Skills { get; private set; }

        public static async Task<HomeIndexViewModel> GetHomeIndexViewModel(TravellerMapService mapService, CharacterBuilder characterBuilder, EquipmentBuilder equipmentBuilder)
        {
            var result = new HomeIndexViewModel()
            {
                Careers = characterBuilder.Careers,
                Skills = characterBuilder.Book.AllSkills.AddRange(characterBuilder.Book.PsionicTalents),
                m_AnimalClasses = Animals.AE.AnimalBuilderAE.AnimalClassList.Select(ac => ac.Name).ToList(),
                m_AnimalTypes = Animals.Mgt.AnimalBuilderMgt.AnimalTypeList.Select(at => at.Name).ToList(),
                m_Terrains = Animals.AE.AnimalBuilderAE.TerrainTypeList.Select(t => t.Name).ToList(),
            };

            mapService.UniverseUpdated += async (s, e) => result.m_Sectors = await mapService.FetchUniverseAsync();

            try
            {
                result.m_Sectors = await mapService.FetchUniverseAsync(); //do this after hooking up the event to avoid race condition.
            }
            catch
            {
                result.m_Sectors = new List<Sector>(); //off-line mode
            }

            return result;
        }

        public IEnumerable<SelectListItem> AgeList()
        {
            yield return new SelectListItem() { Text = "(Random)", Value = "", Selected = true };

            for (var terms = 1; terms <= 15; terms++)
            {
                var text = string.Format("{0} - {1}", 18 + (terms * 4), 18 + (terms * 4) + 3);
                yield return new SelectListItem() { Text = text, Value = terms.ToString() };
            }
        }

        public IEnumerable<SelectListItem> AnimalClassList()
        {
            yield return new SelectListItem() { Text = "", Value = "" };

            foreach (var terrain in m_AnimalClasses.OrderBy(s => s))
                yield return new SelectListItem() { Text = terrain, Value = terrain };
        }

        public IEnumerable<SelectListItem> AnimalTypeList()
        {
            yield return new SelectListItem() { Text = "", Value = "" };

            foreach (var terrain in m_AnimalTypes.OrderBy(s => s))
                yield return new SelectListItem() { Text = terrain, Value = terrain };
        }

        public IEnumerable<SelectListItem> CareerList()
        {
            yield return new SelectListItem() { Text = "(Random)", Value = "", Selected = true };

            foreach (var career in Careers.Select(c => c.Career).Distinct().OrderBy(s => s))
                yield return new SelectListItem() { Text = career, Value = career };
        }

        public IEnumerable<SelectListItem> JumpDistances()
        {
            for (var i = 0; i <= 9; i++)
                yield return new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = (i == 1) };
        }

        public IEnumerable<SelectListItem> LawLevels()
        {
            foreach (var code in Tables.LawLevelCodes)
                yield return new SelectListItem() { Text = $"{code.FlexString} {Tables.LawLevel(code)}", Value = code.Value.ToString() };
        }

        public IEnumerable<SelectListItem> MilieuList()
        {
            //yield return new SelectListItem() { Text = "The Interstellar Wars", Value = "????" };
            yield return new SelectListItem() { Text = "0 – Early Imperium", Value = "M0" };
            yield return new SelectListItem() { Text = "990 – Solomani Rim War", Value = "M990" };
            yield return new SelectListItem() { Text = "1105 – The Golden Age", Value = "M1105", Selected = true };
            yield return new SelectListItem() { Text = "1120 – The Rebellion", Value = "M1120" };
            yield return new SelectListItem() { Text = "1201 – The New Era", Value = "M1201" };
            yield return new SelectListItem() { Text = "1248 – The New, New Era", Value = "M1248" };
            yield return new SelectListItem() { Text = "1900 – The Far Far Future", Value = "M1900" };
        }

        public IEnumerable<SelectListItem> Populations()
        {
            foreach (var code in Tables.PopulationCodes)
                yield return new SelectListItem() { Text = $"{code.FlexString} {Tables.PopulationExponent(code).ToString("N0")}", Value = code.Value.ToString() };
        }

        public IEnumerable<SelectListItem> Scores()
        {
            for (var i = -6; i <= 8; i++)
                yield return new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = (i == 0) };
        }

        public IEnumerable<SelectListItem> SectorList()
        {
            yield return new SelectListItem() { Text = "", Value = "" };

            foreach (var sector in m_Sectors.OrderBy(s => s.Name).Distinct(new SectorComparer()))
                yield return new SelectListItem() { Text = sector.Name, Value = string.Format("{0},{1}", sector.X, sector.Y) };
        }

        public IEnumerable<SelectListItem> SkillList()
        {
            yield return new SelectListItem() { Text = "", Value = "", Selected = true };

            foreach (var skill in Skills)
                yield return new SelectListItem() { Text = skill.ToString(), Value = skill.Specialty ?? skill.Name };
        }

        public IEnumerable<SelectListItem> Starports()
        {
            foreach (var code in Tables.StarportCodes)
                yield return new SelectListItem() { Text = $"{code} {Tables.Starport(code)}", Value = code.Value.ToString() };
        }

        public IEnumerable<SelectListItem> TasZones()
        {
            yield return new SelectListItem() { Text = "Green", Value = TasZone.Green.ToString(), Selected = true };
            yield return new SelectListItem() { Text = "Amber", Value = TasZone.Amber.ToString(), Selected = false };
            yield return new SelectListItem() { Text = "Red", Value = TasZone.Red.ToString(), Selected = false };
        }

        public IEnumerable<SelectListItem> TechLevels()
        {
            foreach (var code in Tables.TechLevelCodes)
                yield return new SelectListItem() { Text = $"{code.FlexString} {Tables.TechLevel(code)}", Value = code.Value.ToString() };
        }

        public IEnumerable<SelectListItem> TerrainList()
        {
            yield return new SelectListItem() { Text = "(All)", Value = "" };

            foreach (var terrain in m_Terrains.OrderBy(s => s))
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