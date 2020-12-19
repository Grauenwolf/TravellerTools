using System;
using System.ComponentModel;
using Tortuga.Anchor.Modeling.Internals;

namespace Tortuga.Yardarm
{
    /// <summary>
    /// A NullableControlBase is a ControlBase with a non-nullable Model property.
    /// </summary>
    /// <typeparam name="T">The type of the model</typeparam>
    /// <seealso cref="Tortuga.Yardarm.ControlBase" />
    public abstract class ControlBase<T> : ControlBase where T : class, new()
    {
        T m_Model = null!;

        public ControlBase()
        {
            Model = new T();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <remarks>This will be automatically be created.</remarks>
        protected T Model
        {
            get => m_Model;
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (m_Model != value)
                {
                    //Remove old event handler
                    if (m_Model is INotifyPropertyChanged oldModel)
                        oldModel.PropertyChanged -= Model_PropertyChanged;

                    m_Model = value;
                    OnModelChanged();
                    TryStateHasChanged();

                    //Add event handler to new value
                    if (m_Model is INotifyPropertyChanged newModel)
                        newModel.PropertyChanged += Model_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// Override this to listen for property changed events in the model.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnModelPropertyChanged(PropertyChangedEventArgs e) { }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnModelPropertyChanged(e);
            TryStateHasChanged();
        }

        /// <summary>
        /// Override this to listen for when the model has been replaced or removed.
        /// </summary>
        protected virtual void OnModelChanged()
        {
        }
    }
}
