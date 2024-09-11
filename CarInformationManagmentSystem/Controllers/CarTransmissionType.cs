using Microsoft.AspNetCore.Mvc;
using CarInformationManagmentSystem.Models.Entities;
using CarInformationManagmentSystem.Repositories;

public class CarTransmissionTypeController : Controller
{
    private readonly ICarTransmissionTypeRepository _carTransmissionTypeRepository;

    public CarTransmissionTypeController(ICarTransmissionTypeRepository carTransmissionTypeRepository)
    {
        _carTransmissionTypeRepository = carTransmissionTypeRepository;
    }

    // GET: CarTransmissionType/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CarTransmissionType carTransmissionType)
    {
        try
        {
            await _carTransmissionTypeRepository.AddAsync(carTransmissionType);
            return RedirectToAction("Index", "Car");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Name", "Unable to save changes. " + ex.Message);
        }
        return View(carTransmissionType);
    }
}
