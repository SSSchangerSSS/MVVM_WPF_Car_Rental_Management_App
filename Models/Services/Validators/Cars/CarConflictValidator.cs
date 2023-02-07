using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.DbContexts;
using Models.DTOs;
using Models.Models.CarModels;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Services.Validators.Cars
{
    /// <summary>
    /// <see cref="CarConflictValidator"/> class implements <see cref="IGConflictValidator{T,DTO}"/> interface.
    /// </summary>
    public class CarConflictValidator : IGConflictValidator<Car, CarDTO> 
    {
        private readonly CarRentalDbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarConflictValidator"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory for <see cref="CarRentalDbContext"/></param>
        /// <param name="mapper">AutoMapper for mapping <see cref="CarDTO"/> to <see cref="Car"/>.</param>
        public CarConflictValidator(CarRentalDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        /// <inheritdoc />
        /// <param name="car">The <see cref="Car"/> is to be checked with existing <see cref="CarDTO"/>s in dbset.</param>
        public async Task<Car> GetConflictingModel(Car car)
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<CarDTO>();
                return _mapper.Map<Car>(await dbset
                     .Where(c => c.Name == car.Name)
                     .Where(c => c.Color == car.Color)
                     .Where(c => c.CreationDate == car.CreationDate).FirstOrDefaultAsync());
            }
        }
    }
}
