using Microsoft.AspNetCore.Mvc;
using CarInformationManagmentSystem.Models.Entities;
using CarInformationManagmentSystem.Repositories;
using System.Threading.Tasks;

public class ManufacturerController : Controller
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public ManufacturerController(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    // GET: Manufacturer/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Manufacturer/Create
    [HttpPost]
    public async Task<IActionResult> Create(Manufacturer manufacturer)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _manufacturerRepository.AddAsync(manufacturer);
                return RedirectToAction("Index","Car");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "Unable to save changes. " + ex.Message);
            }
        }

        return View(manufacturer);
    }

}
