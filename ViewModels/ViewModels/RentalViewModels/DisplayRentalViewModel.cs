using Models.Models.RentalModels;

namespace ViewModels.ViewModels.RentalViewModels
{
    /// <summary>
    /// <see cref="DisplayRentalViewModel"/> class is a viewModel that represents <see cref="Rental"/> with extra attributes in the view
    /// </summary>
    public class DisplayRentalViewModel : ViewModelBase
    {
        public string RentalId { set; get; }
        public string CustomerId { set; get; }
        public string CustomerLastName { set; get; }
        public string CarId { set; get; }
        public string CarName { set; get; }
        public string StartDate { set; get; }
        public string EndDate { set; get; }
        public string Length { set; get; }
    }
}
