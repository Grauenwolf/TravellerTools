using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class Test
    {
        [Parameter]
        public string? A
        {
            get; set;
        }

        [Parameter]
        public string? B
        {
            get; set;
        }
    }
}
