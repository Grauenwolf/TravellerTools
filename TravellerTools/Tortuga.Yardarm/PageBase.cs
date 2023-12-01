using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Tortuga.Yardarm;

/// <summary>
/// A PageBase is a ControlBase that adds support for a Title and for scrolling to a URL fragment identifier (i.e. a hashtag for a heading in the URL).
/// Implements the <see cref="Tortuga.Yardarm.ControlBase" />
/// </summary>
/// <seealso cref="Tortuga.Yardarm.ControlBase" />
public abstract class PageBase : ControlBase
{
    protected string? PageTitle { get; set; }

    private protected sealed override async Task OnAfterRenderForPageAsync()
    {
        await JSRuntime.InvokeVoidAsync("setTitle", PageTitle!);
        await NavigateToElementAsync();
    }

    async Task NavigateToElementAsync()
    {
        if (!IsConnected)
            return;

        //ref: https://github.com/aspnet/AspNetCore/issues/8393

        var fragment = new Uri(Navigation.Uri).Fragment;

        if (string.IsNullOrEmpty(fragment))
            return;

        var elementId = fragment.StartsWith("#") ? fragment.Substring(1) : fragment;

        if (string.IsNullOrEmpty(elementId))
            return;

        await JSRuntime.InvokeAsync<bool>("scrollToElementId", elementId);
    }
}
