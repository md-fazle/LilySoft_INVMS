using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
