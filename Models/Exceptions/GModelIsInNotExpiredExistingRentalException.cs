using Models.Models.RentalModels;
using System;
using System.Runtime.Serialization;

namespace Models.Exceptions
{
    /// <summary>
    /// generic exception that be thrown if a model is to be deleted and it is in a not expired existing rental
    /// </summary>
    /// <typeparam name="T">The type of model that is to be deleted and is in a not expired existing rentals.</typeparam>
    public class GModelIsInNotExpiredExistingRentalException<T> : Exception
    {
        public T Model { get; }
        public Rental RentalModel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GModelIsInNotExpiredExistingRentalException{T}"/> class.
        /// </summary>
        /// <param name="model">Model is to be deleted and it is in a not expired existing rental.</param>
        /// <param name="rentalModel">Not expired existing rental.</param>
        public GModelIsInNotExpiredExistingRentalException(T model, Rental rentalModel)
        {
            Model = model;
            RentalModel = rentalModel;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GModelIsInNotExpiredExistingRentalException{T}"/> class.
        /// </summary>
        /// <param name="model">Model is to be deleted and it is in a not expired existing rental.</param>
        /// <param name="rentalModel">Not expired existing rental.</param>
        /// <param name="message">Message is to be shown if exception happens.</param>
        public GModelIsInNotExpiredExistingRentalException(string? message, T model, Rental rentalModel) : base(message)
        {
            Model = model;
            RentalModel = rentalModel;
        }

        public GModelIsInNotExpiredExistingRentalException(string? message, Exception? innerException, T model, Rental rentalModel) : base(message, innerException)
        {
            Model = model;
            RentalModel = rentalModel;
        }

        protected GModelIsInNotExpiredExistingRentalException(SerializationInfo info, StreamingContext context, T model, Rental rentalModel) : base(info, context)
        {
            Model = model;
            RentalModel = rentalModel;
        }
    }
}
