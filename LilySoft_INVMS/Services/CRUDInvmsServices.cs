using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Services
{
    public class CRUDInvmsServices:ICRUDInvmsServices
    {
        private readonly InvmsDbContext _context;
        public CRUDInvmsServices(InvmsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.FromResult(_context.Products.ToList());
        }
        // ✅ Insert Supplier with Contacts
        public async Task<Supplier> InsertSupplierWithContactsAsync(
            string supplier_id,
            string supplier_name,
            List<SupplierContact> contacts)
        {
            if (string.IsNullOrWhiteSpace(supplier_id))
                throw new ArgumentException("Supplier ID cannot be null or empty.");

            var supplier = new Supplier
            {
                supplier_id = supplier_id,
                supplier_name = supplier_name,
                SupplierContacts = contacts.Select(c => new SupplierContact
                {
                    supplier_id = supplier_id,                 // 🔗 link FK
                    supplier_contact_id = c.supplier_contact_id, // optional business ID
                    contact_name = c.contact_name,
                    contact_email = c.contact_email,
                    contact_phone = c.contact_phone,
                    contact_address = c.contact_address
                }).ToList()
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }

        // ✅ Get all suppliers with contacts
        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                .Include(s => s.SupplierContacts)
                .ToListAsync();
        }

        // ✅ Get supplier by supplier_id
        public async Task<Supplier?> GetSupplierByCodeAsync(string supplier_id)
        {
            return await _context.Suppliers
                .Include(s => s.SupplierContacts)
                .FirstOrDefaultAsync(s => s.supplier_id == supplier_id);
        }
    }
}
