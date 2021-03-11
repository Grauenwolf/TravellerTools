using Grauenwolf.TravellerTools.Characters;
using Grauenwolf.TravellerTools.Names;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class CharacterViewPage
    {
        [Inject] INameService NameService { get; set; } = null!;
        [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
    }
}
