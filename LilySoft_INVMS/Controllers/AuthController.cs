using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.Auth.Services;
using LilySoft_INVMS.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthServices _authServices;
        private readonly AuthDbContext _context;
        public AuthController(AuthServices authServices , AuthDbContext authDbContext)
        {
            _authServices = authServices;
            _context = authDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        // ✅ GET: Auth/RegisterUser (shows registration form)
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View(); // looks for Views/Auth/RegisterUser.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(Users users)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(users.email))
                {
                    ModelState.AddModelError("email", "Email is required.");
                    return View("Index", users);
                }

                if (await _authServices.EmailExistsAsync(users.email))
                {
                    ModelState.AddModelError("email", "Email already exists.");
                    return View("Index", users);
                }

                await _authServices.InsertUserAsync(users);
                return RedirectToAction("Index", "Home");
            }

            return View("Index", users);
        }


    }
}
