using Models.Models.CarModels;
namespace ViewModels.ViewModels.CarViewModels
{
    /// <summary>
    /// <see cref="CarViewModel"/> class is a viewModel that represents <see cref="Car"/> model in the view
    /// </summary>
    public class CarViewModel:ViewModelBase
    {
        private readonly Car _car;
        public string CarId => _car.Id.ToString();
        public string Name => _car.Name;
        public string Color => _car.Color;
        public string CreationDate => _car.CreationDate.ToString("d");

        /// <summary>
        /// Initializes a new instance of the <see cref="CarViewModel"/> class.
        /// </summary>
        /// <param name="car">The <see cref="Car"/> that <see cref="CarViewModel"/> is created according to.</param>
        public CarViewModel(Car car)
        {
            _car = car;
        }
    }
}
