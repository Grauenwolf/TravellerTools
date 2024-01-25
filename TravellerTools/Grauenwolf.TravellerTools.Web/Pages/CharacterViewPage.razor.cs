using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages;

partial class CharacterViewPage
{
    protected CharacterBuilderOptions Options { get; } = new CharacterBuilderOptions();
    [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;

    protected override void ParametersSet()
    {
        Options.FromQueryString(Navigation.ParsedQueryString());

        Model = CharacterBuilder.Build(Options);
    }
}
