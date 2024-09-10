using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarManagement.Data;
using CarManagement.Models;

namespace CarManagement.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarInformationSystemContext _context;

        public CarsController(CarInformationSystemContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.CAR.ToListAsync());
        }
        private ICarRepository ic = new CarRepository();

        //*[HttpGet("SearchCar/{id})"]*/
        // GET: Cars/CarSummary/5
        public IActionResult SearchCar(CarInformationSystemContext _context,string? model)
        {
            if (model == null)
            {
                return NotFound();
            }
            var res = ic.SearchCar(_context, model);
            if (res != null)
            {
                return View(res);
            }
            return NotFound();
        }

        // GET: Cars/AddCar
        public IActionResult AddCar()
        {
            return View();
        }

        // POST: Cars/AddCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddCar([Bind("Id,Model,ManufacturerId,TypeId,Engine,BHP,TransmissionId,Mileage,Seat,AirBagDetails,BootSpace,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                bool res=ic.AddCar(_context,car);
                if (res)
                {
                    return View(car);
                }
                /*await _context.SaveChangesAsync();*/
                /*return RedirectToAction(nameof(Index));*/
            }
            return NotFound();
        }

        // GET: Cars/Modify/1
        public async Task<IActionResult> Modify(string? Model )
        {
            if (Model== null)
            {
                return NotFound();
            }

            var car = await _context.CAR.FindAsync(Model);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        // POST: Cars/Modify/5
        [HttpPut("Modify")]
        public  IActionResult ModifyCar([FromBody] Car updatedCar, [FromQuery] string model)
        {
            var res=ic.ModifyCar(_context, updatedCar, model);
            if (res)
            {
                return Ok(new { message = "Car updated successfully" });
            }

            return StatusCode(500, new { message = "Error updating car" });
   
        }



        // GET: Cars/Remove/5
        public async Task<IActionResult> Remove(string? model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var car = await _context.CAR
                .FirstOrDefaultAsync(m => m.Model == model);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Remove/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveConfirmed(string model)
        {
            var res = ic.RemoveCar(_context, model);
            if (res)
            {
                return Ok();
            }
            return NotFound();


        }

        private bool CarExists(int id)
        {
            return _context.CAR.Any(e => e.Id == id);
        }
        //GET: Cars/GetCarsWithDetails
        public IActionResult GetCarsWithDetails(string manufacturer_name)
        {
            var res=ic.GetCarsWithDetails(_context, manufacturer_name);
            if(res.Count()==0 || res == null){
                return NotFound();
            }
            return Ok(res);
        }
    }
}
