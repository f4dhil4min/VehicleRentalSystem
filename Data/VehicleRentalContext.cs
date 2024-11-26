using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Data
{
    public class VehicleRentalContext : DbContext
    {
        public VehicleRentalContext(DbContextOptions<VehicleRentalContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rental> Rentals { get; set; } // Ensure this line exists
    }
}
