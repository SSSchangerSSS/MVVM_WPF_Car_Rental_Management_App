using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace Models.DbContexts
{
    /// <summary>
    /// DbContext of car rental management
    /// </summary>
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions options) : base(options) { }
        public DbSet<CustomerDTO> Customers { set; get; }
        public DbSet<CarDTO> Cars { set; get; }
        public DbSet<RentalDTO> Rentals { set; get; }
    }
}
