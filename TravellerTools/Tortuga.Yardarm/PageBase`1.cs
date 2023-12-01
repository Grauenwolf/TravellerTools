using System.ComponentModel;

namespace Tortuga.Yardarm;

/// <summary>
/// A NullablePageBase is a PageBase with a nullable Model property.
/// </summary>
/// <typeparam name="T">The type of the model</typeparam>
public abstract class PageBase<T> : PageBase where T : class, new()
{
    T m_Model = null!;

    public PageBase()
    {
        Model = new T();
    }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <remarks>Normally this will be set in the ParametersSet or ParametersSetAsync method.</remarks>
    /// <remarks>If Model is a INotifyPropertyChanged, then StateHasChanged will be automatically called when a PropertyChanged event is fired.</remarks>
    protected T Model
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

    private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnModelPropertyChanged(e);
        TryStateHasChanged();
    }
}
