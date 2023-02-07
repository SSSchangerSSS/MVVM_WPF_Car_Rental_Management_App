using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models.DbContexts;
using Models.DTOs;
using Models.Models.CustomerModels;
using Models.Models.RentalModels;
using System;
using System.Threading.Tasks;
using Models.Services.Validators;
namespace Models.Services.Validators.Customers
{
    /// <summary>
    /// <see cref="CustomerIsInNotExpiredRentalValidator"/> class implements <see cref="IGModelIsInNotExpiredRentalValidator{T}"/> interface.
    /// </summary>
    public class CustomerIsInNotExpiredRentalValidator : IGModelIsInNotExpiredRentalValidator<Customer>
    {
        private readonly CarRentalDbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        ///<summary>
        /// Initializes a new instance of the <see cref="CustomerIsInNotExpiredRentalValidator"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory for <see cref="CarRentalDbContext"/></param>
        /// <param name="mapper">AutoMapper for mapping <see cref="RentalDTO"/> to <see cref="Rental"/> model</param>
        public CustomerIsInNotExpiredRentalValidator(CarRentalDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        /// <inheritdoc />
        /// <param name="customerId">The id of <see cref="Customer"/>  that is to be checked with existing rentals.</param>
        public async Task<Rental> GetNotExpiredExistingRental(Guid customerId)
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<RentalDTO>();
                return _mapper.Map<Rental>(await dbset.FirstOrDefaultAsync(r => r.CustomerId == customerId));
            }
        }
        /// <inheritdoc />
        /// <param name="customer">The <see cref="Customer"/>  that is to be checked with existing rentals.</param>
        public async Task<Rental> GetNotExpiredExistingRental(Customer customer)
        {
            return await GetNotExpiredExistingRental(customer.Id);
        }
    }
}
