using CarManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarManagement.Models;

namespace CarManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly CarInformationSystemContext _context;

        public AdminController(CarInformationSystemContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.CAR.ToListAsync());
        }

        public async Task<IActionResult> AddCar([Bind("Id,Model,ManufacturerId,TypeId,Engine,BHP,TransmissionId,Mileage,Seat,AirBagDetails,BootSpace,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.CAR.
                Where(x => x.Model.Contains(SearchPhrase))
                .ToListAsync());
        }

    }
}
