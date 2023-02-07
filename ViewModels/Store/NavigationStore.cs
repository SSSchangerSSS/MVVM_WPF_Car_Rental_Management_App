using System;
using ViewModels.ViewModels;

namespace ViewModels.Store
{
    /// <summary>
    /// <see cref="NavigationStore"/> is a class that stores the value of current view model
    ///</summary>
    public class NavigationStore 
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;

    }

}
