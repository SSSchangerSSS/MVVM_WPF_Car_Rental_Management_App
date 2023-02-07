using Models.Models.RentalModels;
using System;
using System.Threading.Tasks;

namespace Models.Services.Validators
{
    /// <summary>
    /// This generic interface is a validator for detecting whether a model on deletion is in a not expired rental or not 
    /// </summary>
    public interface IGModelIsInNotExpiredRentalValidator<T>
    {
        /// <summary>
        /// returns a not expired rental that the <typeparamref name="T"/> is in it if there is one
        /// </summary>
        /// <returns>The not expired rental that the <typeparamref name="T"/> is in it.</returns>
        Task<Rental> GetNotExpiredExistingRental(T model);
        /// <summary>
        /// returns a not expired rental that the <typeparamref name="T"/> is in it if there is one
        /// </summary>
        /// <returns>The not expired rental that the <typeparamref name="T"/> is in it.</returns>
        Task<Rental> GetNotExpiredExistingRental(Guid modelId);
    }
}
