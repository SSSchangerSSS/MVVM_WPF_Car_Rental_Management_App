using Models.Exceptions;
using Models.Models.RentalModels;
using System;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.ViewModels.RentalViewModels;
using System.ComponentModel;
using ViewModels.Utilites.CustomMessageBox;
using System.Windows.Media;
using System.Threading.Tasks;
using Models.Store;
using Models.Models;

namespace ViewModels.Commands.RentalCommands.EditRentalViewModelCommands
{
    /// <summary>
    /// <see cref="EditRentalSubmitCommand"/> class is a command for editing rental 
    /// </summary>
    public class EditRentalSubmitCommand : AsyncCommandBase
    {
        private readonly ManagementStore _managementStore;
        private readonly ICarRentalNavigationService _navigationService;
        private readonly EditRentalViewModel _editRentalViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRentalSubmitCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="editRentalViewModel"><see cref="EditRentalViewModel"/> that has called this command</param>
        public EditRentalSubmitCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, EditRentalViewModel editRentalViewModel)
        {
            _managementStore = managementStore;
            _navigationService = navigationService;
            _editRentalViewModel = editRentalViewModel;
            _editRentalViewModel.ErrorsChanged += OnViewModelErrorChanged;
        }

        ///<inheritdoc/>
        public override bool CanExecute(object? parameter)
        {
            return !_editRentalViewModel.HasErrors &&
                   base.CanExecute(parameter);
        }

        ///<inheritdoc/>
        /// <exception cref="GModelConflictException{T}"></exception>
        public override async Task ExecuteAsync(object? parameter)
        {
            Guid rentalId = new Guid(_editRentalViewModel.RentalID);
            Rental rental = new Rental(rentalId,
                                    new Guid(_editRentalViewModel.SelectedCar.CarId),
                                    new Guid(_editRentalViewModel.SelectedCustomer.CustomerId),
                                    _editRentalViewModel.StartDate,
                                    _editRentalViewModel.EndDate);
            try
            {
                await _managementStore.EditRental(rental, rentalId);
                DarkMessageBox.Show("Rental edited successfully.", "Info", Brushes.Green);
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
            if (e.PropertyName == nameof(_editRentalViewModel.StartDate) ||
               e.PropertyName == nameof(_editRentalViewModel.EndDate)) { onCanExecuteChanged(); }
        }
    }
}
