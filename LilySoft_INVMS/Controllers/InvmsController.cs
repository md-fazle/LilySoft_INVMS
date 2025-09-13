using LilySoft_INVMS.Models.Invms;
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
        // ✅ POST: api/suppliers/insert-with-contacts
        [HttpPost("insert-with-contacts")]
        public async Task<IActionResult> InsertSupplierWithContacts([FromBody] SupplierRequest request)
        {
            if(request == null || string.IsNullOrWhiteSpace(request.supplier_id))
            {
                return BadRequest("Invalid supplier data.");
            }
            var supplier = await _crudInvmsServices.InsertSupplierWithContactsAsync(
                request.supplier_id!,
                request.supplier_name!,
                request.Contacts ?? new List<SupplierContact>()
            );
            return Ok(supplier);

        }
        // ✅ GET: api/suppliers
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _crudInvmsServices.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        // ✅ GET: api/suppliers/{supplier_id}
        [HttpGet("{supplier_id}")]
        public async Task<IActionResult> GetSupplierById(string supplier_id)
        {
            var supplier = await _crudInvmsServices.GetSupplierByCodeAsync(supplier_id);
            if (supplier == null)
                return NotFound($"Supplier with ID {supplier_id} not found.");

            return Ok(supplier);
        }

    }

    // ✅ DTO to keep API request clean
    public class SupplierRequest
    {
        public string? supplier_id { get; set; }
        public string? supplier_name { get; set; }
        public List<SupplierContact>? Contacts { get; set; }
    }
}
