using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Grauenwolf.TravellerTools.Web.Startup))]
namespace Grauenwolf.TravellerTools.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
