using Models.Exceptions;
using Models.Models.CarModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.ViewModels.CarViewModels;
using ViewModels.Utilites.CustomMessageBox;
using System.Windows.Media;
using Models.Store;
using Models.Models;

namespace ViewModels.Commands.CarCommands.EditCarViewModelCommands
{
    /// <summary>
    /// <see cref="EditCarSubmitCommand"/> class is a command for editing car 
    /// </summary>
    public class EditCarSubmitCommand : AsyncCommandBase
    {
        private readonly ManagementStore _managementStore;
        private readonly EditCarViewModel _editCarViewModel;
        private readonly ICarRentalNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCarSubmitCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="editCarViewModel"><see cref="EditCarViewModel"/> that has called this command</param>
        public EditCarSubmitCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, EditCarViewModel editCarViewModel)
        {
            _managementStore = managementStore;
            _navigationService = navigationService;
            _editCarViewModel = editCarViewModel;
            _editCarViewModel.ErrorsChanged += OnViewModelErrorChanged;
        }

        ///<inheritdoc />
        public override bool CanExecute(object? parameter)
        {
            return !_editCarViewModel.HasErrors &&
                   base.CanExecute(parameter);
        }

        ///<inheritdoc />
        /// <exception cref="GModelConflictException{T}"></exception>
        public override async Task ExecuteAsync(object? parameter)
        {
            Guid carId = new Guid(_editCarViewModel.CarId);
            Car car = new Car(carId,
                _editCarViewModel.Name,
                _editCarViewModel.Color,
                _editCarViewModel.CreationDate);
            try
            {
                await _managementStore.EditCar(car, carId);
                DarkMessageBox.Show("Car edited successfully.", "Info", Brushes.Green);
                _navigationService.Navigate(ViewModelTypes.CarsView);
            }
            catch (GModelConflictException<Car> e)
            {
                DarkMessageBox.Show($"{e.Message}", "Error");
            }

        }

        /// <summary>
        /// Defines the method to be called when errors of view model is changed.
        /// </summary>
        private void OnViewModelErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_editCarViewModel.Name) ||
                e.PropertyName == nameof(_editCarViewModel.Color)) { onCanExecuteChanged(); }
        }

    }
}
