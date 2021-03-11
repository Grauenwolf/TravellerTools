using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Controls
{
    partial class CharacterViewer
    {
        [Parameter] public Character? Character { get; set; }
    }
}
