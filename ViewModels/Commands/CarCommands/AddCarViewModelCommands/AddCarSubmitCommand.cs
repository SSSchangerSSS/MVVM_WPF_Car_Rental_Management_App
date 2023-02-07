using Models.Exceptions;
using Models.Models.CarModels;
using System;
using System.ComponentModel;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.ViewModels.CarViewModels;
using ViewModels.Utilites.CustomMessageBox;
using System.Windows.Media;
using System.Threading.Tasks;
using Models.Store;
using Models.Models;

namespace ViewModels.Commands.CarCommands.AddCarViewModelCommands
{
    /// <summary>
    /// <see cref="AddCarSubmitCommand"/> class is a command for adding car 
    /// </summary>
    public class AddCarSubmitCommand : AsyncCommandBase
    {
        private readonly ManagementStore _managementStore;
        private readonly AddCarViewModel _addCarViewModel;
        private readonly ICarRentalNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCarSubmitCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="addCarViewModel"><see cref="AddCarViewModel"/> that has called this command</param>
        public AddCarSubmitCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, AddCarViewModel addCarViewModel)
        {
            _managementStore = managementStore;
            _navigationService = navigationService;
            _addCarViewModel = addCarViewModel;
            _addCarViewModel.PropertyChanged += OnViewModelChanged;
            _addCarViewModel.ErrorsChanged += OnViewModelErrorChanged;
        }

        ///<inheritdoc/>
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addCarViewModel.Name) &&
                   !string.IsNullOrEmpty(_addCarViewModel.Color) &&
                   !_addCarViewModel.HasErrors &&
                   base.CanExecute(parameter);
        }

        ///<inheritdoc/>
        /// <exception cref="GModelConflictException{T}"></exception>
        public override async Task ExecuteAsync(object? parameter)
        {
            Car car = new Car(Guid.NewGuid(),
                _addCarViewModel.Name,
                _addCarViewModel.Color,
                _addCarViewModel.CreationDate);
            try
            {
                await _managementStore.AddCar(car);
                DarkMessageBox.Show("Car added successfully.", "Info", Brushes.Green);
                _navigationService.Navigate(ViewModelTypes.CarsView);
            }
            catch (GModelConflictException<Car> e)
            {
                DarkMessageBox.Show($"{e.Message}", "Error");
            }

        }

        /// <summary>
        /// Defines the method to be called when properties of view model is changed.
        /// </summary>
        private void OnViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addCarViewModel.Name) ||
                e.PropertyName == nameof(_addCarViewModel.Color)) { onCanExecuteChanged(); }
        }

        /// <summary>
        /// Defines the method to be called when errors of view model is changed.
        /// </summary>
        private void OnViewModelErrorChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addCarViewModel.Name) ||
                e.PropertyName == nameof(_addCarViewModel.Color)) { onCanExecuteChanged(); }
        }
    }
}
