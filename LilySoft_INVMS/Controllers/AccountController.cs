using LilySoft_INVMS.Auth.Models;
using LilySoft_INVMS.Auth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(
            SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // ===== Login =====
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(
                model.Email!,        
                model.password!,     
                model.rememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }

        // ===== Register =====
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new Users
            {
                FullName = model.Name!,
                UserName = model.Email!,
                Email = model.Email!,
                EmailConfirmed = true // ✅ optional: skip confirmation
            };

            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                await userManager.AddToRoleAsync(user, "User");

                // ✅ Do not auto-login, force them to login manually
                TempData["SuccessMessage"] = "Registration successful. Please log in.";
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();  
            return RedirectToAction("Login", "Account");  
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found");
                return View(model);
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account", new { email = model.Email });
                //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //TempData["SuccessMessage"] = "Verification link has been sent to your email.";
                //return RedirectToAction("VerifyEmail");
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword(string email)
        {
            var model = new ChangePasswordViewModel { Email = email };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(" ", "Something want wrong");
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found");
                return View(model);
            }
            var result = await userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.NewPassword!);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }
                return View(model);
            }
             
        }
    }
}
