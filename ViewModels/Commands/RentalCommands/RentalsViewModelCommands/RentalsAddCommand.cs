using Models.Store;
using ViewModels.Enums;
using ViewModels.Services;
using Models.Models;
using System;

namespace ViewModels.Commands.RentalCommands.RentalsViewModelCommands
{
    /// <summary>
    /// <see cref="RentalsAddCommand"/> class is a command for navigating to adding rental view 
    /// </summary>
    public class RentalsAddCommand : CommandBase
    {
        private readonly ManagementStore _managementStore;
        private readonly ICarRentalNavigationService _navigationService;
        private readonly ViewModelTypes _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsAddCommand"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="type">Type of viewModel that navigation happens according to.</param>
        public RentalsAddCommand(ManagementStore managementStore, ICarRentalNavigationService navigationService, ViewModelTypes type)
        {
            _managementStore = managementStore;
            _navigationService = navigationService;
            _type = type;
        }
        ///<inheritdoc/>
        public override bool CanExecute(object? parameter)
        {
            return _managementStore.CanAddRental() &&
                   base.CanExecute(parameter);
        }

        ///<inheritdoc/>
        /// <exception cref="ArgumentException"></exception>
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate(_type);
        }
    }
}
