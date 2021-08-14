using System;
using AirVinyl.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirVinyl.DataAccess
{
    public class AirVinylDbContextBase : DbContext
    {
        public AirVinylDbContextBase(DbContextOptions<AirVinylDbContextBase> options) : base(options)
        {
        }
        
        protected AirVinylDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<VinylRecord> VinylRecords { get; set; }
        public DbSet<RecordStore> RecordStores { get; set; }
        public DbSet<PressingDetail> PressingDetails { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new Exception("This method must be overriden as we have database typenames which need to be defined");
        }
    }
}
