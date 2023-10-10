using GS_Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace GS_Sample.Data
{
    public class CarsApiDbContext : DbContext
    {
        public CarsApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
