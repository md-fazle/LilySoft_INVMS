using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.Auth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LilySoft_INVMS.Controllers
{
    [Authorize(Roles = "Admin,Manager,Staff,Viewer")]
    public class AuthController : Controller
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            ViewBag.RoleList = await _authServices.GetAllRolesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(Users user)
        {
            ViewBag.RoleList = await _authServices.GetAllRolesAsync();

            if (!ModelState.IsValid) return View(user);
            if (string.IsNullOrWhiteSpace(user.email))
            {
                ModelState.AddModelError("email", "Email is required.");
                return View(user);
            }
            if (await _authServices.EmailExistsAsync(user.email))
            {
                ModelState.AddModelError("email", "Email already exists.");
                return View(user);
            }
            if (string.IsNullOrWhiteSpace(user.password))
            {
                ModelState.AddModelError("password", "Password is required.");
                return View(user);
            }

            await _authServices.InsertUserAsync(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Roles()
        {
            var roles = await _authServices.GetAllRolesAsync();
            return View(roles);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _authServices.GetUserByEmailAndPasswordAsync(model.email!, model.password!);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.userName ?? user.email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User"),
                new Claim("UserId", user.userId.ToString())
            };

            if (user.Role?.RolePermissions != null)
            {
                foreach (var rp in user.Role.RolePermissions.Where(rp => rp.Permission != null))
                {
                    claims.Add(new Claim("Permission", rp.Permission!.PermissionName!));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          principal,
                                          new AuthenticationProperties
                                          {
                                              IsPersistent = model.RememberMe,
                                              ExpiresUtc = DateTime.UtcNow.AddHours(1)
                                          });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}
