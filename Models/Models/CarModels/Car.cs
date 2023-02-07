using System;

namespace Models.Models.CarModels
{
    /// <summary>
    /// <see cref="Car"/> class is a model to communicate with viewModel
    /// </summary>
    public class Car : IIdentifier
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; }
        public string Name { get; }
        public string Color { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        /// <param name="id">Id of car.</param>
        /// <param name="name">Name of car.</param>
        /// <param name="color">Color of car.</param>
        /// <param name="creationDate">The date that car has been created.</param>
        public Car(Guid id, string name, string color, DateTime creationDate)
        {
            Id = id;
            Name = name;
            Color = color;
            CreationDate = creationDate.Date;
        }
        public override string ToString()
        {
            return $"Name : {Name}\nColor : {Color}\nCreationDate : {CreationDate.ToString("d")}";
        }
        public override bool Equals(object? obj)
        {
            return obj is Car car &&
            Name == car.Name &&
            Color == car.Color &&
            CreationDate == car.CreationDate;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Car car1, Car car2)
        {
            if (car1 is null && car2 is null)
                return true;

            return car1 is not null && car1.Equals(car2);
        }
        public static bool operator !=(Car car1, Car car2)
        {
            return !(car1 == car2);
        }
    }
}
