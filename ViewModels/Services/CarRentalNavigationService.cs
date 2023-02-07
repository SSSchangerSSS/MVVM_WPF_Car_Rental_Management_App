using ViewModels.Enums;
using ViewModels.Store;
using ViewModels.ViewModelsFactory;

namespace ViewModels.Services
{
    /// <summary>
    /// <see cref="CarRentalNavigationService"/> class implements <see cref="ICarRentalNavigationService"/> interface.
    /// </summary>
    public class CarRentalNavigationService:ICarRentalNavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly IViewModelFactory _viewModelsFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarRentalNavigationService"/> class.
        /// </summary>
        /// <param name="navigationStore">A store that stores the value of current view model</param>
        /// <param name="viewModelsFactory">A factory for creating viewModels  </param>
        public CarRentalNavigationService(NavigationStore navigationStore, IViewModelFactory viewModelsFactory)
        {
            _navigationStore = navigationStore;
            _viewModelsFactory = viewModelsFactory;
        }

        /// <inheritdoc />
        public void Navigate(ViewModelTypes type) {
            _navigationStore.CurrentViewModel = _viewModelsFactory.CreateViewModel(type);
        }
    }
}
 