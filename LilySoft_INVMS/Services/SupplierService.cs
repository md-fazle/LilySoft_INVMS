using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly InvmsDbContext _context;
        public SupplierService(InvmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                        .Include(s => s.SupplierContacts)
                        .ToListAsync();
        }

        public async Task<SupplierViewModel> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierContacts)
                .FirstOrDefaultAsync(s => s.id == id);

            if (supplier == null)
                throw new Exception("Supplier not found");

            return new SupplierViewModel
            {
                Id = supplier.id,
                SupplierId = supplier.supplier_id,
                SupplierName = supplier.supplier_name,
                Contacts = supplier.SupplierContacts?.ToList() ?? new List<SupplierContact>()
            };
        }


        public async Task AddSupplierAsync(SupplierViewModel model)
        {
            var supplier = new Supplier
            {
                supplier_id = model.SupplierId,
                supplier_name = model.SupplierName,
                SupplierContacts = model.Contacts
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSupplierAsync(SupplierViewModel model)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierContacts)
                .FirstOrDefaultAsync(s => s.id == model.Id);

            if (supplier == null) return;

            supplier.supplier_id = model.SupplierId;
            supplier.supplier_name = model.SupplierName;

            if (supplier.SupplierContacts != null && supplier.SupplierContacts.Any())
            {
                _context.SupplierContacts.RemoveRange(supplier.SupplierContacts);
            }

            supplier.SupplierContacts = model.Contacts;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierContacts)
                .FirstOrDefaultAsync(s => s.id == id);

            if (supplier == null) return;

            if (supplier.SupplierContacts != null && supplier.SupplierContacts.Any())
            {
                _context.SupplierContacts.RemoveRange(supplier.SupplierContacts);
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
