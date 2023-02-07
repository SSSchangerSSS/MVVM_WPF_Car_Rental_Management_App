using System;

namespace Models.Models.RentalModels
{
    /// <summary>
    /// <see cref="Rental"/> class is a model to communicate with viewModel
    /// </summary>
    public class Rental : IIdentifier
    { 
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Length => EndDate - StartDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="id">Id of rental.</param>
        /// <param name="carId">Id of the car that has been rented.</param>
        /// <param name="customerId">Id of the customer that has rented the car.</param>
        /// <param name="startTime">Start date of rental.</param>
        /// <param name="endTime">End date of rental.</param>
        public Rental(Guid id, Guid carId, Guid customerId, DateTime startTime, DateTime endTime)
        {
            Id = id;
            CarId = carId;
            CustomerId = customerId;
            StartDate = startTime.Date;
            EndDate = endTime.Date;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class without any argument for AutoMapper.
        /// </summary>
        public Rental() { }//constructor with 0 argumnets for automapper
        public override string ToString()
        {
            return $"CarId : {CarId}\nCustomerId : {CustomerId}\nStartDate : {StartDate.ToString("d")}\nEndTime : {EndDate.ToString("d")}";
        }
        public override bool Equals(object? obj)
        {
            return obj is Rental rental &&
            CarId == rental.CarId &&
            CustomerId == rental.CustomerId &&
            StartDate == rental.StartDate &&
            EndDate == rental.EndDate;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Rental rental1, Rental rental2)
        {
            if (rental1 is null && rental2 is null)
                return true;

            return rental1 is not null && rental1.Equals(rental2);
        }
        public static bool operator !=(Rental rental1, Rental rental2)
        {
            return !(rental1 == rental2);
        }
    }
}
