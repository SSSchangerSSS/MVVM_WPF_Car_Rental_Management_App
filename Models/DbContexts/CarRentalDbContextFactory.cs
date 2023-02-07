using Microsoft.EntityFrameworkCore;

namespace Models.DbContexts
{
    public class CarRentalDbContextFactory
    {
        private readonly string _connectionString;

        public CarRentalDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CarRentalDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new CarRentalDbContext(options);
        }
    }
}
