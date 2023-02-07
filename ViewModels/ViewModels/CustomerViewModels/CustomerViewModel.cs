using Models.Models.CustomerModels;

namespace ViewModels.ViewModels.CustomerViewModels
{
    /// <summary>
    /// <see cref="CustomerViewModel"/> class is a viewModel that represents <see cref="Customer"/> model in the view
    /// </summary>
    public class CustomerViewModel : ViewModelBase
    {
        private readonly Customer _customer;
        public string CustomerId => _customer.Id.ToString();
        public string FirstName => _customer.FirstName;
        public string LastName => _customer.LastName;
        public string Age => _customer.Age.ToString();
        public string Mobile => _customer.Mobile;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerViewModel"/> class.
        /// </summary>
        /// <param name="customer">The <see cref="Customer"/> that <see cref="CustomerViewModel"/> is created according to.</param>
        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }
    }
}
