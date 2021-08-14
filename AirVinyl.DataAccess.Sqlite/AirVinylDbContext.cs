using AirVinyl.DataAccess.Sqlite.Internals;
using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.DataAccess.Sqlite
{
    public class AirVinylDbContext : AirVinylDbContextBase
    {
        public AirVinylDbContext(DbContextOptions<AirVinylDbContext> options)
           : base(options)
        { 
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.GenerateTestData();
        }
    }
}
