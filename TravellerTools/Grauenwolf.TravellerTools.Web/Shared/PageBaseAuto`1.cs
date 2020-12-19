using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tortuga.Anchor.Modeling.Internals;

namespace Grauenwolf.TravellerTools.Web.Shared
{
    /// <summary>
    /// This subclass of PageBase automatically creates its own model. The model cannot be replaced.
    /// </summary>
    /// <typeparam name="T">The type of model.</typeparam>
    /// <remarks>If Model is a INotifyPropertyChanged, then StateHasChanged will be automatically called when a PropertyChanged event is fired.</remarks>
    public class PageBaseAuto<T> : PageBase where T : class, new()
    {
        T m_Model;
        readonly PropertyBag m_Properties;

        public PageBaseAuto()
        {
            m_Model = new T();
            m_Properties = new PropertyBag(this);
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <remarks>Normally this will be set in the ParametersSet or ParametersSetAsync method.</remarks>
        protected T Model
        {
            get => m_Model;
            private set
            {
                m_Model = value;

                //Add event handler to new value
                var tracker = m_Model as INotifyPropertyChanged;
                if (tracker != null)
                    tracker.PropertyChanged += Model_PropertyChanged;
            }
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        /// <summary>
        /// Sets the specified parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="regenerateModel">If true, set the Model property to null so that it will be regenerated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        protected void Set(object? value, bool regenerateModel, [CallerMemberName] string parameterName = "")
        {
            if (m_Properties.Set(value!, parameterName) && regenerateModel)
            {
                StateHasChanged();
                if (regenerateModel)
                    Model = null;
            }
        }

        /// <summary>
        /// Gets the specified parameter.
        /// </summary>
        /// <typeparam name="TProperty">The type of parameter to return.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>TProperty.</returns>
        protected TProperty Get<TProperty>([CallerMemberName] string parameterName = "") => m_Properties.Get<TProperty>(parameterName);
    }
}
