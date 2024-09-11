using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarInformationManagmentSystem.Models.Entities;
using CarInformationManagmentSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CarInformationManagmentSystem.Controllers
{
    public class CarController : Controller
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICarTypeRepository _carTypeRepository;
        private readonly ICarTransmissionTypeRepository _carTransmissionTypeRepository;
        private readonly ICarRepository _carRepository;

        public CarController(
            IManufacturerRepository manufacturerRepository,
            ICarTypeRepository carTypeRepository,
            ICarTransmissionTypeRepository carTransmissionTypeRepository,
            ICarRepository carRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _carTypeRepository = carTypeRepository;
            _carTransmissionTypeRepository = carTransmissionTypeRepository;
            _carRepository = carRepository;
        }

        // GET: Car/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Manufacturers = await _manufacturerRepository.GetAllAsync();
            ViewBag.CarTypes = await _carTypeRepository.GetAllAsync();
            ViewBag.Transmissions = await _carTransmissionTypeRepository.GetAllAsync();
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            try
            {
                await _carRepository.AddAsync(car);
                return RedirectToAction("Index", "Car");
            }
            catch (Exception ex)
            {
                // Log the exception and show an error message
                ModelState.AddModelError("Model", "Unable to save changes. " + ex.Message);
            }

            // Repopulate ViewBag in case of validation errors
            ViewBag.Manufacturers = await _manufacturerRepository.GetAllAsync();
            ViewBag.CarTypes = await _carTypeRepository.GetAllAsync();
            ViewBag.Transmissions = await _carTransmissionTypeRepository.GetAllAsync();
            return View(car);
        }

        // DELETE: Car/Delete
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string model)
        {
            var success = await _carRepository.DeleteAsync(model);
            if (!success)
            {
                // Handle the case where deletion failed, either because the car was not found or due to another issue.
                TempData["Error"] = $"Car with model '{model}' could not be found or deleted.";
                return RedirectToAction("Index");
            }

            TempData["Success"] = $"Car with model '{model}' has been successfully deleted.";
            return RedirectToAction("Index");
        }

        // Index action for listing and filtering cars
        public async Task<IActionResult> Index(string model, int? manufacturerId, int? typeId, int? transmissionId)
        {
            var cars = await _carRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(model))
            {
                cars = cars.Where(c => c.Model.Contains(model)).ToList();
            }
            if (manufacturerId.HasValue)
            {
                cars = cars.Where(c => c.ManufacturerId == manufacturerId.Value).ToList();
            }
            if (typeId.HasValue)
            {
                cars = cars.Where(c => c.TypeId == typeId.Value).ToList();
            }
            if (transmissionId.HasValue)
            {
                cars = cars.Where(c => c.TransmissionId == transmissionId.Value).ToList();
            }

            ViewBag.Manufacturers = await _manufacturerRepository.GetAllAsync();
            ViewBag.CarTypes = await _carTypeRepository.GetAllAsync();
            ViewBag.Transmissions = await _carTransmissionTypeRepository.GetAllAsync();

            ViewBag.CurrentModel = model;
            ViewBag.CurrentManufacturerId = manufacturerId;
            ViewBag.CurrentTypeId = typeId;
            ViewBag.CurrentTransmissionId = transmissionId;

            return View(cars);
        }

        // GET: Car/Settings/{model}
        [HttpGet("Car/Settings/{model}")]
        public async Task<IActionResult> Settings(string model)
        {
            var car = await _carRepository.GetByModelAsync(model);
            if (car == null)
            {
                return NotFound();
            }

            // Populate ViewBag for dropdown lists
            ViewBag.Manufacturers = await _manufacturerRepository.GetAllAsync();
            ViewBag.CarTypes = await _carTypeRepository.GetAllAsync();
            ViewBag.Transmissions = await _carTransmissionTypeRepository.GetAllAsync();

            return View(car);
        }

        [HttpPost("Car/Settings/{model}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(Car car)
        {
            

            if (ModelState.IsValid)
            {
                await _carRepository.UpdateAsync(car);
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag for dropdown lists in case of validation errors
            ViewBag.Manufacturers = await _manufacturerRepository.GetAllAsync();
            ViewBag.CarTypes = await _carTypeRepository.GetAllAsync();
            ViewBag.Transmissions = await _carTransmissionTypeRepository.GetAllAsync();

            return View(car);
        }

        // GET: Car/Details/{model}
        [HttpGet("Car/Details/{model}")]
        public async Task<IActionResult> Details(string model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetByModelAsync(model);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
    }
}
