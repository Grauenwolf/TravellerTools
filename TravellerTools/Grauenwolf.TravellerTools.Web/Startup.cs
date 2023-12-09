using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Equipment;
using Grauenwolf.TravellerTools.Maps;
using Grauenwolf.TravellerTools.Names;

namespace Grauenwolf.TravellerTools.Web;

public class Startup
{
    string AppDataPath = null!;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        AppDataPath = System.IO.Path.Combine(env.WebRootPath, "App_Data");
    }

    public IConfiguration Configuration { get; }

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

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();

        var mapService = new TravellerMapServiceLocator(false);

        services.AddSingleton(mapService); //make this configurable!
        var nameGenerator = new NameGenerator(AppDataPath);
        var characterBuilderLocator = new CharacterBuilderLocator(AppDataPath, nameGenerator);
        services.AddSingleton(new TradeEngineLocator(mapService, AppDataPath, nameGenerator, characterBuilderLocator));
        services.AddSingleton(new EquipmentBuilder(AppDataPath));
        services.AddSingleton(nameGenerator);
        services.AddSingleton(characterBuilderLocator);
    }
}
