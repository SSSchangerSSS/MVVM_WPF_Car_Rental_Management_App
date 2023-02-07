using Models.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands.NavigateCommands;
using ViewModels.Commands.RentalCommands.AddRentalViewModelCommands;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.ViewModels.CarViewModels;
using ViewModels.ViewModels.CustomerViewModels;
using Models.Models;

namespace ViewModels.ViewModels.RentalViewModels
{
    /// <summary>
    /// <see cref="AddRentalViewModel"/> class is a viewModel (dataContext) for add rental view 
    ///</summary>
    public class AddRentalViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ManagementStore _managementStore;
        private readonly ObservableCollection<CarViewModel> _cars;
        public IEnumerable<CarViewModel> Cars => _cars;

        private CarViewModel _selectedCar;
        public CarViewModel SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
                OnPropertyChanged(nameof(CarName));
                OnPropertyChanged(nameof(CarColor));
            }
        }

        private readonly ObservableCollection<CustomerViewModel> _customers;
        public IEnumerable<CustomerViewModel> Customers => _customers;

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
                OnPropertyChanged(nameof(CustomerFirstName));
                OnPropertyChanged(nameof(CustomerLastName));
            }
        }
        public string CustomerFirstName => SelectedCustomer.FirstName;
        public string CustomerLastName => SelectedCustomer.LastName;
        public string CarName => SelectedCar.Name;

        public string CarColor => SelectedCar.Color;

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(Length));
                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));
                if (StartDate >= EndDate)
                {
                    AddErrors(nameof(StartDate), "Start Date must be before End Date");
                }
            }
        }

        private DateTime _endDate = DateTime.Now.AddDays(1);
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(Length));
                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));
                if (StartDate >= EndDate)
                {
                    AddErrors(nameof(EndDate), "End Date must be after Start Date");
                }
            }
        } 

        public TimeSpan Length => (EndDate - StartDate);
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRentalViewModel"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        public AddRentalViewModel(ManagementStore managementStore, ICarRentalNavigationService navigationService)
        {
            _managementStore = managementStore;
            CancelCommand = new NavigateCommand(navigationService, ViewModelTypes.RentalsView);
            SubmitCommand = new AddRentalSubmitCommand(_managementStore, navigationService, this);
            _customers = new ObservableCollection<CustomerViewModel>();
            _cars = new ObservableCollection<CarViewModel>();
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// A static factory method that Initializes a new instance of the <see cref="AddRentalViewModel"/> class with loaded data in memory.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <returns>new instance of the <see cref="AddRentalViewModel"/> class with loaded data in memory.</returns>
        public static AddRentalViewModel LoadViewModel(ManagementStore managementStore, ICarRentalNavigationService navigationService)
        {
            AddRentalViewModel viewModel = new AddRentalViewModel(managementStore,navigationService);
            viewModel.UpdateCustomers();
            viewModel.UpdateCars();
            viewModel.SelectedCustomer = viewModel._customers[0];
            viewModel.SelectedCar = viewModel._cars[0];
            return viewModel;
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
        /// Gets all errors for a property
        /// </summary>
        /// <param name="propertyName">Name of property that its errors will be returned</param>
        /// <returns>All errors of the given property name</returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        /// <summary>
        /// Will be called when errors of a property changes
        /// </summary>
        /// <param name="propertyName">Name of property that its errors changed</param>
        private void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Clears all errors of a property
        /// </summary>
        /// <param name="propertyName">Name of property that its errors will be cleared</param>
        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorChanged(propertyName);
        }

        /// <summary>
        /// Add error for a property
        /// </summary>
        /// <param name="propertyName">Name of property that error will be added to its list</param>
        /// <param name="error">The Error that will be added to property's list</param>
        private void AddErrors(string propertyName, string error)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName].Add(error);
            OnErrorChanged(propertyName);
        }
    }
}
