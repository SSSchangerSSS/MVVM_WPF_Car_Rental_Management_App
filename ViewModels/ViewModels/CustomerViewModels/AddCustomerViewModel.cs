using Models.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ViewModels.Commands.CustomerCommands.AddCustomerViewModelCommands;
using ViewModels.Commands.NavigateCommands;
using ViewModels.Enums;
using ViewModels.Services;
using Models.Models;

namespace ViewModels.ViewModels.CustomerViewModels
{
    /// <summary>
    /// <see cref="AddCustomerViewModel"/> class is a viewModel (dataContext) for add customer view 
    /// </summary>
    public class AddCustomerViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                ClearErrors(nameof(FirstName));
                if (string.IsNullOrEmpty(FirstName))
                {
                    AddErrors(nameof(FirstName), "First name is empty.");
                }
                else if (!Regex.IsMatch(FirstName, @"^[a-zA-Z]+$"))
                {
                    AddErrors(nameof(FirstName), "First name must contain only letters.");
                }
                else if (FirstName.Length > 50)
                {
                    AddErrors(nameof(FirstName), "FirstName value cannot exceed 50 characters.");
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                ClearErrors(nameof(LastName));
                if (string.IsNullOrEmpty(LastName))
                {
                    AddErrors(nameof(LastName), "Last name is empty.");
                }
                else if (!Regex.IsMatch(LastName, @"^[a-zA-Z]+$"))
                {
                    AddErrors(nameof(LastName), "Last name must contain only letters.");
                }
                else if (LastName.Length > 50)
                {
                    AddErrors(nameof(LastName), "LastName value cannot exceed 50 characters.");
                }
            }
        }

        private byte _age;
        public byte Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
                ClearErrors(nameof(Age));
                if (!(Age <= 80 && Age >= 18))
                {
                    AddErrors(nameof(Age), "Age should be in range 18 to 8 0.");
                }
            }
        }

        private string _mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                OnPropertyChanged(nameof(Mobile));
                ClearErrors(nameof(Mobile));
                if (string.IsNullOrEmpty(Mobile))
                {
                    AddErrors(nameof(Mobile), "Mobile is empty.");
                }
                else if (!Regex.IsMatch(Mobile, @"^[0-9]+$"))
                {
                    AddErrors(nameof(Mobile), "Mobile must contain only digits.");
                }
                else if (Mobile.Length != 11)
                {
                    AddErrors(nameof(Mobile), "Mobile should be exact 11 digits.");
                }
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCustomerViewModel"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        public AddCustomerViewModel(ManagementStore managementStore, ICarRentalNavigationService navigationService)
        {
            CancelCommand = new NavigateCommand(navigationService, ViewModelTypes.CustomersView);
            SubmitCommand = new AddCustomerSubmitCommand(managementStore, navigationService, this);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Gets all errors for a property
        /// </summary>
        /// <param name="propertyName">Name of property that its errors will be returned</param>
        /// <returns>All errors of the given property name</returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        /// <summary>
        /// Will be called when errors of a property changes
        /// </summary>
        /// <param name="propertyName">Name of property that its errors changed</param>
        private void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Clears all errors of a property
        /// </summary>
        /// <param name="propertyName">Name of property that its errors will be cleared</param>
        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorChanged(propertyName);
        }

        /// <summary>
        /// Add error for a property
        /// </summary>
        /// <param name="propertyName">Name of property that error will be added to its list</param>
        /// <param name="error">The Error that will be added to property's list</param>
        private void AddErrors(string propertyName, string error)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }
            _propertyNameToErrorsDictionary[propertyName].Add(error);
            OnErrorChanged(propertyName);
        }
    }
}
