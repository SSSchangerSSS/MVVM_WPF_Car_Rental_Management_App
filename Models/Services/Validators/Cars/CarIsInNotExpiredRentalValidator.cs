using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.DbContexts;
using Models.DTOs;
using Models.Models.CarModels;
using Models.Models.RentalModels;
using System;
using System.Threading.Tasks;

namespace Models.Services.Validators.Cars
{
    /// <summary>
    /// <see cref="CarIsInNotExpiredRentalValidator"/> class implements <see cref="IGModelIsInNotExpiredRentalValidator{T}"/> interface.
    /// </summary>
    public class CarIsInNotExpiredRentalValidator : IGModelIsInNotExpiredRentalValidator<Car>
    {
        private readonly CarRentalDbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        ///<summary>
        /// Initializes a new instance of the <see cref="CarIsInNotExpiredRentalValidator"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory for <see cref="CarRentalDbContext"/></param>
        /// <param name="mapper">AutoMapper for mapping <see cref="RentalDTO"/> to <see cref="Rental"/> model</param>
        public CarIsInNotExpiredRentalValidator(CarRentalDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        /// <inheritdoc />
        /// <param name="carId">The id of <see cref="Car"/>  that is to be checked with existing rentals.</param>
        public async Task<Rental> GetNotExpiredExistingRental(Guid carId)
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<RentalDTO>();
                return _mapper.Map<Rental>(await dbset.FirstOrDefaultAsync(r => r.CarId == carId));
            }
        }
        /// <inheritdoc />
        /// <param name="car">The <see cref="Car"/>  that is to be checked with existing rentals.</param>
        public async Task<Rental> GetNotExpiredExistingRental(Car car)
        {
            return await GetNotExpiredExistingRental(car.Id);
        }
    }
}
