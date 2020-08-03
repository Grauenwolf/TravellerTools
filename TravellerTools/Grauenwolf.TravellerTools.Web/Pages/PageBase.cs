using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Grauenwolf.TravellerTools.Web.Pages
{
    public class PageBase : ComponentBase
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;
        [Inject] protected ILogger<PageBase> Logger { get; set; } = null!;

        [Inject] protected NavigationManager Navigation { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value><c>true</c> if this instance is connected; <c>false</c> if it is pre-rendering.</value>
        protected bool IsConnected { get; set; }

        protected bool LoadFailed { get; private set; }
        protected string LastError { get; private set; }
        protected string PageTitle { get; set; }

        protected virtual void AfterRender(bool firstRender)
        {
        }

        protected virtual Task AfterRenderAsync(bool firstRender) => Task.CompletedTask;

        protected sealed override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            try
            {
                AfterRender(firstRender);
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                LastError = ex.ToString();
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(AfterRender)}");
            }
        }

        async protected sealed override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("setTitle", PageTitle);

            await NavigateToElementAsync();

            await base.OnAfterRenderAsync(firstRender);
            try
            {
                await AfterRenderAsync(firstRender);
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                LastError = ex.ToString();
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(AfterRenderAsync)}");
            }
        }

        protected virtual Task InitializedAsync() => Task.CompletedTask;

        protected async override sealed Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                await JSRuntime.InvokeVoidAsync("isPreRendering");
                IsConnected = true;
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("JavaScript interop calls cannot be issued at this time."))
            {
            }

            try
            {
                await InitializedAsync();
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                LastError = ex.ToString();
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(InitializedAsync)}");
            }
        }

        protected virtual void Initialized()
        {
        }

        protected override sealed void OnInitialized()
        {
            base.OnInitialized();

            try
            {
                Initialized();
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                LastError = ex.ToString();
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(Initialized)}");
            }
        }

        protected virtual void ParametersSet()
        {
        }

        protected override sealed void OnParametersSet()
        {
            base.OnParametersSet();

            try
            {
                ParametersSet();
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                LastError = ex.ToString();
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(ParametersSet)}");
            }
            base.OnParametersSet();
        }

        protected virtual Task ParametersSetAsync() => Task.CompletedTask;

        protected async sealed override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            try
            {
                await ParametersSetAsync();
            }
            catch (Exception ex)
            {
                LoadFailed = true;
                LastError = ex.ToString();
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(ParametersSetAsync)}");
            }
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
}
