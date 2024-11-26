using System.ComponentModel.DataAnnotations;

namespace VehicleRentalSystem.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string VehicleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleType { get; set; }

        [Required]
        public decimal RentalRate { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
