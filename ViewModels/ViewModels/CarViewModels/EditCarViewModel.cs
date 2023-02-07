using Models.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ViewModels.Commands.CarCommands.EditCarViewModelCommands;
using ViewModels.Commands.NavigateCommands;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.Store;
using Models.Models;

namespace ViewModels.ViewModels.CarViewModels
{
    /// <summary>
    /// <see cref="EditCarViewModel"/> class is a viewModel (dataContext) for edit car view 
    ///</summary>
    public class EditCarViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly TViewModelStore<CarViewModel> _carViewModelStore;

        private string _carId;
        public string CarId
        {
            get
            {
                return _carId;
            }
            set
            {
                _carId = value;
                OnPropertyChanged(nameof(CarId));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                ClearErrors(nameof(Name));
                if (string.IsNullOrEmpty(Name))
                {
                    AddErrors(nameof(Name), "Name is empty.");
                }
                else if (!Regex.IsMatch(Name, @"^[a-zA-Z0-9]+$"))
                {
                    AddErrors(nameof(Name), "Name must contain only letters and numbers.");
                }
                else if (Name.Length > 50)
                {
                    AddErrors(nameof(Name), "Name value cannot exceed 50 characters.");
                }
            }
        }

        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
                ClearErrors(nameof(Color));
                if (string.IsNullOrEmpty(Color))
                {
                    AddErrors(nameof(Color), "Color is empty.");
                }
                else if (!Regex.IsMatch(Color, @"^[a-zA-Z]+$"))
                {
                    AddErrors(nameof(Color), "Color must contain only letters.");
                }
                else if (Color.Length > 50)
                {
                    AddErrors(nameof(Color), "Color value cannot exceed 50 characters.");
                }
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            set
            {
                _creationDate = value;
                OnPropertyChanged(nameof(CreationDate));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCarViewModel"/> class.
        /// </summary>
        /// <param name="managementStore">A store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.</param>
        /// <param name="navigationService">Navigation service to navigate between views.</param>
        /// <param name="carViewModelStore">A store to pass the data as <see cref="CarViewModel"/> between viewModels</param>
        public EditCarViewModel(ManagementStore managementStore, ICarRentalNavigationService navigationService, TViewModelStore<CarViewModel> carViewModelStore)
        {
            SubmitCommand = new EditCarSubmitCommand(managementStore, navigationService, this);
            CancelCommand = new NavigateCommand(navigationService, ViewModelTypes.CarsView);
            _carViewModelStore = carViewModelStore;
            _carViewModelStore.ViewModelSelected += OnCarSelected;
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Initializes properties of view model with data from selected row in cars listing view
        /// </summary>
        /// <param name="carViewModel">data from selected row in cars listing view.</param>
        private void OnCarSelected(CarViewModel carViewModel)
        {
            CarId = carViewModel.CarId;
            Name = carViewModel.Name;
            Color = carViewModel.Color;
            CreationDate = Convert.ToDateTime(carViewModel.CreationDate);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _carViewModelStore.ViewModelSelected -= OnCarSelected;
            base.Dispose();
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
