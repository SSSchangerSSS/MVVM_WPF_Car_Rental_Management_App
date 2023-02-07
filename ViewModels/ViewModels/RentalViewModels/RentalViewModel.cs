using Models.Models.RentalModels;

namespace ViewModels.ViewModels.RentalViewModels
{
    /// <summary>
    /// <see cref="RentalViewModel"/> class is a viewModel that represents <see cref="Rental"/> model in the view
    /// </summary>
    public class RentalViewModel : ViewModelBase
    {
        private readonly Rental _rental;
        public string RentalId => _rental.Id.ToString();
        public string CustomerId => _rental.CustomerId.ToString();
        public string CarId => _rental.CarId.ToString();
        public string StartDate => _rental.StartDate.Date.ToString("d");
        public string EndDate => _rental.EndDate.Date.ToString("d");
        public string Length => _rental.Length.ToString();

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalViewModel"/> class.
        /// </summary>
        /// <param name="customer">The <see cref="Rental"/> that <see cref="RentalViewModel"/> is created according to.</param>
        public RentalViewModel(Rental rental)
        { 
            _rental = rental;
        }
    }
}
