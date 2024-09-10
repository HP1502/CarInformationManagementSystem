
﻿using System;
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
        public IActionResult Index()
        {
            return View( _context.CAR.ToList());
        }
        private ICarRepository ic = new CarRepository();


        // GET: Cars/Details/{model}
        public IActionResult Details(string? model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var car = _context.CAR.FirstOrDefault(m => m.Model == model);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        // GET: Cars/AddCar/1
        public IActionResult AddCar()
        {
            return View();
        }

        // POST: Cars/AddCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddCar([Bind("Id,Model,ManufacturerId,TypeId,Engine,BHP,TransmissionId,Mileage,Seat,AirBagDetails,BootSpace,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                bool res=ic.AddCar(_context,car);
                if (res)
                {
                    TempData["SuccessMessage"] = "Car created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return StatusCode(500);
                }
                /*await _context.SaveChangesAsync();*/
                /*return RedirectToAction(nameof(Index));*/
            }
            TempData["SuccessMessage"] = "Car creation unsuccessful check the model name it must be unique";
            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Modify/{model}
        public IActionResult Modify(string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return StatusCode(500, "no");
            }

            var car = _context.CAR
                .FirstOrDefault(m => m.Model == model);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Modify
        [HttpPost,ActionName("Modify")]
        [ValidateAntiForgeryToken]
        public IActionResult Modified([FromForm] Car updatedCar, string model)
        {
            var res = ic.ModifyCar(_context, updatedCar, model);
            if (res)
            {
                TempData["SuccessMessage"] = "Car modified successfully!";
                return RedirectToAction(nameof(Index));
            }

            return StatusCode(500, new { message = "Error updating car" });
        }


        // GET: Cars/Remove/{model}
        public IActionResult Remove(string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return NotFound();
            }

            var car = _context.CAR
                .FirstOrDefault(m => m.Model == model);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Remove
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveConfirmed(string model)
        {
            var res = ic.RemoveCar(_context, model);
            if (res)
            {
                TempData["SuccessMessage"] = "Car removed successfully!";
                return RedirectToAction(nameof(Index));
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
