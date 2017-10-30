using Grauenwolf.TravellerTools.Animals.AE;
using Grauenwolf.TravellerTools.Animals.Mgt;
using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Models;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Grauenwolf.TravellerTools.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string appDataPath = Server.MapPath("~/app_data");
            var filterUnpopulatedSectors = bool.Parse(WebConfigurationManager.AppSettings["FilterUnpopulatedSectors"]);

            MapService = new TravellerMapService(filterUnpopulatedSectors);
            NameService = new LocalNameService(appDataPath);
            TradeEngineMgt = new TradeEngineMgt(MapService, appDataPath, NameService);
            TradeEngineMgt2 = new TradeEngineMgt2(MapService, appDataPath, NameService);

            AnimalBuilderMgt.SetDataPath(appDataPath);
            AnimalBuilderAE.SetDataPath(appDataPath);
            CharacterBuilder = new CharacterBuilder(appDataPath);
            EquipmentBuilder = new EquipmentBuilder(appDataPath);

            HomeIndexViewModel = HomeIndexViewModel.GetHomeIndexViewModel(MapService, CharacterBuilder, EquipmentBuilder).Result;
        }

        public static TradeEngine TradeEngineMgt;
        public static INameService NameService;
        public static TradeEngine TradeEngineMgt2;
        public static TravellerMapService MapService;
        public static CharacterBuilder CharacterBuilder;
        public static EquipmentBuilder EquipmentBuilder;
        public static HomeIndexViewModel HomeIndexViewModel;
    }
}
