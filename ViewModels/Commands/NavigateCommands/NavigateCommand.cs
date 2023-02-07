using ViewModels.Enums;
using ViewModels.Services;
using System;

namespace ViewModels.Commands.NavigateCommands
{
    /// <summary>
    /// <see cref="NavigateCommand"/> class is a command for navigating between views 
    /// </summary>
    public class NavigateCommand : CommandBase
    {
        private readonly ICarRentalNavigationService _navigationService;
        private readonly ViewModelTypes _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateCommand"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="type">Type of viewModel that navigation happens according to.</param>
        public NavigateCommand(ICarRentalNavigationService navigationService , ViewModelTypes type)
        {
            _navigationService = navigationService;
            _type = type;
        }

        ///<inheritdoc/>
        /// <exception cref="ArgumentException"></exception>
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate(_type);
        }
    }
}
