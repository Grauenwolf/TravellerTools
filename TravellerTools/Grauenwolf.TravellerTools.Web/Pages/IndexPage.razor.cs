using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class IndexPage
    {
        [Inject] IWebHostEnvironment Environment { get; set; } = null!;
    }
}
