using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace Grauenwolf.TravellerTools.Web.Controls
{
    partial class CharacterViewer
    {
        [Parameter] public Character? Character { get; set; }



        protected void Permalink(MouseEventArgs _)
        {
            string uri = Permalink();
            Navigation.NavigateTo(uri, true);
        }

        protected string Permalink()
        {
            if (Character == null)
                return $"/character";

            var uri = $"/character/view";
            uri = QueryHelpers.AddQueryString(uri, Character.GetCharacterBuilderOptions().ToQueryString());
            return uri;
        }
    }
}
