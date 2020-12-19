using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tortuga.Anchor.Modeling.Internals;

namespace Grauenwolf.TravellerTools.Web.Shared
{
    public class ControlBase<T> : ControlBase where T : class
    {
        T? m_Model;
        readonly PropertyBag m_Properties;

        public ControlBase()
        {
            m_Properties = new PropertyBag(this);
        }

        void TryStateHasChanged()
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
        /// Gets or sets the model.
        /// </summary>
        /// <remarks>Normally this will be set in the ParametersSet or ParametersSetAsync method.</remarks>
        protected T? Model
        {
            get => m_Model;
            set
            {
                if (m_Model != value)
                {
                    //Remove old event handler
                    var tracker = m_Model as INotifyPropertyChanged;
                    if (tracker != null)
                        tracker.PropertyChanged -= Model_PropertyChanged;

                    m_Model = value;
                    OnModelChanged();
                    TryStateHasChanged();

                    //Add event handler to new value
                    tracker = m_Model as INotifyPropertyChanged;
                    if (tracker != null)
                        tracker.PropertyChanged += Model_PropertyChanged;
                }
            }
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            TryStateHasChanged();
        }

        /// <summary>
        /// This is called after the model has been replaced or removed.
        /// </summary>
        protected virtual void OnModelChanged()
        {
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
