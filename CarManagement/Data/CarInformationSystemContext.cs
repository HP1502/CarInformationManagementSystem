using CarManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManagement.Data
{
    public class CarInformationSystemContext : DbContext
    {
        public CarInformationSystemContext(DbContextOptions options) : base(options){ }
    
        public DbSet<Car> CAR { get; set; }
        public DbSet<ManufacturerClass> Manufacturer { get; set; }
        public DbSet<CarTypeClass> CarType { get; set; }
        public DbSet<CarTransmissionTypeClass> CarTransmissionType { get; set; }

    }
}
