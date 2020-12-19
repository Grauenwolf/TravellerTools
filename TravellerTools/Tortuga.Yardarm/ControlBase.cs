using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tortuga.Anchor.Modeling.Internals;

namespace Tortuga.Yardarm
{
    /// <summary>
    /// The ControlBase provides a framework for Blazor pages and controls. This includes basic error handling and logging.
    /// </summary>
    public abstract class ControlBase : ComponentBase
    {
        readonly PropertyBag m_Properties;

        protected ControlBase()
        {
            m_Properties = new PropertyBag(this);
        }

        /// <summary>
        /// Exposes access the JavaScript runtime.
        /// </summary>
        /// <remarks>This is provided by Blazor infrastructure.</remarks>
        [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;

        /// <summary>
        /// Exposes the application log.
        /// </summary>
        /// <remarks>This is provided by Blazor infrastructure.</remarks>
        [Inject] protected ILogger<ControlBase> Logger { get; set; } = null!;

        /// <summary>
        /// Exposes access to the Blazor navigation framework.
        /// </summary>
        /// <remarks>This is provided by Blazor infrastructure.</remarks>
        [Inject] protected NavigationManager Navigation { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected to a browser.
        /// </summary>
        /// <value><c>true</c> if this instance is connected; <c>false</c> if it is pre-rendering.</value>
        protected bool IsConnected { get; private set; }

        /// <summary>
        /// Gets a value indicating whether load the page failed during a Blazor event.
        /// </summary>
        /// <remarks>This will be true is LastError is set.</remarks>
        protected bool LoadFailed { get => LastError != null; }

        /// <summary>
        /// Gets the last error caught during a Blazor event.
        /// </summary>
        /// <value>The last error.</value>
        protected Exception? LastError { get; private set; }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">The first render.</param>
        /// <remarks>Any errors are automatically caught and logged. </remarks>
        protected virtual void AfterRender(bool firstRender)
        {
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered. Note that the component does
        /// not automatically re-render after the completion of any returned <see cref="T:System.Threading.Tasks.Task" />, because
        /// that would cause an infinite render loop.
        /// </summary>
        /// <param name="firstRender">The first render.</param>
        /// <remarks>Any errors are automatically caught and logged. </remarks>
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
                LastError = ex;
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(AfterRender)}");
            }
        }

        async protected sealed override Task OnAfterRenderAsync(bool firstRender)
        {
            await OnAfterRenderForPageAsync();

            await base.OnAfterRenderAsync(firstRender);
            try
            {
                await AfterRenderAsync(firstRender);
            }
            catch (Exception ex)
            {
                LastError = ex;
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(AfterRenderAsync)}");
            }
        }

        private protected virtual Task OnAfterRenderForPageAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <remarks>Any errors are automatically caught and logged. </remarks>
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
                LastError = ex;
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(InitializedAsync)}");
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        /// <remarks>Any errors are automatically caught and logged. </remarks>
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
                LastError = ex;
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(Initialized)}");
            }
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        /// <remarks>Any errors are automatically caught and logged. </remarks>
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
                LastError = ex;
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(ParametersSet)}");
            }
            base.OnParametersSet();
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        /// <remarks>Any errors are automatically caught and logged. </remarks>
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
                LastError = ex;
                Logger.LogError(ex, $"Internal error, loading failed during {nameof(ParametersSetAsync)}");
            }
        }

        protected void TryStateHasChanged()
        {
            try
            {
                StateHasChanged();
            }
            catch (InvalidOperationException)
            {
                //no-op, can't render yet
            }
        }

        /// <summary>
        /// Gets the specified parameter.
        /// </summary>
        /// <typeparam name="TProperty">The type of parameter to return.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>TProperty.</returns>
        protected TProperty Get<TProperty>([CallerMemberName] string parameterName = "") => m_Properties.Get<TProperty>(parameterName);

        /// <summary>
        /// Sets the specified parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="regenerateModel">If true, set the Model property to null so that it will be regenerated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        protected bool Set(object? value, [CallerMemberName] string parameterName = "")
        {
            var result = m_Properties.Set(value, parameterName);
            if (result)
                TryStateHasChanged();
            return result;
        }
    }
}
