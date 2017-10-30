using Grauenwolf.TravellerTools.Animals.AE;
using Grauenwolf.TravellerTools.Animals.Mgt;
using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.TradeCalculator;
using Grauenwolf.TravellerTools.Web.Models;
using System;
using System.Collections.Concurrent;
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

            s_AppDataPath = Server.MapPath("~/app_data");
            s_FilterUnpopulatedSectors = bool.Parse(WebConfigurationManager.AppSettings["FilterUnpopulatedSectors"]);

            //MapService = new TravellerMapService(filterUnpopulatedSectors);
            NameService = new LocalNameService(s_AppDataPath);
            //TradeEngineMgt = new TradeEngineMgt(MapService, appDataPath, NameService);
            //TradeEngineMgt2 = new TradeEngineMgt2(MapService, appDataPath, NameService);

            AnimalBuilderMgt.SetDataPath(s_AppDataPath);
            AnimalBuilderAE.SetDataPath(s_AppDataPath);
            CharacterBuilder = new CharacterBuilder(s_AppDataPath);
            EquipmentBuilder = new EquipmentBuilder(s_AppDataPath);

            HomeIndexViewModel = HomeIndexViewModel.GetHomeIndexViewModel(GetMapService("M1105"), CharacterBuilder, EquipmentBuilder).Result;
        }

        //public static TradeEngine TradeEngineMgt;
        public static INameService NameService;
        //public static TradeEngine TradeEngineMgt2;
        //public static TravellerMapService MapService;
        public static CharacterBuilder CharacterBuilder;
        public static EquipmentBuilder EquipmentBuilder;
        public static HomeIndexViewModel HomeIndexViewModel;
        readonly static ConcurrentDictionary<string, TravellerMapService> s_MapServices = new ConcurrentDictionary<string, TravellerMapService>();
        static string s_AppDataPath;
        static bool s_FilterUnpopulatedSectors;


        public static TravellerMapService GetMapService(string milieu)
        {
            return s_MapServices.GetOrAdd(milieu, (x) => new TravellerMapService(s_FilterUnpopulatedSectors, x));
        }

        public static TradeEngine GetTradeEngine(string milieu, Edition edition)
        {
            var mapService = s_MapServices.GetOrAdd(milieu, (x) => new TravellerMapService(s_FilterUnpopulatedSectors, x));

            switch (edition)
            {
                case Edition.Mongoose:
                    return new TradeEngineMgt(mapService, s_AppDataPath, NameService);
                case Edition.Mongoose2:
                    return new TradeEngineMgt2(mapService, s_AppDataPath, NameService);
            }
            throw new NotSupportedException();
        }
    }
}
