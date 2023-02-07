using System;
using ViewModels.Enums;
using ViewModels.ViewModels;
using ViewModels.ViewModels.CarViewModels;
using ViewModels.ViewModels.CustomerViewModels;
using ViewModels.ViewModels.MainMenuViewModels;
using ViewModels.ViewModels.RentalViewModels;

namespace ViewModels.ViewModelsFactory
{
    /// <summary>
    /// <see cref="ViewModelFactory"/> class implements <see cref="IViewModelFactory"/> interface.
    /// </summary>
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<MainMenuViewModel> _createMainMenuViewModel;
        private readonly CreateViewModel<CustomersViewModel> _createCustomersViewModel;
        private readonly CreateViewModel<AddCustomerViewModel> _createAddCustomerViewModel;
        private readonly CreateViewModel<EditCustomerViewModel> _createEditCustomerViewModel;
        private readonly CreateViewModel<CarsViewModel> _createCarsViewModel;
        private readonly CreateViewModel<AddCarViewModel> _createAddCarViewModel;
        private readonly CreateViewModel<EditCarViewModel> _createEditCarViewModel;
        private readonly CreateViewModel<RentalsViewModel> _createRentalsViewModel;
        private readonly CreateViewModel<AddRentalViewModel> _createAddRentalViewModel;
        private readonly CreateViewModel<EditRentalViewModel> _createEditRentalViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelFactory"/> class.
        /// </summary>
        /// <param name="createMainMenuViewModel">Delegate factory to create <see cref="MainMenuViewModel"/>.</param>
        /// <param name="createCustomersViewModel">Delegate factory to create <see cref="CustomersViewModel"/>.</param>
        /// <param name="createAddCustomerViewModel">Delegate factory to create <see cref="AddCustomerViewModel"/>.</param>
        /// <param name="createEditCustomerViewModel">Delegate factory to create <see cref="EditCustomerViewModel"/>.</param>
        /// <param name="createCarsViewModel">Delegate factory to create <see cref="CarsViewModel"/>.</param>
        /// <param name="createAddCarViewModel">Delegate factory to create <see cref="AddCarViewModel"/>.</param>
        /// <param name="createEditCarViewModel">Delegate factory to create <see cref="EditCarViewModel"/>.</param>
        /// <param name="createRentalsViewModel">Delegate factory to create <see cref="RentalsViewModel"/>.</param>
        /// <param name="createAddRentalViewModel">Delegate factory to create <see cref="AddRentalViewModel"/>.</param>
        /// <param name="createEditRentalViewModel">Delegate factory to create <see cref="EditRentalViewModel"/>.</param>
        public ViewModelFactory(CreateViewModel<MainMenuViewModel> createMainMenuViewModel,
            CreateViewModel<CustomersViewModel> createCustomersViewModel,
            CreateViewModel<AddCustomerViewModel> createAddCustomerViewModel,
            CreateViewModel<EditCustomerViewModel> createEditCustomerViewModel,
            CreateViewModel<CarsViewModel> createCarsViewModel,
            CreateViewModel<AddCarViewModel> createAddCarViewModel,
            CreateViewModel<EditCarViewModel> createEditCarViewModel,
            CreateViewModel<RentalsViewModel> createRentalsViewModel,
            CreateViewModel<AddRentalViewModel> createAddRentalViewModel,
            CreateViewModel<EditRentalViewModel> createEditRentalViewModel)
        {
            _createMainMenuViewModel = createMainMenuViewModel;
            _createCustomersViewModel = createCustomersViewModel;
            _createAddCustomerViewModel = createAddCustomerViewModel;
            _createEditCustomerViewModel = createEditCustomerViewModel;
            _createCarsViewModel = createCarsViewModel;
            _createAddCarViewModel = createAddCarViewModel;
            _createEditCarViewModel = createEditCarViewModel;
            _createRentalsViewModel = createRentalsViewModel;
            _createAddRentalViewModel = createAddRentalViewModel;
            _createEditRentalViewModel = createEditRentalViewModel;
        }

        /// <inheritdoc />
        public ViewModelBase CreateViewModel(ViewModelTypes type)
        {
            switch (type)
            {
                case ViewModelTypes.MainMenu:
                    return _createMainMenuViewModel();
                case ViewModelTypes.CustomersView:
                    return _createCustomersViewModel();
                case ViewModelTypes.AddCustomer:
                    return _createAddCustomerViewModel();
                case ViewModelTypes.EditCustomer:
                    return _createEditCustomerViewModel();
                case ViewModelTypes.CarsView:
                    return _createCarsViewModel();
                case ViewModelTypes.AddCar:
                    return _createAddCarViewModel();
                case ViewModelTypes.EditCar:
                    return _createEditCarViewModel();
                case ViewModelTypes.RentalsView:
                    return _createRentalsViewModel();
                case ViewModelTypes.AddRental:
                    return _createAddRentalViewModel();
                case ViewModelTypes.EditRental:
                    return _createEditRentalViewModel();
                default:
                    throw new ArgumentException("The ViewModelType does not have a ViewModel.", "ViewModelTypes");
            }
        }

    }
}
