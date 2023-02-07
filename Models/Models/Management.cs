using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs;
using Models.Exceptions;
using Models.Models.CarModels;
using Models.Models.CustomerModels;
using Models.Models.RentalModels;
using Models.Services.Validators;

namespace Models.Models
{
    /// <summary>
    /// <see cref="Management"/> class implements <see cref="IManagement"/> interface.
    /// </summary>
    public class Management : IManagement
    {
        private readonly IGModelList<Car,CarDTO> _cars;
        private readonly IGModelList<Customer, CustomerDTO> _customers;
        private readonly IGModelList<Rental, RentalDTO> _rentals;
        private readonly IGModelIsInNotExpiredRentalValidator<Customer> _customerIsInNotExpiredRentalValidator;
        private readonly IGModelIsInNotExpiredRentalValidator<Car> _carIsInNotExpiredRentalValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Management"/> class.
        /// </summary>
        /// <param name="cars">implementation for crud operations on <see cref="CarDTO"/> dbset according to <see cref="Car"/> models.</param>
        /// <param name="customers">implementation for crud operations on <see cref="CustomerDTO"/> dbset according to <see cref="Customer"/> models.</param>
        /// <param name="rentals">implementation for crud operations on <see cref="RentalDTO"/> dbset according to <see cref="Rental"/> models.</param>
        /// <param name="customerIsInNotExpiredRentalValidator">A validator to detect whether a customer is in a not expired existing rental on deletion.</param>
        /// <param name="carIsInNotExpiredRentalValidator">A validator to detect whether a car is in a not expired existing rental on deletion.</param>
        public Management(IGModelList<Car, CarDTO> cars,
            IGModelList<Customer,CustomerDTO> customers,
            IGModelList<Rental, RentalDTO> rentals,
            IGModelIsInNotExpiredRentalValidator<Customer> customerIsInNotExpiredRentalValidator,
            IGModelIsInNotExpiredRentalValidator<Car> carIsInNotExpiredRentalValidator)
        {
            _cars = cars;
            _customers = customers;
            _rentals = rentals;
            _customerIsInNotExpiredRentalValidator = customerIsInNotExpiredRentalValidator;
            _carIsInNotExpiredRentalValidator = carIsInNotExpiredRentalValidator;
        }

        /// <inheritdoc />
        public async Task AddCustomer(Customer customer)
        {
           await _customers.Add(customer);
        }
        /// <inheritdoc />
        public async Task AddCar(Car car)
        {
            await _cars.Add(car);
        }
        /// <inheritdoc />
        public async Task AddRental(Rental rental)
        {
            await _rentals.Add(rental);
        }
        /// <inheritdoc />
        public async Task RemoveCustomer(Customer customer)
        {
            Rental notExpiredExistingRental = await _customerIsInNotExpiredRentalValidator.GetNotExpiredExistingRental(customer);
            if (notExpiredExistingRental != null)
            {
                throw new GModelIsInNotExpiredExistingRentalException <Customer>($"The customer :\nID : {customer.Id}\n{customer.ToString()}\nis in not expired rental :\nID : {notExpiredExistingRental.Id}\n{notExpiredExistingRental.ToString()}",customer, notExpiredExistingRental);
            }
           await _customers.Delete(customer);
        }
        /// <inheritdoc />
        public async Task RemoveCustomer(Guid customerIndex)
        {
            Rental notExpiredExistingRental = await _customerIsInNotExpiredRentalValidator.GetNotExpiredExistingRental(customerIndex);
            if (notExpiredExistingRental != null)
            {
                var customer = await _customers.GetRowByID(customerIndex);
                throw new GModelIsInNotExpiredExistingRentalException<Customer>($"The customer :\nID : {customer.Id}\n{customer.ToString()}\nis in not expired rental :\nID : {notExpiredExistingRental.Id}\n{notExpiredExistingRental.ToString()}", customer, notExpiredExistingRental);
            }
            await _customers.Delete(customerIndex);
        }
        /// <inheritdoc />
        public async Task RemoveCar(Car car)
        {
            Rental notExpiredExistingRental = await _carIsInNotExpiredRentalValidator.GetNotExpiredExistingRental(car);
            if (notExpiredExistingRental != null)
            {
                throw new GModelIsInNotExpiredExistingRentalException<Car>($"The car :\nID : {car.Id}\n{car.ToString()}\nis in not expired rental :\nID : {notExpiredExistingRental.Id}\n{notExpiredExistingRental.ToString()}",car,notExpiredExistingRental);
            }
            await _cars.Delete(car);
        }
        /// <inheritdoc />
        public async Task RemoveCar(Guid carIndex)
        {
            Rental notExpiredExistingRental = await _carIsInNotExpiredRentalValidator.GetNotExpiredExistingRental(carIndex);
            if (notExpiredExistingRental != null)
            {
                var car = await _cars.GetRowByID(carIndex);
                throw new GModelIsInNotExpiredExistingRentalException<Car>($"The car :\nID : {car.Id}\n{car.ToString()}\nis in not expired rental :\nID : {notExpiredExistingRental.Id}\n{notExpiredExistingRental.ToString()}", car, notExpiredExistingRental);
            }
            await _cars.Delete(carIndex);
        }
        /// <inheritdoc />
        public async Task RemoveRental(Rental rental)
        {
            await _rentals.Delete(rental);
        }
        /// <inheritdoc />
        public async Task RemoveRental(Guid rentalIndex)
        {
            await _rentals.Delete(rentalIndex);
        }
        /// <inheritdoc />
        public async Task EditCustomer(Customer customer, Guid index)
        {
            await _customers.Update(index, customer);
        }
        /// <inheritdoc />
        public async Task EditCar(Car car, Guid index)
        {
            await _cars.Update(index, car);
        }
        /// <inheritdoc />
        public async Task EditRental(Rental rental, Guid index)
        {
            await _rentals.Update(index, rental);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customers.GetAll();
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await _cars.GetAll();
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Rental>> GetAllRentals()
        {
            return await _rentals.GetAll();
        }
    }
}
