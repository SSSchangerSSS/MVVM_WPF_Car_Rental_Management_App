using ViewModels.ViewModels;

namespace ViewModels.ViewModelsFactory
{
    /// <summary>
    /// Generic delegate factory for creating viemodels
    /// </summary>
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
}
