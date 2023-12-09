using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class CharacterViewPage
{
    protected CharacterBuilderOptions Options { get; } = new CharacterBuilderOptions();
    [Inject] CharacterBuilderLocator CharacterBuilderLocator { get; set; } = null!;

    protected override void ParametersSet()
    {
        Options.FromQueryString(Navigation.ParsedQueryString());

        Model = CharacterBuilderLocator.Build(Options);
    }
}
