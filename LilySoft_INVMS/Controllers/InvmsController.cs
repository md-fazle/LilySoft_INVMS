using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class InvmsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
