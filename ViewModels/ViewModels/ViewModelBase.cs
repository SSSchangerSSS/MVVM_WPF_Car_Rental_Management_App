using System.ComponentModel;

namespace ViewModels.ViewModels
{
    /// <summary>
    /// <see cref="ViewModelBase"/> is a class that has the ability to notify when a property value changes
    ///</summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Will be called when value of a property changes
        /// </summary>
        /// <param name="propertyName">Name of property that its value changed</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Will be called before destructor
        /// </summary>
        public virtual void Dispose() { }
    }
}
