using Models.Exceptions;
using Models.Models.CustomerModels;
using Models.Store;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.Utilites.CustomMessageBox;
using ViewModels.ViewModels.CustomerViewModels;
using Models.Models;

namespace ViewModels.Commands.CustomerCommands.AddCustomerViewModelCommands
{
    /// <summary>
    /// <see cref="AddCustomerSubmitCommand"/> class is a command for adding customer
    /// </summary>
    public class AddCustomerSubmitCommand : AsyncCommandBase
    {
        private readonly ManagementStore _managementStore;
        private readonly AddCustomerViewModel _addCustomerViewModel;
        private readonly ICarRentalNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCustomerSubmitCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="addCustomerViewModel"><see cref="AddCustomerViewModel"/> that has called this command</param>
        public AddCustomerSubmitCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, AddCustomerViewModel addCustomerViewModel)
        {
            _managementStore = managementStore;
            _navigationService = navigationService;
            _addCustomerViewModel = addCustomerViewModel;
            _addCustomerViewModel.PropertyChanged += OnViewModelChanged;
            _addCustomerViewModel.ErrorsChanged += OnViewModelErrorChanged;
        }

        /// <inheritdoc />
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addCustomerViewModel.FirstName) &&
                   !string.IsNullOrEmpty(_addCustomerViewModel.LastName) &&
                   _addCustomerViewModel.Age != 0 &&
                   !string.IsNullOrEmpty(_addCustomerViewModel.Mobile) &&
                   !_addCustomerViewModel.HasErrors &&
                   base.CanExecute(parameter);
        }

        /// <inheritdoc />
        /// <exception cref="GModelConflictException{T}"></exception>
        public override async Task ExecuteAsync(object? parameter)
        {
            Customer customer = new Customer(Guid.NewGuid(),
                _addCustomerViewModel.FirstName,
                _addCustomerViewModel.LastName,
                _addCustomerViewModel.Age,
                _addCustomerViewModel.Mobile);
            try
            {
                await _managementStore.AddCustomer(customer);
                DarkMessageBox.Show("Customer added successfully.", "Info", Brushes.Green);
                _navigationService.Navigate(ViewModelTypes.CustomersView);
            }
            catch (GModelConflictException<Customer> e)
            {
                DarkMessageBox.Show($"{e.Message}", "Error");
            }

        }

        /// <summary>
        /// Defines the method to be called when properties of view model is changed.
        /// </summary>
        private void OnViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addCustomerViewModel.FirstName) ||
                e.PropertyName == nameof(_addCustomerViewModel.LastName) ||
                e.PropertyName == nameof(_addCustomerViewModel.Mobile) ||
                e.PropertyName == nameof(_addCustomerViewModel.Age)) { onCanExecuteChanged(); }
        }

        /// <summary>
        /// Defines the method to be called when errors of view model is changed.
        /// </summary>
        private void OnViewModelErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addCustomerViewModel.FirstName) ||
                e.PropertyName == nameof(_addCustomerViewModel.LastName) ||
                e.PropertyName == nameof(_addCustomerViewModel.Mobile) ||
                e.PropertyName == nameof(_addCustomerViewModel.Age)) { onCanExecuteChanged(); }
        }
    }
}
