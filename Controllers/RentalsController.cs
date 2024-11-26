using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Data;
using VehicleRentalSystem.Models;
using System.Threading.Tasks;
using System.Linq;

namespace VehicleRentalSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly VehicleRentalContext _context;

        public RentalsController(VehicleRentalContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            try
            {
                // Load rentals and include Customer and Vehicle details
                var rentals = _context.Rentals
                    .Include(r => r.Customer)
                    .Include(r => r.Vehicle);

                // Pass Customer and Vehicle data for dropdown lists
                ViewData["Customers"] = await _context.Customers.ToListAsync();
                ViewData["Vehicles"] = await _context.Vehicles.ToListAsync();

                return View(await rentals.ToListAsync());
            }
            catch (Exception ex)
            {
                // Handle errors when loading rentals
                ModelState.AddModelError("", $"Error loading rentals: {ex.Message}");
                Console.WriteLine($"Error loading rentals: {ex.Message}");
                return View("Error");
            }
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(rental);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Rental added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error adding rental: {ex.Message}");
                }
            }

            // Reload data for dropdowns in case of error
            ViewData["Customers"] = await _context.Customers.ToListAsync();
            ViewData["Vehicles"] = await _context.Vehicles.ToListAsync();
            return View(nameof(Index), await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .ToListAsync());
        }

        // POST: Rentals/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int RentalID)
        {
            try
            {
                var rental = await _context.Rentals.FindAsync(RentalID);
                if (rental != null)
                {
                    _context.Rentals.Remove(rental);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Rental deleted successfully!";
                }
                else
                {
                    ModelState.AddModelError("", "Rental not found.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting rental: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
