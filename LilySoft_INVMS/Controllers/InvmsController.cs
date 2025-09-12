using LilySoft_INVMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class InvmsController : Controller
    {
        private readonly ICRUDInvmsServices _crudInvmsServices;
        public InvmsController(ICRUDInvmsServices crudInvmsServices)
        {
            _crudInvmsServices = crudInvmsServices;
        }
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = _crudInvmsServices.GetAllProducts();
            return View(products);
        }
    }
}
