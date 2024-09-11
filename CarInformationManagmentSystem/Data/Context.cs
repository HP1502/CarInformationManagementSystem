using Microsoft.EntityFrameworkCore;
using CarInformationManagmentSystem.Models.Entities;

namespace CarInformationManagmentSystem.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarTransmissionType> CarTransmissionTypes { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasIndex(c => c.Model)
                .IsUnique();
            modelBuilder.Entity<Manufacturer>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<Manufacturer>()
                .HasIndex(c => c.ContactPerson)
                .IsUnique();
            modelBuilder.Entity<CarType>()
                .HasIndex(c => c.Type)
                .IsUnique();
            modelBuilder.Entity<CarTransmissionType>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(c => c.UserName)
                .IsUnique();
        }
    }
}
