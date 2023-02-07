using System;
using System.Runtime.Intrinsics.X86;
using ViewModels.ViewModels;

namespace ViewModels.Store
{
    /// <summary>
    /// <see cref="TViewModelStore{T}"/> is a generic class with ability to store the data as <typeparamref name="T"/>
    ///</summary>
    public class TViewModelStore<T> where T : ViewModelBase
    {
        public event Action<T>? ViewModelSelected;

        /// <summary>
        /// stores the incoming <typeparamref name="T"/> view model 
        ///</summary>
        ///<param name="ViewModel">the view model that is to be stored</param>
        public void ViewModelSelect(T ViewModel)
        {
            ViewModelSelected?.Invoke(ViewModel);
        }
    }
}
