using Grauenwolf.TravellerTools.Animals;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Grauenwolf.TravellerTools.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var model = new HomeIndexViewModel(await Maps.TravellerMapService.FetchUniverseAsync());
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

        public async Task<ActionResult> TradeInfo(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance, bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0)
        {
            var model = await TradeEngine.BuildManifestsAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance, advancedMode, illegalGoods, brokerScore);

            return View(model);
        }

        public ActionResult Animals(string terrainType = "", string animalType = "")
        {
            System.Collections.Generic.Dictionary<string, List<Grauenwolf.TravellerTools.Animals.Animal>> model;
            if (!string.IsNullOrWhiteSpace(terrainType) && !string.IsNullOrWhiteSpace(animalType))
            {
                var list = new List<Animal>();
                model = new Dictionary<string, List<Animal>>();
                model.Add(terrainType, list);
                list.Add(AnimalBuilder.BuildAnimal(terrainType, animalType));
            }
            else if (!string.IsNullOrWhiteSpace(terrainType))
            {
                model = new Dictionary<string, List<Animal>>();
                model.Add(terrainType, AnimalBuilder.BuildTerrainSet(terrainType));
            }
            else
            {
                model = AnimalBuilder.BuildPlanetSet();
            }

            return View(model);
        }
    }
}



