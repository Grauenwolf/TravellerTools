using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Controls;

partial class ContactListViewer
{
    [Parameter] public List<Contact>? Contacts { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
}
