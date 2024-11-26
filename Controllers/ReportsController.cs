using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Data;
using System.Linq;

namespace VehicleRentalSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly VehicleRentalContext _context;

        // Constructor to initialize the context
        public ReportsController(VehicleRentalContext context)
        {
            _context = context;
        }

        // Action for the Reports Dashboard
        public IActionResult Index()
        {
            return View();
        }

        // Action for Vehicle Usage Report
        public IActionResult VehicleUsageReport()
        {
            var vehicles = _context.Vehicles
                .Include(v => v.Rentals)
                .AsEnumerable()
                .Select(v => new
                {
                    VehicleName = v.VehicleName,
                    RentalRate = v.RentalRate,
                    RentalCount = v.Rentals.Count,
                    TotalDaysRented = v.Rentals.Sum(r => (r.ReturnDate - r.RentalDate).Days)
                })
                .ToList();

            ViewData["UsageReport"] = vehicles;
            return View(); // Ensure the default view is returned
        }



        // Action for Revenue Report
        public IActionResult RevenueReport()
        {
            var revenueReport = _context.Rentals
                .Include(r => r.Vehicle)
                .Include(r => r.Customer)
                .Select(r => new
                {
                    CustomerName = r.Customer.CustomerName, // Ensure Customer has a Name property
                    PhoneNumber = r.Customer.PhoneNumber, // Ensure Customer has a Phone property
                    VehicleName = r.Vehicle.VehicleName, // Ensure Vehicle has a VehicleName property
                    RentalRate = r.Vehicle.RentalRate, // Ensure Vehicle has a RentalRate property
                    TotalDaysRented = (r.ReturnDate - r.RentalDate).Days, // Calculate total days
                    TotalRevenue = (r.ReturnDate - r.RentalDate).Days * r.Vehicle.RentalRate // Calculate revenue
                })
                .ToList();

            ViewData["RevenueReport"] = revenueReport;
            return View();
        }
    }
}
