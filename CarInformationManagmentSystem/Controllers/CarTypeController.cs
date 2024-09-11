using Microsoft.AspNetCore.Mvc;
using CarInformationManagmentSystem.Models.Entities;
using CarInformationManagmentSystem.Repositories;
using System.Threading.Tasks;

public class CarTypeController : Controller
{
    private readonly ICarTypeRepository _carTypeRepository;

    public CarTypeController(ICarTypeRepository carTypeRepository)
    {
        _carTypeRepository = carTypeRepository;
    }


    // GET: CarType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CarType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Type")] CarType carType)
    {
        if (ModelState.IsValid)
        {
            await _carTypeRepository.AddAsync(carType);
            return RedirectToAction("Index","Car");
        }
        return View(carType);
    }
}
