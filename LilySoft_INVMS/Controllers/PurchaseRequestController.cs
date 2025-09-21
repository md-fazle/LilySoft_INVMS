using LilySoft_INVMS.Models.Invms;
using LilySoft_INVMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class PurchaseRequestController : Controller
    {
        private readonly IPurchaseRequestService _service;

        public PurchaseRequestController(IPurchaseRequestService service)
        {
            _service = service;
        }

        // GET: PurchaseRequest
        public async Task<IActionResult> Index()
        {
            var purchaseRequests = await _service.GetAllAsync();
            return View(purchaseRequests);
        }

        // GET: PurchaseRequest/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var purchaseRequest = await _service.GetByIdAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }
            return View(purchaseRequest);
        }

        // GET: PurchaseRequest/Create
        public IActionResult Create()
        {
            var model = new PurchaseRequestViewModel
            {
                RequestDate = DateTime.Now
            };
            return View(model);
        }

        // POST: PurchaseRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(model);
                    TempData["SuccessMessage"] = "Purchase request created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating purchase request: {ex.Message}");
                }
            }
            return View(model);
        }

        // GET: PurchaseRequest/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var purchaseRequest = await _service.GetByIdAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            var model = new PurchaseRequestViewModel
            {
                Id = purchaseRequest.id,
                PurchaseRequestId = purchaseRequest.purchase_request_id,
                RequestDate = purchaseRequest.RequestDate,
                Details = purchaseRequest.PRDetails.Select(d => new PRDetailViewModel
                {
                    ProductId = d.product_id,
                    ProductName = d.Product?.product_name, // Assuming Product has ProductName
                    Quantity = d.quantity,
                    UnitPrice = d.unit_price
                }).ToList()
            };

            return View(model);
        }

        // POST: PurchaseRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseRequestViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(model);
                    TempData["SuccessMessage"] = "Purchase request updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating purchase request: {ex.Message}");
                }
            }
            return View(model);
        }

        // GET: PurchaseRequest/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseRequest = await _service.GetByIdAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }
            return View(purchaseRequest);
        }

        // POST: PurchaseRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Purchase request deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting purchase request: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // AJAX endpoint for adding detail rows in the form
        [HttpPost]
        public IActionResult AddDetailRow([FromBody] PRDetailViewModel detail)
        {
            // This can be used for dynamic form handling in the UI
            return PartialView("_PRDetailRow", detail);
        }

        // Helper action for checking if purchase request exists
        public async Task<JsonResult> CheckPurchaseRequestIdExists(string purchaseRequestId)
        {
            var exists = (await _service.GetAllAsync())
                .Any(pr => pr.purchase_request_id == purchaseRequestId);
            
            return Json(!exists); // Return true if available (for remote validation)
        }
    }
}