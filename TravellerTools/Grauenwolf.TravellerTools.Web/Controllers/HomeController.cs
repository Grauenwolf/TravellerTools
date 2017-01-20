using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AE = Grauenwolf.TravellerTools.Animals.AE;
using Mgt = Grauenwolf.TravellerTools.Animals.Mgt;

namespace Grauenwolf.TravellerTools.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = new HomeIndexViewModel(
                await Maps.TravellerMapService.FetchUniverseAsync(),
                AE.AnimalBuilderAE.TerrainTypeList.Select(t => t.Name).ToList(),
                Mgt.AnimalBuilderMgt.AnimalTypeList.Select(at => at.Name).ToList(),
                AE.AnimalBuilderAE.AnimalClassList.Select(ac => ac.Name).ToList()
                );
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> TradeInfo(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance, bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0, Edition edition = Edition.MGT)
        {
            ManifestCollection model = null;

            if (edition == Edition.MGT)
                model = await Global.TradeEngineMgt.BuildManifestsAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance, advancedMode, illegalGoods, brokerScore);
            else if (edition == Edition.MGT2)
                model = await Global.TradeEngineMgt2.BuildManifestsAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance, advancedMode, illegalGoods, brokerScore);

            return View(model);
        }

        public ActionResult Animals(string terrainType = "", string animalType = "")
        {
            Dictionary<string, List<Mgt.Animal>> model;
            if (!string.IsNullOrWhiteSpace(terrainType) && !string.IsNullOrWhiteSpace(animalType))
            {
                var list = new List<Mgt.Animal>();
                model = new Dictionary<string, List<Mgt.Animal>>();
                model.Add(terrainType, list);
                list.Add(Mgt.AnimalBuilderMgt.BuildAnimal(terrainType, animalType));
            }
            else if (!string.IsNullOrWhiteSpace(terrainType))
            {
                model = new Dictionary<string, List<Mgt.Animal>>();
                model.Add(terrainType, Mgt.AnimalBuilderMgt.BuildTerrainSet(terrainType));
            }
            else
            {
                model = Mgt.AnimalBuilderMgt.BuildPlanetSet();
            }

            return View(model);
        }

        public ActionResult AnimalEncounters(string terrainType = "", string animalClass = "", int evolution = 0)
        {
            Dictionary<string, List<AE.Animal>> model;
            if (!string.IsNullOrWhiteSpace(terrainType) && !string.IsNullOrWhiteSpace(animalClass))
            {
                var list = new List<AE.Animal>();
                model = new Dictionary<string, List<AE.Animal>>();
                model.Add(terrainType, list);
                list.Add(AE.AnimalBuilderAE.BuildAnimal(terrainType, animalClass, evolution));
            }
            else if (!string.IsNullOrWhiteSpace(terrainType))
            {
                model = new Dictionary<string, List<AE.Animal>>();
                model.Add(terrainType, AE.AnimalBuilderAE.BuildTerrainSet(terrainType));
            }
            else
            {
                model = AE.AnimalBuilderAE.BuildPlanetSet();
            }

            return View(model);
        }

        public async Task<ActionResult> Character(int? minAge = null, int? maxAge = null, string name = null)
        {
            var dice = new Dice();
            var options = new CharacterBuilderOptions();

            if (!string.IsNullOrEmpty(name))
                options.Name = name;
            else
                options.Name = (await NameService.CreateRandomPersonAsync()).FullName;

            if (minAge.HasValue && maxAge.HasValue)
                options.MaxAge = minAge.Value + dice.D(1, maxAge.Value - minAge.Value);
            else if (maxAge.HasValue)
                options.MaxAge = 12 + dice.D(1, maxAge.Value - 12);
            else
                options.MaxAge = 12 + dice.D(1, 60);

            var model = CharacterBuilder.Build(options);

            return View(model);
        }
    }
}



