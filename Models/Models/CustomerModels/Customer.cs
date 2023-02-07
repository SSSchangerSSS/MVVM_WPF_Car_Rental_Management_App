using System;

namespace Models.Models.CustomerModels
{
    /// <summary>
    /// <see cref="Customer"/> class is a model to communicate with viewModel
    /// </summary>
    public class Customer: IIdentifier
    {
        public Guid Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public byte Age { get; }
        public string Mobile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="id">Id of customer.</param>
        /// <param name="firstName">First Name of customer.</param>
        /// <param name="lastName">Last name of customer.</param>
        /// <param name="age">Age of customer.</param>
        /// <param name="mobile">Mobile of customer.</param>
        public Customer(Guid id, string firstName, string lastName, byte age, string mobile)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Mobile = mobile;
        }
        public override string ToString()
        {
            return $"FirstName : {FirstName}\nLastName : {LastName}\nAge : {Age}\nMobile:{Mobile}";
        }
        public override bool Equals(object? obj)
        {
            return obj is Customer customer &&
            FirstName == customer.FirstName &&
            LastName == customer.LastName &&
            Age == customer.Age &&
            Mobile == customer.Mobile;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Customer customer1, Customer customer2)
        {
            if (customer1 is null && customer2 is null)
                return true;

            return customer1 is not null && customer1.Equals(customer2);
        }
        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }
    }
}
