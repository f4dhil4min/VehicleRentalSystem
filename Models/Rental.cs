﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRentalSystem.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Required]
        public int VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
    }
}
