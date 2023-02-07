using AutoMapper;
using Models.DbContexts;
using Models.DTOs;
using Models.Models.CustomerModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.Services.Validators.Customers
{
    /// <summary>
    /// <see cref="CustomerConflictValidator"/> class implements <see cref="IGConflictValidator{T,DTO}"/> interface.
    /// </summary>
    public class CustomerConflictValidator : IGConflictValidator<Customer,CustomerDTO> 
    {
        private readonly CarRentalDbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerConflictValidator"/> class.
        /// </summary>
        /// <param name="contextFactory">DbContext factory for <see cref="CarRentalDbContext"/></param>
        /// <param name="mapper">AutoMapper for mapping <see cref="CustomerDTO"/> to <see cref="Customer"/>.</param>
        public CustomerConflictValidator(CarRentalDbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        /// <inheritdoc />
        /// <param name="customer">The <see cref="Customer"/> is to be checked with existing <see cref="CustomerDTO"/>s in dbset.</param>
        public async Task<Customer> GetConflictingModel(Customer customer)
        {
            using (CarRentalDbContext context = _contextFactory.CreateDbContext())
            {
                var dbset = context.Set<CustomerDTO>();
                return _mapper.Map<Customer>(await dbset
                     .Where(c => c.LastName == customer.LastName)
                     .Where(c => c.FirstName == customer.FirstName)
                     .Where(c => c.Age == customer.Age)
                     .Where(c => c.Mobile == customer.Mobile).FirstOrDefaultAsync());
            }
        }
    }
}
