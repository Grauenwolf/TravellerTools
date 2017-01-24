using Grauenwolf.TravellerTools.Animals.AE;
using Grauenwolf.TravellerTools.Animals.Mgt;
using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.TradeCalculator;
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
            TradeEngineMgt = new TradeEngineMgt(MapService, appDataPath);
            TradeEngineMgt2 = new TradeEngineMgt2(MapService, appDataPath);

            AnimalBuilderMgt.SetDataPath(appDataPath);
            AnimalBuilderAE.SetDataPath(appDataPath);
            CharacterBuilder = new CharacterBuilder(appDataPath);

        }

        public static TradeEngine TradeEngineMgt;
        public static TradeEngine TradeEngineMgt2;
        public static MapService MapService;
        public static CharacterBuilder CharacterBuilder;
    }
}
