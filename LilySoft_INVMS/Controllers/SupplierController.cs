using LilySoft_INVMS.Models.Invms;
using LilySoft_INVMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class SupplierController : Controller
    {
         
            private readonly ISupplierService _service;

            public SupplierController(ISupplierService service)
            {
                _service = service;
            }

            public async Task<IActionResult> Index()
            {
                var suppliers = await _service.GetAllSuppliersAsync();
                return View(suppliers);
            }

            public IActionResult Create()
            {
                var model = new SupplierViewModel();
                model.Contacts.Add(new LilySoft_INVMS.Models.Invms.SupplierContact());
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(SupplierViewModel model)
            {
                if (ModelState.IsValid)
                {
                    await _service.AddSupplierAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }

            public async Task<IActionResult> Edit(int id)
            {
                var model = await _service.GetSupplierByIdAsync(id);
                if (model == null) return NotFound();
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(SupplierViewModel model)
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateSupplierAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }

            public async Task<IActionResult> Delete(int id)
            {
                await _service.DeleteSupplierAsync(id);
                return RedirectToAction(nameof(Index));
            }
        
    }   
}
