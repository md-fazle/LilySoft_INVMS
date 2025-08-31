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

        public AuthController(AuthServices authServices, AuthDbContext authDbContext)
        {
            _authServices = authServices;
            _context = authDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Auth/RegisterUser (shows registration form with roles dropdown)
        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            // Fix: always populate roles for the dropdown
            ViewBag.RoleList = await _authServices.GetAllRolesAsync();
            return View();
        }

        // POST: Auth/RegisterUser (handles form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(Users users)
        {
            // Repopulate roles for dropdown if validation fails
            ViewBag.RoleList = await _authServices.GetAllRolesAsync();

            if (!ModelState.IsValid)
                return View(users);

            if (string.IsNullOrWhiteSpace(users.email))
            {
                ModelState.AddModelError("email", "Email is required.");
                return View(users);
            }

            if (await _authServices.EmailExistsAsync(users.email))
            {
                ModelState.AddModelError("email", "Email already exists.");
                return View(users);
            }

            // Ensure password is not null or empty
            if (string.IsNullOrWhiteSpace(users.password))
            {
                ModelState.AddModelError("password", "Password is required.");
                return View(users);
            }

            // Insert user via service (service will set isActive=true and hash password)
            await _authServices.InsertUserAsync(users);

            return RedirectToAction("Index", "Home");
        }


        // GET: Auth/Roles (shows roles list)
        public async Task<IActionResult> Roles()
        {
            var roles = await _authServices.GetAllRolesAsync();
            return View(roles);
        }
    }
}
