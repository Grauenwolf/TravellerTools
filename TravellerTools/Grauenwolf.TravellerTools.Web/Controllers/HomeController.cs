using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.TradeCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AE = Grauenwolf.TravellerTools.Animals.AE;
using Mgt = Grauenwolf.TravellerTools.Animals.Mgt;

namespace Grauenwolf.TravellerTools.Web.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
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

        public async Task<ActionResult> Character(int? minAge = null, int? maxAge = null, string name = null, string firstAssignment = null, string finalCareer = null, string finalAssignment = null, int? seed = null, string gender = null, string[] skills = null)
        {
            var dice = new Dice();
            var options = new CharacterBuilderOptions();

            if (skills == null)
                skills = new string[0];

            var desiredSkills = new HashSet<string>(skills, StringComparer.InvariantCultureIgnoreCase);

            if (!string.IsNullOrEmpty(name))
            {
                options.Name = name;
                options.Gender = gender;
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

            options.FirstAssignment = firstAssignment;

            options.Seed = seed;

            if ((!string.IsNullOrEmpty(finalCareer) || !string.IsNullOrEmpty(finalAssignment) || desiredSkills.Count > 0) && seed == null)
            {
                //create a lot of characters, then pick the best fit.

                var preferredCharacters = new List<Character>(); //better matches
                var characters = new List<Character>(); //all

                for (int i = 0; i < 5000; i++)
                {
                    var candidateCharacter = Global.CharacterBuilder.Build(options);
                    if (!candidateCharacter.IsDead)
                    {
                        if (!string.IsNullOrEmpty(finalAssignment))
                        { //looking for a particular assignment
                            if (string.Equals(candidateCharacter.LastCareer?.Assignment, finalAssignment, StringComparison.InvariantCultureIgnoreCase)) //found that assignment
                                preferredCharacters.Add(candidateCharacter);
                        }
                        else if (!string.IsNullOrEmpty(finalCareer))
                        {
                            if (string.Equals(candidateCharacter.LastCareer?.Career, finalCareer, StringComparison.InvariantCultureIgnoreCase))
                                preferredCharacters.Add(candidateCharacter); //found the career
                        }
                    }

                    characters.Add(candidateCharacter);
                }

                if (preferredCharacters.Count > 0)
                {
                    //choose the one with the most skills
                    var sortedList = preferredCharacters.Select(c => new
                    {
                        Character = c,
                        Suitability =
                        c.Skills.Where(s => desiredSkills.Contains(s.Name) || desiredSkills.Contains(s.Specialty)).Count()
                        + (c.IsDead ? -100.00 : 0.00)
                    }).OrderByDescending(x => x.Suitability).ToList();

                    return View(sortedList.First().Character);
                }
                else
                {
                    //No character's last career was the requested one. Choose the one who spend the most time in the desired career.
                    var sortedList = characters.Select(c => new
                    {
                        Character = c,
                        Suitability =
                        (c.CareerHistory.Where(ch => string.Equals(ch.Assignment, finalAssignment, StringComparison.InvariantCultureIgnoreCase)).Sum(ch => ch.Terms * 5.0) / c.CurrentTerm)
                        + (c.CareerHistory.SingleOrDefault(ch => string.Equals(ch.Career, finalCareer, StringComparison.InvariantCultureIgnoreCase))?.Terms * 1.0 ?? 0.00) / c.CurrentTerm
                        + c.Skills.Where(s => desiredSkills.Contains(s.Name) || desiredSkills.Contains(s.Specialty)).Count()
                        + (c.IsDead ? -100.00 : 0.00)
                    }).OrderByDescending(x => x.Suitability).ToList();

                    return View(sortedList.First().Character);
                }
            }
            {
                return View(Global.CharacterBuilder.Build(options));
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            return View(Global.HomeIndexViewModel);
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

        public async Task<ActionResult> TradeInfo(int sectorX, int sectorY, int hexX, int hexY, int maxJumpDistance, bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0, Edition edition = Edition.MGT, int? seed = null, bool advancedCharacters = false, int streetwiseScore = 0, bool raffleGoods = false, string milieu = "M1105")
        {
            var tradeEngine = Global.GetTradeEngine(milieu, edition);

            ManifestCollection model = await tradeEngine.BuildManifestsAsync(sectorX, sectorY, hexX, hexY, maxJumpDistance, advancedMode, illegalGoods, brokerScore, seed, advancedCharacters, streetwiseScore, raffleGoods, milieu);

            return View(model);
        }

        [Route("QuickTradeInfo")]
        public async Task<ActionResult> TradeInfo(string originUwp, string destinationUwp, int maxJumpDistance, bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0, Edition edition = Edition.MGT2, int? seed = null, bool advancedCharacters = false, int streetwiseScore = 0, bool raffleGoods = false, string milieu = "M1105", TasZone originTasZone = TasZone.Green, TasZone destinationTasZone = TasZone.Green)
        {
            var tradeEngine = Global.GetTradeEngine(milieu, edition);

            ManifestCollection model = await tradeEngine.BuildManifestsAsync(originUwp, destinationUwp, maxJumpDistance, advancedMode, illegalGoods, brokerScore, seed, advancedCharacters, streetwiseScore, raffleGoods, milieu, originTasZone, destinationTasZone);

            return View(model);
        }

        [Route("RandomWorld")]
        public async Task<ActionResult> TradeInfo(bool advancedMode = false, bool illegalGoods = false, int brokerScore = 0, Edition edition = Edition.MGT2, int? seed = null, bool advancedCharacters = false, int streetwiseScore = 0, bool raffleGoods = false, string milieu = "M1105")
        {
            var tradeEngine = Global.GetTradeEngine(milieu, edition);

            var world = tradeEngine.GenerateRandomWorld();

            ManifestCollection model = await tradeEngine.BuildManifestsAsync(world.UWP, null, 1, advancedMode, illegalGoods, brokerScore, seed, advancedCharacters, streetwiseScore, raffleGoods, milieu, TasZone.Green, TasZone.Green);

            return View(model);
        }
    }
}