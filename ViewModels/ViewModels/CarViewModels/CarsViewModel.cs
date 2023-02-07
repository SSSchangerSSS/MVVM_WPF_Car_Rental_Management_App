using Models.Exceptions;
using Models.Models.CarModels;
using Models.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Commands.NavigateCommands;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.Store;
using ViewModels.Utilites.CustomMessageBox;
using Models.Models;

namespace ViewModels.ViewModels.CarViewModels
{
    /// <summary>
    /// <see cref="CarsViewModel"/> class is a viewModel (dataContext) for cars listing view 
    ///</summary>
    public class CarsViewModel : ViewModelBase
    {
        private readonly ManagementStore _managementStore;
        private readonly ObservableCollection<CarViewModel> _cars;
        public IEnumerable<CarViewModel> Cars => _cars;
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }
        public string NumberOfCars => $"Number Of Cars : {_cars.Count.ToString()}";

        private CarViewModel _selectedRow;
        public CarViewModel SelectedRow
        {
            get
            {
                return _selectedRow;
            }
            set
            {
                _selectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }
        private string _searchInput;
        public string SearchInput
        {
            get
            {
                return _searchInput;
            }
            set
            {
                _searchInput = value;
                OnPropertyChanged(nameof(SearchInput));
                SearchCars(_searchInput);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarsViewModel"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="carViewModelStore">A store to pass the data as <see cref="CarViewModel"/> between viewModels</param>
        public CarsViewModel(ManagementStore managementStore,
               ICarRentalNavigationService navigationService,
               TViewModelStore<CarViewModel> carViewModelStore)
        {
            _managementStore = managementStore;
            AddCommand = new NavigateCommand(navigationService, ViewModelTypes.AddCar);
            EditCommand = new RelayCommand(p => { EditButtonRelayCommand(navigationService, carViewModelStore); }, p => true);
            DeleteCommand = new RelayCommand(async p => { await DeleteButtonRelayCommand(); }, p => true);
            BackCommand = new NavigateCommand(navigationService, ViewModelTypes.MainMenu);
            _cars = new ObservableCollection<CarViewModel>();
        }

        /// <summary>
        /// A static factory method that Initializes a new instance of the <see cref="CarsViewModel"/> class with loaded data in memory.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="carViewModelStore">A store to pass the data as <see cref="CarViewModel"/> between viewModels</param>
        /// <returns>new instance of the <see cref="CarsViewModel"/> class with loaded data in memory</returns>
        public static CarsViewModel LoadViewModel(ManagementStore managementStore,
                      ICarRentalNavigationService navigationService,
                      TViewModelStore<CarViewModel> carViewModelStore)
        {
            CarsViewModel viewModel = new CarsViewModel(managementStore, navigationService, carViewModelStore);
            viewModel.UpdateCars(managementStore.Cars);
            return viewModel;
        }

        /// <summary>
        /// Stores the data of selected row as <see cref="CarViewModel"/> and navigate to edit car view
        /// </summary>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="carViewModelStore">A store to pass the data as <see cref="CarViewModel"/> between viewModels</param>
        private void EditButtonRelayCommand(
          ICarRentalNavigationService navigationService,
          TViewModelStore<CarViewModel> carViewModelStore)
        {
            navigationService.Navigate(ViewModelTypes.EditCar);
            carViewModelStore.ViewModelSelect(SelectedRow);
        }

        /// <summary>
        /// Deletes selected row from database and memory
        /// <para><see cref="GModelIsInNotExpiredExistingRentalException{T}"/> : Thrown if the selected <see cref="Car"/> is in a not expired existing rental.</para>
        /// </summary>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{T}"></exception>
        private async Task DeleteButtonRelayCommand()
        {
            if (DarkMessageBox.ShowResult("Are you sure you want to delete this car??", "Warning") == MessageBoxResult.OK)
            {
                try
                {
                    await _managementStore.RemoveCar(new Guid(_selectedRow.CarId));
                }
                catch (GModelIsInNotExpiredExistingRentalException<Car> e)
                {
                    DarkMessageBox.Show($"{e.Message}", "Error");
                }
                UpdateCars(_managementStore.Cars);
            }
        }

        /// <summary>
        /// Update list of cars according to given list
        /// </summary>
        /// <param name="cars">The list that Update will do according to</param>
        private void UpdateCars(IEnumerable<Car> cars)
        {
            _cars.Clear();
            foreach (var car in cars)
            {
                _cars.Add(new CarViewModel(car));
            }
            OnPropertyChanged(nameof(NumberOfCars));
        }

        /// <summary>
        /// Updates list of cars with cars that match the search input
        /// </summary>
        /// <param name="searchInput"><see cref="Car"/>s should contain this in their attributes</param>
        private void SearchCars(string searchInput)
        {
            UpdateCars(_managementStore.SearchCars(searchInput));
        }

    }
}
