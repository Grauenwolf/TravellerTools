namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class UwpParserPage
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
