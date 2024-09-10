using CarManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarManagement.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                // Add authentication logic here
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: Account/SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Account/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                // Add user creation logic here
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
