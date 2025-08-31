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

        // ✅ GET: Auth/RegisterUser (shows registration form with roles dropdown)
        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            var roles = await _authServices.GetAllRolesAsync();
            ViewBag.RoleList = roles; // pass roles to the view

            return View();
        }

        // ✅ POST: Auth/RegisterUser (handles form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(Users users)
        {
            var roles = await _authServices.GetAllRolesAsync();
            ViewBag.RoleList = roles; // needed to repopulate dropdown if validation fails

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(users.email))
                {
                    ModelState.AddModelError("email", "Email is required.");
                    return View(users); // stay on registration page
                }

                if (await _authServices.EmailExistsAsync(users.email))
                {
                    ModelState.AddModelError("email", "Email already exists.");
                    return View(users); // stay on registration page
                }

                await _authServices.InsertUserAsync(users);
                return RedirectToAction("Index", "Home");
            }

            return View(users); // return view with validation errors
        }

        // ✅ GET: Auth/Roles (shows roles list)
        public async Task<IActionResult> Roles()
        {
            var roles = await _authServices.GetAllRolesAsync();
            return View(roles);
        }
    }
}
