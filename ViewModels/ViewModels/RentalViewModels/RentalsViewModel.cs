using Models.Models.RentalModels;
using Models.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Commands.NavigateCommands;
using ViewModels.Commands.RentalCommands.RentalsViewModelCommands;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.Store;
using ViewModels.Utilites.CustomMessageBox;
using ViewModels.ViewModels.CarViewModels;
using ViewModels.ViewModels.CustomerViewModels;
using Models.Models;

namespace ViewModels.ViewModels.RentalViewModels
{
    /// <summary>
    /// <see cref="RentalsViewModel"/> class is a viewModel (dataContext) for rentals listing view 
    ///</summary>
    public class RentalsViewModel : ViewModelBase
    {
        private ObservableCollection<DisplayRentalViewModel> _displayRentals;
        public IEnumerable<DisplayRentalViewModel> DisplayRentals => _displayRentals;
        private ObservableCollection<RentalViewModel> _rentals;
        public IEnumerable<RentalViewModel> Rentals => _rentals;
        private readonly ObservableCollection<CarViewModel> _cars;
        public IEnumerable<CarViewModel> Cars => _cars;
        private readonly ObservableCollection<CustomerViewModel> _customers;
        public IEnumerable<CustomerViewModel> Customers => _customers;
        private readonly ManagementStore _managementStore;
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand DeleteCommand { get; }
        public string NumberOfRentals => $"Number Of Rentals : {_rentals.Count.ToString()}";

        private DisplayRentalViewModel _selectedRow;
        public DisplayRentalViewModel SelectedRow
        {
            get
            {
                return _selectedRow;
            }
            set
            {
                _selectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }
        private string _searchInput;
        public string SearchInput
        {
            get
            {
                return _searchInput;
            }
            set
            {
                _searchInput = value;
                OnPropertyChanged(nameof(SearchInput));
                SearchRentals(_searchInput);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsViewModel"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="displayRentalViewModelStore">A store to pass the data as <see cref="DisplayRentalViewModel"/> between viewModels</param>
        public RentalsViewModel(ManagementStore managementStore,
            ICarRentalNavigationService navigationService,
            TViewModelStore<DisplayRentalViewModel> displayRentalViewModelStore)
        {
            _managementStore = managementStore;
            AddCommand = new RentalsAddCommand(_managementStore, navigationService, ViewModelTypes.AddRental);
            EditCommand = new RelayCommand(p => { EditButtonRelayCommand(navigationService, displayRentalViewModelStore); }, p => true);
            DeleteCommand = new RelayCommand(async p => { await DeleteButtonRelayCommand(); }, p => true);
            BackCommand = new NavigateCommand(navigationService, ViewModelTypes.MainMenu);
            _customers = new ObservableCollection<CustomerViewModel>();
            _cars = new ObservableCollection<CarViewModel>();
            _rentals = new ObservableCollection<RentalViewModel>();
            _displayRentals = new ObservableCollection<DisplayRentalViewModel>();
        }

        /// <summary>
        /// A static factory method that Initializes a new instance of the <see cref="RentalsViewModel"/> class with loaded data in memory.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="displayRentalViewModelStore">A store to pass the data as <see cref="DisplayRentalViewModel"/> between viewModels</param>
        /// <returns>new instance of the <see cref="RentalsViewModel"/> class with loaded data in memory.</returns>
        public static RentalsViewModel LoadViewModel(ManagementStore managementStore,
            ICarRentalNavigationService navigationService,
            TViewModelStore<DisplayRentalViewModel> displayRentalViewModelStore)
        {
            RentalsViewModel viewModel = new RentalsViewModel(managementStore, navigationService, displayRentalViewModelStore);
            viewModel.UpdateCustomers();
            viewModel.UpdateCars();
            viewModel.UpdateRentals(managementStore.Rentals);
            return viewModel;
        }

        /// <summary>
        /// Stores the data of selected row as <see cref="DisplayRentalViewModel"/> and navigate to edit rental view
        /// </summary>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="displayRentalViewModelStore">A store to pass the data as <see cref="DisplayRentalViewModel"/> between viewModels</param>
        private void EditButtonRelayCommand(
            ICarRentalNavigationService navigationService,
            TViewModelStore<DisplayRentalViewModel> displayRentalViewModelStore)
        {
            navigationService.Navigate(ViewModelTypes.EditRental);
            displayRentalViewModelStore.ViewModelSelect(SelectedRow);
        }

        /// <summary>
        /// Deletes selected row from database and memory
        /// </summary>
        private async Task DeleteButtonRelayCommand()
        {
            if (DarkMessageBox.ShowResult("Are you sure you want to delete this rental??", "Warning") == MessageBoxResult.OK)
            {
                await _managementStore.RemoveRental(new Guid(_selectedRow.RentalId));
                UpdateRentals(_managementStore.Rentals);
            }
        }

        /// <summary>
        /// Update list of cars according to rentals list
        /// </summary>
        private void UpdateDisplayRentals()
        {
            _displayRentals.Clear();

            foreach (var r in _rentals)
            {
                _displayRentals?.Add(new DisplayRentalViewModel()
                {
                    RentalId = r.RentalId,
                    CustomerId = r.CustomerId,
                    CustomerLastName = _customers.Where(c => c.CustomerId == r.CustomerId).Select(c => c.LastName).First(),
                    CarId = r.CarId,
                    CarName = _cars.Where(c => c.CarId == r.CarId).Select(c => c.Name).First(),
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Length = r.Length
                });
            }
        }

        /// <summary>
        /// Update list of customers according to customers in memory
        /// </summary>
        private void UpdateCustomers()
        {
            _customers.Clear();
            foreach (var customer in _managementStore.Customers)
            {
                _customers.Add(new CustomerViewModel(customer));
            }
        }

        /// <summary>
        /// Update list of cars according to cars in memory
        /// </summary>
        private void UpdateCars()
        {
            _cars.Clear();
            foreach (var car in _managementStore.Cars)
            {
                _cars.Add(new CarViewModel(car));
            }
        }

        /// <summary>
        /// Update list of rentals according to given list
        /// </summary>
        /// <param name="rentals">The list that Update will do according to</param>
        private void UpdateRentals(IEnumerable<Rental> rentals)
        {
            _rentals.Clear();
            foreach (var rental in rentals)
            {
                _rentals.Add(new RentalViewModel(rental));
            }
            UpdateDisplayRentals();
            OnPropertyChanged(nameof(NumberOfRentals));
        }

        /// <summary>
        /// Updates list of rentals with rentals that match the search input
        /// </summary>
        /// <param name="searchInput"><see cref="Rental"/>s should contain this in their attributes</param>
        private void SearchRentals(string searchInput)
        {
            UpdateRentals(_managementStore.SearchRentals(searchInput));
        }
    }
}
