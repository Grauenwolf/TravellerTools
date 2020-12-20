namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class UwpParserPage
    {
        protected void GotoPlanet()
        {
            if (Model.UwpNotSelected)
                return;
            Navigation.NavigateTo($"/uwp/{Model.RawUwp}/{"info"}");
        }

        protected void GotoPlanet2()
        {
            Navigation.NavigateTo($"/uwp/{Model.CalculatedUwp}/{"info"}");
        }
    }
}
