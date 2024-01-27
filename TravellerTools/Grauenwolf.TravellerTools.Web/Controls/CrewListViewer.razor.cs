using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Controls;

partial class CrewListViewer
{
    [Parameter] public List<CrewMember>? Crew { get; set; }
    [Parameter] public bool ShowDetails { get; set; }
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
}
