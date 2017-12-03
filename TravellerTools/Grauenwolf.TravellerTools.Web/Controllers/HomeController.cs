using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.TradeCalculator;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AE = Grauenwolf.TravellerTools.Animals.AE;
using Mgt = Grauenwolf.TravellerTools.Animals.Mgt;

namespace Grauenwolf.TravellerTools.Web.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Global.HomeIndexViewModel);
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

        public async Task<ActionResult> TradeInfo(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance, bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0, Edition edition = Edition.MGT, int? seed = null, bool advancedCharacters = false, int streetwiseScore = 0, bool raffleGoods = false, string milieu = "M1105")
        {
            var tradeEngine = Global.GetTradeEngine(milieu, edition);

            ManifestCollection model = await tradeEngine.BuildManifestsAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance, advancedMode, illegalGoods, brokerScore, seed, advancedCharacters, streetwiseScore, raffleGoods, milieu);

            return View(model);
        }


        [Route("QuickTradeInfo")]
        public async Task<ActionResult> TradeInfo(string originUwp, string destinationUwp, int jumpDistance, bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0, Edition edition = Edition.MGT, int? seed = null, bool advancedCharacters = false, int streetwiseScore = 0, bool raffleGoods = false, string milieu = "M1105")
        {
            var tradeEngine = Global.GetTradeEngine(milieu, edition);

            ManifestCollection model = await tradeEngine.BuildManifestsAsync(originUwp, destinationUwp, jumpDistance, advancedMode, illegalGoods, brokerScore, seed, advancedCharacters, streetwiseScore, raffleGoods, milieu);

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

        public async Task<ActionResult> Character(int? minAge = null, int? maxAge = null, string name = null, string career = null, int? seed = null)
        {
            var dice = new Dice();
            var options = new CharacterBuilderOptions();

            if (!string.IsNullOrEmpty(name))
            {
                options.Name = name;
            }
            else
            {
                var temp = await Global.NameService.CreateRandomPersonAsync(dice);
                options.Name = temp.FullName;
                options.Gender = temp.Gender;
            }

            if (minAge.HasValue && minAge == maxAge)
                options.MaxAge = maxAge;
            else if (minAge.HasValue && maxAge.HasValue)
                options.MaxAge = minAge.Value + dice.D(1, maxAge.Value - minAge.Value);
            else if (maxAge.HasValue)
                options.MaxAge = 12 + dice.D(1, maxAge.Value - 12);
            else
                options.MaxAge = 12 + dice.D(1, 60);

            options.FirstCareer = career;

            options.Seed = seed;

            var model = Global.CharacterBuilder.Build(options);

            return View(model);
        }

        public ActionResult Store(int brokerScore = 0,
             string lawLevel = "0",
             string population = "0",
             bool roll = false,
             int? seed = null,
             string starport = "X",
             int streetwiseScore = 0,
             string techLevel = "0",
             string tradeCodes = null,
             string name = null)
        {
            var options = new StoreOptions()
            {
                BrokerScore = brokerScore,
                LawLevel = lawLevel,
                Population = population,
                Roll = roll,
                Seed = seed,
                Starport = starport,
                StreetwiseScore = streetwiseScore,
                TechLevel = techLevel,
                Name = (name == "") ? null : name
            };
            options.TradeCodes.AddRange((tradeCodes ?? "").Split(' '));


            var model = Global.EquipmentBuilder.AvailabilityTable(options);

            return View(model);

        }
    }
}



