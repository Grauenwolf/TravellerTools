using Grauenwolf.TravellerTools.Web.Data;
using Grauenwolf.TravellerTools.Web.Shared;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    public class UwpParserPageBase : PageBaseAuto<UwpOptions>
    {
        protected void GotoPlanet() => GotoPlanet("info");

        protected void GotoPlanet(string suffix)
        {
            if (Model.UwpNotSelected)
                return;
            Navigation.NavigateTo($"/uwp/{Model.RawUwp}/{suffix}");
        }
    }
}
