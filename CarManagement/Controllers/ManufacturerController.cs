using Microsoft.AspNetCore.Mvc;

namespace CarManagement.Controllers
{
    public class ManufacturerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
