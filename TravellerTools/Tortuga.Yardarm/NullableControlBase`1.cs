using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tortuga.Yardarm;

/// <summary>
/// A NullableControlBase is a ControlBase with a nullable Model property.
/// </summary>
/// <typeparam name="T">The type of the model</typeparam>
/// <seealso cref="Tortuga.Yardarm.ControlBase" />
public abstract class NullableControlBase<T> : ControlBase where T : class
{
    T? m_Model;

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
    /// Override this to listen for when the model has been replaced or removed.
    /// </summary>
    protected virtual void OnModelChanged()
    {
    }

    /// <summary>
    /// Override this to listen for property changed events in the model.
    /// </summary>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
    protected virtual void OnModelPropertyChanged(PropertyChangedEventArgs e) { }

    /// <summary>
    /// Sets the specified parameter.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="regenerateModel">If true, set the Model property to null so that it will be regenerated.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    protected bool Set(object? value, bool regenerateModel, [CallerMemberName] string parameterName = "")
    {
        var result = Set(value, parameterName);
        if (result && regenerateModel)
            Model = null;
        return result;
    }

    private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnModelPropertyChanged(e);
        TryStateHasChanged();
    }
}
