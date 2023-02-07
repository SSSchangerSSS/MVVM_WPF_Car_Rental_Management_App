using Models.Models;
using Models.Models.CarModels;
using Models.Models.CustomerModels;
using Models.Models.RentalModels;
using Models.DTOs;
using Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Store
{
    /// <summary>
    /// <see cref="ManagementStore"/> class is a store for <see cref="IManagement"/> so that we hit the database once for loading data to memory.
    /// </summary>
    public class ManagementStore
    {
        private readonly List<Car> _cars;
        public IEnumerable<Car> Cars => _cars;

        private readonly List<Customer> _customers;
        public IEnumerable<Customer> Customers => _customers;

        private readonly List<Rental> _rentals;
        public IEnumerable<Rental> Rentals => _rentals;

        private readonly IManagement _management;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagementStore"/> class.
        /// </summary>
        /// <param name="management">Implementation for crud operations on all of dbsets of database.</param>
        public ManagementStore(IManagement management)
        {
            _management = management;
            _cars = new List<Car>();
            _customers = new List<Customer>();
            _rentals = new List<Rental>();
        }
        /// <summary>
        /// Load all of the data from database asynchronously to memory
        /// </summary>
        public async Task Load()
        {
            IEnumerable<Customer> customers = await _management.GetAllCustomers();
            IEnumerable<Car> cars = await _management.GetAllCars();
            IEnumerable<Rental> rentals = await _management.GetAllRentals();
            _customers.Clear();
            _customers.AddRange(customers);
            _cars.Clear();
            _cars.AddRange(cars);
            _rentals.Clear();
            _rentals.AddRange(rentals);
        }
        /// <summary>
        /// Add a row to dbset <see cref="CustomerDTO"/> according to incomingModel <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model).
        /// Also adds the incoming model to modellist that is in memory
        /// </summary>
        /// <param name="customer">The incoming customer to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{Customer}">Thrown if incoming customer has conflict with an existing row in dbset.</exception>
        public async Task AddCustomer(Customer customer)
        {
            await _management.AddCustomer(customer);
            _customers.Add(customer);
        }
        /// <summary>
        /// Add a row to dbset <see cref="CarDTO"/> according to incomingModel <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model).
        /// Also adds the incoming model to modellist that is in memory
        /// </summary>
        /// <param name="car">The incoming car to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{Car}">Thrown if incoming car has conflict with an existing row in dbset.</exception>
        public async Task AddCar(Car car)
        {
            await _management.AddCar(car);
            _cars.Add(car);
        }
        /// <summary>
        /// Add a row to dbset <see cref="RentalDTO"/> according to incomingModel <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model).
        /// Also adds the incoming model to modellist that is in memory
        /// </summary>
        /// <param name="rental">The incoming car to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{Rental}">Thrown if incoming rental has conflict with an existing row in dbset.</exception>
        public async Task AddRental(Rental rental)
        {
            await _management.AddRental(rental);
            _rentals.Add(rental);
        }
        /// <summary>
        /// Delete a row from dbset <see cref="CustomerDTO"/> according to incomingModel <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// Also deletes the incoming model from modellist that is in memory
        /// </summary>
        /// <param name="customer">The incoming customer to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{Customer}">Thrown if the customer is in a not expired existing rental.</exception>
        public async Task RemoveCustomer(Customer customer)
        {
            await _management.RemoveCustomer(customer);
            _customers.Remove(customer);
        }
        /// <summary>
        /// Delete a row from dbset <see cref="CustomerDTO"/> according to incomingModelIndex of <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// Also deletes the incoming model from modellist that is in memory
        /// </summary>
        /// <param name="customerIndex">The ID of customer to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{Customer}">Thrown if the customer is in a not expired existing rental.</exception>
        public async Task RemoveCustomer(Guid customerIndex)
        {
            await _management.RemoveCustomer(customerIndex);
            var toDelete = _customers.FirstOrDefault(c => c.Id == customerIndex);
            _customers.Remove(toDelete);
        }
        /// <summary>
        /// Delete a row from dbset <see cref="CarDTO"/> according to incomingModel <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// Also deletes the incoming model from modellist that is in memory
        /// </summary>
        /// <param name="car">The incoming car to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{Car}">Thrown if the car is in a not expired existing rental.</exception>
        public async Task RemoveCar(Car car)
        {
            await _management.RemoveCar(car);
            _cars.Remove(car);
        }
        /// <summary>
        /// Delete a row from dbset <see cref="CarDTO"/> according to incomingModelIndex of <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// Also deletes the incoming model from modellist that is in memory
        /// </summary>
        /// <param name="carIndex">The ID of car to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{Car}">Thrown if the car is in a not expired existing rental.</exception>
        public async Task RemoveCar(Guid carIndex)
        {
            await _management.RemoveCar(carIndex);
            var toDelete = _cars.FirstOrDefault(c => c.Id == carIndex);
            _cars.Remove(toDelete);
        }
        /// <summary>
        /// Delete a row from dbset <see cref="RentalDTO"/> according to incomingModel <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// Also deletes the incoming model from modellist that is in memory
        /// </summary>
        /// <param name="rental">The incoming rental to be deleted from the dbset.</param>
        public async Task RemoveRental(Rental rental)
        {
            await _management.RemoveRental(rental);
            _rentals.Remove(rental);
        }
        /// <summary>
        /// Delete a row from dbset <see cref="RentalDTO"/> according to incomingModelIndex of <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// Also deletes the incoming model from modellist that is in memory
        /// </summary>
        /// <param name="rentalIndex">The ID rental to be deleted from the dbset.</param>
        public async Task RemoveRental(Guid rentalIndex)
        {
            await _management.RemoveRental(rentalIndex);
            var toDelete = _rentals.FirstOrDefault(r => r.Id == rentalIndex);
            _rentals.Remove(toDelete);
        }
        /// <summary>
        /// Update an existing row from dbset <see cref="CustomerDTO"/> according to <see cref="Customer"/> incomingModel (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// Also updates the existing model in modellist that is in memory
        /// </summary>
        /// <param name="index">The old row ID to be updated in the dbset.</param>
        /// <param name="customer">The incoming customer to be replaced with old row in the dbset.</param>
        /// <exception cref="GModelConflictException{Customer}">Thrown if incoming customer has conflict with an existing row in dbset.</exception>
        public async Task EditCustomer(Customer customer, Guid index)
        {
            await _management.EditCustomer(customer, index);
            var indexToUpdate = _rentals.FindIndex(c => c.Id == index);
            _customers[indexToUpdate] = customer;
        }
        /// <summary>
        /// Update an existing row from dbset <see cref="CarDTO"/> according to <see cref="Car"/> incomingModel (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// Also updates the existing model in modellist that is in memory
        /// </summary>
        /// <param name="index">The old row ID to be updated in the dbset.</param>
        /// <param name="car">The incoming car to be replaced with old row in the dbset.</param>
        /// <exception cref="GModelConflictException{Car}">Thrown if incoming car has conflict with an existing row in dbset.</exception>
        public async Task EditCar(Car car, Guid index)
        {
            await _management.EditCar(car, index);
            var indexToUpdate = _cars.FindIndex(c => c.Id == index);
            _cars[indexToUpdate] = car;
        }
        /// <summary>
        /// Update an existing row from dbset <see cref="RentalDTO"/> according to <see cref="Rental"/> incomingModel (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// Also updates the existing model in modellist that is in memory
        /// </summary>
        /// <param name="index">The old row ID to be updated in the dbset.</param>
        /// <param name="rental">The incoming rental to be replaced with old row in the dbset.</param>
        /// <exception cref="GModelConflictException{Rental}">Thrown if incoming rental has conflict with an existing row in dbset.</exception>
        public async Task EditRental(Rental rental, Guid index)
        {
            await _management.EditRental(rental, index);
            var indexToUpdate = _rentals.FindIndex(r => r.Id == index);
            _rentals[indexToUpdate] = rental;
        }
        /// <summary>
        /// Get all customers that contain filter in their attributes from modellist that is in memory 
        /// </summary>
        /// <param name="filter">Searching element that will be searched in all attributes of all <see cref="Customer"/>s in modellist.</param>
        /// <returns>All customers that contain filter in their attributes</returns>
        public IEnumerable<Customer> SearchCustomers(string filter)
        {
            return _customers.Where(c =>
                c.FirstName.Contains(filter) ||
                c.LastName.Contains(filter) ||
                c.Age.ToString().Contains(filter) ||
                c.Mobile.Contains(filter) ||
                c.Id.ToString().Contains(filter));
        }
        /// <summary>
        /// Get all customers that contain filter in their attributes from modellist that is in memory 
        /// </summary>
        /// <param name="filter">Searching element that will be searched in all attributes of all <see cref="Car"/>s in modellist.</param>
        /// <returns>All customers that contain filter in their attributes</returns>
        public IEnumerable<Car> SearchCars(string filter)
        {
            return _cars.Where(c =>
                c.Name.Contains(filter) ||
                c.Color.Contains(filter) ||
                c.CreationDate.ToString().Contains(filter) ||
                c.Id.ToString().Contains(filter));
        }
        /// <summary>
        /// Get all customers that contain filter in their attributes from modellist that is in memory 
        /// </summary>
        /// <param name="filter">Searching element that will be searched in all attributes of all <see cref="Rental"/>s in modellist.</param>
        /// <returns>All customers that contain filter in their attributes</returns>
        public IEnumerable<Rental> SearchRentals(string filter)
        {
            return _rentals.Where(c =>
                c.CustomerId.ToString().Contains(filter) ||
                c.CarId.ToString().Contains(filter) ||
                c.StartDate.ToString().Contains(filter) ||
                c.EndDate.ToString().Contains(filter) ||
                c.Id.ToString().Contains(filter));
        }
        /// <summary>
        ///Checks that a rental can be created or not(the requirement for this action is existance of at least one <see cref="Car"/> and one <see cref="Customer"/>)
        /// </summary>
        /// <returns>Existance of at least one <see cref="Car"/> and one <see cref="Customer"/> as a boolean</returns>
        public bool CanAddRental()
        {
            var noCar = _cars.Any();
            var noCustomers = _customers.Any();
            return noCar && noCustomers;
        }
    }
}
