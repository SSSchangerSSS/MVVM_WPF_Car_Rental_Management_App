using Models.Exceptions;
using Models.Models.RentalModels;
using Models.Store;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.Utilites.CustomMessageBox;
using ViewModels.ViewModels.RentalViewModels;
using Models.Models;

namespace ViewModels.Commands.RentalCommands.AddRentalViewModelCommands
{
    /// <summary>
    /// <see cref="AddRentalSubmitCommand"/> class is a command for adding rental 
    /// </summary>
    public class AddRentalSubmitCommand : AsyncCommandBase
    {
        private readonly ManagementStore _managementStore;
        private readonly ICarRentalNavigationService _navigationService;
        private readonly AddRentalViewModel _addRentalViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRentalSubmitCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="addRentalViewModel"><see cref="AddRentalViewModel"/> that has called this command</param>
        public AddRentalSubmitCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, AddRentalViewModel addRentalViewModel)
        {
            _managementStore = managementStore;
            _navigationService = navigationService;
            _addRentalViewModel = addRentalViewModel;
            _addRentalViewModel.ErrorsChanged += OnViewModelErrorChanged;
        }

        /// <inheritdoc />
        public override bool CanExecute(object? parameter)
        {
            return !_addRentalViewModel.HasErrors &&
                   base.CanExecute(parameter);
        }

        /// <inheritdoc />
        /// <exception cref="GModelConflictException{T}"></exception>
        public override async Task ExecuteAsync(object? parameter)
        {
            Rental rental = new Rental(Guid.NewGuid(),
                                    new Guid(_addRentalViewModel.SelectedCar.CarId),
                                    new Guid(_addRentalViewModel.SelectedCustomer.CustomerId),
                                    _addRentalViewModel.StartDate,
                                    _addRentalViewModel.EndDate);
            try
            {
                await _managementStore.AddRental(rental);
                DarkMessageBox.Show("Rental added successfully.", "Info", Brushes.Green);
                _navigationService.Navigate(ViewModelTypes.RentalsView);
            }
            catch (GModelConflictException<Rental> e)
            {
                DarkMessageBox.Show($"{e.Message}", "Error");
            }
        }

        /// <summary>
        /// Defines the method to be called when errors of view model is changed.
        /// </summary>
        private void OnViewModelErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addRentalViewModel.StartDate) ||
               e.PropertyName == nameof(_addRentalViewModel.EndDate)) { onCanExecuteChanged(); }
        }
    }
}
