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

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.CAR
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/AddCar
        public IActionResult AddCar()
        {
            return View();
        }

        // POST: Cars/AddCar
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Cars/Modify/1
        public async Task<IActionResult> Modify(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.CAR.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Modify/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(int id, [Bind("Id,Model,ManufacturerId,TypeId,Engine,BHP,TransmissionId,Mileage,Seat,AirBagDetails,BootSpace,Price")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Remove/5
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.CAR
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Remove/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var car = await _context.CAR.FindAsync(id);
            if (car != null)
            {
                _context.CAR.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.CAR.Any(e => e.Id == id);
        }
    }
}
