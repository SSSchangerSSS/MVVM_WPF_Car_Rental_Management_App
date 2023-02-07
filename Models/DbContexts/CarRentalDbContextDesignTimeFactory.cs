using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Models.DbContexts
{
    public class CarRentalDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
    {
            public CarRentalDbContext CreateDbContext(string[] args)
            {
                DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=CarRentalManagementDB.db").Options;

                return new CarRentalDbContext(options);
            }
    }

}
