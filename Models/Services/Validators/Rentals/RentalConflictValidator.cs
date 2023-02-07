using AutoMapper;
using Models.DbContexts;
using Models.DTOs;
using Models.Models.RentalModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.Services.Validators.Rentals
{
    /// <summary>
    /// <see cref="RentalConflictValidator"/> class implements <see cref="IGConflictValidator{T,DTO}"/> interface.
    /// </summary>
    public class RentalConflictValidator : IGConflictValidator<Rental,RentalDTO>
    {
        private readonly CarRentalDbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalConflictValidator"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory for <see cref="CarRentalDbContext"/></param>
        /// <param name="mapper">AutoMapper for mapping <see cref="RentalDTO"/> to <see cref="Rental"/>.</param>
        public RentalConflictValidator(CarRentalDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        /// <inheritdoc />
        /// <param name="rental">The <see cref="Rental"/> is to be checked with existing <see cref="RentalDTO"/>s in dbset.</param>
        public async Task<Rental> GetConflictingModel(Rental rental)
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<RentalDTO>();
                return _mapper.Map<Rental>(await dbset
                     .Where(r => r.CarId == rental.CarId)
                     .Where(r => r.StartDate <= rental.EndDate)
                     .Where(r => r.EndDate >= rental.StartDate).FirstOrDefaultAsync());
            }
        }
    }
}
