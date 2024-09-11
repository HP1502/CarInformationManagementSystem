using CarInformationManagmentSystem.Models.Dto;
using CarInformationManagmentSystem.Models.Entities;
using CarInformationManagmentSystem.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarInformationManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Car");
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await _accountRepository.Authenticate(user.UserName, user.Password);
                if (loggedInUser != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loggedInUser.UserName),
                        new Claim(ClaimTypes.Role, loggedInUser.UserRole)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Car"); // Redirect to the Cars dashboard or any other page
                }
                ModelState.AddModelError("Password", "Invalid Credentials");
            }

            return View(user);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _accountRepository.UsernameExists(user.UserName)==true)
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                    return View(user);
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    ModelState.AddModelError("Password", "Password is required.");
                    return View(user);
                }
                user.UserRole = "User";

                await _accountRepository.Create(user);

                return RedirectToAction("Login");
            }

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account"); // Redirect to home or any other page after logout
        }

        [Authorize]
        public async Task<IActionResult> Settings()
        {
            var userName = User.Identity.Name;
            var user = await _accountRepository.GetUserByUserName(userName);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            var userName = User.Identity.Name;
            var user = await _accountRepository.GetUserByUserName(userName);

            if (user == null)
            {
                return RedirectToAction("Index", "Account");
            }

            await _accountRepository.Delete(user.UserName,user.Password);
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Account"); 
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _accountRepository.GetUserByUserName(user.UserName);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.FullName = user.FullName;
                existingUser.EMail = user.EMail;
                existingUser.DOB = user.DOB;
                existingUser.Password = user.Password;

                await _accountRepository.Update(existingUser);
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Account");
            }
            return View("Settings", user);
        }

    }
}
