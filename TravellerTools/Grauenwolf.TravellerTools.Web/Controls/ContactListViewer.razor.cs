using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Web.Controls;

partial class ContactListViewer
{
    [Parameter] public List<Contact>? Contacts { get; set; }
    //[Parameter] public ContactOptions ContactOptions { get; set; }

    //protected void Permalink(MouseEventArgs _)
    //{
    //    string uri = Permalink();
    //    Navigation.NavigateTo(uri, true);
    //}

    //protected string Permalink()
    //{
    //    if (Character == null)
    //        return $"/character";

    //    return QueryHelpers.AddQueryString("/character/view", Character.GetCharacterBuilderOptions().ToQueryString());
    //}
}
