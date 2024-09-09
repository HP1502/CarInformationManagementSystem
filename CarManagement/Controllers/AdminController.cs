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
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
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

        public async Task<IActionResult> Modify(string model, Car updatedCar)
        {
            var car = await _context.CAR.FirstOrDefaultAsync(c => c.Model == model);
            if (car == null)
            {
                return NotFound();
            }

            // Update car details
            if (updatedCar.ManufacturerId != null)
            {
                car.ManufacturerId = updatedCar.ManufacturerId;
            }
            if (updatedCar.TypeId != null)
            {
                car.TypeId = updatedCar.TypeId;
            }
            if (updatedCar.Engine != null)
            {
                car.Engine = updatedCar.Engine;
            }
            if (updatedCar.BHP != null)
            {
                car.BHP = updatedCar.BHP;
            }
            if (updatedCar.TransmissionId != null)
            {
                car.TransmissionId = updatedCar.TransmissionId;
            }
            if (updatedCar.Mileage != null)
            {
                car.Mileage = updatedCar.Mileage;
            }
            if (updatedCar.Seat != null)
            {
                car.Seat = updatedCar.Seat;
            }
            if (updatedCar.AirBagDetails != null)
            {
                car.AirBagDetails = updatedCar.AirBagDetails;
            }
            if (updatedCar.BootSpace != null)
            {
                car.BootSpace = updatedCar.BootSpace;
            }
            if (updatedCar.Price != null)
            {
                car.Price = updatedCar.Price;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        public async Task<IActionResult> RemoveCar(string model)

        {

            var car = _context.CAR.FirstOrDefault(c => c.Model == model);

            if (car == null)

            {

                return View("Error");

            }


            _context.Remove(car);

            await _context.SaveChangesAsync();

            return View();

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
