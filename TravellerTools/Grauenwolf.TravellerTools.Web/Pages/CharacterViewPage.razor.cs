using Grauenwolf.TravellerTools.Characters;
using Microsoft.AspNetCore.Components;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    partial class CharacterViewPage
    {
        [Inject] CharacterBuilder CharacterBuilder { get; set; } = null!;
        protected CharacterBuilderOptions Options { get; } = new CharacterBuilderOptions();

        protected override void ParametersSet()
        {
            Options.FromQueryString(Navigation.ParsedQueryString());


            Model = CharacterBuilder.Build(Options);
        }
    }
}
