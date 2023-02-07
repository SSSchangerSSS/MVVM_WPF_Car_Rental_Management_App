using Models.Exceptions;
using Models.Models.CustomerModels;
using System;
using System.ComponentModel;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.ViewModels.CustomerViewModels;
using ViewModels.Utilites.CustomMessageBox;
using System.Windows.Media;
using System.Threading.Tasks;
using Models.Store;
using Models.Models;

namespace ViewModels.Commands.CustomerCommands.EditCustomerViewModelCommands
{
    /// <summary>
    /// <see cref="EditCustomerSubmitCommand"/> class is a command for editing customer 
    /// </summary>
    public class EditCustomerSubmitCommand : AsyncCommandBase
    {
        private readonly ManagementStore _managementStoer;
        private readonly EditCustomerViewModel _editCustomerViewModel;
        private readonly ICarRentalNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCustomerSubmitCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="editCustomerViewModel"><see cref="EditCustomerViewModel"/> that has called this command</param>
        public EditCustomerSubmitCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, EditCustomerViewModel editCustomerViewModel)
        {
            _managementStoer = managementStore;
            _navigationService = navigationService;
            _editCustomerViewModel = editCustomerViewModel;
            _editCustomerViewModel.ErrorsChanged += OnViewModelErrorChanged;
        }

        /// <inheritdoc />
        public override bool CanExecute(object? parameter)
        {
            return !_editCustomerViewModel.HasErrors &&
                   base.CanExecute(parameter);
        }

        /// <inheritdoc />
        /// <exception cref="GModelConflictException{T}"></exception>
        public override async Task ExecuteAsync(object? parameter)
        {
            Guid customerId = new Guid(_editCustomerViewModel.CustomerId);
            Customer customer = new Customer(customerId,
                _editCustomerViewModel.FirstName,
                _editCustomerViewModel.LastName,
                _editCustomerViewModel.Age,
                _editCustomerViewModel.Mobile);
            try
            {
              await _managementStoer.EditCustomer(customer, customerId);
                DarkMessageBox.Show("Customer edited successfully.", "Info",Brushes.Green);
                _navigationService.Navigate(ViewModelTypes.CustomersView);
            }
            catch (GModelConflictException<Customer> e)
            {
                DarkMessageBox.Show($"{e.Message}", "Error");
            }

        }

        /// <summary>
        /// Defines the method to be called when errors of view model is changed.
        /// </summary>
        private void OnViewModelErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_editCustomerViewModel.FirstName) ||
                 e.PropertyName == nameof(_editCustomerViewModel.LastName) ||
                 e.PropertyName == nameof(_editCustomerViewModel.Mobile) ||
                 e.PropertyName == nameof(_editCustomerViewModel.Age)) { onCanExecuteChanged(); }
        }
    }
}

