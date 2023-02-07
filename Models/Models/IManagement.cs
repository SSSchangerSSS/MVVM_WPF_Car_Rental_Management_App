using Models.DTOs;
using Models.Exceptions;
using Models.Models.CarModels;
using Models.Models.CustomerModels;
using Models.Models.RentalModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Models
{
    /// <summary>
    /// This interface is a managment for all entities in car rental management
    /// </summary>
    public interface IManagement
    {
        /// <summary>
        /// add a row to dbset <see cref="CustomerDTO"/> according to incomingModel <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <see cref="Customer"/> has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="customer">The incoming customer to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{T}"></exception>
        public Task AddCustomer(Customer customer);
        /// <summary>
        /// add a row to dbset <see cref="CarDTO"/> according to incomingModel <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <see cref="Car"/> has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="car">The incoming car to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{T}"></exception>
        public Task AddCar(Car car);
        /// <summary>
        /// add a row to dbset <see cref="RentalDTO"/> according to incomingModel <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <see cref="Rental"/> has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="rental">The incoming car to be added to the dbset.</param>
        /// <exception cref="GModelConflictException{T}"></exception>
        public Task AddRental(Rental rental);
        /// <summary>
        /// delete a row from dbset <see cref="CustomerDTO"/> according to incomingModel <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// <para><see cref="GModelIsInNotExpiredExistingRentalException{T}"/> : Thrown if the <see cref="Customer"/> is in a not expired existing rental.</para>
        /// </summary>
        /// <param name="customer">The incoming customer to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{T}"></exception>
        public Task RemoveCustomer(Customer customer);
        /// <summary>
        /// delete a row from dbset <see cref="CustomerDTO"/> according to incomingModelIndex of <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// <para><see cref="GModelIsInNotExpiredExistingRentalException{T}"/> : Thrown if the <see cref="Customer"/> is in a not expired existing rental.</para>
        /// </summary>
        /// <param name="customerIndex">The ID of customer to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{T}"></exception>
        public Task RemoveCustomer(Guid customerIndex);
        /// <summary>
        /// delete a row from dbset <see cref="CarDTO"/> according to incomingModel <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// <para><see cref="GModelIsInNotExpiredExistingRentalException{T}"/> : Thrown if the <see cref="Car"/> is in a not expired existing rental.</para>
        /// </summary>
        /// <param name="car">The incoming car to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{T}"></exception>
        public Task RemoveCar(Car car);
        /// <summary>
        /// delete a row from dbset <see cref="CarDTO"/> according to incomingModelIndex of <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// <para><see cref="GModelIsInNotExpiredExistingRentalException{T}"/> : Thrown if the <see cref="Car"/> is in a not expired existing rental.</para>
        /// </summary>
        /// <param name="carIndex">The ID of car to be deleted from the dbset.</param>
        /// <exception cref="GModelIsInNotExpiredExistingRentalException{T}"></exception>
        public Task RemoveCar(Guid carIndex);
        /// <summary>
        /// delete a row from dbset <see cref="RentalDTO"/> according to incomingModel <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// </summary>
        /// <param name="rental">The incoming rental to be deleted from the dbset.</param>
        public Task RemoveRental(Rental rental);
        /// <summary>
        /// delete a row from dbset <see cref="RentalDTO"/> according to incomingModelIndex of <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// </summary>
        /// <param name="rentalIndex">The ID rental to be deleted from the dbset.</param>
        public Task RemoveRental(Guid rentalIndex);
        /// <summary>
        /// update an existing row from dbset <see cref="CustomerDTO"/> according to <see cref="Customer"/> incomingModel (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <see cref="Customer"/> has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="index">The old row ID to be updated in the dbset.</param>
        /// <param name="customer">The incoming customer to be replaced with old row in the dbset.</param>
        /// <exception cref="GModelConflictException{T}"></exception>
        public Task EditCustomer(Customer customer, Guid index);
        /// <summary>
        /// update an existing row from dbset <see cref="CarDTO"/> according to <see cref="Car"/> incomingModel (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <see cref="Car"/> has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="index">The old row ID to be updated in the dbset.</param>
        /// <param name="car">The incoming car to be replaced with old row in the dbset.</param>
        /// <exception cref="GModelConflictException{T}"></exception>
        public Task EditCar(Car car, Guid index);
        /// <summary>
        /// update an existing row from dbset <see cref="RentalDTO"/> according to <see cref="Rental"/> incomingModel (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// <para><see cref="GModelConflictException{T}"/> : Thrown if incoming <see cref="Rental"/> has conflict with an existing row in dbset.</para>
        /// </summary>
        /// <param name="index">The old row ID to be updated in the dbset.</param>
        /// <param name="rental">The incoming rental to be replaced with old row in the dbset.</param>
        /// <exception cref="GModelConflictException{Rental}"></exception>
        public Task EditRental(Rental rental, Guid index);
        /// <summary>
        /// fetch all rows from dbset <see cref="CustomerDTO"/> and return them all after mapping them to a list of <see cref="Customer"/> (<see cref="CustomerDTO"/> is data transfer object for <see cref="Customer"/> model)
        /// </summary>
        /// <returns>All rows in dbset mapped to modelList</returns>
        public Task<IEnumerable<Customer>> GetAllCustomers();
        /// <summary>
        /// fetch all rows from dbset <see cref="CarDTO"/> and return them all after mapping them to a list of <see cref="Car"/> (<see cref="CarDTO"/> is data transfer object for <see cref="Car"/> model)
        /// </summary>
        /// <returns>All rows in dbset mapped to modelList</returns>
        public Task<IEnumerable<Car>> GetAllCars();
        /// <summary>
        /// fetch all rows from dbset <see cref="RentalDTO"/> and return them all after mapping them to a list of <see cref="Rental"/> (<see cref="RentalDTO"/> is data transfer object for <see cref="Rental"/> model)
        /// </summary>
        /// <returns>All rows in dbset mapped to modelList</returns>
        public Task<IEnumerable<Rental>> GetAllRentals();
    }
}