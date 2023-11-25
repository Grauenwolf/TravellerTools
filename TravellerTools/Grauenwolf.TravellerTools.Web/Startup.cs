using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;
using Grauenwolf.TravellerTools.TradeCalculator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;

namespace Grauenwolf.TravellerTools.Web
{
    public class Startup
    {
        string AppDataPath = null!;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            AppDataPath = System.IO.Path.Combine(env.WebRootPath, "App_Data");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var mapService = new TravellerMapServiceLocator(false);

            services.AddSingleton(mapService); //make this configurable!
            var nameGenerator = new NameGenerator(AppDataPath);
            services.AddSingleton(new TradeEngineLocator(mapService, AppDataPath, nameGenerator));
            services.AddSingleton(new EquipmentBuilder(AppDataPath));
            services.AddSingleton(nameGenerator);
            services.AddSingleton(new CharacterBuilder(AppDataPath));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }

    public class TradeEngineLocator
    {
        readonly ConcurrentDictionary<(string, int), TradeEngine> TradeEngines = new ConcurrentDictionary<(string, int), TradeEngine>();

        public TradeEngineLocator(TravellerMapServiceLocator mapService, string dataPath, NameGenerator nameGenerator)
        {
            MapService = mapService;
            DataPath = dataPath;
            NameGenerator = nameGenerator;
        }

        public TradeEngine GetTradeEngine(string milieu, Edition edition)
        {
            if (TradeEngines.TryGetValue((milieu, (int)edition), out var engine))
                return engine;

            switch (edition)
            {
                case Edition.Mongoose:
                    engine = new TradeEngineMgt(MapService.GetMapService(milieu), DataPath, NameGenerator);
                    break;

                case Edition.Mongoose2:
                    engine = new TradeEngineMgt2(MapService.GetMapService(milieu), DataPath, NameGenerator);
                    break;

                case Edition.Mongoose2022:
                    engine = new TradeEngineMgt2022(MapService.GetMapService(milieu), DataPath, NameGenerator);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(edition));
            }

            TradeEngines[(milieu, (int)edition)] = engine;
            return engine;
        }

        public TravellerMapServiceLocator MapService { get; }
        public string DataPath { get; }
        public NameGenerator NameGenerator { get; }
    }
}
