using Models.Exceptions;
using Models.Models.CustomerModels;
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

namespace ViewModels.ViewModels.CustomerViewModels
{
    /// <summary>
    /// <see cref="CustomersViewModel"/> class is a viewModel (dataContext) for customers listing view 
    ///</summary>
    public class CustomersViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CustomerViewModel> _customers;
        public IEnumerable<CustomerViewModel> Customers => _customers;
        private readonly ManagementStore _managementStore;
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }
        public string NumberOfCustomers => $"Number Of Customers : {_customers.Count.ToString()}";

        private CustomerViewModel _selectedRow;
        public CustomerViewModel SelectedRow
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
                SearchCustomers(_searchInput);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersViewModel"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="customerViewModelStore">A store to pass the data as <see cref="CustomerViewModel"/> between viewModels</param>
        public CustomersViewModel(ManagementStore managementStore,
            ICarRentalNavigationService navigationService,
            TViewModelStore<CustomerViewModel> customerViewModelStore)
        {
            _managementStore = managementStore;
            BackCommand = new NavigateCommand(navigationService, ViewModelTypes.MainMenu);
            AddCommand = new NavigateCommand(navigationService, ViewModelTypes.AddCustomer);
            EditCommand = new RelayCommand(p => { EditButtonRelayCommand(navigationService, customerViewModelStore); }, p => true);
            DeleteCommand = new RelayCommand(async p => { await DeleteButtonRelayCommand(); }, p => true);
            _customers = new ObservableCollection<CustomerViewModel>();
        }

        /// <summary>
        /// A static factory method that Initializes a new instance of the <see cref="CustomersViewModel"/> class with loaded data in memory.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="customerViewModelStore">A store to pass the data as <see cref="CustomerViewModel"/> between viewModels</param>
        /// <returns>new instance of the <see cref="CustomersViewModel"/> class with loaded data in memory.</returns>
        public static CustomersViewModel LoadViewModel(ManagementStore managementStore,
                      ICarRentalNavigationService navigationService,
                      TViewModelStore<CustomerViewModel> customerViewModelStore)
        {
            CustomersViewModel viewModel = new CustomersViewModel(managementStore, navigationService, customerViewModelStore);
            viewModel.UpdateCustomers(managementStore.Customers);
            return viewModel;
        }

        /// <summary>
        /// Stores the data of selected row as <see cref="CustomerViewModel"/> and navigate to edit customer view
        /// </summary>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="customerViewModelStore">A store to pass the data as <see cref="CustomerViewModel"/> between viewModels</param>
        private void EditButtonRelayCommand(
             ICarRentalNavigationService navigationService,
             TViewModelStore<CustomerViewModel> customerViewModelStore)
        {
            navigationService.Navigate(ViewModelTypes.EditCustomer);
            customerViewModelStore.ViewModelSelect(SelectedRow);
        }

        /// <summary>
        /// Deletes selected row from database and memory
        /// <para><see cref="GModelIsInNotExpiredExistingRentalException{T}"/> : Thrown if the selected <see cref="Customer"/> is in a not expired existing rental.</para>
        /// </summary>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{T}"></exception>
        private async Task DeleteButtonRelayCommand()
        {
            if (DarkMessageBox.ShowResult("Are you sure you want to delete this customer??", "Warning") == MessageBoxResult.OK)
            {
                try
                {
                    await _managementStore.RemoveCustomer(new Guid(_selectedRow.CustomerId));
                }
                catch (GModelIsInNotExpiredExistingRentalException<Customer> e)
                {
                    DarkMessageBox.Show($"{e.Message}", "Error");
                }
                UpdateCustomers(_managementStore.Customers);
            }
        }

        /// <summary>
        /// Update list of customers according to given list
        /// </summary>
        /// <param name="customers">The list that Update will do according to</param>
        private void UpdateCustomers(IEnumerable<Customer> customers)
        {
            _customers.Clear();
            foreach (var customer in customers)
            {
                _customers.Add(new CustomerViewModel(customer));
            }
            OnPropertyChanged(nameof(NumberOfCustomers));
        }

        /// <summary>
        /// Updates list of customers with customers that match the search input
        /// </summary>
        /// <param name="searchInput"><see cref="Customer"/>s should contain this in their attributes</param>
        private void SearchCustomers(string searchInput)
        {
            UpdateCustomers(_managementStore.SearchCustomers(searchInput));
        }
    }
}
