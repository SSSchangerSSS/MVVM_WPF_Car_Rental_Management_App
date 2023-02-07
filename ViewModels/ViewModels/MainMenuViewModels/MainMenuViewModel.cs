using System.Windows.Input;
using ViewModels.Commands.MainMenuCommands;
using ViewModels.Commands.NavigateCommands;
using ViewModels.Enums;
using ViewModels.Services;

namespace ViewModels.ViewModels.MainMenuViewModels
{
    /// <summary>
    /// <see cref="MainMenuViewModel"/> class is a viewModel (dataContext) for main menu view 
    ///</summary>
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand GoToCustomersCommand { get; }
        public ICommand GoToCarsCommand { get; }
        public ICommand GoToRentalsCommand { get; }
        public ICommand ExitCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        public MainMenuViewModel(ICarRentalNavigationService navigationService)
        {
            GoToCustomersCommand = new NavigateCommand(navigationService,ViewModelTypes.CustomersView);
            GoToCarsCommand = new NavigateCommand(navigationService,ViewModelTypes.CarsView);
            GoToRentalsCommand = new NavigateCommand(navigationService,ViewModelTypes.RentalsView);
            ExitCommand = new MainMenuExitCommand();
        }
    }
}
