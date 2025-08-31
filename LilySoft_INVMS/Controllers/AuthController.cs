using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.Auth.Services;
using LilySoft_INVMS.DBContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        // GET: Auth/RegisterUser
        [Authorize(Policy = "ManageUsers")] // Permission-based authorization
        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            ViewBag.RoleList = await _authServices.GetAllRolesAsync();
            return View();
        }

        // POST: Auth/RegisterUser
        [Authorize(Policy = "ManageUsers")] // Permission-based authorization
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(Users users)
        {
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

            if (string.IsNullOrWhiteSpace(users.password))
            {
                ModelState.AddModelError("password", "Password is required.");
                return View(users);
            }

            await _authServices.InsertUserAsync(users);

            return RedirectToAction("Index", "Home");
        }

        // GET: Auth/Roles
        [Authorize(Policy = "ManageRoles")] // Only users with ManageRoles permission
        public async Task<IActionResult> Roles()
        {
            var roles = await _authServices.GetAllRolesAsync();
            return View(roles);
        }

        // GET: Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Fetch user with RolePermissions and Permissions included
            var user = await _authServices.GetUserByEmailAndPasswordAsync(model.email!, model.password!);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User")
            };

            // Add permission claims dynamically
            if (user.Role?.RolePermissions != null)
            {
                var permissionClaims = user.Role.RolePermissions
                    .Select(rp => new Claim("Permission", rp.Permission?.PermissionName ?? string.Empty));
                claims.AddRange(permissionClaims);
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign in user with cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          principal,
                                          new AuthenticationProperties
                                          {
                                              IsPersistent = model.RememberMe
                                          });

            return RedirectToAction("Index", "Home");
        }

        // POST: Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // GET: Auth/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View(); // Create a simple AccessDenied.cshtml view
        }
    }
}
